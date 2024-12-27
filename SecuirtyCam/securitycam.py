from flask import Flask, Response
from flask_cors import CORS
import cv2
from ultralytics import YOLO

app = Flask(__name__)
CORS(app)

# Load YOLO model
model_path = r"E:\Profit Distribution\SecuirtyCam\Python\model\best.pt"
model = YOLO(model_path)

# Initialize the webcam
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    raise RuntimeError("Error: Could not open webcam.")

@app.route('/')
def index():
    return "YOLO Detection API is running!"

@app.route('/favicon.ico')
def favicon():
    return '', 204

def generate_frames():
    """Video streaming generator function."""
    while True:
        ret, frame = cap.read()
        if not ret:
            break

        # Perform YOLO detection
        results = model.predict(frame, conf=0.5)
        annotated_frame = results[0].plot()

        # Encode the frame to JPEG
        _, buffer = cv2.imencode('.jpg', annotated_frame)
        frame_bytes = buffer.tobytes()

        # Yield the frame as a response
        yield (b'--frame\r\n'
               b'Content-Type: image/jpeg\r\n\r\n' + frame_bytes + b'\r\n')

@app.route('/video_feed')
def video_feed():
    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame')

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
