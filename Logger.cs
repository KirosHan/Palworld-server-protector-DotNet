using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palworld_server_protector_DotNet
{
    public class Logger
    {
        private static string errorLogName = $"error_{DateTime.Now:yyyyMMddHHmmss}.log";
        private static string LogName = $"log_{DateTime.Now:yyyyMMddHHmmss}.log";

        private static readonly string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "log");

        private static readonly string errorLogPath = Path.Combine(logFolderPath, errorLogName);
        private static readonly string LogPath = Path.Combine(logFolderPath, LogName);

        private static readonly object errorLogLock = new object();
        private static readonly object logLock = new object();
        static Logger()
        {
            // 确保日志目录存在
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }
        }

        public static void AppendToErrorLog(string content)
        {
            lock (errorLogLock)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(errorLogPath))
                    {
                        writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}");
                    }
                }
                catch (Exception ex)
                {
                    // 处理写入日志时发生的异常，例如显示一个错误消息
                    // 避免产生无限递归
                    Console.WriteLine($"Error writing to log: {ex.Message}");
                }
            }
        }

        public static void AppendToLog(string content)
        {
            lock (logLock)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(LogPath))
                    {
                        writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}");
                    }
                }
                catch (Exception ex)
                {
                    // 处理写入日志时发生的异常，例如显示一个错误消息
                    // 避免产生无限递归
                    Console.WriteLine($"Error writing to log: {ex.Message}");
                }

            }
            
        }
    }

}
