using Microsoft.Extensions.Logging;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedLibrary
{
	public class RconCommandClient
	{
		private readonly RconClient _client;
		private readonly ILogger<RconCommandClient> _logger;

		public RconCommandClient(string host, int port, string password, ILoggerFactory iLoggerFactory = null, ILogger<RconCommandClient> logger = null)
		{
			_logger = logger;
			_client = new RconClient(host, port, password, iLoggerFactory?.CreateLogger<RconClient>());
			Task<bool> connectionTask = _client.Connect();
			connectionTask.Wait();
			if (!connectionTask.Result)
			{
				throw new Exception("Connection error");
			}
		}

		public async Task<bool> ShutDown(TimeSpan time, string text)
		{
			_logger?.LogError($"Requested shutdown in {time} with text: {text}");
			String responseString = await _client.SendCommand("shutdown " + time.TotalSeconds + " " + text);

			if (responseString != $"The server will shut down in {time.TotalSeconds} seconds. Please prepare to exit the game.")
			{
				return false;
			}
			return true;

		}

		public async Task<bool> Save()
		{
			var responseString = await _client.SendCommand("Save");
			_logger.LogError($"Requested save");

			if (responseString != "Complete Save")
			{
				_logger.LogCritical("Save error: {@responseString}", responseString);
				return false;
			}

			return true;
		}

		public async Task<bool> Broadcast(String text)
		{
			var responseString = await _client.SendCommand("broadcast " + text);
			_logger?.LogTrace($"Sent broadcast message with text: {text}");
			if (responseString != $"Broadcasted: {text}")
			{
				_logger?.LogCritical("Broadcast error: {@responseString}", responseString);
				return false;
			}
			return true;
		}

		public async Task<String> GetServerInformation()
		{
			String data = await _client.SendCommand("info");
			_logger?.LogTrace($"Requested server information");
			return data;
		}
		
		public async Task<bool> KickPlayer(long steamId)
		{
			_logger?.LogError($"Kick Player with SteamId: {steamId}");
			string responseString = await _client.SendCommand($"KickPlayer {steamId}");

			if (responseString != $"Kicked: {steamId}")
			{
				_logger?.LogCritical(responseString);
				return false;
			}

			return true;
		}

		public async Task<bool> BanPlayer(long steamId)
		{
			_logger?.LogError($"Ban Player with SteamId: {steamId}");
			string responseString = await _client.SendCommand($"BanPlayer {steamId}");

			if (responseString != $"Baned: {steamId}")
			{
				_logger?.LogCritical(responseString);
				return false;
			}

			return true;
		}

		public async Task<List<PalUserInfo>> GetPlayers()
		{
			List<PalUserInfo> result = [];
			String responseString = null;
			try
			{
				responseString = await _client.SendCommand("ShowPlayers");
			} catch (Exception e)
			{
				_logger?.LogError(e, "Catching exception and trying again in 30 seconds");
				await Task.Delay(30000);
				responseString = await _client.SendCommand("ShowPlayers");
			}
			_logger?.LogTrace($"Requested connected players");

			String[] lines = responseString.Split('\n');

			if (lines.Length <= 1)
			{
				return result;
			}

			for (int i = 1; i <= lines.Length - 1; i++)
			{
				var line = lines[i];

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
