using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPatcher
{
	public class PatchSequenceNotFoundException : Exception
	{
		public int Bottom { get; protected set; }
		public int Top { get; protected set; }

		public PatchSequenceNotFoundException(int bottom, int top)
			: base("Cannot find a way to patch from " + bottom + " to " + top + "!")
		{
			Bottom = bottom;
			Top = top;
		}
	}
}
