using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using SharedLibrary;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWhiteList
{
	internal class Program
	{
		private static CancellationTokenSource ctSourcea = new();

		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.ProcessExit += (sender, args) => ExitHandler(ctSourcea);
			Console.CancelKeyPress += (sender, args) =>
			{
				args.Cancel = true;
				ExitHandler(ctSourcea);
			};

			CancellationToken canct = ctSourcea.Token;

			LoggingConfiguration lc = new();

			ColoredConsoleTarget ct = new();
			lc.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, ct, "*");

			LogManager.Configuration = lc;

			LogManager.ReconfigExistingLoggers();


			NLogLoggerProvider pippo = new();
			NLogLoggerFactory nlogFactory = new(pippo);

			FileInfo settingsFile = new(@"config.json");
			SettingsHandler s = new(settingsFile, nlogFactory, nlogFactory.CreateLogger<SettingsHandler>());

			RconCommandClient rconClient = new(s.RconHost, s.RconPort, s.RconPassword, nlogFactory, nlogFactory.CreateLogger<RconCommandClient>());

			Task enforceWhitelistTask = null;
			if (s.EnforceWhitelist)
			{
				enforceWhitelistTask = Task.Run(() => EnforceWhitelist(s, rconClient, canct), canct);
			}

			if (s.EnforceWhitelist)
			{
				try
				{
					enforceWhitelistTask?.Wait(canct);
				}
				catch (OperationCanceledException) { }
			}


			LogManager.Shutdown();
			Console.WriteLine("Bye");

			//var tak2 = rconClient.SendCommand("BanPlayer steamID");
			//tak2.Wait();
			//Console.WriteLine(tak2.Result);

			//var tak2 = rconClient.ShutDown(TimeSpan.FromSeconds(5), "ciaone");
			//tak2.Wait();
			//Console.WriteLine(tak2.Result);
			
		}

		private static void ExitHandler(CancellationTokenSource ctSource)
		{
			// You can add any arbitrary global clean up
			Console.WriteLine("Exiting...");
			ctSource.Cancel();
		}

		private static async Task EnforceWhitelist(SettingsHandler s, RconCommandClient rconClient, CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				List<PalUserInfo> tak2 = await rconClient.GetPlayers();
				Console.WriteLine("[" + string.Join("\n", tak2) + "]");

				foreach (PalUserInfo p in tak2)
				{
					if (!s.CheckSteamIdWhiteListStatus(p.SteamId))
					{
						await rconClient.BanPlayer(p.SteamId);
					}
				}

				await Task.Delay(5000, ct);
			}
		}
	}
}
