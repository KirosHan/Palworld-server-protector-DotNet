using Microsoft.Extensions.Logging;
using SharedLibrary;
using SharedLibrary.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWhiteList.PalworldTask
{
	public class EnforceWhitelist : IPalworldTask
	{
		private readonly SettingsHandler _settings;
		private readonly CancellationToken _cancellationToken;
		private readonly ILogger<EnforceWhitelist> _logger;
		private readonly RconCommandClient _rconClient;

		public EnforceWhitelist(SettingsHandler settings, RconCommandClient rconClient, CancellationToken ct = default, ILogger<EnforceWhitelist> logger = null)
		{
			_logger = logger;
			_settings = settings;
			_cancellationToken = ct;
			_rconClient = rconClient;
		}

		public void Run()
		{
			Task t = EnforceWhitelistTask(_rconClient);
			t.Wait();
			
			if (_settings.EnforceWhitelist)
			{
				_logger.LogInformation("Starting whitelist enforcement");
				Task.Run(() => EnforceWhitelistTask(_rconClient), _cancellationToken).ContinueWith((t) =>
				{
					if (t.IsFaulted) { throw t.Exception; }
				});
			}
		}

		private async Task EnforceWhitelistTask(RconCommandClient rconClient)
		{
			while (!_cancellationToken.IsCancellationRequested)
			{
				List<PalUserInfo> tak2 = await rconClient.GetPlayers();
				_logger.LogDebug("Currently connected players: {@num}", tak2.Count);
				foreach (PalUserInfo p in tak2)
				{
					if (!_settings.CheckSteamIdWhiteListStatus(p.SteamId))
					{
						bool success = await rconClient.BanPlayer(p.SteamId);
						if (!success)
						{
							_logger.LogCritical("Error in banning, player {@steamId} not banned!", p.SteamId);
						}
					}
				}

				await Task.Delay(5000, _cancellationToken);
			}
			_logger.LogInformation("Gracefully stopped whitelist enforcement");
		}
	}
}
