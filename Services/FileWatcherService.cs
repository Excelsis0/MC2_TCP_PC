using System;
using System.IO;
using System.Threading;

namespace MC2_TCP_PC.Services
{
    public class FileWatcherService
    {
        private FileSystemWatcher _watcher;
        private string _filePath = "MC2_status.txt";

        public FileWatcherService()
        {
            InitializeWatcher();
        }

        private void InitializeWatcher()
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = Path.GetDirectoryName(_filePath);
            _watcher.Filter = Path.GetFileName(_filePath);
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            // Handle file change
            Console.WriteLine($"File {e.FullPath} has been modified.");
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }
    }
}