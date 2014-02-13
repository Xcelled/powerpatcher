using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerPatcher
{
	public partial class UnhandledExceptionReporter : Form
	{
		static readonly string[] phrases = new string[]
		{
			"\"While copying download files, a problem occurs.\"",
			"We made a Nexon...",
			"\"The performance was a total mess... \"",
			"\"I'm sorry. My hand must've slipped...\"",
			"\"Enchant was a huge failure!\""
		};

		public UnhandledExceptionReporter(Exception ex)
		{
			InitializeComponent();
			label2.Text = phrases[new Random().Next(phrases.Length)];
			try
			{
				Logger.Error("{0}{1}{2}", ex.Message, Environment.NewLine, ex);
			}
			catch { }

			textBox1.Text = ex.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
