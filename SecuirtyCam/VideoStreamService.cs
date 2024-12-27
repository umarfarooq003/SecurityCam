using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuirtyCam
{
    internal class VideoStreamService
    {
        private readonly HttpClient _httpClient;
        private const int BufferSize = 4096; // Buffer size for reading data
        private CancellationTokenSource _cts; // Token to cancel streaming

        public VideoStreamService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10) // Timeout for the stream
            };
        }

        public async Task StartVideoStream(PictureBox pictureBox, string apiUrl, CancellationToken cancellationToken)
        {
            try
            {
                if (pictureBox == null)
                {
                    throw new ArgumentNullException(nameof(pictureBox), "PictureBox cannot be null.");
                }

                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Ensure the image stretches to fit

                // Fetch the MJPEG stream
                var response = await _httpClient.GetAsync(apiUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to connect to API. Status code: {response.StatusCode}");
                }

                // Get the stream and read frames
                var stream = await response.Content.ReadAsStreamAsync();

                // Start reading frames from the MJPEG stream
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Read the JPEG image from the stream (looking for JPEG frames)
                        byte[] jpegImage = await ReadJpegFrame(stream);
                        if (jpegImage != null)
                        {
                            using (var ms = new MemoryStream(jpegImage))
                            {
                                // Update the PictureBox with the new frame
                                pictureBox.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any frame reading issues gracefully
                        MessageBox.Show($"Error reading frame: {ex.Message}", "Stream Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    throw new Exception($"An error occurred while starting the video stream: {ex.Message}");
                }
            }
        }

        private async Task<byte[]> ReadJpegFrame(Stream stream)
        {
            List<byte> frameBytes = new List<byte>();
            bool isJpegStarted = false;

            byte[] buffer = new byte[BufferSize];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    byte byteRead = buffer[i];

                    if (!isJpegStarted)
                    {
                        if (byteRead == 0xFF)
                        {
                            byte nextByte = buffer[i + 1];
                            if (nextByte == 0xD8)
                            {
                                frameBytes.Add(0xFF);
                                frameBytes.Add(0xD8);
                                isJpegStarted = true;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        frameBytes.Add(byteRead);

                        if (frameBytes.Count >= 2 && frameBytes[frameBytes.Count - 2] == 0xFF && frameBytes[frameBytes.Count - 1] == 0xD9)
                        {
                            byte[] jpegFrame = frameBytes.ToArray();
                            frameBytes.Clear();
                            return jpegFrame;
                        }
                    }
                }
            }

            return null;
        }

        public void StopStream()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void StartCancellation()
        {
            _cts = new CancellationTokenSource();
        }
    }
}
