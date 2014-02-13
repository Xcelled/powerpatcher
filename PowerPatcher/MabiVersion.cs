using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPatcher
{
	public class MabiVersion
	{
		public string Name { get; protected set; }
		public string PatchInfoUrl { get; protected set; }
		public bool SmartPatch { get; protected set; }

		public MabiVersion(string name, string url)
		{
			Name = name;
			PatchInfoUrl = url;
			SmartPatch = false;
		}

		public OfficialPatchInfo GetPatchInfo()
		{
			var opi = OfficialPatchInfo.Parse(PatchInfoUrl);

			if (Name.StartsWith("North America")) // Thanks, Nexon
			{
				var li = opi.MainFtp.LastIndexOf("/game");
				if (li != -1)
					opi.MainFtp = opi.MainFtp.Remove(li);
			}

			return opi;
		}

		public override string ToString()
		{
			return Name;
		}

		public readonly static List<MabiVersion> Versions = new List<MabiVersion>(10)
			{
				new MabiVersion("Japan", "http://patch.mabinogi.jp/patch/patch.txt"),
				new MabiVersion("Japan Hangame", "http://patch.mabinogi.jp/patch/patch_hangame.txt"),
				new MabiVersion("Korea", "http://211.218.233.238/patch/patch.txt"),
				new MabiVersion("Korea Test", "http://211.218.233.238/patch/patch_test.txt"),
				new MabiVersion("North America", "http://mabipatchinfo.nexon.net/patch/patch.txt"),
				new MabiVersion("Taiwan", "http://tw.mabipatch.mabinogi.gamania.com/mabinogi/patch.txt")
			};
	}
}
