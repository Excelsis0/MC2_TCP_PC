using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MC2_TCP_PC.Services
{
    public class TcpService
    {
        private TcpListener _server;
        private bool _isRunning;

        public TcpService(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
        }

        public async Task StartAsync()
        {
            _server.Start();
            _isRunning = true;
            Console.WriteLine("Server started...");
            await AcceptClientsAsync();
        }

        public void Stop()
        {
            _isRunning = false;
            _server.Stop();
            Console.WriteLine("Server stopped...");
        }

        private async Task AcceptClientsAsync()
        {
            while (_isRunning)
            {
                var client = await _server.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Client connected...");
            var buffer = new byte[1024];
            var stream = client.GetStream();

            try
            {
                while (_isRunning)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break; // Client disconnected

                    var message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {message}");

                    // Echo back the message to the client
                    await stream.WriteAsync(buffer, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected...");
            }
        }
    }
}