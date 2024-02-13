using Microsoft.Extensions.Logging;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
	public class RconClient
	{
		private readonly ILogger<RconClient> _logger;
		private readonly String _host;
		private readonly int _port;
		private readonly String _password;

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
				_logger?.LogInformation("Trying to connect with RCON protocol");
				Task ppp = Client.ConnectAsync(_host, _port);
				ppp.Wait();
			}
			catch (Exception e)
			{
				_logger?.LogError("Error", e);
				return false;
			}


			if (!Client.Connected)
			{
				_logger?.LogError("RCON Client unable to connect");
				Client.Dispose();
				Client = null;
				return false;
			}

			networkStream = Client.GetStream();

			Pck auth = new Pck(0xf5, PacketType.AUTH, Encoding.ASCII.GetBytes(_password));

			networkStream.Write(auth.ToBytes(), 0, auth.Len);

			byte[] size = new byte[4];
			int bytesread = networkStream.Read(size, 0, size.Length);
			if (bytesread == -1 || bytesread != 4)
			{
				return false;
			}
			int realSize = BitConverter.ToInt32(size, 0);
			_logger?.LogTrace($"RCON packet body size: {realSize}");
			byte[] data = new byte[realSize];

			await networkStream.ReadAsync(data, 0, realSize);

			if (data[0] != 0xf5)
			{
				_logger?.LogCritical("RCON passowrd wrong. Exiting...");
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

		public async Task<String> SendCommand(String command)
		{
			var ensureConnectedResult = await EnsureConnected();
			if (!ensureConnectedResult)
			{
				throw new Exception("Unable to connect to RCON");
			}

			try
			{
				Pck packet = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.UTF8.GetBytes(command));
				await networkStream.WriteAsync(packet.ToBytes(), 0, packet.Len);

				byte[] size = new byte[4];
				int bytesread = networkStream.Read(size, 0, size.Length);
				if (bytesread == -1 || bytesread != 4)
				{
					throw new Exception("RCON packet malformed");
				}
				int realSize = BitConverter.ToInt32(size, 0);
				_logger?.LogTrace($"RCON packet body size: {realSize}");

				Memory<byte> memory = new(new byte[realSize]);

				await networkStream.ReadAsync(memory);

				return Encoding.UTF8.GetString(memory.Slice(8, realSize - 8 - 2).Span).Trim();
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error");
				ProcessExit(null, null);
				throw;
			}
		}

		public async Task<bool> EnsureConnected()
		{
			bool statusConnected = false;

			if (Client != null && Client.Client != null && Client.Client.Connected)
			{
				/* pear to the documentation on Poll:
				 * When passing SelectMode.SelectRead as a parameter to the Poll method it will return 
				 * -either- true if Socket.Listen(Int32) has been called and a connection is pending;
				 * -or- true if data is available for reading; 
				 * -or- true if the connection has been closed, reset, or terminated; 
				 * otherwise, returns false
				 */

				// Detect if client disconnected
				if (Client.Client.Poll(0, SelectMode.SelectRead))
				{
					byte[] buff = new byte[1];
					try
					{
						if (Client.Client.Receive(buff, SocketFlags.Peek) == 0)
						{
							// Client disconnected
							statusConnected = false;
						}
						else
						{
							statusConnected = true;
						}
					} catch
					{
						statusConnected = false;
					}
				}

				statusConnected = true;
			}

			if (!statusConnected)
			{
				_logger?.LogError("RCON Socket not connected, opening it again");
				return await Connect();
			}
			_logger?.LogTrace("RCON socket still open, continuing");
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
