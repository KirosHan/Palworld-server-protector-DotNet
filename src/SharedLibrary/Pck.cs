using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedLibrary.RconClient;

namespace SharedLibrary
{
	[Serializable]
	public class Pck
	{
		public int Size;
		public int ID;
		public PacketType Type;
		public byte[] Body;

		public Pck(int id, PacketType type, byte[] body)
		{
			ID = id; Type = type; Body = [.. body, .. new byte[] { 0x00, 0x00 }]; Size = body.Length + 10;
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

}
