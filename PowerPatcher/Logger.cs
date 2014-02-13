using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace PowerPatcher
{
	internal static class Logger
	{
		internal enum LogLevel
		{
			None,
			Info,
			Warning,
			Debug,
			Error
		}

		static string logPath = "powerpatcher.log";

		static Logger()
		{
			try
			{
				var dirs = Path.GetDirectoryName(logPath);
				if (!string.IsNullOrWhiteSpace(dirs))
					Directory.CreateDirectory(dirs);

				if (File.Exists(logPath))
					File.Delete(logPath);

				Logger.Debug("Logging initialized successfully");
			}
			catch (Exception ex)
			{
				throw new Exception("There has been a problem while initializing the logger", ex);
			}
		}

		public static void Error(string format, params object[] args)
		{
			Write(LogLevel.Error, format, args);
		}

		[Conditional("DEBUG")]
		public static void Debug(string format, params object[] args)
		{
			Write(LogLevel.Debug, format, args);
		}

		public static void Warning(string format, params object[] args)
		{
			Write(LogLevel.Warning, format, args);
		}

		public static void Info(string format, params object[] args)
		{
			Write(LogLevel.Info, format, args);
		}

		private static readonly object locker = new object();
		public static void Write(LogLevel level, string format, params object[] args)
		{
			lock (locker)
				File.AppendAllText(logPath, DateTime.Now + "  [" + level.ToString() + "] - " + string.Format(format, args) + Environment.NewLine);
		}
	}
}
