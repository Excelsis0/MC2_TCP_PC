using System;
using System.IO;
using System.Text.Json;

namespace MC2_TCP_PC.Services
{
    public class StatusFileService
    {
        private readonly string filePath = "status.txt";

        // Method to write AppState to status file
        public void Write(AppState state)
        {
            var jsonString = JsonSerializer.Serialize(state);
            File.WriteAllText(filePath, jsonString);
        }

        // Method to read AppState from status file
        public AppState Read()
        {
            if (!File.Exists(filePath))
                return null;

            var jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppState>(jsonString);
        }
    }

    public class AppState
    {
        // Define properties of AppState here
    }
}