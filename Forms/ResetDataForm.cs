namespace WoodsRandomizer;

internal partial class ResetDataForm : Form {

	private ResetSettingsFlag _result = ResetSettingsFlag.None;
	public ResetSettingsFlag Result => _result;

	private readonly List<ResetOptionCheckBox> boxes = [];

	public ResetDataForm() {
		InitializeComponent();

		SuspendLayout();

		AddBox(ResetSettingsFlag.CopyVanillaROM, true, "Create a copy of my saved vanilla ROM in the output directory.");
		AddBox(ResetSettingsFlag.ReloadVanillaPlayerGraphics, true, "Reload vanilla sprite graphics from.");
		AddBox(ResetSettingsFlag.RecompressBaseSprites, false, "Recompress the base sprite sheets.");
		AddBox(ResetSettingsFlag.RecreatePreviewImages, false, "Recreate character preview images");

		ResumeLayout();

		void AddBox(ResetSettingsFlag flag, bool requiresVanilla, string text) {
			ResetOptionCheckBox add = new(text, flag) {
				RequiresVanilla = requiresVanilla,
				TabStop = false,
				Margin = new Padding(3, 1, 3, 1),
				AutoSize = true,
				UseVisualStyleBackColor = true,
			};

			add.CheckedChanged += ResetSettingCheckChanged;

			CheckBoxFlowPanel.Controls.Add(add);
			boxes.Add(add);
		}


	}

	private bool internalchange = false;

	private void ResetSettingCheckChanged(object? sender, EventArgs e) {
		if (internalchange) return;

		if (sender is not ResetOptionCheckBox cb) return;

		if (cb.Checked) {
			_result |= cb.Flag;
		} else {
			_result &= ~cb.Flag;
		}
	}

	public DialogResult ShowDialog(bool haveVanilla) {
		internalchange = true;
		
		foreach (ResetOptionCheckBox cb in boxes) {
			cb.Enabled = (!cb.RequiresVanilla) | haveVanilla;
			cb.Checked = false;
		}

		_result = ResetSettingsFlag.None;

		internalchange = false;
		Refresh();
		return ShowDialog();
	}

	protected override void OnFormClosing(FormClosingEventArgs e) {
		if (e.CloseReason == CloseReason.UserClosing) {
			Hide();
			e.Cancel = true;
		}

	}

	private class ResetOptionCheckBox : CheckBox {
		public ResetSettingsFlag Flag { get; }
		public bool RequiresVanilla { get; init; } = false;

		public ResetOptionCheckBox(string text, ResetSettingsFlag flag) {
			Text = text;
			Flag = flag;
		}
	}
}
