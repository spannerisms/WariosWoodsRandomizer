namespace WoodsRandomizer;

internal static class MessageDialogs {

	internal static readonly OpenFileDialog OpenROM = new() {
		Filter = "SNES ROM file|*.sfc; *.smc",
		Title = "Select a ROM file",
	};


	public static readonly TaskDialogPage SarissaWarning;
	public static readonly TaskDialogPage GeneralWarning;
	public static readonly TaskDialogExpander GeneralWarningExpander;
	public static readonly TaskDialogPage TryAgainPage;

	static MessageDialogs() {
		var sadlady = new TaskDialogIcon(Properties.Resources.sarissabroke);

		// create common dialogs
		SarissaWarning = new() {
			Caption = "I like players that respect women.",
			Heading = "Sarissa is not just some little girl.",
			Text = "She is a lady, and \"Ladies first\" isn't just a polite remark.",
			Footnote = new("Play me again at half speed out of respect!"),
			Icon = TaskDialogIcon.Warning,
			SizeToContent = false,
			Buttons = [
				new TaskDialogButton("Whoa!") {
					Tag = DialogResult.Yes,
				},
			],
			AllowMinimize = false,
		};
		SarissaWarning.Buttons.Clear();

		GeneralWarning = new() {
			Caption = "Error",
			Heading = "Some error",
			Text = "With information",
			Icon = sadlady,
			SizeToContent = false,
			Buttons = [
				new TaskDialogButton("Oh no.") {
					Tag = DialogResult.OK
				},
			],
			Expander = null,
			AllowCancel = true,
		};

		GeneralWarningExpander = new TaskDialogExpander() {
			CollapsedButtonText = "Show error details",
			ExpandedButtonText = "Hide error details",
			Text = "",
		};

		TryAgainPage = new() {
			Caption = "Something went wrong",
			Heading = "Failed to save",
			Text = "Your vanilla ROM failed to save to user storage.\r\nIf you do not try again, you will need to provide a file again later.",
			Icon = sadlady,
			SizeToContent = false,
			Buttons = [
				new TaskDialogButton("Try again") {
					Tag = DialogResult.TryAgain
				},
				new TaskDialogButton("Cancel") {
					Tag = DialogResult.Cancel
				},
			],
			AllowCancel = true,
		};
	}


	public static DialogResult ShowException(IWin32Window owner, string caption, string header, Exception exception) {
		return ShowWarning(owner, caption, header, exception.Message, exception.StackTrace);
	}


	public static DialogResult ShowWarning(IWin32Window owner, string caption, string header, string details, string? stack = null) {
		GeneralWarning.Caption = caption;
		GeneralWarning.Heading = header;
		GeneralWarning.Text = details;

		if (stack is null) {
			GeneralWarning.Expander = null;
		} else {
			GeneralWarning.Expander = GeneralWarningExpander;
			GeneralWarningExpander.Text = stack;
		}

		return ShowTaskDialog(owner, GeneralWarning);
	}

	public static DialogResult ShowTaskDialog(IWin32Window owner, TaskDialogPage page) {
		var sel = TaskDialog.ShowDialog(owner, page, TaskDialogStartupLocation.CenterOwner);
		return (sel.Tag as DialogResult?) ?? DialogResult.None;
	}
}
