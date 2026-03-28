namespace WoodsRandomizer;

partial class WoodsRandoForm {
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WoodsRandoForm));
		DirectDepositBox = new CheckBox();
		SimpleRoundsBox = new CheckBox();
		StartingLivesBox = new ComboBox();
		EnduranceBox = new CheckBox();
		AllowSpud = new CheckBox();
		AllowSpook = new CheckBox();
		AllowFuzz = new CheckBox();
		AllowBeaker = new CheckBox();
		AllowSqueak = new CheckBox();
		AllowScram = new CheckBox();
		AllowDovo = new CheckBox();
		groupBox1 = new GroupBox();
		AllMonstersCheckbox = new CheckBox();
		GenerateButton = new Button();
		GenCount = new BetterUpDown();
		label1 = new Label();
		label2 = new Label();
		PlayAsBox = new ComboBox();
		SarissaTheLady = new PictureBox();
		SarissaLittleGirlBox = new CheckBox();
		BoardFillBox = new ComboBox();
		ColorFillBox = new ComboBox();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		DifficultyBox = new ComboBox();
		label6 = new Label();
		NeedsRomBox = new Panel();
		CharacterPaletteBox = new ComboBox();
		label8 = new Label();
		ThemeSelectBox = new ComboBox();
		label7 = new Label();
		RNGBox = new ComboBox();
		LogBox = new TextBox();
		OutputDirectoryButton = new TextBox();
		RomPanel = new Panel();
		OpenOutputDirectoryButton = new Button();
		ClearLogButton = new Button();
		LoggerPanel = new Panel();
		StatusTime = new ToolStripStatusLabel();
		ShowLogButton = new ToolStripStatusLabel();
		StatusBar = new StatusStrip();
		StatusInfoText = new ToolStripStatusLabel();
		TaskProgress = new ToolStripProgressBar();
		toolStrip1 = new ToolStrip();
		UploadARomButton = new ToolStripButton();
		ResetStuffButton = new ToolStripButton();
		VersionLabel = new ToolStripLabel();
		toolStripSeparator1 = new ToolStripSeparator();
		GithubButton = new ToolStripButton();
		HelpMeButton = new ToolStripButton();
		PalettesDialogButton = new ToolStripButton();
		groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize) GenCount).BeginInit();
		((System.ComponentModel.ISupportInitialize) SarissaTheLady).BeginInit();
		NeedsRomBox.SuspendLayout();
		RomPanel.SuspendLayout();
		LoggerPanel.SuspendLayout();
		StatusBar.SuspendLayout();
		toolStrip1.SuspendLayout();
		SuspendLayout();
		// 
		// DirectDepositBox
		// 
		DirectDepositBox.AutoSize = true;
		DirectDepositBox.Checked = true;
		DirectDepositBox.CheckState = CheckState.Checked;
		DirectDepositBox.Location = new Point(276, 154);
		DirectDepositBox.Name = "DirectDepositBox";
		DirectDepositBox.Size = new Size(99, 19);
		DirectDepositBox.TabIndex = 16;
		DirectDepositBox.TabStop = false;
		DirectDepositBox.Text = "Direct deposit";
		DirectDepositBox.UseVisualStyleBackColor = true;
		// 
		// SimpleRoundsBox
		// 
		SimpleRoundsBox.AutoSize = true;
		SimpleRoundsBox.Checked = true;
		SimpleRoundsBox.CheckState = CheckState.Checked;
		SimpleRoundsBox.Location = new Point(276, 129);
		SimpleRoundsBox.Name = "SimpleRoundsBox";
		SimpleRoundsBox.Size = new Size(102, 19);
		SimpleRoundsBox.TabIndex = 19;
		SimpleRoundsBox.TabStop = false;
		SimpleRoundsBox.Text = "Simple rounds";
		SimpleRoundsBox.UseVisualStyleBackColor = true;
		// 
		// StartingLivesBox
		// 
		StartingLivesBox.DropDownStyle = ComboBoxStyle.DropDownList;
		StartingLivesBox.FormattingEnabled = true;
		StartingLivesBox.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "∞" });
		StartingLivesBox.Location = new Point(462, 32);
		StartingLivesBox.Name = "StartingLivesBox";
		StartingLivesBox.Size = new Size(45, 23);
		StartingLivesBox.TabIndex = 22;
		StartingLivesBox.TabStop = false;
		// 
		// EnduranceBox
		// 
		EnduranceBox.AutoSize = true;
		EnduranceBox.Location = new Point(276, 66);
		EnduranceBox.Name = "EnduranceBox";
		EnduranceBox.Size = new Size(82, 19);
		EnduranceBox.TabIndex = 29;
		EnduranceBox.TabStop = false;
		EnduranceBox.Text = "Endurance";
		EnduranceBox.UseVisualStyleBackColor = true;
		// 
		// AllowSpud
		// 
		AllowSpud.Checked = true;
		AllowSpud.CheckState = CheckState.Checked;
		AllowSpud.Image = Properties.Resources.icon_spud;
		AllowSpud.ImageAlign = ContentAlignment.MiddleLeft;
		AllowSpud.Location = new Point(19, 34);
		AllowSpud.Name = "AllowSpud";
		AllowSpud.Size = new Size(37, 24);
		AllowSpud.TabIndex = 0;
		AllowSpud.TabStop = false;
		AllowSpud.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowSpud.UseVisualStyleBackColor = true;
		AllowSpud.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowSpook
		// 
		AllowSpook.Checked = true;
		AllowSpook.CheckState = CheckState.Checked;
		AllowSpook.Image = Properties.Resources.icon_spook;
		AllowSpook.ImageAlign = ContentAlignment.MiddleLeft;
		AllowSpook.Location = new Point(19, 160);
		AllowSpook.Name = "AllowSpook";
		AllowSpook.Size = new Size(37, 24);
		AllowSpook.TabIndex = 6;
		AllowSpook.TabStop = false;
		AllowSpook.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowSpook.UseVisualStyleBackColor = true;
		AllowSpook.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowFuzz
		// 
		AllowFuzz.Checked = true;
		AllowFuzz.CheckState = CheckState.Checked;
		AllowFuzz.Image = Properties.Resources.icon_fuzz;
		AllowFuzz.ImageAlign = ContentAlignment.MiddleLeft;
		AllowFuzz.Location = new Point(19, 55);
		AllowFuzz.Name = "AllowFuzz";
		AllowFuzz.Size = new Size(37, 24);
		AllowFuzz.TabIndex = 1;
		AllowFuzz.TabStop = false;
		AllowFuzz.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowFuzz.UseVisualStyleBackColor = true;
		AllowFuzz.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowBeaker
		// 
		AllowBeaker.Checked = true;
		AllowBeaker.CheckState = CheckState.Checked;
		AllowBeaker.Image = Properties.Resources.icon_beaker;
		AllowBeaker.ImageAlign = ContentAlignment.MiddleLeft;
		AllowBeaker.Location = new Point(19, 118);
		AllowBeaker.Name = "AllowBeaker";
		AllowBeaker.Size = new Size(37, 24);
		AllowBeaker.TabIndex = 5;
		AllowBeaker.TabStop = false;
		AllowBeaker.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowBeaker.UseVisualStyleBackColor = true;
		AllowBeaker.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowSqueak
		// 
		AllowSqueak.Checked = true;
		AllowSqueak.CheckState = CheckState.Checked;
		AllowSqueak.Image = Properties.Resources.icon_squeak;
		AllowSqueak.ImageAlign = ContentAlignment.MiddleLeft;
		AllowSqueak.Location = new Point(19, 76);
		AllowSqueak.Name = "AllowSqueak";
		AllowSqueak.Size = new Size(37, 24);
		AllowSqueak.TabIndex = 2;
		AllowSqueak.TabStop = false;
		AllowSqueak.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowSqueak.UseVisualStyleBackColor = true;
		AllowSqueak.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowScram
		// 
		AllowScram.Checked = true;
		AllowScram.CheckState = CheckState.Checked;
		AllowScram.Image = Properties.Resources.icon_scram;
		AllowScram.ImageAlign = ContentAlignment.MiddleLeft;
		AllowScram.Location = new Point(19, 97);
		AllowScram.Name = "AllowScram";
		AllowScram.Size = new Size(37, 24);
		AllowScram.TabIndex = 4;
		AllowScram.TabStop = false;
		AllowScram.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowScram.UseVisualStyleBackColor = true;
		AllowScram.CheckedChanged += AllowType_CheckedChanged;
		// 
		// AllowDovo
		// 
		AllowDovo.Checked = true;
		AllowDovo.CheckState = CheckState.Checked;
		AllowDovo.Image = Properties.Resources.icon_dovo;
		AllowDovo.ImageAlign = ContentAlignment.MiddleLeft;
		AllowDovo.Location = new Point(19, 139);
		AllowDovo.Name = "AllowDovo";
		AllowDovo.Size = new Size(37, 24);
		AllowDovo.TabIndex = 3;
		AllowDovo.TabStop = false;
		AllowDovo.TextImageRelation = TextImageRelation.ImageAboveText;
		AllowDovo.UseVisualStyleBackColor = true;
		AllowDovo.CheckedChanged += AllowType_CheckedChanged;
		// 
		// groupBox1
		// 
		groupBox1.Controls.Add(AllMonstersCheckbox);
		groupBox1.Controls.Add(AllowSpud);
		groupBox1.Controls.Add(AllowSpook);
		groupBox1.Controls.Add(AllowFuzz);
		groupBox1.Controls.Add(AllowBeaker);
		groupBox1.Controls.Add(AllowSqueak);
		groupBox1.Controls.Add(AllowScram);
		groupBox1.Controls.Add(AllowDovo);
		groupBox1.Location = new Point(3, 3);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(75, 190);
		groupBox1.TabIndex = 7;
		groupBox1.TabStop = false;
		groupBox1.Text = "Monsters";
		// 
		// AllMonstersCheckbox
		// 
		AllMonstersCheckbox.AutoSize = true;
		AllMonstersCheckbox.Checked = true;
		AllMonstersCheckbox.CheckState = CheckState.Checked;
		AllMonstersCheckbox.Location = new Point(16, 19);
		AllMonstersCheckbox.Name = "AllMonstersCheckbox";
		AllMonstersCheckbox.Size = new Size(40, 19);
		AllMonstersCheckbox.TabIndex = 7;
		AllMonstersCheckbox.TabStop = false;
		AllMonstersCheckbox.Text = "All";
		AllMonstersCheckbox.UseVisualStyleBackColor = true;
		AllMonstersCheckbox.CheckedChanged += AllMonstersCheckbox_CheckedChanged;
		// 
		// GenerateButton
		// 
		GenerateButton.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		GenerateButton.BackColor = SystemColors.ControlLight;
		GenerateButton.Cursor = Cursors.Hand;
		GenerateButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point,  0);
		GenerateButton.Location = new Point(3, 215);
		GenerateButton.Name = "GenerateButton";
		GenerateButton.Size = new Size(400, 27);
		GenerateButton.TabIndex = 8;
		GenerateButton.TabStop = false;
		GenerateButton.Text = "Generate";
		GenerateButton.UseVisualStyleBackColor = false;
		GenerateButton.Click += GenerateButton_Click;
		// 
		// GenCount
		// 
		GenCount.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
		GenCount.InterceptArrowKeys = false;
		GenCount.Location = new Point(455, 219);
		GenCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		GenCount.Name = "GenCount";
		GenCount.Size = new Size(52, 23);
		GenCount.TabIndex = 9;
		GenCount.TabStop = false;
		GenCount.TextAlign = HorizontalAlignment.Right;
		GenCount.Value = new decimal(new int[] { 15, 0, 0, 0 });
		// 
		// label1
		// 
		label1.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(409, 221);
		label1.Name = "label1";
		label1.Size = new Size(40, 15);
		label1.TabIndex = 10;
		label1.Text = "Count";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(96, 7);
		label2.Name = "label2";
		label2.Size = new Size(43, 15);
		label2.TabIndex = 11;
		label2.Text = "Play as";
		// 
		// PlayAsBox
		// 
		PlayAsBox.DropDownStyle = ComboBoxStyle.DropDownList;
		PlayAsBox.FormattingEnabled = true;
		PlayAsBox.Location = new Point(149, 4);
		PlayAsBox.Name = "PlayAsBox";
		PlayAsBox.Size = new Size(121, 23);
		PlayAsBox.TabIndex = 12;
		PlayAsBox.TabStop = false;
		PlayAsBox.SelectedIndexChanged += PlayAsBox_SelectedIndexChanged;
		// 
		// SarissaTheLady
		// 
		SarissaTheLady.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
		SarissaTheLady.BackColor = Color.Transparent;
		SarissaTheLady.Image = Properties.Resources.sarissacool;
		SarissaTheLady.Location = new Point(379, 58);
		SarissaTheLady.Margin = new Padding(0);
		SarissaTheLady.Name = "SarissaTheLady";
		SarissaTheLady.Size = new Size(133, 158);
		SarissaTheLady.SizeMode = PictureBoxSizeMode.CenterImage;
		SarissaTheLady.TabIndex = 13;
		SarissaTheLady.TabStop = false;
		SarissaTheLady.Click += SarissaClick;
		// 
		// SarissaLittleGirlBox
		// 
		SarissaLittleGirlBox.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left;
		SarissaLittleGirlBox.AutoSize = true;
		SarissaLittleGirlBox.Location = new Point(4, 195);
		SarissaLittleGirlBox.Name = "SarissaLittleGirlBox";
		SarissaLittleGirlBox.Size = new Size(172, 19);
		SarissaLittleGirlBox.TabIndex = 18;
		SarissaLittleGirlBox.TabStop = false;
		SarissaLittleGirlBox.Text = "Sarissa is just some little girl";
		SarissaLittleGirlBox.UseVisualStyleBackColor = true;
		// 
		// BoardFillBox
		// 
		BoardFillBox.DropDownStyle = ComboBoxStyle.DropDownList;
		BoardFillBox.FormattingEnabled = true;
		BoardFillBox.Location = new Point(149, 62);
		BoardFillBox.Name = "BoardFillBox";
		BoardFillBox.Size = new Size(121, 23);
		BoardFillBox.TabIndex = 20;
		BoardFillBox.TabStop = false;
		// 
		// ColorFillBox
		// 
		ColorFillBox.DropDownStyle = ComboBoxStyle.DropDownList;
		ColorFillBox.FormattingEnabled = true;
		ColorFillBox.Location = new Point(149, 91);
		ColorFillBox.Name = "ColorFillBox";
		ColorFillBox.Size = new Size(121, 23);
		ColorFillBox.TabIndex = 21;
		ColorFillBox.TabStop = false;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(428, 37);
		label3.Name = "label3";
		label3.Size = new Size(33, 15);
		label3.TabIndex = 23;
		label3.Text = "Lives";
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Location = new Point(101, 66);
		label4.Name = "label4";
		label4.Size = new Size(38, 15);
		label4.TabIndex = 24;
		label4.Text = "Board";
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Location = new Point(98, 96);
		label5.Name = "label5";
		label5.Size = new Size(41, 15);
		label5.TabIndex = 25;
		label5.Text = "Colors";
		// 
		// DifficultyBox
		// 
		DifficultyBox.DropDownStyle = ComboBoxStyle.DropDownList;
		DifficultyBox.FormattingEnabled = true;
		DifficultyBox.Location = new Point(149, 121);
		DifficultyBox.Name = "DifficultyBox";
		DifficultyBox.Size = new Size(121, 23);
		DifficultyBox.TabIndex = 26;
		DifficultyBox.TabStop = false;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Location = new Point(84, 124);
		label6.Name = "label6";
		label6.Size = new Size(55, 15);
		label6.TabIndex = 27;
		label6.Text = "Difficulty";
		// 
		// NeedsRomBox
		// 
		NeedsRomBox.Controls.Add(CharacterPaletteBox);
		NeedsRomBox.Controls.Add(SarissaTheLady);
		NeedsRomBox.Controls.Add(label8);
		NeedsRomBox.Controls.Add(ThemeSelectBox);
		NeedsRomBox.Controls.Add(label7);
		NeedsRomBox.Controls.Add(RNGBox);
		NeedsRomBox.Controls.Add(EnduranceBox);
		NeedsRomBox.Controls.Add(label6);
		NeedsRomBox.Controls.Add(DifficultyBox);
		NeedsRomBox.Controls.Add(label5);
		NeedsRomBox.Controls.Add(label4);
		NeedsRomBox.Controls.Add(label3);
		NeedsRomBox.Controls.Add(StartingLivesBox);
		NeedsRomBox.Controls.Add(ColorFillBox);
		NeedsRomBox.Controls.Add(BoardFillBox);
		NeedsRomBox.Controls.Add(SimpleRoundsBox);
		NeedsRomBox.Controls.Add(SarissaLittleGirlBox);
		NeedsRomBox.Controls.Add(DirectDepositBox);
		NeedsRomBox.Controls.Add(PlayAsBox);
		NeedsRomBox.Controls.Add(label2);
		NeedsRomBox.Controls.Add(label1);
		NeedsRomBox.Controls.Add(GenCount);
		NeedsRomBox.Controls.Add(GenerateButton);
		NeedsRomBox.Controls.Add(groupBox1);
		NeedsRomBox.Dock = DockStyle.Fill;
		NeedsRomBox.Location = new Point(0, 56);
		NeedsRomBox.Name = "NeedsRomBox";
		NeedsRomBox.Size = new Size(512, 245);
		NeedsRomBox.TabIndex = 0;
		// 
		// CharacterPaletteBox
		// 
		CharacterPaletteBox.DropDownStyle = ComboBoxStyle.DropDownList;
		CharacterPaletteBox.FormattingEnabled = true;
		CharacterPaletteBox.Location = new Point(276, 4);
		CharacterPaletteBox.Name = "CharacterPaletteBox";
		CharacterPaletteBox.Size = new Size(173, 23);
		CharacterPaletteBox.TabIndex = 34;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Location = new Point(96, 37);
		label8.Name = "label8";
		label8.Size = new Size(43, 15);
		label8.TabIndex = 33;
		label8.Text = "Theme";
		// 
		// ThemeSelectBox
		// 
		ThemeSelectBox.DropDownStyle = ComboBoxStyle.DropDownList;
		ThemeSelectBox.FormattingEnabled = true;
		ThemeSelectBox.Location = new Point(149, 33);
		ThemeSelectBox.Name = "ThemeSelectBox";
		ThemeSelectBox.Size = new Size(121, 23);
		ThemeSelectBox.TabIndex = 32;
		ThemeSelectBox.TabStop = false;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Location = new Point(108, 154);
		label7.Name = "label7";
		label7.Size = new Size(31, 15);
		label7.TabIndex = 31;
		label7.Text = "RNG";
		// 
		// RNGBox
		// 
		RNGBox.DropDownStyle = ComboBoxStyle.DropDownList;
		RNGBox.FormattingEnabled = true;
		RNGBox.Location = new Point(149, 151);
		RNGBox.Name = "RNGBox";
		RNGBox.Size = new Size(121, 23);
		RNGBox.TabIndex = 30;
		RNGBox.TabStop = false;
		// 
		// LogBox
		// 
		LogBox.Dock = DockStyle.Fill;
		LogBox.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point,  0);
		LogBox.Location = new Point(0, 0);
		LogBox.Multiline = true;
		LogBox.Name = "LogBox";
		LogBox.ReadOnly = true;
		LogBox.ScrollBars = ScrollBars.Vertical;
		LogBox.Size = new Size(82, 9);
		LogBox.TabIndex = 30;
		// 
		// OutputDirectoryButton
		// 
		OutputDirectoryButton.Anchor =  AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		OutputDirectoryButton.Cursor = Cursors.Hand;
		OutputDirectoryButton.ForeColor = SystemColors.AppWorkspace;
		OutputDirectoryButton.Location = new Point(3, 4);
		OutputDirectoryButton.MaxLength = 512;
		OutputDirectoryButton.Name = "OutputDirectoryButton";
		OutputDirectoryButton.PlaceholderText = "Click to set an output directory";
		OutputDirectoryButton.ReadOnly = true;
		OutputDirectoryButton.ShortcutsEnabled = false;
		OutputDirectoryButton.Size = new Size(483, 23);
		OutputDirectoryButton.TabIndex = 2;
		OutputDirectoryButton.TabStop = false;
		OutputDirectoryButton.MouseClick += OutputDirectoryButton_MouseClick;
		// 
		// RomPanel
		// 
		RomPanel.Controls.Add(OpenOutputDirectoryButton);
		RomPanel.Controls.Add(OutputDirectoryButton);
		RomPanel.Dock = DockStyle.Top;
		RomPanel.Location = new Point(0, 25);
		RomPanel.Name = "RomPanel";
		RomPanel.Size = new Size(512, 31);
		RomPanel.TabIndex = 4;
		// 
		// OpenOutputDirectoryButton
		// 
		OpenOutputDirectoryButton.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
		OpenOutputDirectoryButton.BackgroundImage = Properties.Resources.tool_opendir;
		OpenOutputDirectoryButton.BackgroundImageLayout = ImageLayout.Center;
		OpenOutputDirectoryButton.Cursor = Cursors.Hand;
		OpenOutputDirectoryButton.FlatAppearance.BorderSize = 0;
		OpenOutputDirectoryButton.FlatStyle = FlatStyle.Flat;
		OpenOutputDirectoryButton.Location = new Point(490, 7);
		OpenOutputDirectoryButton.MaximumSize = new Size(18, 18);
		OpenOutputDirectoryButton.MinimumSize = new Size(18, 18);
		OpenOutputDirectoryButton.Name = "OpenOutputDirectoryButton";
		OpenOutputDirectoryButton.Size = new Size(18, 18);
		OpenOutputDirectoryButton.TabIndex = 3;
		OpenOutputDirectoryButton.TabStop = false;
		OpenOutputDirectoryButton.UseVisualStyleBackColor = true;
		OpenOutputDirectoryButton.Click += OpenOutputDirectoryButton_Click;
		// 
		// ClearLogButton
		// 
		ClearLogButton.Dock = DockStyle.Bottom;
		ClearLogButton.Location = new Point(0, 9);
		ClearLogButton.Name = "ClearLogButton";
		ClearLogButton.Size = new Size(82, 23);
		ClearLogButton.TabIndex = 31;
		ClearLogButton.Text = "ClearLog";
		ClearLogButton.UseVisualStyleBackColor = true;
		ClearLogButton.Click += ClearLogButton_Click;
		// 
		// LoggerPanel
		// 
		LoggerPanel.Controls.Add(LogBox);
		LoggerPanel.Controls.Add(ClearLogButton);
		LoggerPanel.Location = new Point(511, 229);
		LoggerPanel.Name = "LoggerPanel";
		LoggerPanel.Size = new Size(82, 32);
		LoggerPanel.TabIndex = 5;
		LoggerPanel.Visible = false;
		// 
		// StatusTime
		// 
		StatusTime.ForeColor = SystemColors.ControlDark;
		StatusTime.Margin = new Padding(0, 4, 0, 2);
		StatusTime.Name = "StatusTime";
		StatusTime.Size = new Size(43, 19);
		StatusTime.Text = "--:--:--";
		// 
		// ShowLogButton
		// 
		ShowLogButton.BackColor = SystemColors.ButtonFace;
		ShowLogButton.BorderSides =  ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
		ShowLogButton.BorderStyle = Border3DStyle.Raised;
		ShowLogButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
		ShowLogButton.Font = new Font("Segoe UI", 8F);
		ShowLogButton.ForeColor = SystemColors.ControlDarkDark;
		ShowLogButton.ImageScaling = ToolStripItemImageScaling.None;
		ShowLogButton.Margin = new Padding(0, 2, 0, 1);
		ShowLogButton.Name = "ShowLogButton";
		ShowLogButton.Size = new Size(30, 22);
		ShowLogButton.Text = "Log";
		ShowLogButton.TextImageRelation = TextImageRelation.Overlay;
		ShowLogButton.Click += ShowLogButton_Click;
		// 
		// StatusBar
		// 
		StatusBar.AllowMerge = false;
		StatusBar.AutoSize = false;
		StatusBar.BackColor = SystemColors.ControlLight;
		StatusBar.Items.AddRange(new ToolStripItem[] { StatusTime, StatusInfoText, TaskProgress, ShowLogButton });
		StatusBar.Location = new Point(0, 301);
		StatusBar.Name = "StatusBar";
		StatusBar.Size = new Size(512, 25);
		StatusBar.SizingGrip = false;
		StatusBar.TabIndex = 3;
		StatusBar.Text = "---";
		// 
		// StatusInfoText
		// 
		StatusInfoText.ForeColor = SystemColors.ControlDark;
		StatusInfoText.Margin = new Padding(0, 4, 0, 2);
		StatusInfoText.Name = "StatusInfoText";
		StatusInfoText.Size = new Size(322, 19);
		StatusInfoText.Spring = true;
		StatusInfoText.Text = "Initializing...";
		StatusInfoText.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// TaskProgress
		// 
		TaskProgress.Alignment = ToolStripItemAlignment.Right;
		TaskProgress.AutoSize = false;
		TaskProgress.ForeColor = SystemColors.ControlText;
		TaskProgress.Name = "TaskProgress";
		TaskProgress.Size = new Size(100, 19);
		TaskProgress.Style = ProgressBarStyle.Continuous;
		// 
		// toolStrip1
		// 
		toolStrip1.AllowMerge = false;
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { UploadARomButton, ResetStuffButton, VersionLabel, toolStripSeparator1, GithubButton, HelpMeButton, PalettesDialogButton });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Padding = new Padding(10, 0, 1, 0);
		toolStrip1.RightToLeft = RightToLeft.No;
		toolStrip1.Size = new Size(512, 25);
		toolStrip1.TabIndex = 6;
		// 
		// UploadARomButton
		// 
		UploadARomButton.Image = Properties.Resources.tool_upload;
		UploadARomButton.ImageTransparentColor = Color.Magenta;
		UploadARomButton.Name = "UploadARomButton";
		UploadARomButton.Size = new Size(82, 22);
		UploadARomButton.Text = "Set a ROM";
		UploadARomButton.ToolTipText = "A vanilla copy of Wario's Woods (SNES) must be provided before any games may be generated.";
		UploadARomButton.Click += UploadROMButton_Click;
		// 
		// ResetStuffButton
		// 
		ResetStuffButton.Image = Properties.Resources.tool_vanilla;
		ResetStuffButton.ImageTransparentColor = Color.Magenta;
		ResetStuffButton.Name = "ResetStuffButton";
		ResetStuffButton.Size = new Size(55, 22);
		ResetStuffButton.Text = "Reset";
		ResetStuffButton.ToolTipText = "Use this to reset settings and data used by the randomizer.";
		ResetStuffButton.Click += ResetStuffButton_Click;
		// 
		// VersionLabel
		// 
		VersionLabel.Alignment = ToolStripItemAlignment.Right;
		VersionLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
		VersionLabel.Name = "VersionLabel";
		VersionLabel.Size = new Size(37, 22);
		VersionLabel.Text = "v1.0.0";
		// 
		// toolStripSeparator1
		// 
		toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.RightToLeft = RightToLeft.No;
		toolStripSeparator1.Size = new Size(6, 25);
		// 
		// GithubButton
		// 
		GithubButton.Alignment = ToolStripItemAlignment.Right;
		GithubButton.Image = Properties.Resources.tool_github;
		GithubButton.ImageTransparentColor = Color.Magenta;
		GithubButton.Name = "GithubButton";
		GithubButton.Size = new Size(63, 22);
		GithubButton.Text = "Github";
		GithubButton.ToolTipText = "Opens this project's github in your browser.";
		GithubButton.Click += GithubButton_Click;
		// 
		// HelpMeButton
		// 
		HelpMeButton.Alignment = ToolStripItemAlignment.Right;
		HelpMeButton.Image = Properties.Resources.tool_help;
		HelpMeButton.ImageTransparentColor = Color.Magenta;
		HelpMeButton.Name = "HelpMeButton";
		HelpMeButton.Size = new Size(52, 22);
		HelpMeButton.Text = "Help";
		HelpMeButton.TextAlign = ContentAlignment.MiddleRight;
		HelpMeButton.ToolTipText = "Opens the help dialog for information on settings.";
		HelpMeButton.Click += BonusWindowClick;
		// 
		// PalettesDialogButton
		// 
		PalettesDialogButton.Image = Properties.Resources.tool_palettes;
		PalettesDialogButton.ImageTransparentColor = Color.Magenta;
		PalettesDialogButton.Name = "PalettesDialogButton";
		PalettesDialogButton.Size = new Size(68, 22);
		PalettesDialogButton.Text = "Palettes";
		PalettesDialogButton.ToolTipText = "Opens the palettes dialog.";
		PalettesDialogButton.Click += BonusWindowClick;
		// 
		// WoodsRandoForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(512, 326);
		Controls.Add(LoggerPanel);
		Controls.Add(NeedsRomBox);
		Controls.Add(RomPanel);
		Controls.Add(StatusBar);
		Controls.Add(toolStrip1);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		Icon = (Icon) resources.GetObject("$this.Icon");
		MaximizeBox = false;
		MaximumSize = new Size(528, 400);
		MinimumSize = new Size(528, 306);
		Name = "WoodsRandoForm";
		SizeGripStyle = SizeGripStyle.Hide;
		Text = "Wario's Woods Randomizer";
		FormClosing += WoodsFormClosing;
		Shown += InitializeStuff;
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize) GenCount).EndInit();
		((System.ComponentModel.ISupportInitialize) SarissaTheLady).EndInit();
		NeedsRomBox.ResumeLayout(false);
		NeedsRomBox.PerformLayout();
		RomPanel.ResumeLayout(false);
		RomPanel.PerformLayout();
		LoggerPanel.ResumeLayout(false);
		LoggerPanel.PerformLayout();
		StatusBar.ResumeLayout(false);
		StatusBar.PerformLayout();
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	private GroupBox groupBox1;
	private CheckBox AllMonstersCheckbox;
	private CheckBox AllowSpud;
	private CheckBox AllowSpook;
	private CheckBox AllowFuzz;
	private CheckBox AllowBeaker;
	private CheckBox AllowSqueak;
	private CheckBox AllowScram;
	private CheckBox AllowDovo;
	private Button GenerateButton;
	private BetterUpDown GenCount;
	private Label label1;
	private Label label2;
	private ComboBox PlayAsBox;
	private PictureBox SarissaTheLady;
	private CheckBox DirectDepositBox;
	private CheckBox SarissaLittleGirlBox;
	private CheckBox SimpleRoundsBox;
	private ComboBox BoardFillBox;
	private ComboBox ColorFillBox;
	private ComboBox StartingLivesBox;
	private Label label3;
	private Label label4;
	private Label label5;
	private ComboBox DifficultyBox;
	private Label label6;
	private CheckBox EnduranceBox;
	private Panel NeedsRomBox;
	private TextBox OutputDirectoryButton;
	private Panel RomPanel;
	private TextBox LogBox;
	private Button ClearLogButton;
	private Panel LoggerPanel;
	private ToolStripStatusLabel StatusTime;
	private ToolStripStatusLabel ShowLogButton;
	private StatusStrip StatusBar;
	private ToolStripStatusLabel StatusInfoText;
	private Label label7;
	private ComboBox RNGBox;
	private ToolStrip toolStrip1;
	private ToolStripButton UploadARomButton;
	private ToolStripButton HelpMeButton;
	private ToolStripButton GithubButton;
	private ToolStripButton ResetStuffButton;
	private Button OpenOutputDirectoryButton;
	private ToolStripLabel VersionLabel;
	private Label label8;
	private ComboBox ThemeSelectBox;
	private ToolStripButton PalettesDialogButton;
	private ToolStripSeparator toolStripSeparator1;
	private ComboBox CharacterPaletteBox;
	private ToolStripProgressBar TaskProgress;
}
