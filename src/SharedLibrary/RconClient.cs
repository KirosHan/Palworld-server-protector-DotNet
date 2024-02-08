using Microsoft.Extensions.Logging;
using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
	public class RconClient
	{
		private ILogger<RconClient> _logger;
		private String _host;
		private int _port;
		private String _password;

		private TcpClient Client;
		private NetworkStream networkStream;

		public RconClient(string host, int port, string password, ILogger<RconClient> logger = null)
		{
			_logger = logger;
			_host = host;
			_port = port;
			_password = password;
		}

		public async Task<bool> Connect()
		{
			Client = new TcpClient();

			try
			{
				await Client.ConnectAsync(_host, _port);
			}
			catch (Exception e)
			{
				_logger.LogError("Error", e);
				return false;
			}


			if (!Client.Connected)
			{
				Client.Dispose();
				Client = null;
				return false;
			}

			networkStream = Client.GetStream();

			Pck auth = new Pck(0xf5, PacketType.AUTH, Encoding.ASCII.GetBytes(_password));

			networkStream.Write(auth.ToBytes(), 0, auth.Len);

			int size = networkStream.ReadByte();
			byte[] data = new byte[size];

			await networkStream.ReadAsync(data, 0, size);

			if (data[3] != 0xf5)
			{
				if (Client != null)
				{
					if (Client.Connected)
					{
						Client.GetStream().Close();
					}
					Client.Dispose();
				}
				return false;
			}

			return true;
		}

		public async Task<string> GetWelcomeString(bool keep = false)
		{

			if (Client != null)
			{
				try
				{
					while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
					Pck pck = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.ASCII.GetBytes("info"));
					networkStream.Write(pck.ToBytes(), 0, pck.Len);

					int size = networkStream.ReadByte();
					var data = new byte[size];
					await networkStream.ReadAsync(data, 0, size);

					if (!keep)
					{
						return Encoding.UTF8.GetString(data.Skip(11).ToArray());
					}
				}
				catch
				{
					return "获取失败";
				}
			}

			return "";
		}

		public async Task<List<PalUserInfo>> GetPlayers()
		{
			List<PalUserInfo> result = [];
			bool ensureConnectedResult = await EnsureConnected();
			if (!ensureConnectedResult)
			{
				return null;
			}

			String resstring = "";
			try
			{
				if (networkStream != null)
				{
					while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
					Pck pck = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.ASCII.GetBytes("ShowPlayers"));
					networkStream.Write(pck.ToBytes(), 0, pck.Len);

					int size = networkStream.ReadByte();
					byte[] data = new byte[size];

					await networkStream.ReadAsync(data, 0, size);

					resstring = Encoding.UTF8.GetString(data.Skip(34).ToArray());

					var lines = resstring.Split('\n');

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
				}
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return result;
			}
		}

		public async Task<string> Broadcast(String text)
		{
			var ensureConnectedResult = await EnsureConnected();
			if (!ensureConnectedResult)
			{
				return "ConnectionError";
			}
			try
			{
				if (networkStream != null)
				{
					while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
					Pck packet = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.UTF8.GetBytes("broadcast " + text));
					networkStream.Write(packet.ToBytes(), 0, packet.Len);

					int size = networkStream.ReadByte();
					byte[] data = new byte[size];

					await networkStream.ReadAsync(data, 0, size);

					string resstring = Encoding.UTF8.GetString(data.Skip(11).ToArray());

					return resstring;
				}
				return "";
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return "发送失败";
		}

		public async Task Reload()
		{
			if (Client.Connected)
			{
				await GetWelcomeString(true);
			}
		}

		public async Task<string> ShutDown(TimeSpan time, string text)
		{
			var ensureConnectedResult = await EnsureConnected();
			if (!ensureConnectedResult)
			{
				return "Connection Error";
			}

			//Bisogna prima salvare!


			try
			{
				if (networkStream != null)
				{
					while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
					Pck packet = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.ASCII.GetBytes("shutdown " + time.TotalSeconds + " " + text));
					networkStream.Write(packet.ToBytes(), 0, packet.Len);

					int size = networkStream.ReadByte();
					byte[] data = new byte[size];

					await networkStream.ReadAsync(data, 0, size);

					string resstring = Encoding.UTF8.GetString(data.Skip(11).ToArray());

					return resstring;
				}
				return "";
			}
			catch (Exception ex)
			{
				_logger.LogError("Error", ex);
			}

			return "发送失败";
		}

		public async Task<string> SendCommand(String command)
		{
			// 检查客户端是否已连接
			var ensureConnectedResult = await EnsureConnected();


			try
			{
				// 清除可用数据，以避免读取残留数据
				while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }

				// 创建自定义命令的数据包
				Pck packet = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.UTF8.GetBytes(command));
				networkStream.Write(packet.ToBytes(), 0, packet.Len);

				// 读取响应大小
				int size = networkStream.ReadByte();
				if (size == -1) // 检查是否成功读取到数据
				{
					return "未能接收到命令响应"; // 未能接收到命令的响应
				}
				byte[] data = new byte[size];

				// 异步读取服务器响应
				await networkStream.ReadAsync(data, 0, size);

				// 将响应转换为字符串并返回
				string responseString = Encoding.UTF8.GetString(data.Skip(11).ToArray());
				return responseString;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error", ex);
				return "Error";
			}
		}

		public async Task<bool> EnsureConnected()
		{
			if (Client == null || !Client.Connected)
			{
				return await Connect();
			}
			return true;
		}

		public void ProcessExit(object sender, EventArgs ea)
		{
			if (Client != null)
			{
				if (Client.Connected)
				{
					Client.GetStream().Close();
				}
				Client.Dispose();
				Client = null;
			}
		}
	}
}
