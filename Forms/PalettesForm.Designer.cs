namespace WoodsRandomizer;

partial class PalettesForm {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			PreviewRed.Image.Dispose();
			PreviewYellow.Image.Dispose();
			PreviewBlue.Image.Dispose();
			PreviewGreen.Image.Dispose();
			PreviewWhite.Image.Dispose();
			PreviewBlack.Image.Dispose();
			PreviewAzure.Image.Dispose();
			PreviewPink.Image.Dispose();
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
		ThemesList = new ListBox();
		PreviewRed = new PictureBox();
		PreviewYellow = new PictureBox();
		PreviewBlue = new PictureBox();
		PreviewGreen = new PictureBox();
		PreviewWhite = new PictureBox();
		PreviewBlack = new PictureBox();
		PreviewAzure = new PictureBox();
		PreviewPink = new PictureBox();
		RedPaletteName = new Label();
		YellowPaletteName = new Label();
		GreenPaletteName = new Label();
		BluePaletteName = new Label();
		PinkPaletteName = new Label();
		AzurePaletteName = new Label();
		BlackPaletteName = new Label();
		WhitePaletteName = new Label();
		tabControl1 = new TabControl();
		ThemesTab = new TabPage();
		PlayerTabPage = new TabPage();
		PlayerPreviewTree = new TreeView();
		PlayerPreviewImage = new Panel();
		CharacterNameLabel = new Label();
		PalettesTab = new TabPage();
		ColorsTab = new TabPage();
		ImportTab = new TabPage();
		PickRomButton = new Button();
		PaletteImportXMLOut = new TextBox();
		label1 = new Label();
		StatusTab = new TabPage();
		StatusTextbox = new TextBox();
		panel1 = new Panel();
		RefreshEverythingButton = new Button();
		((System.ComponentModel.ISupportInitialize) PreviewRed).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewYellow).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewBlue).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewGreen).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewWhite).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewBlack).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewAzure).BeginInit();
		((System.ComponentModel.ISupportInitialize) PreviewPink).BeginInit();
		tabControl1.SuspendLayout();
		ThemesTab.SuspendLayout();
		PlayerTabPage.SuspendLayout();
		ImportTab.SuspendLayout();
		StatusTab.SuspendLayout();
		panel1.SuspendLayout();
		SuspendLayout();
		// 
		// ThemesList
		// 
		ThemesList.Dock = DockStyle.Left;
		ThemesList.FormattingEnabled = true;
		ThemesList.ItemHeight = 15;
		ThemesList.Location = new Point(3, 3);
		ThemesList.Name = "ThemesList";
		ThemesList.Size = new Size(179, 525);
		ThemesList.TabIndex = 0;
		// 
		// PreviewRed
		// 
		PreviewRed.InitialImage = null;
		PreviewRed.Location = new Point(188, 21);
		PreviewRed.Name = "PreviewRed";
		PreviewRed.Size = new Size(226, 109);
		PreviewRed.TabIndex = 1;
		PreviewRed.TabStop = false;
		// 
		// PreviewYellow
		// 
		PreviewYellow.InitialImage = null;
		PreviewYellow.Location = new Point(420, 21);
		PreviewYellow.Name = "PreviewYellow";
		PreviewYellow.Size = new Size(226, 109);
		PreviewYellow.TabIndex = 3;
		PreviewYellow.TabStop = false;
		// 
		// PreviewBlue
		// 
		PreviewBlue.InitialImage = null;
		PreviewBlue.Location = new Point(420, 151);
		PreviewBlue.Name = "PreviewBlue";
		PreviewBlue.Size = new Size(226, 109);
		PreviewBlue.TabIndex = 5;
		PreviewBlue.TabStop = false;
		// 
		// PreviewGreen
		// 
		PreviewGreen.InitialImage = null;
		PreviewGreen.Location = new Point(188, 151);
		PreviewGreen.Name = "PreviewGreen";
		PreviewGreen.Size = new Size(226, 109);
		PreviewGreen.TabIndex = 4;
		PreviewGreen.TabStop = false;
		// 
		// PreviewWhite
		// 
		PreviewWhite.InitialImage = null;
		PreviewWhite.Location = new Point(420, 412);
		PreviewWhite.Name = "PreviewWhite";
		PreviewWhite.Size = new Size(226, 109);
		PreviewWhite.TabIndex = 9;
		PreviewWhite.TabStop = false;
		// 
		// PreviewBlack
		// 
		PreviewBlack.InitialImage = null;
		PreviewBlack.Location = new Point(188, 412);
		PreviewBlack.Name = "PreviewBlack";
		PreviewBlack.Size = new Size(226, 109);
		PreviewBlack.TabIndex = 8;
		PreviewBlack.TabStop = false;
		// 
		// PreviewAzure
		// 
		PreviewAzure.InitialImage = null;
		PreviewAzure.Location = new Point(420, 282);
		PreviewAzure.Name = "PreviewAzure";
		PreviewAzure.Size = new Size(226, 109);
		PreviewAzure.TabIndex = 7;
		PreviewAzure.TabStop = false;
		// 
		// PreviewPink
		// 
		PreviewPink.InitialImage = null;
		PreviewPink.Location = new Point(188, 282);
		PreviewPink.Name = "PreviewPink";
		PreviewPink.Size = new Size(226, 109);
		PreviewPink.TabIndex = 6;
		PreviewPink.TabStop = false;
		// 
		// RedPaletteName
		// 
		RedPaletteName.AutoSize = true;
		RedPaletteName.Location = new Point(188, 3);
		RedPaletteName.Name = "RedPaletteName";
		RedPaletteName.Size = new Size(27, 15);
		RedPaletteName.TabIndex = 10;
		RedPaletteName.Text = "Red";
		// 
		// YellowPaletteName
		// 
		YellowPaletteName.AutoSize = true;
		YellowPaletteName.Location = new Point(420, 3);
		YellowPaletteName.Name = "YellowPaletteName";
		YellowPaletteName.Size = new Size(41, 15);
		YellowPaletteName.TabIndex = 11;
		YellowPaletteName.Text = "Yellow";
		// 
		// GreenPaletteName
		// 
		GreenPaletteName.AutoSize = true;
		GreenPaletteName.Location = new Point(188, 133);
		GreenPaletteName.Name = "GreenPaletteName";
		GreenPaletteName.Size = new Size(38, 15);
		GreenPaletteName.TabIndex = 12;
		GreenPaletteName.Text = "Green";
		// 
		// BluePaletteName
		// 
		BluePaletteName.AutoSize = true;
		BluePaletteName.Location = new Point(420, 133);
		BluePaletteName.Name = "BluePaletteName";
		BluePaletteName.Size = new Size(30, 15);
		BluePaletteName.TabIndex = 13;
		BluePaletteName.Text = "Blue";
		// 
		// PinkPaletteName
		// 
		PinkPaletteName.AutoSize = true;
		PinkPaletteName.Location = new Point(188, 263);
		PinkPaletteName.Name = "PinkPaletteName";
		PinkPaletteName.Size = new Size(30, 15);
		PinkPaletteName.TabIndex = 14;
		PinkPaletteName.Text = "Pink";
		// 
		// AzurePaletteName
		// 
		AzurePaletteName.AutoSize = true;
		AzurePaletteName.Location = new Point(420, 263);
		AzurePaletteName.Name = "AzurePaletteName";
		AzurePaletteName.Size = new Size(37, 15);
		AzurePaletteName.TabIndex = 15;
		AzurePaletteName.Text = "Azure";
		// 
		// BlackPaletteName
		// 
		BlackPaletteName.AutoSize = true;
		BlackPaletteName.Location = new Point(188, 394);
		BlackPaletteName.Name = "BlackPaletteName";
		BlackPaletteName.Size = new Size(35, 15);
		BlackPaletteName.TabIndex = 16;
		BlackPaletteName.Text = "Black";
		// 
		// WhitePaletteName
		// 
		WhitePaletteName.AutoSize = true;
		WhitePaletteName.Location = new Point(420, 394);
		WhitePaletteName.Name = "WhitePaletteName";
		WhitePaletteName.Size = new Size(38, 15);
		WhitePaletteName.TabIndex = 17;
		WhitePaletteName.Text = "White";
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(ThemesTab);
		tabControl1.Controls.Add(PlayerTabPage);
		tabControl1.Controls.Add(PalettesTab);
		tabControl1.Controls.Add(ColorsTab);
		tabControl1.Controls.Add(ImportTab);
		tabControl1.Controls.Add(StatusTab);
		tabControl1.Dock = DockStyle.Fill;
		tabControl1.Location = new Point(0, 0);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(678, 559);
		tabControl1.TabIndex = 18;
		// 
		// ThemesTab
		// 
		ThemesTab.Controls.Add(RedPaletteName);
		ThemesTab.Controls.Add(ThemesList);
		ThemesTab.Controls.Add(WhitePaletteName);
		ThemesTab.Controls.Add(PreviewRed);
		ThemesTab.Controls.Add(BlackPaletteName);
		ThemesTab.Controls.Add(PreviewYellow);
		ThemesTab.Controls.Add(AzurePaletteName);
		ThemesTab.Controls.Add(PreviewGreen);
		ThemesTab.Controls.Add(PinkPaletteName);
		ThemesTab.Controls.Add(PreviewBlue);
		ThemesTab.Controls.Add(BluePaletteName);
		ThemesTab.Controls.Add(PreviewPink);
		ThemesTab.Controls.Add(GreenPaletteName);
		ThemesTab.Controls.Add(PreviewAzure);
		ThemesTab.Controls.Add(YellowPaletteName);
		ThemesTab.Controls.Add(PreviewBlack);
		ThemesTab.Controls.Add(PreviewWhite);
		ThemesTab.Location = new Point(4, 24);
		ThemesTab.Name = "ThemesTab";
		ThemesTab.Padding = new Padding(3);
		ThemesTab.Size = new Size(670, 531);
		ThemesTab.TabIndex = 0;
		ThemesTab.Text = "Themes";
		ThemesTab.UseVisualStyleBackColor = true;
		// 
		// PlayerTabPage
		// 
		PlayerTabPage.Controls.Add(PlayerPreviewTree);
		PlayerTabPage.Controls.Add(PlayerPreviewImage);
		PlayerTabPage.Controls.Add(CharacterNameLabel);
		PlayerTabPage.Location = new Point(4, 24);
		PlayerTabPage.Name = "PlayerTabPage";
		PlayerTabPage.Size = new Size(670, 531);
		PlayerTabPage.TabIndex = 5;
		PlayerTabPage.Text = "Player";
		PlayerTabPage.UseVisualStyleBackColor = true;
		// 
		// PlayerPreviewTree
		// 
		PlayerPreviewTree.Dock = DockStyle.Left;
		PlayerPreviewTree.Location = new Point(0, 0);
		PlayerPreviewTree.Name = "PlayerPreviewTree";
		PlayerPreviewTree.Size = new Size(219, 531);
		PlayerPreviewTree.TabIndex = 5;
		PlayerPreviewTree.AfterSelect += PlayerPreviewTree_AfterSelect;
		// 
		// PlayerPreviewImage
		// 
		PlayerPreviewImage.Location = new Point(225, 49);
		PlayerPreviewImage.MaximumSize = new Size(384, 144);
		PlayerPreviewImage.MinimumSize = new Size(384, 144);
		PlayerPreviewImage.Name = "PlayerPreviewImage";
		PlayerPreviewImage.Size = new Size(384, 144);
		PlayerPreviewImage.TabIndex = 4;
		PlayerPreviewImage.Paint += PlayerPreviewImage_Paint;
		// 
		// CharacterNameLabel
		// 
		CharacterNameLabel.Location = new Point(225, 19);
		CharacterNameLabel.Name = "CharacterNameLabel";
		CharacterNameLabel.Size = new Size(284, 27);
		CharacterNameLabel.TabIndex = 1;
		CharacterNameLabel.Text = "--";
		// 
		// PalettesTab
		// 
		PalettesTab.Location = new Point(4, 24);
		PalettesTab.Name = "PalettesTab";
		PalettesTab.Padding = new Padding(3);
		PalettesTab.Size = new Size(670, 531);
		PalettesTab.TabIndex = 1;
		PalettesTab.Text = "Palettes";
		PalettesTab.UseVisualStyleBackColor = true;
		// 
		// ColorsTab
		// 
		ColorsTab.Location = new Point(4, 24);
		ColorsTab.Name = "ColorsTab";
		ColorsTab.Size = new Size(670, 531);
		ColorsTab.TabIndex = 2;
		ColorsTab.Text = "Colors";
		ColorsTab.UseVisualStyleBackColor = true;
		// 
		// ImportTab
		// 
		ImportTab.Controls.Add(PickRomButton);
		ImportTab.Controls.Add(PaletteImportXMLOut);
		ImportTab.Controls.Add(label1);
		ImportTab.Location = new Point(4, 24);
		ImportTab.Name = "ImportTab";
		ImportTab.Size = new Size(670, 531);
		ImportTab.TabIndex = 4;
		ImportTab.Text = "Import";
		ImportTab.UseVisualStyleBackColor = true;
		// 
		// PickRomButton
		// 
		PickRomButton.Location = new Point(539, 11);
		PickRomButton.Name = "PickRomButton";
		PickRomButton.Size = new Size(75, 23);
		PickRomButton.TabIndex = 2;
		PickRomButton.Text = "Pick ROM";
		PickRomButton.UseVisualStyleBackColor = true;
		PickRomButton.Click += PickRomButton_Click;
		// 
		// PaletteImportXMLOut
		// 
		PaletteImportXMLOut.Dock = DockStyle.Bottom;
		PaletteImportXMLOut.Location = new Point(0, 52);
		PaletteImportXMLOut.Multiline = true;
		PaletteImportXMLOut.Name = "PaletteImportXMLOut";
		PaletteImportXMLOut.ReadOnly = true;
		PaletteImportXMLOut.ScrollBars = ScrollBars.Vertical;
		PaletteImportXMLOut.Size = new Size(670, 479);
		PaletteImportXMLOut.TabIndex = 1;
		// 
		// label1
		// 
		label1.Location = new Point(8, 11);
		label1.Name = "label1";
		label1.Size = new Size(498, 38);
		label1.TabIndex = 0;
		label1.Text = "If you ended up with a random theme and really liked it, you can select that ROM from here to get a copy of the colors.";
		// 
		// StatusTab
		// 
		StatusTab.Controls.Add(StatusTextbox);
		StatusTab.Controls.Add(panel1);
		StatusTab.Location = new Point(4, 24);
		StatusTab.Name = "StatusTab";
		StatusTab.Size = new Size(670, 531);
		StatusTab.TabIndex = 3;
		StatusTab.Text = "Status";
		StatusTab.UseVisualStyleBackColor = true;
		// 
		// StatusTextbox
		// 
		StatusTextbox.BackColor = SystemColors.ButtonHighlight;
		StatusTextbox.Dock = DockStyle.Fill;
		StatusTextbox.Location = new Point(0, 30);
		StatusTextbox.Multiline = true;
		StatusTextbox.Name = "StatusTextbox";
		StatusTextbox.ReadOnly = true;
		StatusTextbox.ScrollBars = ScrollBars.Vertical;
		StatusTextbox.Size = new Size(670, 501);
		StatusTextbox.TabIndex = 0;
		// 
		// panel1
		// 
		panel1.Controls.Add(RefreshEverythingButton);
		panel1.Dock = DockStyle.Top;
		panel1.Location = new Point(0, 0);
		panel1.Name = "panel1";
		panel1.Size = new Size(670, 30);
		panel1.TabIndex = 19;
		// 
		// RefreshEverythingButton
		// 
		RefreshEverythingButton.Location = new Point(3, 3);
		RefreshEverythingButton.Name = "RefreshEverythingButton";
		RefreshEverythingButton.Size = new Size(112, 23);
		RefreshEverythingButton.TabIndex = 0;
		RefreshEverythingButton.Text = "Refresh data";
		RefreshEverythingButton.UseVisualStyleBackColor = true;
		RefreshEverythingButton.Click += RefreshEverythingButton_Click;
		// 
		// PalettesForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(678, 559);
		Controls.Add(tabControl1);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		Name = "PalettesForm";
		Text = "Palettes Preview";
		Shown += PalettesForm_OnFirstShow;
		((System.ComponentModel.ISupportInitialize) PreviewRed).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewYellow).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewBlue).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewGreen).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewWhite).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewBlack).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewAzure).EndInit();
		((System.ComponentModel.ISupportInitialize) PreviewPink).EndInit();
		tabControl1.ResumeLayout(false);
		ThemesTab.ResumeLayout(false);
		ThemesTab.PerformLayout();
		PlayerTabPage.ResumeLayout(false);
		ImportTab.ResumeLayout(false);
		ImportTab.PerformLayout();
		StatusTab.ResumeLayout(false);
		StatusTab.PerformLayout();
		panel1.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private ListBox ThemesList;
	private PictureBox PreviewRed;
	private PictureBox PreviewYellow;
	private PictureBox PreviewBlue;
	private PictureBox PreviewGreen;
	private PictureBox PreviewWhite;
	private PictureBox PreviewBlack;
	private PictureBox PreviewAzure;
	private PictureBox PreviewPink;
	private Label RedPaletteName;
	private Label YellowPaletteName;
	private Label GreenPaletteName;
	private Label BluePaletteName;
	private Label PinkPaletteName;
	private Label AzurePaletteName;
	private Label BlackPaletteName;
	private Label WhitePaletteName;
	private TabControl tabControl1;
	private TabPage ThemesTab;
	private TabPage PalettesTab;
	private TabPage ColorsTab;
	private TabPage StatusTab;
	private TextBox StatusTextbox;
	private TabPage ImportTab;
	private Label label1;
	private TextBox PaletteImportXMLOut;
	private Button PickRomButton;
	private TabPage PlayerTabPage;
	private Label CharacterNameLabel;
	private Panel PlayerPreviewImage;
	private TreeView PlayerPreviewTree;
	private Panel panel1;
	private Button RefreshEverythingButton;
}