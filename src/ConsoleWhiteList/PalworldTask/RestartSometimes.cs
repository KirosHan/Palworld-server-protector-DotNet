using Microsoft.Extensions.Logging;
using SharedLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWhiteList.PalworldTask
{
	public class RestartSometimes : IPalworldTask
	{
		private readonly SettingsHandler _settings;
		private readonly CancellationToken _cancellationToken;
		private readonly ILogger<RestartSometimes> _logger;
		private readonly RconCommandClient _rconClient;
		private Timer timer;

		public RestartSometimes(SettingsHandler settings, RconCommandClient rconClient, CancellationToken ct = default, ILogger<RestartSometimes> logger = null)
		{
			_logger = logger;
			_settings = settings;
			_cancellationToken = ct;
			_rconClient = rconClient;
		}

		public void Run()
		{
			if (_settings.AutoShutdown)
			{
				//TODO Add time settings from settings file
				timer = ScheduleTask.Schedule(22, 22, () =>
				{
					Task<bool> save = _rconClient.Save();
					save.Wait();
					if (save.Result)
					{
						TimeSpan timeToShutdown = TimeSpan.FromMinutes(1);
						Task restart = _rconClient.ShutDown(timeToShutdown, $"The server will be shut down in {timeToShutdown.TotalMinutes} minutes for daily maintenance");
						restart.Wait();
					} else
					{
						_logger.LogCritical("Restart aborted due to error in saving progress");
					}
				});
			}
		}
	}
}
