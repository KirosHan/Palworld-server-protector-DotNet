using ConsoleWhiteList.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ConsoleWhiteList
{
	public class SettingsHandler
	{
		private readonly FileInfo _configFilePath;
		private readonly SettingsChecker _checker;
		private readonly ILogger<SettingsHandler> _logger;
		private Settings _settings;

		public String RconHost { get { return _settings.RconHost; } }
		public String RconPassword { get { return _settings.RconPassword; } }
		public int RconPort { get { return _settings.RconPort; } }

		public SettingsHandler(FileInfo configFilePath, ILoggerFactory loggerFactory, ILogger<SettingsHandler> logger = null)
		{
			_logger = logger;
			_configFilePath = configFilePath;
			LoadFromConfigFile();
			_checker = new(this, configFilePath, loggerFactory.CreateLogger<SettingsChecker>());
			_checker.Start();
		}

		public void LoadFromConfigFile()
		{
			if (!_configFilePath.Exists)
			{
				return;
			}

			String json;
			using (var reader = _configFilePath.OpenText())
			{
				json = reader.ReadToEnd();
			}
			Settings s = JsonConvert.DeserializeObject<Settings>(json);
			if (s == null)
			{
				throw new Exception($"Error deserialize config file in {_configFilePath}");
			}

			_settings = s;
		}

		public void SaveToConfigFile()
		{
			_checker.Stop();
			string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
			using (StreamWriter writer = _configFilePath.CreateText())
			{
				writer.Write(json);
			}
			_checker.Start();
		}

		public bool CheckSteamIdWhiteListStatus(long steamId)
		{
			return _settings.WhiteListSteamIds.Contains(steamId);
		}

		public void AddSteamIdWhiteList(long steamId)
		{
			_settings.WhiteListSteamIds.Add(steamId);
			SaveToConfigFile();
		}
	}

}
