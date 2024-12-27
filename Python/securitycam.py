from flask import Flask, Response, request, jsonify
from flask_cors import CORS
import cv2
import signal
import sys
import threading
from ultralytics import YOLO
import os
import time
import logging

# Initialize Flask application
app = Flask(__name__)
CORS(app)

# Logging setup
log = logging.getLogger('werkzeug')
log.setLevel(logging.ERROR)
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

# Application settings
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
SNAPSHOT_DIR = os.path.join(BASE_DIR, "snapshots")

# Global flags and resources
is_running = True
alert_triggered = False
is_streaming = False
last_snapshot_time = 0
snapshot_path = ""
lock = threading.Lock()  # Lock for shared resources

# Load YOLO model
model_path = r"E:\Profit Distribution\SecuirtyCam\Python\model\best.pt"
if not os.path.exists(model_path):
    logging.error(f"Error: YOLO model file not found at {model_path}")
    sys.exit(1)

try:
    model = YOLO(model_path)
except Exception as e:
    logging.error(f"Error loading YOLO model: {e}")
    sys.exit(1)

# Initialize webcam
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    logging.error("Error: Could not open webcam.")
    sys.exit(1)

# Signal handler for graceful termination
def signal_handler(sig, frame):
    global is_running
    logging.info("Received termination signal. Shutting down...")
    is_running = False
    cleanup()
    sys.exit(0)

signal.signal(signal.SIGINT, signal_handler)
signal.signal(signal.SIGTERM, signal_handler)

@app.route('/shutdown', methods=['POST'])
def shutdown():
    """Handle server shutdown requests."""
    global is_running, is_streaming
    logging.info("Received shutdown command.")
    is_running = False  # Stop the video feed
    while is_streaming:  # Wait for video feed to stop
        time.sleep(0.1)
    func = request.environ.get('werkzeug.server.shutdown')
    if func:
        func()
    return jsonify({"message": "Server is shutting down. Disconnecting clients..."}), 200

@app.route('/video_feed')
def video_feed():
    """Stream video feed to the client."""
    global is_running
    if not is_running:
        return "Video feed is not running. Please start the application.", 400
    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame')

@app.route('/alert', methods=['GET'])
def get_alert():
    """Send alerts if a person is detected."""
    global alert_triggered, snapshot_path
    with lock:
        if alert_triggered:
            alert_triggered = False
            return jsonify({"alert": True, "message": "Person detected!", "image_path": snapshot_path})
    return jsonify({"alert": False})

def generate_frames():
    """Generate frames from the webcam and process them."""
    global is_running, is_streaming, alert_triggered, last_snapshot_time, snapshot_path
    is_streaming = True  # Indicate streaming has started
    try:
        while is_running:
            ret, frame = cap.read()
            if not ret:
                logging.warning("Failed to read frame from webcam.")
                break

            # YOLO detection
            results = model.predict(frame, conf=0.3)
            detected_classes = results[0].boxes.cls.tolist()

            # Check for "person" detection (class ID 0, adjust for your model)
            if 0 in detected_classes:
                current_time = time.time()
                if current_time - last_snapshot_time > 3:  # Snapshot interval
                    last_snapshot_time = current_time
                    os.makedirs(SNAPSHOT_DIR, exist_ok=True)
                    snapshot_path = os.path.join(SNAPSHOT_DIR, f"person_detected_{int(current_time)}.jpg")
                    cv2.imwrite(snapshot_path, frame)
                    with lock:
                        alert_triggered = True
                        logging.info(f"Alert triggered! Snapshot saved to {snapshot_path}")

            # Encode the processed frame for streaming
            _, buffer = cv2.imencode('.jpg', results[0].plot())
            yield (b'--frame\r\nContent-Type: image/jpeg\r\n\r\n' + buffer.tobytes() + b'\r\n')

    except GeneratorExit:
        logging.info("Client disconnected from video feed.")
    except Exception as e:
        logging.error(f"Error in generate_frames: {e}")
    finally:
        logging.info("Stopping frame generation...")
        is_streaming = False  # Indicate streaming has stopped
        cleanup()

def cleanup():
    """Release resources like the webcam."""
    global cap
    if cap.isOpened():
        cap.release()
        logging.info("Webcam released.")
    cv2.destroyAllWindows()
    logging.info("All OpenCV windows closed.")

def monitor_input():
    """Monitor user input in a separate thread."""
    global is_running
    while is_running:
        try:
            user_input = input("Press 'q' or 'Q' to stop the webcam: ")
            if user_input.lower() == 'q':
                is_running = False
                logging.info("Stopping the webcam...")
                break
        except EOFError:
            break  # Handle edge cases like running without a console

if __name__ == "__main__":
    try:
        # Start a thread to monitor user input
        threading.Thread(target=monitor_input, daemon=True).start()

        # Run the Flask app (use a production WSGI server for production)
        logging.info("Starting Flask server...")
        app.run(host="0.0.0.0", port=5000, debug=False)
    except Exception as e:
        logging.error(f"Error: {e}")
    finally:
        logging.info("Cleaning up resources...")
        cleanup()
