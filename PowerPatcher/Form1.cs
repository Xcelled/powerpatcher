using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace PowerPatcher
{
	public partial class Form1 : Form
	{
		private readonly Control[] disableWhenPatching;

		private volatile bool _patching = false;
		public bool IsPatching
		{
			get
			{
				return _patching;
			}
			protected set
			{
				if (InvokeRequired)
					Invoke(new Action(() => IsPatching = value));
				else
				{
					_patching = value;

					foreach (var c in disableWhenPatching)
						c.Enabled = !value;

					cancelBtn.Enabled = value;
					cancelBtn.Text = "Cancel Patch";
					if (tokenSource != null)
						tokenSource.Dispose();
					tokenSource = new CancellationTokenSource();
				}
			}
		}

		public CancellationTokenSource tokenSource { get; protected set; }

		Patcher patcher;

		public Form1()
		{
			InitializeComponent();

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

			disableWhenPatching = new Control[]
			{
				startGameBtn,
				patchToBtn,
				writeCustomVersionBtn,
				redownloadInFullBtn,
				redownloadLangBtn,
				localSelect
			};
		}

		public void SetClientVersionText(int version)
		{
			if (InvokeRequired)
				Invoke(new Action<int>(SetClientVersionText), version);
			else
			{
				clientVersionLabel.Text = version.ToString();
			}
		}

		public void SetMainProgressDisplay(int progressPercent = -1, string text = null, bool indeterminate = false)
		{
			if (InvokeRequired)
				Invoke(new Action<int, string, bool>(SetMainProgressDisplay), progressPercent, text, indeterminate);
			else
			{
				if (progressPercent >= 0)
					progressBar1.Value = progressPercent;
				if (text != null)
					progressLabel.Text = text;

				progressBar1.Style = (indeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
			}
		}

		public void AddProgressReporter(ProgressReporter reporter)
		{
			if (InvokeRequired)
				Invoke(new Action<ProgressReporter>(AddProgressReporter), reporter);
			else
			{
				try
				{
					progressContainer.Controls.Add(reporter);
					reporter.Width = progressContainer.Width - 2;
				}
				catch { }
			}
		}

		public void RemoveProgressReporter(ProgressReporter reporter)
		{
			if (InvokeRequired)
				Invoke(new Action<ProgressReporter>(RemoveProgressReporter), reporter);
			else
				progressContainer.Controls.Remove(reporter);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			var ppVers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			Logger.Info("Power Patcher version: {0}", ppVers);
			var mabiVers = Patcher.ReadVersion();
			Logger.Info("Mabinogi version: {0}", mabiVers);
			ppVersionLabel.Text = ppVers.ToString();
			SetClientVersionText(mabiVers);

			localSelect.Items.AddRange(MabiVersion.Versions.ToArray());
			localSelect.SelectedIndex = MabiVersion.Versions.IndexOf(
				MabiVersion.Versions.FirstOrDefault(v => v.ToString() == Properties.Settings.Default.Locale) ??
				MabiVersion.Versions.FirstOrDefault(v => v.Name.StartsWith("North Am")) ?? MabiVersion.Versions.First());

			Task.Factory.StartNew(() => CheckForUpdates());
			Task.Factory.StartNew(() => webBrowser1.Navigate(Properties.Settings.Default.StartupPage));
		}

		private void CheckForUpdates()
		{
			IsPatching = true;
			localSelect.SelectedIndex = MabiVersion.Versions.IndexOf(
	MabiVersion.Versions.FirstOrDefault(v => v.ToString() == Properties.Settings.Default.Locale) ??
	MabiVersion.Versions.FirstOrDefault(v => v.Name.StartsWith("North Am")) ?? MabiVersion.Versions.First());

			if (patcher.PatchInfo.MainVersion > Patcher.ReadVersion())
			{
				try
				{
					var seq = patcher.FindSequence();
					patcher.Patch(seq);
				}
				catch (PatchSequenceNotFoundException ex)
				{
					MessageBox.Show(ex.Message, "Can't find patch", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception exc)
				{
					Logger.Warning("Failed to patch to version: {0}", exc.ToString());
					MessageBox.Show("Failed to patch to version. (See the log for more details)");
				}

			}

			IsPatching = false;
		}

		private void progressUpdater_Tick(object sender, EventArgs e)
		{
			try
			{
				foreach (var c in progressContainer.Controls.Cast<ProgressReporter>())
				{
					c.UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
			}
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			tokenSource.Cancel();
			cancelBtn.Enabled = false;
			cancelBtn.Text = "Cancelling...";
		}

		private void progressContainer_SizeChanged(object sender, EventArgs e)
		{
			foreach (var c in progressContainer.Controls.Cast<ProgressReporter>())
			{
				c.Width = progressContainer.Width - 2;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (IsPatching)
			{
				e.Cancel = true;
				cancelBtn_Click(sender, e);
				closeTimer.Start();
				return;
			}
		}

		private void closeTimer_Tick(object sender, EventArgs e)
		{
			if (!IsPatching)
			{
				this.Close();
				closeTimer.Stop();
			}
		}

		private void startGameBtn_Click(object sender, EventArgs e)
		{
			Logger.Info("Beginning client launch...");

			try
			{
				runCommands(Properties.Settings.Default.PreArgs);

				var args = Properties.Settings.Default.CustomArgs;
				if (string.IsNullOrEmpty(args))
					args = string.Format("code:1622 ver:{0} logip:{1} logport:11000 {2}", Patcher.ReadVersion(), patcher.PatchInfo["login"], patcher.PatchInfo["arg"]);

				if (Program.CmdArgs.Any(a => a.ToLower().StartsWith("/p")))
				{
					args += " " + Program.CmdArgs.First(a => a.ToLower().StartsWith("/p"));
				}

				Logger.Info("Starting {0} with the following args: {1}", Properties.Settings.Default.TargetName, args);

				try
				{
					Process.Start(Properties.Settings.Default.TargetName, args);
				}
				catch (Exception ex)
				{
					Logger.Error("Cannot start Mabinogi: {0}", ex.ToString());
					MessageBox.Show("Cannot start Mabinogi: " + ex.Message);

					throw new IOException();
				}

				runCommands(Properties.Settings.Default.PostArgs);

				Logger.Info("Client start success");

				if (Properties.Settings.Default.CloseAfterStart)
					Close();
			}
			catch (IOException)
			{
				Logger.Warning("Client start finished with errors");
			}
		}

		private static void runCommands(string commands)
		{
			using (var sr = new StringReader(commands))
			{
				string cmd;

				while (sr.Peek() != -1)
				{
					cmd = sr.ReadLine();

					if (string.IsNullOrWhiteSpace(cmd))
						continue;

					Logger.Info("Executing \"{0}\" on the command line", cmd);

					try
					{
						using (var p = System.Diagnostics.Process.Start("cmd.exe", "/c " + cmd))
						{
							p.WaitForExit();
						}
					}
					catch (Exception ex)
					{
						Logger.Warning("Error while running command \"{0}\" : {1}", cmd, ex.Message);
						var response = MessageBox.Show(string.Format("There was a problem running the following command:\r\n\r\n{0}\r\n\r\nThe error is: {1}\r\n\r\nAbort client launch?", cmd, ex.Message), "Error", MessageBoxButtons.YesNo);
						Logger.Info("User chose to terminate client start: {0}", response);
						if (response == DialogResult.Yes)
							throw new IOException();
					}
				}
			}
		}

		private void localSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			MabiVersion version = (MabiVersion)localSelect.Items[localSelect.SelectedIndex];

			Properties.Settings.Default.Locale = version.ToString();

			patcher = new Patcher(this, version.GetPatchInfo());

			if (patcher.PatchInfo["patch_accept"] != "1")
			{
				MessageBox.Show("Mabinogi is currently offline!");
			}
		}

		private void writeCustonVersionBtn_Click(object sender, EventArgs e)
		{
			Patcher.WriteVersion((int)cVersionNumber.Value);
			SetClientVersionText((int)cVersionNumber.Value);
		}

		private void patchToBtn_Click(object sender, EventArgs e)
		{
			IsPatching = true;
			Task.Factory.StartNew(() =>
			{
				try
				{
					var seq = patcher.FindSequence(topVersion: (int)patchToVersion.Value);
					patcher.Patch(seq);
				}
				catch (PatchSequenceNotFoundException ex)
				{
					MessageBox.Show(ex.Message, "Can't find patch", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception exc)
				{
					Logger.Warning("Failed to patch to version: {0}", exc.ToString());
					MessageBox.Show("Failed to patch to version. (See the log for more details)");
				}

				IsPatching = false;
			});
		}

		private void redownloadInFullBtn_Click(object sender, EventArgs e)
		{
			IsPatching = true;
			Task.Factory.StartNew(() =>
			{
				var clean = MessageBox.Show("Would you like to clean the package folder? (If unsure, press Yes)", "Clean?", MessageBoxButtons.YesNo);
				Logger.Info("Redownload in full: User opts to clean package folder: {0}", clean);

				if (clean == System.Windows.Forms.DialogResult.Yes)
				{
					var files = Directory.GetFiles("package", "*.pack");

					foreach (var file in files)
					{
						Logger.Info("Cleaning {0}", file);

						try
						{
							File.Delete(file);
						}
						catch (Exception ex)
						{
							Logger.Warning("Unable to delete: {0}", ex.ToString());
						}
					}
				}

				try
				{
					var seq = patcher.FindSequence(bottomVersion: 0, topVersion: Patcher.ReadVersion());
					patcher.Patch(seq);
				}
				catch (PatchSequenceNotFoundException ex)
				{
					Logger.Warning(ex.ToString());
					MessageBox.Show(ex.Message, "Can't find patch", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception exc)
				{
					Logger.Warning("Cannot redownload in full: {0}", exc.ToString());
					MessageBox.Show("Failed to redownload in full. (See the log for more details)");
				}
				
				IsPatching = false;
			});
		}

		private void redownloadLangBtn_Click(object sender, EventArgs e)
		{
			Task.Factory.StartNew(() =>
			{
				IsPatching = true;
				try
				{
					patcher.RedownloadLanugagePack();
				}
				catch (Exception ex)
				{
					Logger.Warning("Cannpt redownload language pack: {0}", ex.ToString());
					MessageBox.Show("Cannot redownload language pack: " + ex.Message);
				}

				IsPatching = false;
			});
		}
	}
}
