using Microsoft.Extensions.Logging;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
	public class RconCommandClient
	{
		private readonly RconClient _client;
		private readonly ILogger<RconCommandClient> _logger;

		public RconCommandClient(string host, int port, string password, ILoggerFactory iLoggerFactory, ILogger<RconCommandClient> logger = null)
		{
			_logger = logger;
			_client = new RconClient(host, port, password, iLoggerFactory.CreateLogger<RconClient>());
			Task<bool> connectionTask = _client.Connect();
			connectionTask.Wait();
			if (!connectionTask.Result)
			{
				throw new Exception("Connection error");
			}
		}

		public async Task<bool> ShutDown(TimeSpan time, string text)
		{
			if (await Save())
			{
				var data = await _client.SendCommand("shutdown " + time.TotalSeconds + " " + text);
				_logger.LogError($"Requested shutdown in {time} with text: {text}");
				string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());

				if (responseString != $"The server will shut down in {time.TotalSeconds} seconds. Please prepare to exit the game.")
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public async Task<bool> Save()
		{
			var data = await _client.SendCommand("Save");
			_logger.LogError($"Requested save");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());

			if (responseString != "Complete Save")
			{
				_logger.LogCritical("Save error: {@responseString}", responseString);
				return false;
			}

			return true;
		}

		public async Task<string> Broadcast(String text)
		{
			var data = await _client.SendCommand("broadcast " + text);
			_logger.LogTrace($"Sent broadcast message with text: {text}");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
			return responseString;
		}

		public async Task<string> GetServerInformation()
		{
			var data = await _client.SendCommand("info");
			_logger.LogTrace($"Requested server information");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
			return responseString;
		}

		public async Task<bool> BanPlayer(long steamId)
		{
			var data = await _client.SendCommand($"BanPlayer {steamId}");
			_logger.LogError($"Ban Player with SteamId: {steamId}");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());

			if (responseString != $"Baned: {steamId}")
			{
				_logger.LogCritical(responseString);
				return false;
			}

			return true;
		}

		public async Task<List<PalUserInfo>> GetPlayers()
		{
			List<PalUserInfo> result = [];
			var data = await _client.SendCommand("ShowPlayers");
			_logger.LogTrace($"Requested connected players");
			String responseString = Encoding.UTF8.GetString(data.Skip(34).ToArray());

			var lines = responseString.Split('\n');

			if (lines.Length < 1)
			{
				return result;
			}

			for (int i = 1; i <= lines.Length; i++)
			{
				var line = lines[i - 1];

				if (string.IsNullOrEmpty(line))
				{
					continue;
				}

				result.Add(PalUserInfo.Parse(line));
			}
			return result;
		}

	}
}
