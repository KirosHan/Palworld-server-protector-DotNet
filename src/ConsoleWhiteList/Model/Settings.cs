using System;
using System.Collections.Generic;

namespace ConsoleWhiteList.Model
{
	public class Settings
	{
		public String RconHost { get; set; } = "127.0.0.1";
		public int RconPort { get; set; } = 25575;
		public String RconPassword { get; set; } = "admin";
		public bool EnforceWhitelist { get; set; } = false;
		public HashSet<long> WhiteListSteamIds { get; set; } = new();
	}
}
