using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Net;
namespace Palworld_server_protector_DotNet
{
    public class PalUserInfo
    {
        public string name;
        public string uid;
        public string steam_id;

        public PalUserInfo(string row)
        {
            var splits = row.Split(',');
            name = splits[0];
            uid = splits[1];
            steam_id = splits[2];
        }
    }
    internal class Rcon
    {
        internal static TcpClient Client;
        private static NetworkStream networkStream;

        static Form form;

        [STAThread]
        static async Task Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);

            AppDomain.CurrentDomain.ProcessExit += ProcessExit;
        }

        public static async Task<string> Connect(string address, Int32 port, string pass)
        {
            Client = new TcpClient();

            try
            {
                await Client.ConnectAsync(address, port);
            }
            catch(Exception e)
            {
                Task.Run(() => Logger.AppendToErrorLog($"连接时发生错误: {e.Message}"));
                return "连接失败";
            }
        

            if (!Client.Connected)
            {
                Client.Dispose();
                Client = null;
                return "连接失败";
            }

            networkStream = Client.GetStream();

            Pck auth = new Pck(0xf5, PacketType.AUTH, Encoding.ASCII.GetBytes(pass));

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
                return "密码认证失败";
            }

            var output = await SInfo();

            return string.Format("连接成功" + Environment.NewLine + $"{output}");
        }

        public static async Task<string> SInfo(bool keep = false)
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

        public static async Task<List<PalUserInfo>> GetPlayers(string address, Int32 port, string pass)
        {
            var result = new List<PalUserInfo>();
            var ensureConnectedResult = await EnsureConnected(address, port, pass);
            if (ensureConnectedResult != "连接成功")
            {
                return result; // 如果连接失败，返回失败消息
            }

            try
                {
                    if (networkStream != null)
                    {
                        while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
                        Pck pck = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.ASCII.GetBytes("showplayers"));
                        networkStream.Write(pck.ToBytes(), 0, pck.Len);

                        int size = networkStream.ReadByte();
                        byte[] data = new byte[size];

                        await networkStream.ReadAsync(data, 0, size);

                        string resstring = Encoding.UTF8.GetString(data.Skip(34).ToArray());
                    
                    var lines = resstring.Split('\n');

                    if (lines.Length < 1)
                    {
                        return result;
                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i];

                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        result.Add(new PalUserInfo(line));
                    }
            /*            string[] players = resstring.Split('\n');

                        string output = "";

                        for (int i = 1; i <= players.Length; i++)
                        {
                            output += $"{i}:" + players[i - 1] + Environment.NewLine;
                        }

                        return output;*/
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Task.Run(() => Logger.AppendToErrorLog($"发送命令时发生错误: {ex.Message}"));
                    return result;
                }
            
            return result;
        }

        public static async Task<string> Broadcast(string address, Int32 port, string pass, string text)
        {
            var ensureConnectedResult = await EnsureConnected(address, port, pass);
            if (ensureConnectedResult != "连接成功")
            {
                return ensureConnectedResult; // 如果连接失败，返回失败消息
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
                    Task.Run(() => Logger.AppendToErrorLog($"发送命令时发生错误: {ex.Message}"));
                    return "发送失败";
                }
            
            return "发送失败";
        }

        public static async Task Reload()
        {
            if (Client.Connected)
            {
                await SInfo(true);
            }
        }

        public static async Task<string> ShutDown(string address, Int32 port, string pass, string time, string text)
        {
            var ensureConnectedResult = await EnsureConnected(address, port, pass);
            if (ensureConnectedResult != "连接成功")
            {
                return ensureConnectedResult; // 如果连接失败，返回失败消息
            }

            try
            {
                    if (networkStream != null)
                    {
                        while (networkStream.DataAvailable) { _ = networkStream.ReadByte(); }
                        Pck packet = new Pck(0x1f, PacketType.EXECCOMMAND, Encoding.ASCII.GetBytes("shutdown " + time + " " + text));
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
                    Task.Run(() => Logger.AppendToErrorLog($"发送命令时发生错误: {ex.Message}"));
                    return "发送失败";
                }
            
            return "发送失败";
        }

        public static async Task<string> SendCommand(string address, Int32 port, string pass,string command)
        {
            // 检查客户端是否已连接
            var ensureConnectedResult = await EnsureConnected(address, port, pass);


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
                // 异常处理，返回错误信息
                Task.Run(() => Logger.AppendToErrorLog($"发送命令时发生错误: {ex.Message}"));
                return $"发送命令时发生错误。"; // 发送命令时发生错误
            }
        }

        // 这个函数尝试确保客户端连接到服务器。如果客户端未连接，它会尝试建立连接。
        public static async Task<string> EnsureConnected(string address, Int32 port, string pass)
        {
            if (Client == null || !Client.Connected)
            {
                // 尝试连接
                return await Connect(address, port, pass);
            }
            return "连接成功"; // 如果已连接，返回成功消息
        }


        [Serializable]
        public class Pck
        {
            public int Size;
            public int ID;
            public PacketType Type;
            public byte[] Body;

            public Pck(int id, PacketType type, byte[] body)
            {
                ID = id; Type = type; Body = body.Concat(new byte[] { 0x00, 0x00 }).ToArray(); Size = body.Length + 10;
            }

            internal byte[] ToBytes()
            {
                byte[] res = new byte[Size + 4];
                BitConverter.GetBytes(Size).CopyTo(res, 0);
                BitConverter.GetBytes(ID).CopyTo(res, 4);
                BitConverter.GetBytes((int)Type).CopyTo(res, 8);
                Body.CopyTo(res, 12);
                return res;
            }

            internal int Len => Body.Length + 12;
        }

        public enum PacketType : int
        {
            RESPONSE_VALUE = 0,
            AUTH_RESPONSE = 2,
            EXECCOMMAND = 2,
            AUTH = 3
        }

        public static void ProcessExit(object sender, EventArgs ea)
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
