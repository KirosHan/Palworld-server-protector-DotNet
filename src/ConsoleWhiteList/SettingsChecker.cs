using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;

namespace ConsoleWhiteList
{
	public class SettingsChecker
	{
		private SettingsHandler _actualSettings;
		private readonly ILogger<SettingsChecker> _logger;
		private FileSystemWatcher _watcher;

		public SettingsChecker(SettingsHandler actualSettings, FileInfo path, ILogger<SettingsChecker> logger = null)
		{
			_logger = logger;
			_actualSettings = actualSettings;
			CreateFileWatcher(path);
		}

		public void Start()
		{
			_watcher.EnableRaisingEvents = true;
			_logger.LogInformation("Start watcher on settings file");
		}

		public void Stop()
		{
			_watcher.EnableRaisingEvents = false;
			_logger.LogInformation("Stop watcher on settings file");
		}

		public void CreateFileWatcher(FileInfo path)
		{
			// Create a new FileSystemWatcher and set its properties.
			_watcher = new FileSystemWatcher();
			_watcher.Path = path.DirectoryName;
			_watcher.Filter = path.Name;
			/* Watch for changes in LastAccess and LastWrite times, and 
			   the renaming of files or directories. */
			_watcher.NotifyFilter = NotifyFilters.LastWrite;

			// Add event handlers.
			_watcher.Changed += new FileSystemEventHandler(OnChanged);
		}

		// Define the event handlers.
		private void OnChanged(object source, FileSystemEventArgs e)
		{
			// Specify what is done when a file is changed, created, or deleted.
			_logger.LogInformation($"File: {e.FullPath} was changed, reloading");
			Thread.Sleep(500);
			_actualSettings.LoadFromConfigFile();
		}
	}
}
