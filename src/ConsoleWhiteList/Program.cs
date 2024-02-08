using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using SharedLibrary;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWhiteList
{
	internal class Program
	{
		//
		static void Main(string[] args)
		{
			LoggingConfiguration lc = new();

			ConsoleTarget ct = new ConsoleTarget();
			lc.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, ct, "*");

			LogManager.Configuration = lc;

			LogManager.ReconfigExistingLoggers();


			NLogLoggerProvider pippo = new NLogLoggerProvider();
			NLogLoggerFactory nlogFactory = new NLogLoggerFactory(pippo);

			RconClient rconClient = new("host", 27016, "password", nlogFactory.CreateLogger<RconClient>());

			Task<bool> tak = rconClient.Connect();
			tak.Wait();

			//while (true)
			//{
			//	Task<List<PalUserInfo>> tak2 = rconClient.GetPlayers();
			//	tak2.Wait();
			//	Console.WriteLine("[" + string.Join("\n", tak2.Result) + "]");
			//	Thread.Sleep(1000);
			//}

			//var tak2 = rconClient.SendCommand("BanPlayer steamID");
			//tak2.Wait();
			//Console.WriteLine(tak2.Result);

			//var tak2 = rconClient.ShutDown(TimeSpan.FromSeconds(5), "ciaone");
			//tak2.Wait();
			//Console.WriteLine(tak2.Result);

			Console.ReadLine();
		}
	}
}
