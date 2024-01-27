using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
namespace Palworld_server_protector_DotNet
{

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

        public static async Task<string> Connect(string address, string port, string pass)
        {
            Client = new TcpClient();

            try
            {
                await Client.ConnectAsync(address, int.Parse(port));
            }
            catch
            {
                return "Rcon连接失败";
            }

            if (!Client.Connected)
            {
                Client.Dispose();
                Client = null;
                return "Rcon连接失败";
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
                return "认证失败";
            }

            var output = await SInfo();

            return string.Format("连接成功" + Environment.NewLine + $"{output}");
        }

        public static async Task<string> SInfo(bool keep = false)
        {
            if (Client != null)
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

            return "";
        }

        public static async Task<string> GetPlayers()
        {
            if (Client != null)
            {
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
                        string[] players = resstring.Split('\n');

                        string output = "";

                        for (int i = 1; i <= players.Length; i++)
                        {
                            output += $"{i}:" + players[i - 1] + Environment.NewLine;
                        }

                        return resstring;
                    }
                    return "";
                }
                catch (NullReferenceException)
                {
                    return "获取玩家失败";
                }
            }
            return "获取玩家失败";
        }

        public static async Task<string> Broadcast(string text)
        {
            if (Client != null)
            {
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
                catch (NullReferenceException)
                {
                    return "广播失败";
                }
            }
            return "广播失败";
        }

        public static async Task Reload()
        {
            if (Client.Connected)
            {
                await SInfo(true);
            }
        }

        /*public static async Task<string> ShutDown(string time, string text)
        {

        }*/

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
