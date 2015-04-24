namespace PowerPatcher
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.redownloadLangBtn = new System.Windows.Forms.Button();
			this.redownloadInFullBtn = new System.Windows.Forms.Button();
			this.patchToVersion = new System.Windows.Forms.NumericUpDown();
			this.patchToBtn = new System.Windows.Forms.Button();
			this.cVersionNumber = new System.Windows.Forms.NumericUpDown();
			this.writeCustomVersionBtn = new System.Windows.Forms.Button();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.localSelect = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.startGameBtn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.progressLabel = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.ppVersionLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.clientVersionLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.progressUpdater = new System.Windows.Forms.Timer(this.components);
			this.progressContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.closeTimer = new System.Windows.Forms.Timer(this.components);
			this.label9 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.patchToVersion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cVersionNumber)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(739, 426);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.webBrowser1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(731, 400);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Startup";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(3, 3);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScriptErrorsSuppressed = true;
			this.webBrowser1.Size = new System.Drawing.Size(725, 394);
			this.webBrowser1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.redownloadLangBtn);
			this.tabPage2.Controls.Add(this.redownloadInFullBtn);
			this.tabPage2.Controls.Add(this.patchToVersion);
			this.tabPage2.Controls.Add(this.patchToBtn);
			this.tabPage2.Controls.Add(this.cVersionNumber);
			this.tabPage2.Controls.Add(this.writeCustomVersionBtn);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(731, 400);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Advanced Features";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// redownloadLangBtn
			// 
			this.redownloadLangBtn.Location = new System.Drawing.Point(6, 93);
			this.redownloadLangBtn.Name = "redownloadLangBtn";
			this.redownloadLangBtn.Size = new System.Drawing.Size(249, 23);
			this.redownloadLangBtn.TabIndex = 5;
			this.redownloadLangBtn.Text = "Redownload language pack";
			this.redownloadLangBtn.UseVisualStyleBackColor = true;
			this.redownloadLangBtn.Click += new System.EventHandler(this.redownloadLangBtn_Click);
			// 
			// redownloadInFullBtn
			// 
			this.redownloadInFullBtn.Location = new System.Drawing.Point(6, 64);
			this.redownloadInFullBtn.Name = "redownloadInFullBtn";
			this.redownloadInFullBtn.Size = new System.Drawing.Size(249, 23);
			this.redownloadInFullBtn.TabIndex = 4;
			this.redownloadInFullBtn.Text = "Redownload current version in full";
			this.redownloadInFullBtn.UseVisualStyleBackColor = true;
			this.redownloadInFullBtn.Click += new System.EventHandler(this.redownloadInFullBtn_Click);
			// 
			// patchToVersion
			// 
			this.patchToVersion.Location = new System.Drawing.Point(180, 38);
			this.patchToVersion.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.patchToVersion.Name = "patchToVersion";
			this.patchToVersion.Size = new System.Drawing.Size(75, 20);
			this.patchToVersion.TabIndex = 3;
			// 
			// patchToBtn
			// 
			this.patchToBtn.Location = new System.Drawing.Point(6, 35);
			this.patchToBtn.Name = "patchToBtn";
			this.patchToBtn.Size = new System.Drawing.Size(171, 23);
			this.patchToBtn.TabIndex = 2;
			this.patchToBtn.Text = "Patch to version:";
			this.patchToBtn.UseVisualStyleBackColor = true;
			this.patchToBtn.Click += new System.EventHandler(this.patchToBtn_Click);
			// 
			// cVersionNumber
			// 
			this.cVersionNumber.Location = new System.Drawing.Point(180, 9);
			this.cVersionNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.cVersionNumber.Name = "cVersionNumber";
			this.cVersionNumber.Size = new System.Drawing.Size(75, 20);
			this.cVersionNumber.TabIndex = 1;
			// 
			// writeCustomVersionBtn
			// 
			this.writeCustomVersionBtn.Location = new System.Drawing.Point(6, 6);
			this.writeCustomVersionBtn.Name = "writeCustomVersionBtn";
			this.writeCustomVersionBtn.Size = new System.Drawing.Size(171, 23);
			this.writeCustomVersionBtn.TabIndex = 0;
			this.writeCustomVersionBtn.Text = "Write custom version number:";
			this.writeCustomVersionBtn.UseVisualStyleBackColor = true;
			this.writeCustomVersionBtn.Click += new System.EventHandler(this.writeCustonVersionBtn_Click);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox3);
			this.tabPage3.Controls.Add(this.groupBox2);
			this.tabPage3.Controls.Add(this.groupBox1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(731, 400);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Settings";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBox5);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.textBox4);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.textBox3);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.textBox2);
			this.groupBox3.Location = new System.Drawing.Point(3, 113);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(720, 178);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Client Start Options";
			// 
			// textBox4
			// 
			this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PowerPatcher.Properties.Settings.Default, "PostArgs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBox4.Location = new System.Drawing.Point(398, 56);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox4.Size = new System.Drawing.Size(316, 116);
			this.textBox4.TabIndex = 9;
			this.textBox4.Text = global::PowerPatcher.Properties.Settings.Default.PostArgs;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(450, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(264, 13);
			this.label7.TabIndex = 8;
			this.label7.Text = "Shell commands to run AFTER client.exe (one per line)";
			// 
			// textBox3
			// 
			this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PowerPatcher.Properties.Settings.Default, "PreArgs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBox3.Location = new System.Drawing.Point(6, 56);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox3.Size = new System.Drawing.Size(316, 116);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = global::PowerPatcher.Properties.Settings.Default.PreArgs;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(272, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Shell commands to run BEFORE client.exe (one per line)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(222, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(125, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Custom client arguments:";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PowerPatcher.Properties.Settings.Default, "CustomArgs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBox2.Location = new System.Drawing.Point(353, 13);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(361, 20);
			this.textBox2.TabIndex = 5;
			this.textBox2.Text = global::PowerPatcher.Properties.Settings.Default.CustomArgs;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBox6);
			this.groupBox2.Controls.Add(this.checkBox5);
			this.groupBox2.Controls.Add(this.localSelect);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Location = new System.Drawing.Point(3, 45);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(720, 68);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "General Patcher Options";
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Checked = global::PowerPatcher.Properties.Settings.Default.WarnIfNotInMabiDir;
			this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox6.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "WarnIfNotInMabiDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox6.Location = new System.Drawing.Point(5, 44);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(164, 17);
			this.checkBox6.TabIndex = 7;
			this.checkBox6.Text = "Warn if not in Mabinogi folder";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Checked = global::PowerPatcher.Properties.Settings.Default.CloseAfterStart;
			this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox5.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "CloseAfterStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox5.Location = new System.Drawing.Point(265, 19);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(142, 17);
			this.checkBox5.TabIndex = 6;
			this.checkBox5.Text = "Close after starting game";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// localSelect
			// 
			this.localSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.localSelect.FormattingEnabled = true;
			this.localSelect.Location = new System.Drawing.Point(561, 17);
			this.localSelect.Name = "localSelect";
			this.localSelect.Size = new System.Drawing.Size(151, 21);
			this.localSelect.TabIndex = 5;
			this.localSelect.SelectedIndexChanged += new System.EventHandler(this.localSelect_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(466, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Mabinogi Version:";
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Checked = global::PowerPatcher.Properties.Settings.Default.RequireAdmin;
			this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "RequireAdmin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox4.Location = new System.Drawing.Point(5, 19);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(153, 17);
			this.checkBox4.TabIndex = 1;
			this.checkBox4.Text = "Require administrator rights";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(262, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Startup page url:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PowerPatcher.Properties.Settings.Default, "StartupPage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBox1.Location = new System.Drawing.Point(353, 42);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(361, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = global::PowerPatcher.Properties.Settings.Default.StartupPage;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox3);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(720, 41);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patch Clean Up Options";
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Checked = global::PowerPatcher.Properties.Settings.Default.DeleteContent;
			this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "DeleteContent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox3.Location = new System.Drawing.Point(540, 19);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(174, 17);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "Delete temporary content folder";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Checked = global::PowerPatcher.Properties.Settings.Default.DeleteZips;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "DeleteZips", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox2.Location = new System.Drawing.Point(265, 19);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(145, 17);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Delete generated zip files";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = global::PowerPatcher.Properties.Settings.Default.DeletePartFiles;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PowerPatcher.Properties.Settings.Default, "DeletePartFiles", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBox1.Location = new System.Drawing.Point(6, 19);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(129, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Delete patch part files";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.label8);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(731, 400);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "About";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(247, 39);
			this.label8.TabIndex = 0;
			this.label8.Text = "Power Patcher Copyright © Xcelled194 9/20/2013\r\n\r\nPlease see the README for more " +
    "information";
			// 
			// startGameBtn
			// 
			this.startGameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.startGameBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startGameBtn.Location = new System.Drawing.Point(561, 0);
			this.startGameBtn.Name = "startGameBtn";
			this.startGameBtn.Size = new System.Drawing.Size(175, 88);
			this.startGameBtn.TabIndex = 1;
			this.startGameBtn.Text = "Start Game";
			this.startGameBtn.UseVisualStyleBackColor = true;
			this.startGameBtn.Click += new System.EventHandler(this.startGameBtn_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cancelBtn);
			this.panel1.Controls.Add(this.progressLabel);
			this.panel1.Controls.Add(this.progressBar1);
			this.panel1.Controls.Add(this.ppVersionLabel);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.clientVersionLabel);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.startGameBtn);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 426);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(739, 116);
			this.panel1.TabIndex = 2;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cancelBtn.Enabled = false;
			this.cancelBtn.Location = new System.Drawing.Point(332, 2);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 51);
			this.cancelBtn.TabIndex = 8;
			this.cancelBtn.Text = "Cancel Patch";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(181, 95);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(0, 13);
			this.progressLabel.TabIndex = 7;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(6, 90);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(169, 23);
			this.progressBar1.TabIndex = 6;
			// 
			// ppVersionLabel
			// 
			this.ppVersionLabel.AutoSize = true;
			this.ppVersionLabel.Location = new System.Drawing.Point(96, 20);
			this.ppVersionLabel.Name = "ppVersionLabel";
			this.ppVersionLabel.Size = new System.Drawing.Size(13, 13);
			this.ppVersionLabel.TabIndex = 5;
			this.ppVersionLabel.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Launcher Version:";
			// 
			// clientVersionLabel
			// 
			this.clientVersionLabel.AutoSize = true;
			this.clientVersionLabel.Location = new System.Drawing.Point(96, 3);
			this.clientVersionLabel.Name = "clientVersionLabel";
			this.clientVersionLabel.Size = new System.Drawing.Size(13, 13);
			this.clientVersionLabel.TabIndex = 3;
			this.clientVersionLabel.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Client Version:";
			// 
			// progressUpdater
			// 
			this.progressUpdater.Enabled = true;
			this.progressUpdater.Tick += new System.EventHandler(this.progressUpdater_Tick);
			// 
			// progressContainer
			// 
			this.progressContainer.AutoScroll = true;
			this.progressContainer.BackColor = System.Drawing.Color.White;
			this.progressContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progressContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.progressContainer.Location = new System.Drawing.Point(0, 542);
			this.progressContainer.Name = "progressContainer";
			this.progressContainer.Size = new System.Drawing.Size(739, 149);
			this.progressContainer.TabIndex = 3;
			this.progressContainer.WrapContents = false;
			this.progressContainer.SizeChanged += new System.EventHandler(this.progressContainer_SizeChanged);
			// 
			// closeTimer
			// 
			this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 13);
			this.label9.TabIndex = 10;
			this.label9.Text = "Target Name:";
			// 
			// textBox5
			// 
			this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PowerPatcher.Properties.Settings.Default, "TargetName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBox5.Location = new System.Drawing.Point(84, 13);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(122, 20);
			this.textBox5.TabIndex = 11;
			this.textBox5.Text = global::PowerPatcher.Properties.Settings.Default.TargetName;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(739, 691);
			this.Controls.Add(this.progressContainer);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Power Patcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.patchToVersion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cVersionNumber)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button startGameBtn;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label ppVersionLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label clientVersionLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer progressUpdater;
		private System.Windows.Forms.FlowLayoutPanel progressContainer;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Timer closeTimer;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox localSelect;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.Button redownloadLangBtn;
		private System.Windows.Forms.Button redownloadInFullBtn;
		private System.Windows.Forms.NumericUpDown patchToVersion;
		private System.Windows.Forms.Button patchToBtn;
		private System.Windows.Forms.NumericUpDown cVersionNumber;
		private System.Windows.Forms.Button writeCustomVersionBtn;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label9;
	}
}

