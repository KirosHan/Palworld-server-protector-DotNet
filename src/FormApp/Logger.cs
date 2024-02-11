using System;
using System.IO;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
	public sealed class Logger
	{
		private static readonly Logger instance = new Logger();

		private readonly string errorLogName = $"error_{DateTime.Now:yyyyMMddHHmmss}.log";
		private readonly string LogName = $"log_{DateTime.Now:yyyyMMddHHmmss}.log";
		private readonly string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");
		private readonly string ErrorLogPath ;
		private readonly string LogPath;
		private readonly StreamWriter Logwriter;
		private readonly StreamWriter ErrorLogwriter;

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Logger()
		{
		}

		public static Logger Instance
		{
			get
			{
				return instance;
			}
		}

		private Logger()
		{
			if (!Directory.Exists(logFolderPath))
			{
				Directory.CreateDirectory(logFolderPath);
			}
			ErrorLogPath = Path.Combine(logFolderPath, errorLogName);
			LogPath = Path.Combine(logFolderPath, LogName);
			Logwriter = File.AppendText(LogPath);
			ErrorLogwriter = File.AppendText(ErrorLogPath);
		}

		~Logger()
		{
			Logwriter.Close();
			ErrorLogwriter.Close();
		}

		public async Task AppendToErrorLogAsync(string content)
		{
			try
			{
				await Logwriter.WriteLineAsync($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}");
				await Logwriter.FlushAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error writing to log: {ex.Message}");
			}
		}

		public void AppendToErrorLog(string content)
		{
			try
			{
				Logwriter.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}");
				Logwriter.Flush();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error writing to log: {ex.Message}");
			}
		}

		public async Task AppendToLog(string content)
		{
			try
			{
				await ErrorLogwriter.WriteLineAsync($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}");
				await ErrorLogwriter.FlushAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error writing to log: {ex.Message}");
			}
		}
	}
}
