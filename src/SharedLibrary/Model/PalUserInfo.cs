using System;

namespace SharedLibrary.Model
{
	public class PalUserInfo : IEquatable<PalUserInfo>
	{
		public String Name { get; set; }
		public String Uid { get; set; }
		public long SteamId { get; set; }

		public override bool Equals(object obj)
		{
			return obj is PalUserInfo info && this.Equals(info);
		}

		public bool Equals(PalUserInfo other)
		{
			return SteamId == other.SteamId;
		}

		public override int GetHashCode()
		{
			return SteamId.GetHashCode();
		}
		public override string ToString()
		{
			return $"{Name}\t{Uid}\t{SteamId}";
		}

		public static PalUserInfo Parse(String row)
		{
			var splits = row.Split(',');
			String name = splits[0];
			String uid = splits.Length > 1 ? splits[1] : "";
			String steamId = splits.Length > 2 ? splits[2] : "";
			return new PalUserInfo()
			{
				Name = name,
				Uid = uid,
				SteamId = long.Parse(steamId)
			};
		}

	}
}
