using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace PowerPatcher
{
	static class Program
	{
		public static string[] CmdArgs;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			CmdArgs = args;

			if (args.Contains("/?") || args.Contains("-?"))
			{
				Console.WriteLine("Usage: PowerPatcher.exe [/?] [/noadmin]");
				Console.WriteLine("\tnoadmin - Disables checking to see if current user is an administrator");
				Environment.Exit(0);
			}

			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

			Logger.Info("Application started. Command line args: {0}{1}", Environment.NewLine, string.Join(Environment.NewLine, Environment.GetCommandLineArgs()));

			if (args.Contains("/noadmin"))
				Properties.Settings.Default.RequireAdmin = false;

			SetCWD();
			CheckNexonDir();
#if !DEBUG
			CheckAdmin();
#endif
			System.Net.ServicePointManager.DefaultConnectionLimit = 100;

			Logger.Info("Application initialized. Loading main form");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		static void Application_ApplicationExit(object sender, EventArgs e)
		{
			Logger.Info("Power Patcher is shutting down");
			Properties.Settings.Default.Save();
		}

		private static void CheckAdmin()
		{
			var isAdmin = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
			Logger.Info("Admin required: {0}\tCurrently admin: {1}", Properties.Settings.Default.RequireAdmin, isAdmin);
			if (Properties.Settings.Default.RequireAdmin && !isAdmin)
			{
				System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
				proc.UseShellExecute = true;
				proc.WorkingDirectory = Environment.CurrentDirectory;
				proc.FileName = Application.ExecutablePath;
				proc.Arguments = string.Join(" ", Environment.GetCommandLineArgs());
				proc.Verb = "runas";
				try
				{
					System.Diagnostics.Process.Start(proc);
				}
				catch (Exception ex)
				{
					// The user refused the elevation.
					Logger.Error("Cannot start elevated instance:\r\n{0}", ex);
				}
				Environment.Exit(0);
			}
		}

		private static void CheckNexonDir()
		{
			if (Properties.Settings.Default.WarnIfNotInMabiDir)
			{
				var nxdir = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Nexon\Mabinogi", null, "");
				Logger.Info("Mabinogi directory: {0}", nxdir);

				if (string.IsNullOrWhiteSpace(nxdir))
				{
					MessageBox.Show("Oh dear. It appears bandits have made off with your Mabinogi, or Mabinogi isn't installed on this computer.\r\n\r\n"
					+ " Power Patcher is NOT an installer... ;) If you're trying to install Mabinogi, you should exit and use an official installer.\r\n\r\n"
					+ "It is also possible that Mabinogi is installed but in an unusual place. If this is the case, feel free to ignore this message.", "Dem bandits!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Logger.Error("Nexon registry key not found.");
					return;
				}

#if !DEBUG
				if (!string.Equals(Path.GetFullPath(nxdir), Path.GetFullPath(Environment.CurrentDirectory), StringComparison.OrdinalIgnoreCase))
				{
					Logger.Warning("Power Patcher is not located in the Mabinogi folder!");
					if (MessageBox.Show("Power Patcher is not located in the Mabinogi folder (" + nxdir + ")\r\n\r\nThis will most likely result in improper operation. Please restore Power Patcher to the Mabinogi directory as soon as possible. " +
						"\r\n\r\nPress \"Yes\" to continue (not reccommended!) or \"No\" to exit.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					{
						Application.Exit();
						Environment.Exit(0);
					}
				}
#endif
			}
		}

		private static void SetCWD()
		{
			var appDir = Path.GetDirectoryName(Application.ExecutablePath);

			Logger.Info("Current directory: {0}", Environment.CurrentDirectory);
			if (Environment.CurrentDirectory != appDir)
			{
				Logger.Info("Setting CWD to {0}", appDir);
				Environment.CurrentDirectory = appDir;
			}
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			new UnhandledExceptionReporter((Exception)e.ExceptionObject).ShowDialog();
			Application.Exit();
		}
	}
}
