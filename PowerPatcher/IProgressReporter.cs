using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPatcher
{
	public interface IProgressReporter
	{
		string LeftText { get; }
		int ProgressBarPercent { get; }
		string RightText { get; }
	}
}
