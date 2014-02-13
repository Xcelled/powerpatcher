using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PowerPatcher
{
	public class FileCopier : IProgressReporter
	{
		private string src, dst;
		private double fSize;
		private long totalRead;
		private Stopwatch s = new Stopwatch();
		private CancellationToken tok;

		public FileCopier(string source, string destination, CancellationToken token)
		{
			src = source;
			dst = destination;
			tok = token;
		}

		public void Copy()
		{
			Directory.CreateDirectory(Path.GetDirectoryName(dst));

			Logger.Info("Copying from {0} to {1}", src, dst);

			using (var output = new FileStream(dst, FileMode.Create))
			{
				using (var input = new FileStream(src, FileMode.Open))
				{
					fSize = input.Length;
					s.Start();
					byte[] buffer = new byte[4096];
					int read;
					while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
					{
						tok.ThrowIfCancellationRequested();

						output.Write(buffer, 0, read);
						totalRead += read;
					}

					s.Stop();
				}
			}

			Logger.Info("Finished copying to {0}. Speed: {1}/s", dst, ByteSizeHelper.ToString(fSize / s.Elapsed.TotalSeconds));
		}

		public string LeftText
		{
			get { return Path.GetFileName(src); }
		}

		public int ProgressBarPercent
		{
			get { return (int)((totalRead / fSize) * 100); }
		}

		public string RightText
		{
			get
			{
				if (s.Elapsed.TotalSeconds == 0)
					return "";

				return ByteSizeHelper.ToString(totalRead) + " copied at " + ByteSizeHelper.ToString(totalRead / s.Elapsed.TotalSeconds) + "/s";
			}
		}
	}
}
