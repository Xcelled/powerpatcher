using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoreLinq;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using Ionic.Zip;

namespace PowerPatcher
{
	public class Patcher
	{
		private const int VERSION_OVERSHOOT = 5;

		public OfficialPatchInfo PatchInfo { get; private set; }

		Form1 ui;

		public static void WriteVersion(int version)
		{
			Logger.Info("Writing version {0} to version.dat", version);
			File.WriteAllBytes("version.dat", BitConverter.GetBytes(version));
		}

		public static int ReadVersion()
		{
			Logger.Info("Reading version from version.dat");
			try
			{
				return BitConverter.ToInt32(File.ReadAllBytes("version.dat"), 0);
			}
			catch
			{
				return 0;
			}
		}

		public Patcher(Form1 form, OfficialPatchInfo opi)
		{
			ui = form;
			PatchInfo = opi;
		}

		public void Patch(PatchSequence sequence)
		{
			Logger.Info("Beginning patch of sequence : {0}", sequence);

			foreach (var patch in sequence.Patches)
				Patch(patch);
		}

		public void Patch(PatchInfo info)
		{
			Logger.Info("Beginning patch {0}", info);

			Directory.CreateDirectory(info.PatchName);

			var toDowload = info.Files;

			try
			{
				do
				{
					toDowload = verifyFiles(info, toDowload);
					downloadFiles(info, toDowload);
				} while (toDowload.Count != 0);

				combineFiles(info);
				extract(info);

				if (PatchInfo["lang"] != null)
					downloadAndExtractLanguage(info);

				copy(info);
				WriteVersion(info.EndVersion);
				ui.SetClientVersionText(info.EndVersion);
			}
			catch
			{
				if (!ui.tokenSource.Token.IsCancellationRequested)
					throw;

				Logger.Info("Current patch cancelled");
			}
			finally
			{
				cleanUp(info);
			}

			Logger.Info("Patch complete");
			ui.SetMainProgressDisplay(0, "");
		}

		private List<PatchFileInfo> verifyFiles(PatchInfo info, List<PatchFileInfo> files)
		{
			Logger.Info("Beginning verification of {0} files: [{1}]", files.Count, string.Join(", ", files.Select(f => f.Filename)));

			ui.SetMainProgressDisplay(0, "Verifying files...");

			var fails = new List<PatchFileInfo>();
			double completed = 0;

			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				foreach (var file in files)
				{
					ui.tokenSource.Token.ThrowIfCancellationRequested();

					ui.SetMainProgressDisplay(text: "Verifying " + file.Filename);

					try
					{
						using (FileStream input = new FileStream(Path.Combine(info.PatchName, file.Filename), FileMode.Open))
						{
							var hash = BitConverter.ToString(md5.ComputeHash(input)).Replace("-", "").ToLower();
							if (hash != file.Md5Hash)
							{
								Logger.Warning("MD5 fail for {0}. Expected hash: {1}\tGot hash: {2}", file.Filename, file.Md5Hash, hash);
								fails.Add(file);
							}

							completed++;

							ui.SetMainProgressDisplay(progressPercent: (int)((completed / files.Count) * 100));
						}
					}
					catch (Exception ex)
					{
						Logger.Warning("Failed verifaction of {0} due to exception: {1}", file.Filename, ex.Message);
						fails.Add(file); 
					}
				}
			}

			Logger.Info("Verification complete. {0} hashfails.", fails.Count);

			return fails;
		}

