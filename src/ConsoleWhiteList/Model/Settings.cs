using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWhiteList.Model
{
	public class Settings
	{
		public String RconHost { get; set; } = "127.0.0.1";
		public int RconPort { get; set; } = 25575;
		public String RconPassword { get; set; } = "admin";
		public HashSet<long> WhiteListSteamIds { get; set; } = new();
	}
}
