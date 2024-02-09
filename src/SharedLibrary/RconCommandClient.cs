using Microsoft.Extensions.Logging;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        }

        public async Task<string> ShutDown(TimeSpan time, string text)
        {
            var data = await _client.SendCommand("shutdown " + time.TotalSeconds + " " + text);
            _logger.LogInformation($"Requested shutdown in {time} with text: {text}");
            string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> Broadcast(String text)
        {
            var data = await _client.SendCommand("broadcast " + text);
			_logger.LogInformation($"Sent broadcast message with text: {text}");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> GetServerInformation()
        {
            var data = await _client.SendCommand("info");
			_logger.LogInformation($"Requested server information");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> BanPlayer(long steamId)
        {
            var data = await _client.SendCommand($"BanPlayer {steamId}");
			_logger.LogInformation($"Ban Player with SteamId: {steamId}");
			string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<List<PalUserInfo>> GetPlayers()
        {
            List<PalUserInfo> result = [];
            var data = await _client.SendCommand("ShowPlayers");
			_logger.LogInformation($"Requested connected players");
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
