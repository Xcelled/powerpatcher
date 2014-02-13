using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPatcher
{
	public static class ByteSizeHelper
	{
		static string[] prefixes = new string[] { "b", "kb", "mb", "gb", "tb" };

		public static string ToString(double bytes)
		{
			int pf = 0;
			while (bytes >= 1024)
			{
				bytes /= 1024;
				pf++;
			}

			return string.Format("{0:N} {1}", bytes, prefixes[pf]);
		}
	}
}
