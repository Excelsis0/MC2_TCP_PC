using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MC2_TCP_PC.Services
{
    public class UdpService
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;
        private bool _isRunning;

        public UdpService(int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _udpClient = new UdpClient(port);
        }

        // Start the UDP service
        public void Start()
        {
            _isRunning = true;
            ReceiveAsync();
        }

        // Stop the UDP service
        public void Stop()
        {
            _isRunning = false;
            _udpClient.Close();
        }

        // Asynchronous method to receive data
        private async Task ReceiveAsync()
        {
            while (_isRunning)
            {
                var receivedResult = await _udpClient.ReceiveAsync();
                ProcessCommand(receivedResult);
            }
        }

        // Process command received
        private void ProcessCommand(UdpReceiveResult receivedResult)
        {
            string message = Encoding.UTF8.GetString(receivedResult.Buffer);
            Console.WriteLine($"Received: {message}");
            // Add further command processing logic here
        }

        // Asynchronous method to send manual data
        public async Task SendManualAsync(string message, string ipAddress, int port)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            await _udpClient.SendAsync(data, data.Length, endPoint);
        }
    }
}