using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PowerPatcher
{
	public class PatchInfo
	{
		public int StartVersion { get; protected set; }
		public int EndVersion { get; protected set; }
		public List<PatchFileInfo> Files { get; protected set; }
		public long PatchSize { get { return Files.Sum(f => (long)f.Size); } }
		public string PatchName { get { return StartVersion == 0 ? (EndVersion + "_full") : (StartVersion + "_to_" + EndVersion); } }

		public string ZipFilePath { get { return Path.Combine(PatchName, Path.ChangeExtension(PatchName, ".zip")); } }
		public string LanguageFilePath { get { return Path.Combine(PatchName, "language.zip"); } }
		public string ContentDirectory { get { return Path.Combine(PatchName, "content"); } }
		public string LanguageContentDir { get { return Path.Combine(ContentDirectory, "package"); } }

		public PatchInfo(string name, string info)
		{
			if (name.Contains("_to_"))
			{
				var split = name.Split(new string[] { "_to_" }, StringSplitOptions.None);
				StartVersion = int.Parse(split[0]);
				EndVersion = int.Parse(split[1]);
			}
			else
			{
				StartVersion = 0;
				EndVersion = int.Parse(new string(name.TakeWhile(c => c != '_').ToArray()));
			}

			Files = new List<PatchFileInfo>();

			using (var r = new System.IO.StringReader(info))
			{
				r.ReadLine(); // Count

				while (r.Peek() != -1)
				{
					var line = r.ReadLine();
					if (string.IsNullOrWhiteSpace(line))
						continue;

					var parts = line.Split(new string[] { ", " }, StringSplitOptions.None);
					Files.Add(new PatchFileInfo(parts[0], int.Parse(parts[1]), parts[2]));
				}
			}
		}

		public override string ToString()
		{
			return string.Format("{0}, {1} files, {2}", PatchName, Files.Count, ByteSizeHelper.ToString(PatchSize));
		}
	}

	public class PatchFileInfo
	{
		public string RemoteName { get; protected set; }
		public string Filename { get { return System.IO.Path.GetFileName(RemoteName); } }
		public int Size { get; protected set; }
		public string Md5Hash { get; protected set; }

		public PatchFileInfo(string remotePath, int size, string md5)
		{
			this.RemoteName = remotePath;
			this.Size = size;
			this.Md5Hash = md5;
		}
	}

	public class PatchSequence
	{
		public int StartVersion { get { return Patches.First().StartVersion; } }
		public int EndVersion { get { return Patches.Last().EndVersion; } }
		public List<PatchInfo> Patches { get; protected set; }
		public long Size { get { return Patches.Sum(p => p.PatchSize); } }

		public PatchSequence(IEnumerable<PatchInfo> patches)
		{
			Patches = new List<PatchInfo>(patches.OrderBy(p => p.StartVersion));
		}

		public override string ToString()
		{
			return string.Format("Sequence from {0} to {1}. {2} files. Total size: {3}", StartVersion, EndVersion, Patches.Sum(p => p.Files.Count), ByteSizeHelper.ToString(Size)); 
		}
	}
}
