using ConsoleWhiteList.PalworldTask;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using SharedLibrary;
using System;
using System.IO;
using System.Threading;

namespace ConsoleWhiteList
{
	internal class Program
	{
		private static CancellationTokenSource ctSourcea = new();
		private static ManualResetEvent mre = new ManualResetEvent(false);

		static void Main(string[] args)
		{
			Console.CancelKeyPress += (sender, args) =>
			{
				args.Cancel = true;
				ExitHandler(ctSourcea);
			};

			CancellationToken canct = ctSourcea.Token;

			NLogLoggerFactory nlogFactory = InitializeLogging();

			FileInfo settingsFile = new(@"config.json");
			SettingsHandler s = new(settingsFile, nlogFactory, nlogFactory.CreateLogger<SettingsHandler>());

			RconCommandClient rconClient = new(s.RconHost, s.RconPort, s.RconPassword, nlogFactory, nlogFactory.CreateLogger<RconCommandClient>());

			//var tak2 = rconClient.ShutDown(TimeSpan.FromSeconds(50), "ciaone");
			//tak2.Wait();
			//Console.WriteLine(tak2.Result);

			RestartSometimes rs = new(s, rconClient, canct, nlogFactory.CreateLogger<RestartSometimes>());
			rs.Run();

			EnforceWhitelist enforceWhitelist = new(s, rconClient, canct, nlogFactory.CreateLogger<EnforceWhitelist>());
			enforceWhitelist.Run();

			mre.WaitOne();

			LogManager.Shutdown();
			Console.WriteLine("Bye");
		}

		private static NLogLoggerFactory InitializeLogging()
		{
			LoggingConfiguration lc = new();

			ColoredConsoleTarget ct = new();
			lc.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, ct, "*");

			LogManager.Configuration = lc;

			LogManager.ReconfigExistingLoggers();

			NLogLoggerProvider pippo = new();
			NLogLoggerFactory nlogFactory = new(pippo);
			return nlogFactory;
		}

		private static void ExitHandler(CancellationTokenSource ctSource)
		{
			Console.WriteLine("Exiting...");
			ctSource.Cancel();
			mre.Set();
		}
	}
}
