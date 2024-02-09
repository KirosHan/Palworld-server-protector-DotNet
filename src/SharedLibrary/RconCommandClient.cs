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

        public RconCommandClient(string host, int port, string password, ILogger<RconClient> loggerRcon = null, ILogger<RconCommandClient> logger = null)
        {
            _client = new RconClient(host, port, password, loggerRcon);
            Task<bool> connectionTask = _client.Connect();
            connectionTask.Wait();
        }

        public async Task<string> ShutDown(TimeSpan time, string text)
        {
            var data = await _client.SendCommand("shutdown " + time.TotalSeconds + " " + text);
            string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> Broadcast(String text)
        {
            var data = await _client.SendCommand("broadcast " + text);
            string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> GetServerInformation()
        {
            var data = await _client.SendCommand("info");
            string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<string> BanPlayer(String steamId)
        {
            var data = await _client.SendCommand($"BanPlayer {steamId}");
            string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
            return responseString;
        }

        public async Task<List<PalUserInfo>> GetPlayers()
        {
            List<PalUserInfo> result = [];
            var data = await _client.SendCommand("ShowPlayers");
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
