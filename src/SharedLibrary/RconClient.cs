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

        public async Task<byte[]> SendCommand(String command)
        {
            var ensureConnectedResult = await EnsureConnected();
            if (!ensureConnectedResult)
            {
                return null;
            }


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
                    return null;
                }
                byte[] data = new byte[size];

                // 异步读取服务器响应
                await networkStream.ReadAsync(data, 0, size);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return null;
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
