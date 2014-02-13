using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerPatcher
{
	public class ProgressReporter : FlowLayoutPanel
	{
		public IProgressReporter Reporter { get; set; }

		private Label leftLabel, rightLabel;
		private ProgressBar bar;

		public ProgressReporter()
		{
			FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

			leftLabel = new Label() { Width = 220 };
			rightLabel = new Label() { Width = 220 };
			bar = new ProgressBar() { Width = 220 };

			Height = bar.Height + 4;

			BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

			Controls.Add(leftLabel);
			Controls.Add(bar);
			Controls.Add(rightLabel);
		}

		protected override void OnResize(EventArgs eventargs)
		{
			leftLabel.Width = Width / 3 - 6;
			bar.Width = Width / 3 - 6;
			rightLabel.Width = Width / 3 - 6;

			base.OnResize(eventargs);
		}

		public void UpdateDisplay()
		{
			if (Reporter == null)
			{
				leftLabel.Text = "";
				rightLabel.Text = "";
				bar.Value = 0;
				bar.Style = ProgressBarStyle.Continuous;
			}
			else
			{
				leftLabel.Text = Reporter.LeftText;
				rightLabel.Text = Reporter.RightText;
				int barPercent = Reporter.ProgressBarPercent;

				if (barPercent != -1)
				{
					bar.Style = ProgressBarStyle.Continuous;
					if (barPercent >= 0 && barPercent <= 100)
						bar.Value = barPercent;
				}
				else
				{
					bar.Style = ProgressBarStyle.Marquee;
				}
			}

			Update();
		}
	}
}
