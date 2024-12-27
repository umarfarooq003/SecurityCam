from flask import Flask, Response, request, jsonify
from flask_cors import CORS
import cv2
import signal
import sys
import threading
from ultralytics import YOLO
import os
import time

app = Flask(__name__)
CORS(app)

# Global flags to control webcam stream and alert
is_running = True
alert_triggered = False
last_snapshot_time = 0
snapshot_path = ""

# Load YOLO model
model_path = r"E:\Profit Distribution\SecuirtyCam\Python\model\best.pt"
if not os.path.exists(model_path):
    print(f"Error: YOLO model file not found at {model_path}")
    sys.exit(1)

model = YOLO(model_path)

# Initialize the webcam
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    print("Error: Could not open webcam.")
    sys.exit(1)

# Signal handler to release resources
def signal_handler(sig, frame):
    global is_running
    print("Terminating and releasing webcam...")
    is_running = False
    if cap.isOpened():
        cap.release()  # Release the webcam
    cv2.destroyAllWindows()
    sys.exit(0)

# Register signal handlers for Ctrl+C or termination
signal.signal(signal.SIGINT, signal_handler)
signal.signal(signal.SIGTERM, signal_handler)

@app.route('/')
def index():
    return "YOLO Detection API is running!"

@app.route('/favicon.ico')
def favicon():
    return '', 204

def generate_frames():
    """Video streaming generator function."""
    global is_running, alert_triggered, last_snapshot_time, snapshot_path
    while is_running:
        try:
            ret, frame = cap.read()
            if not ret:
                print("Error: Failed to read from webcam.")
                break

            # Perform YOLO detection
            results = model.predict(frame, conf=0.5)
            detected_classes = results[0].boxes.cls.tolist()  # Detected classes

            # Check if 'person' is detected (class ID for 'person' is typically 0)
            if 0 in detected_classes:  # Replace 0 with the correct ID for 'person' in your model
                current_time = time.time()
                if current_time - last_snapshot_time > 3:  # Take snapshot every 3 seconds
                    last_snapshot_time = current_time
                    alert_triggered = True

                    # Save snapshot to a file
                    snapshot_path = os.path.join("snapshots", f"person_detected_{int(current_time)}.jpg")
                    os.makedirs("snapshots", exist_ok=True)
                    cv2.imwrite(snapshot_path, frame)

                    print(f"Person detected! Snapshot saved to {snapshot_path}")

            # Annotate the frame for live view
            annotated_frame = results[0].plot()

            # Encode the frame to JPEG
            _, buffer = cv2.imencode('.jpg', annotated_frame)
            frame_bytes = buffer.tobytes()

            # Yield the frame as a response
            yield (b'--frame\r\n'
                   b'Content-Type: image/jpeg\r\n\r\n' + frame_bytes + b'\r\n')

        except Exception as e:
            print(f"Error during frame generation: {e}")
            break

    # Release webcam resources when the loop exits
    if cap.isOpened():
        cap.release()
    cv2.destroyAllWindows()

@app.route('/video_feed')
def video_feed():
    """Endpoint to start the video feed."""
    global is_running
    if not is_running:
        return "Video feed is not running. Please start the application.", 400
    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame')

@app.route('/alert', methods=['GET'])
def get_alert():
    """Endpoint for the Windows Forms application to poll for alerts."""
    global alert_triggered, snapshot_path
    if alert_triggered:
        alert_triggered = False
        return jsonify({"alert": True, "message": "Person detected!", "image_path": snapshot_path})
    return jsonify({"alert": False})


@app.route('/shutdown', methods=['POST'])
def shutdown():
    """Shutdown the webcam stream via HTTP request."""
    global is_running
    print("Received shutdown command.")
    is_running = False
    return "Shutting down video feed...", 200

def monitor_input():
    """Monitor user input in a separate thread."""
    global is_running
    while is_running:
        user_input = input("Press 'q' or 'Q' to stop the webcam: ")
        if user_input.lower() == 'q':
            is_running = False
            print("Stopping the webcam...")
            break

if __name__ == "__main__":
    try:
        # Start the input monitoring thread
        input_thread = threading.Thread(target=monitor_input, daemon=True)
        input_thread.start()

        # Start Flask application
        app.run(host="0.0.0.0", port=5000, debug=False)
    except Exception as e:
        print(f"Error: {e}")
    finally:
        # Cleanup resources on exit
        print("Shutting down...")
        if cap.isOpened():
            cap.release()
        cv2.destroyAllWindows()