		private void downloadFiles(PatchInfo info, List<PatchFileInfo> files)
		{
			Logger.Info("Beginning download of {0} files: [{1}]", files.Count, string.Join(", ", files.Select(f => f.Filename)));

			ui.SetMainProgressDisplay(0, "Downloading files...");

			var tasks = new List<Action>();

			int completed = 0;

			foreach (var file in files)
			{
				string url = PatchInfo.MainFtp;
				if (!url.EndsWith("/"))
					url += "/";
				url += info.EndVersion + "/" + file.RemoteName;

				var dl = new FileDownloader(url, Path.Combine(info.PatchName, file.Filename), file.Size, ui.tokenSource.Token);
				var reporter = new ProgressReporter() { Reporter = dl };

				tasks.Add(() =>
				{
					ui.AddProgressReporter(reporter);

					try
					{
						dl.Download();

						ui.SetMainProgressDisplay(progressPercent: (int)((Interlocked.Increment(ref completed) / (double)(files.Count)) * 100), text: "Downloading files... " + completed + " / " + files.Count);
					}
					finally
					{
						ui.RemoveProgressReporter(reporter);
					}
				});
			}

			Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 10, CancellationToken = ui.tokenSource.Token }, tasks.ToArray());

			Logger.Info("All downloads complete");
		}

		private void combineFiles(PatchInfo info)
		{
			Logger.Info("Beginning file combine");

			string message = "Combining patch files... {0} out of {1} at {2}/s";
			ui.SetMainProgressDisplay(0, string.Format(message, 0, info.Files.Count, "0b"));

			var stopwatch = new System.Diagnostics.Stopwatch();

			byte[] buffer = new byte[4096];
			int read, completed = 0;
			long totalRead = 0;

			Logger.Info("Opening {0} for output", info.ZipFilePath);

			using (FileStream output = new FileStream(info.ZipFilePath, FileMode.Create))
			{
				stopwatch.Start();
				System.Threading.Timer t = new System.Threading.Timer((o) =>
					{ ui.SetMainProgressDisplay((int)((completed / (double)info.Files.Count) * 100), string.Format(message, completed, info.Files.Count, ByteSizeHelper.ToString(totalRead / stopwatch.Elapsed.TotalSeconds))); },
					null, 200, 200);

				foreach (var file in info.Files)
				{
					Logger.Info("Adding {0}", file.Filename);
					using (FileStream input = new FileStream(Path.Combine(info.PatchName, file.Filename), FileMode.Open))
					{
						while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
						{
							ui.tokenSource.Token.ThrowIfCancellationRequested();

							output.Write(buffer, 0, read);
							totalRead += read;
						}
					}
					completed++;
				}

				t.Change(Timeout.Infinite, Timeout.Infinite);
				t.Dispose();
			}

			Logger.Info("Patch combine success");

			ui.SetMainProgressDisplay(100, string.Format(message, info.Files.Count, info.Files.Count, ""));
		}

		private void extract(PatchInfo info)
		{
			Logger.Info("Extracting patch zip to {0}", info.ContentDirectory);

			Directory.CreateDirectory(info.ContentDirectory);

			using (var zipFile = ZipFile.Read(info.ZipFilePath))
			{
				zipFile.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
				zipFile.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(zipFile_ExtractProgress);
				zipFile.ExtractAll(info.ContentDirectory);
			}

			Logger.Info("Extract complete");
		}

		private void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e)
		{
			if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
			{
				try
				{
					ui.tokenSource.Token.ThrowIfCancellationRequested();
				}
				catch
				{
					e.Cancel = true; // Ensure we cancel
					throw;
				}
				Logger.Info("Extracting {0}", e.CurrentEntry.FileName);
				ui.SetMainProgressDisplay(text: "Extracting " + e.CurrentEntry.FileName);
			}
			else if (e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
			{
				ui.SetMainProgressDisplay(progressPercent: (int)(((double)e.EntriesExtracted / e.EntriesTotal) * 100));
			}
		}

		private void downloadAndExtractLanguage(PatchInfo info)
		{
			Logger.Info("Beginning download of language pack to {0}", info.LanguageFilePath);

			string message = "Downloading language pack at {0}/s";
			ui.SetMainProgressDisplay(0, string.Format(message, 0, info.Files.Count, "0b"), true);

			var stopwatch = new System.Diagnostics.Stopwatch();

			byte[] buffer = new byte[4096];
			int read, totalRead = 0;

			string url = PatchInfo.MainFtp;
			if (!url.EndsWith("/"))
				url += "/";
			url += info.EndVersion + "/" + info.EndVersion + "_language.p_";


			using (var output = new FileStream(info.LanguageFilePath, FileMode.Create))
			{
				using (var wc = new WebClient())
				{
					using (var input = wc.OpenRead(url))
					{
						stopwatch.Start();

						System.Threading.Timer t = new System.Threading.Timer((o) =>
						{ ui.SetMainProgressDisplay(text: string.Format(message, ByteSizeHelper.ToString(totalRead / stopwatch.Elapsed.TotalSeconds)), indeterminate:true); },
							null, 200, 200);

						while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
						{
							ui.tokenSource.Token.ThrowIfCancellationRequested();

							output.Write(buffer, 0, read);
							totalRead += read;
						}

						stopwatch.Stop();

						t.Change(Timeout.Infinite, Timeout.Infinite);
						t.Dispose();
					}
				}
			}

			Logger.Info("Download complete. Speed: {0}/s", ByteSizeHelper.ToString(totalRead / stopwatch.Elapsed.TotalSeconds));

			Logger.Info("Extracting language pack to {0}", info.LanguageContentDir);
			ui.SetMainProgressDisplay(0, "Extracting language pack");

			using (var zip = ZipFile.Read(info.LanguageFilePath))
			{
				zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
				zip.ExtractAll(info.LanguageContentDir);
			}

			ui.tokenSource.Token.ThrowIfCancellationRequested();

			Logger.Info("Extraction complete");
		}

		private void copy(PatchInfo info)
		{
			Logger.Info("Preparing to copy patch files");
			ui.SetMainProgressDisplay(0, "Copying patch files...");

			var dst = Environment.CurrentDirectory;
			var src = info.ContentDirectory;
			var files = Directory.GetFiles(src, "*", SearchOption.AllDirectories);

			Logger.Info("Files to copy: [{0}]", string.Join(", ", files.Select(f => Path.GetFileName(f))));

			int completed = 0;

			var tasks = new List<Action>();

			foreach (var file in files)
			{
				FileCopier copier = new FileCopier(file, file.Replace(src, dst), ui.tokenSource.Token);
				ProgressReporter reporter = new ProgressReporter() { Reporter = copier };

				tasks.Add(() =>
				{
					ui.AddProgressReporter(reporter);

					try
					{
						copier.Copy();

						ui.SetMainProgressDisplay(progressPercent: (int)((Interlocked.Increment(ref completed) / (double)(files.Length)) * 100), text: "Copying patch files... " + completed + " / " + files.Length);
					}
					finally
					{
						ui.RemoveProgressReporter(reporter);
					}
				});
			}

			Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 10, CancellationToken = ui.tokenSource.Token }, tasks.ToArray());

			Logger.Info("Copy done");
		}

		private void cleanUp(PatchInfo info)
		{
			Logger.Info("Beginning clean up");

			if (Properties.Settings.Default.DeletePartFiles)
			{
				Logger.Info("Deleting part files...");
				ui.SetMainProgressDisplay(0, "Cleaning part files", true);

				foreach (var file in info.Files.Select(f => Path.Combine(info.PatchName, f.Filename)))
				{
					tryDeleteFile(file);
				}
				Logger.Info("Done deleting part files");
			}

			if (Properties.Settings.Default.DeleteZips)
			{
				ui.SetMainProgressDisplay(0, "Cleaning zip files", true);

				foreach (var file in new string[] { info.ZipFilePath, info.LanguageFilePath })
				{
					tryDeleteFile(file);
				}
			}

			if (Properties.Settings.Default.DeleteContent)
			{
				Logger.Info("Deleting content");

				ui.SetMainProgressDisplay(0, "Cleaning temporary content directory", true);

				tryDeleteDirectory(info.ContentDirectory);

				Logger.Info("Content deleted");
			}

			if (Properties.Settings.Default.DeleteContent && Properties.Settings.Default.DeletePartFiles && Properties.Settings.Default.DeleteZips)
			{
				Logger.Info("Removing empty patch folder {0}", info.PatchName);

				ui.SetMainProgressDisplay(0, "Cleaning patch folder", true);

				tryDeleteDirectory(info.PatchName);
			}

			Logger.Info("Clean up finished");
		}

		private void tryDeleteFile(string file)
		{
			Logger.Info("Deleting {0}", file);
			try
			{
				File.Delete(file);
			}
			catch (Exception ex)
			{
				Logger.Warning("Can't delete {0}: {1}", file, ex.Message);
			}
		}

		private void tryDeleteDirectory(string dir)
		{
			try
			{
				Directory.Delete(dir, true);
			}
			catch (Exception ex)
			{
				Logger.Warning("Can't delete {0}: {1}", dir, ex.Message);
			}
		}

		public void RedownloadLanugagePack()
		{
			var version = ReadVersion();

			string url = PatchInfo.MainFtp;
			if (!url.EndsWith("/"))
				url += "/";
			url += version + "/" + version + "_language.p_";

			string tmpPath = "lang.tmp";

			Logger.Info("Redownloading language pack from {0} to {1}", url, tmpPath);
			ui.SetMainProgressDisplay(0, "Downloading language pack", true);

			using (var wc = new WebClient())
			{
				wc.DownloadFile(url, tmpPath);
			}

			Logger.Info("Extracting lang pack");
			ui.SetMainProgressDisplay(0, "Extracting language pack", true);
			using (var zip = ZipFile.Read(tmpPath))
			{
				zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
				zip.ExtractAll("package");
			}

			Logger.Info("Deleting temporary file");
			ui.SetMainProgressDisplay(0, "Cleaning up", true);
			File.Delete(tmpPath);

			Logger.Info("Redownload complete");
			ui.SetMainProgressDisplay(0, "");

			MessageBox.Show("Redownload complete");
		}

		public PatchSequence FindSequence(int bottomVersion = -1, int topVersion = -1)
		{
			if (bottomVersion == -1)
				bottomVersion = ReadVersion();

			if (topVersion == -1)
				topVersion = PatchInfo.MainVersion;

			Logger.Info("Attempting to find sequence from {0} to {1}", bottomVersion, topVersion);

			List<PatchInfo> infos = new List<PatchInfo>();

			PatchInfo pi;

			string patchMsg = "Attempting to patch from {0} to {1}. Currently checking {2}.";

			try
			{
				using (WebClient wc = new WebClient())
				{
					if (bottomVersion == 0)
					{
						ui.SetMainProgressDisplay(0, string.Format(patchMsg, 0, topVersion, topVersion));
						pi = tryGetPatchInfo(wc, topVersion, topVersion + "_full.txt");
						if (pi != null)
						{
							infos.Add(pi);
						}
						else
						{
							throw new PatchSequenceNotFoundException(0, topVersion);
						}
					}
					else
					{
						int current = bottomVersion;
						for (int i = topVersion; current != topVersion; )
						{
							ui.SetMainProgressDisplay((int)(((double)current - bottomVersion) / (topVersion - bottomVersion)), string.Format(patchMsg, bottomVersion, topVersion, i));
							pi = tryGetPatchInfo(wc, i, current + "_to_" + i + ".txt");
							if (pi != null)
							{
								infos.Add(pi);
								current = i;
								i += VERSION_OVERSHOOT;
							}
							else
							{
								if (--i == current)
								{
									return FindSequence(0, topVersion);
								}
							}
						}
					}
				}
			}
			finally
			{				
				ui.SetMainProgressDisplay(0, "");
			}

			return new PatchSequence(infos);
		}

		private PatchInfo tryGetPatchInfo(WebClient wc, int version, string filename)
		{
			string url = PatchInfo.MainFtp;
			if (!url.EndsWith("/"))
				url += "/";
			url += version + "/" + filename;

			ui.tokenSource.Token.ThrowIfCancellationRequested();

			Logger.Info("Trying to get patch info from {0}", url);

			try
			{
				return new PatchInfo(Path.GetFileNameWithoutExtension(filename), wc.DownloadString(url));
			}
			catch (WebException)
			{
				return null;
			}
		}
	}
}
