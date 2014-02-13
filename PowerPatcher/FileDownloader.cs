using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PowerPatcher
{
	public class FileDownloader : IProgressReporter
	{
		private string url, localFile;
		private double fSize;
		private long totalRead;
		private Stopwatch s = new Stopwatch();
		private CancellationToken tok;

		public FileDownloader(string remote, string local, int size, CancellationToken token)
		{
			url = remote;
			localFile = local;
			fSize = size;
			tok = token;
		}

		public void Download()
		{
			Logger.Info("Beginning download of {0} to {1}", url, localFile);
 
			using (FileStream output = new FileStream(localFile, FileMode.Create))
			{
				using (var wc = new WebClient())
				{
					using (var input = wc.OpenRead(url))
					{
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
			}

			Logger.Info("Download of {0} complete, average speed {1}/s", localFile, ByteSizeHelper.ToString(fSize / s.Elapsed.TotalSeconds));
		}

		public string LeftText
		{
			get { return Path.GetFileName(url); }
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

				return ByteSizeHelper.ToString(totalRead) + " read at " + ByteSizeHelper.ToString(totalRead / s.Elapsed.TotalSeconds) + "/s";
			}
		}
	}
}
