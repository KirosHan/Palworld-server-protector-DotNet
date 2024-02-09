using Microsoft.Extensions.Logging;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWhiteList.PalworldTask
{
	public class RestartSometimes : IPalworldTask
	{
		private readonly SettingsHandler _settings;
		private Task enforceWhitelistTask;
		private readonly CancellationToken _cancellationToken;
		private readonly ILogger<RestartSometimes> _logger;
		private readonly RconCommandClient _rconClient;

		public RestartSometimes(SettingsHandler settings, RconCommandClient rconClient, CancellationToken ct = default, ILogger<RestartSometimes> logger = null)
		{
			_logger = logger;
			_settings = settings;
			_cancellationToken = ct;
			_rconClient = rconClient;
		}

		public void Run()
		{
			throw new NotImplementedException();
		}
	}
}
