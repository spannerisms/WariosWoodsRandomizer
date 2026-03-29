namespace WoodsRandomizer;

internal partial class PalettesForm : Form {
	private BoardTheme? selectedPaletteSet = null;
	private Image? SelectedPlayerImage = null;

	private const int playerimgscale = 3;
	private const float playerdrawscale = playerimgscale;
	
	public PalettesForm() {
		InitializeComponent();

		SuspendLayout();

		PreviewRed.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewYellow.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewBlue.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewGreen.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewWhite.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewBlack.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewAzure.Image = (Bitmap) Properties.Resources.palette_preview.Clone();
		PreviewPink.Image = (Bitmap) Properties.Resources.palette_preview.Clone();

		ThemesList.SelectedValueChanged += PalettesList_SelectedValueChanged!;

		tabControl1.TabPages.Remove(PalettesTab);
		tabControl1.TabPages.Remove(ColorsTab);

		PlayerPreviewImage.Size = new(128 * playerimgscale, 48 * playerimgscale);

		ResumeLayout();
	}

	private void PalettesList_SelectedValueChanged(object sender, EventArgs e) {
		selectedPaletteSet = ThemesList.SelectedItem as BoardTheme;

		if (selectedPaletteSet is null) {
			return;
		}

		SuspendLayout();

		SetImagePalette(PreviewRed, selectedPaletteSet.RedPalette);
		SetImagePalette(PreviewYellow, selectedPaletteSet.YellowPalette);
		SetImagePalette(PreviewBlue, selectedPaletteSet.BluePalette);
		SetImagePalette(PreviewGreen, selectedPaletteSet.GreenPalette);
		SetImagePalette(PreviewWhite, selectedPaletteSet.WhitePalette);
		SetImagePalette(PreviewBlack, selectedPaletteSet.BlackPalette);
		SetImagePalette(PreviewAzure, selectedPaletteSet.AzurePalette);
		SetImagePalette(PreviewPink, selectedPaletteSet.PinkPalette);

		SetPaletteText("Red", RedPaletteName, selectedPaletteSet.RedPalette);
		SetPaletteText("Yellow", YellowPaletteName, selectedPaletteSet.YellowPalette);
		SetPaletteText("Blue", BluePaletteName, selectedPaletteSet.BluePalette);
		SetPaletteText("Green", GreenPaletteName, selectedPaletteSet.GreenPalette);
		SetPaletteText("White", WhitePaletteName, selectedPaletteSet.WhitePalette);
		SetPaletteText("Black", BlackPaletteName, selectedPaletteSet.BlackPalette);
		SetPaletteText("Azure", AzurePaletteName, selectedPaletteSet.AzurePalette);
		SetPaletteText("Pink", PinkPaletteName, selectedPaletteSet.PinkPalette);

		StatusTextbox.Text = string.Join("\r\n", ThemesHandler.PaletteStatusLog);

		ResumeLayout();

		static void SetPaletteText(string color, Label label, MonsterPalette pal) {
			if (string.IsNullOrWhiteSpace(pal.Name)) {
				label.Text = color;
				return;
			}

			label.Text = $"{color}: {pal.Name}";
		}
	}

	private static void SetImagePalette(PictureBox pic, MonsterPalette pal) {
		pal.ApplyToImage(pic.Image);
		pic.Invalidate();
	}

	private void PickRomButton_Click(object sender, EventArgs e) {
		MessageDialogs.OpenROM.InitialDirectory = Properties.Settings.Default.OutputDirectory;

		if (MessageDialogs.OpenROM.ShowDialog() is not DialogResult.OK) {
			return;
		}

		using var filestream = MessageDialogs.OpenROM.OpenFile();
		byte[] rom = filestream.GetAsArray();

		BoardTheme thisTheme = BoardTheme.FromROM(rom);

		foreach (var otherTheme in ThemesHandler.NamedThemes) {
			if (thisTheme.ThemesEqual(otherTheme)) {
				PaletteImportXMLOut.Text = $"This appears to be the named theme \"{otherTheme.Name}\".";
				return;
			}
		}

		PaletteImportXMLOut.Text = ThemesHandler.CreateXMLForTheme(thisTheme).ToString();
	}

	protected override void OnFormClosing(FormClosingEventArgs e) {
		if (e.CloseReason == CloseReason.UserClosing) {
			Hide();
			e.Cancel = true;
		}
	}

	private void PalettesForm_OnFirstShow(object sender, EventArgs e) {
		RefreshEverything();
	}

	private void RefreshEverythingButton_Click(object sender, EventArgs e) {
		RefreshEverything();
	}

	private void RefreshEverything() {
		ThemesList.BeginUpdate();
		ThemesList.DataSource = null;
		ThemesList.DataSource = ThemesHandler.NamedThemes;

		PlayerPreviewTree.BeginUpdate();
		PlayerPreviewTree.Nodes.Clear();

		SelectedPlayerImage = null;

		foreach (CharacterGraphics guy in CharacterGraphics.AllSheets) {
			var pn = new TreeNode($"{guy.Name} [{guy.Palettes.Count}]") {
				Tag = guy,
			};

			foreach (CharacterPalette cp in guy.Palettes) {
				var cn = new TreeNode(cp.Name) {
					Tag = cp,
				};

				pn.Nodes.Add(cn);
			}

			PlayerPreviewTree.Nodes.Add(pn);
		}

		PlayerPreviewTree.EndUpdate();
		ThemesList.EndUpdate();
	}

	private void PlayerPreviewImage_Paint(object sender, PaintEventArgs e) {
		using var g = e.Graphics;

		g.Clear(PlayerPreviewImage.BackColor);

		g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

		g.ScaleTransform(playerdrawscale, playerdrawscale);

		if (SelectedPlayerImage is not null) {
			g.DrawImageUnscaled(SelectedPlayerImage, 0, 0);
		}
		g.Flush();
	}

	private void PlayerPreviewTree_AfterSelect(object sender, TreeViewEventArgs e) {
		if (PlayerPreviewTree.SelectedNode is not TreeNode sel) return;
		if (sel.Tag is not CharacterPalette cp) return;

		if (sel.Parent.Tag is not CharacterGraphics cg) return;

		SelectedPlayerImage = cg.PreviewImage;

		if (SelectedPlayerImage is null) return;

		CharacterNameLabel.Text = $"{cg.Name} / {cp.Name}";

		cp.ApplyToImage(SelectedPlayerImage);
		PlayerPreviewImage.Invalidate();
	}
}
