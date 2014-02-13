using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PowerPatcher
{
	public class OfficialPatchInfo
	{
		private Dictionary<string, string> info = new Dictionary<string, string>();
		public bool PatchAccept;
		public int MainVersion;
		public string MainFtp;

		public string this[string index]
		{
			get
			{
				if (info.ContainsKey(index))
					return info[index];
				return null;
			}
			protected set
			{
				info[index] = value;
			}
		}

		public static OfficialPatchInfo Parse(string url)
		{
			var ret = new OfficialPatchInfo();

			Logger.Info("Downloading official patch information from {0}", url);
			using (var wc = new System.Net.WebClient())
			{
				var opi = wc.DownloadString(url);
				Logger.Info("Official Patch Info:\r\n{0}", opi);
				using (var r = new System.IO.StringReader(opi))
				{
					while (r.Peek() != -1)
					{
						var line = r.ReadLine();
						if (string.IsNullOrWhiteSpace(line) || !line.Contains('='))
							continue;
						var parts = line.Split(new char[] { '=' }, 2);
						ret.info[parts[0]] = parts[1];
					}
				}
			}

			ret.PatchAccept = Convert.ToBoolean(int.Parse(ret["patch_accept"]));
			ret.MainVersion = int.Parse(ret["main_version"]);
			ret.MainFtp = parseFtp(ret, "main_ftp");

			return ret;
		}

		private static Regex authRegex = new Regex(@"(://)?(([^:]+):([^@]+)@)");

		private static string parseFtp(OfficialPatchInfo opi, string id)
		{
			var url = opi[id];
			var m = authRegex.Match(url);
			if (m.Success)
			{
				opi["username"] = m.Groups[3].Value;
				opi["password"] = m.Groups[4].Value;
			}

			if (!url.Contains("://"))
			{
				url = (url.Contains(":80") ? "http://" : "ftp://") + url;
			}

			opi[id] = url;

			return url;
		}
	}
}
