using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Xml;

namespace WoodsRandomizer;

public partial class WoodsRandoForm : Form {
	private const string romname = "VANILLAWOODS";

	private const string RandomChoice = $"{SPLAT}Random";
	private const string DefaultChoice = $"{SPLAT}Default";

	private static readonly string[] RandomCharPalettes = [DefaultChoice, RandomChoice];

	private static readonly Properties.Settings Settings = Properties.Settings.Default;

	private bool internalMonsterChange = false;

	private readonly HelpForm helpWindow = new();
	private readonly ResetDataForm resetWindow = new();
	private readonly PalettesForm palWindow = new();
	private readonly UpdateAnnouncer UpdateShower = new();

	private bool BTypeControls = false;

	private WoodsROM? Vanilla = null;

	private readonly List<LoggedMessage> StatusLog = [];

	private readonly List<object> CharacterOptions = [];
	private readonly List<object> SongOptions = [.. SongSelect.ListOf, SongSelect.Muted, SongSelect.Random];


	private readonly Dictionary<MonsterType, bool> AllowedTypes = new() {
		{ MonsterType.Squeak, true },
		{ MonsterType.Spud, true },
		{ MonsterType.Fuzz, true },
		{ MonsterType.Beaker, true },
		{ MonsterType.Scram, true },
		{ MonsterType.Dovo, true },
		{ MonsterType.Spook, true },
	};

	public WoodsRandoForm() {
		StatusLog.LogMessage("Initializing...");

		InitializeComponent();

		if (!Program.PalettesXML.Exists) {
			StatusLog.LogMessage("No palettes.xml file found; creating default file");
			using var f1 = Program.PalettesXML.Open(FileMode.Create);
			using var f2 = Program.GetResourceStream("data/palettes.xml");
			f2?.CopyTo(f1);
		}

		if (!Program.SpritesXML.Exists) {
			StatusLog.LogMessage("No sprites.xml file found; creating default file");
			using var f1 = Program.SpritesXML.Open(FileMode.Create);
			using var f2 = Program.GetResourceStream("data/sprites.xml");
			f2?.CopyTo(f1);
		}

		Form[] initforms = [ helpWindow, resetWindow, palWindow ];

		foreach (Form f in initforms) {
			f.StartPosition = FormStartPosition.Manual;
			f.Icon = Icon;
		}

		PalettesDialogButton.Tag = palWindow;
		HelpMeButton.Tag = helpWindow;

		SuspendLayout();
		RestyleForVersion();

		Settings.Reload();

		AllowSpud.Tag = MonsterType.Spud;
		AllowSpook.Tag = MonsterType.Spook;
		AllowBeaker.Tag = MonsterType.Beaker;
		AllowScram.Tag = MonsterType.Scram;
		AllowDovo.Tag = MonsterType.Dovo;
		AllowSqueak.Tag = MonsterType.Squeak;
		AllowFuzz.Tag = MonsterType.Fuzz;

		BoardFillBox.DataSource = BoardFiller.ListOf;
		ColorFillBox.DataSource = ColorFiller.ListOf;
		DifficultyBox.DataSource = DifficultySelect.ListOf;
		RNGBox.DataSource = RNGAlgorithm.ListOf;
		SongSelectBox.DataSource = SongOptions;


		BoardFillBox.SelectedItem = BoardFiller.ListOf.FindMatchingSetting(Settings.BoardFiller, BoardFiller.Expert);
		ColorFillBox.SelectedItem = ColorFiller.ListOf.FindMatchingSetting(Settings.ColorFiller, ColorFiller.AllColors);
		DifficultyBox.SelectedItem = DifficultySelect.ListOf.FindMatchingSetting(Settings.Difficulty, DifficultySelect.Expert);
		RNGBox.SelectedItem = RNGAlgorithm.ListOf.FindMatchingSetting(Settings.RNGAlgorithm, RNGAlgorithm.Floppy);

		DirectDepositBox.Checked = Settings.DirectDeposit;
		SimpleRoundsBox.Checked = Settings.SimpleRounds;
		EnduranceBox.Checked = Settings.EnduranceMode;

		StartingLivesBox.SelectedIndex = int.Clamp(Settings.StartingLives, 0, StartingLivesBox.Items.Count - 1);
		SetControlType(Settings.ButtonSelect);

		StatusLog.LogMessage("Settings loaded.");

		LoggerPanel.Dock = DockStyle.Fill;

		ResumeLayout(true);
	}

	private void InitializeStuff(object sender, EventArgs e) {
		SetOutputDirectory(Settings.OutputDirectory);

		// look for saved ROM
		if (Program.Storage.FileExists(romname)) {
			byte[] romData;

			// need to dispose immediately incase the file should be deleted
			using (var s = new IsolatedStorageFileStream(romname, FileMode.Open, Program.Storage)) {
				romData = s.GetAsArray();
			}

			var status = TryROM(romData);

			if (status is WoodsROMValidity.AllGood) {
				StatusLog.LogMessage("Found a valid ROM.");
			} else {
				Program.Storage.DeleteFile(romname);
				StatusLog.LogMessage("The stored ROM appears to be invalid; it has been deleted.");
			}
		} else {
			StatusLog.LogMessage("No ROM found.");
			SetVanillaROM(null);
		}

		Refresh();

		UpdateThemeOptions();

		TryScaryTasks(RefreshCharacterSelection);

		RandomGreeting();
	}


	private void StartNewTask(int count) {
		TaskProgress.Visible = true;
		TaskProgress.Maximum = count;
		TaskProgress.Value = 0;
		TaskProgress.Step = 1;
	}

	private void ResetStuffButton_Click(object sender, EventArgs e) {
		resetWindow.Location = Location + new Size(20, 20);

		if (resetWindow.ShowDialog(Vanilla is not null) is not DialogResult.OK) {
			return;
		}

		ResetSettingsFlag rf = resetWindow.Result;

		if (rf.HasFlag(ResetSettingsFlag.CopyVanillaROM)) {
			CopyVanilla();
		}

		List<Action> totalTask = [];

		if (rf.HasFlag(ResetSettingsFlag.ReloadVanillaPlayerGraphics)) {
			totalTask.Add(CreateVanillaPlayerSheets);
		}

		if (rf.HasFlag(ResetSettingsFlag.RecreatePreviewImages)) {
			totalTask.Add(CreateCharacterPreviewImages);
		}

		if (rf.HasFlag(ResetSettingsFlag.RecompressBaseSprites)) {
			totalTask.Add(CreateCompressedCharacterData);
		}

		if (totalTask.Count > 0) {
			totalTask.Add(RefreshCharacterSelection);

			TryScaryTasks(totalTask);
		}
	}

	private void TryScaryTasks(params Action[] tasks) {
		TryScaryTasks((IEnumerable<Action>) tasks);
	}

	private void TryScaryTasks(IEnumerable<Action> tasks) {
		try {
			TaskStarted();

			foreach (Action t in tasks) {
				t.Invoke();
			}

			TaskFinished();
		} catch (Exception e) {
			TaskAborted();

			MessageDialogs.ShowException(this,
				caption: "Something went wrong.",
				header: "A problem occurred during the task.",
				exception: e
			);
		}
	}

	private void TaskStarted() {
		SetWaiting(true);
	}

	private void TaskAborted() {
		SetWaiting(false);
		TaskProgress.Visible = false;
		UpdateStatus("Aborted.");
	}

	private void TaskFinished() {
		SetWaiting(false);
		TaskProgress.Visible = false;
		UpdateStatus("Ready.");
	}

	private void SetWaiting(bool waiting) {
		UseWaitCursor = waiting;
		Enabled = !waiting;
	}


	private void CreateVanillaPlayerSheets() {
		if (Vanilla is null) throw new WoodsException("Missing vanilla ROM.");

		UpdateStatus("Reloading vanilla sheets...");

		CharacterGraphics.CreateVanillaPlayerSheets(Vanilla);
	}

	private void CreateCharacterPreviewImages() {
		FileInfo[] files = CharacterGraphics.GetBaseCharacterSheetFiles();

		if (files.Length == 0) {
			StatusLog.LogMessage("No character sheets found.");
			return;
		}

		UpdateStatusHard($"Creating previews for {files.Length} base sheets...");
		StartNewTask(files.Length);

		foreach (FileInfo file in files) {
			string filename = Path.GetFileNameWithoutExtension(file.FullName);
			try {
				if (file.Length is not CharacterGraphics.CharacterSheetSize) {
					StatusLog.LogMessage($"Skipping {filename}{CharacterGraphics.BaseSheetExtension} - wrong size.");
				} else {
					CharacterGraphics.CreateCharacterPreview(filename, file);
				}
			} catch (Exception e) {
				StatusLog.LogMessage($"Error creating preivew: {e.Message}");
			}

			TaskProgress.PerformStep();
		}

		StatusLog.LogMessage("Finished creating character previews.");
	}

	private void CreateCompressedCharacterData() {
		FileInfo[] files = CharacterGraphics.GetBaseCharacterSheetFiles();

		if (files.Length == 0) {
			StatusLog.LogMessage("No character sheets found.");
			return;
		}

		// try to get the base sheets
		byte[] basePlayerSheet, baseOverworldSheet;

		basePlayerSheet = Program.BasePlayerSheet.GetAsArray();
		baseOverworldSheet = Program.BaseOverworldSheet.GetAsArray();

		if (basePlayerSheet.Length is not CharacterGraphics.PlayerSheetSize) {
			throw new WoodsException("Base player sheet is wrong size.");
		}

		if (baseOverworldSheet.Length is not CharacterGraphics.OverworldSheetSize) {
			throw new WoodsException("Base overworld sheet is wrong size.");
		}

		UpdateStatusHard($"Creating graphics for {files.Length} base sheets...");
		StartNewTask(files.Length * 4);

		foreach (FileInfo file in files) {
			string filename = Path.GetFileNameWithoutExtension(file.FullName);

			if (file.Length is not CharacterGraphics.CharacterSheetSize) {
				StatusLog.LogMessage($"Skipping {filename}{CharacterGraphics.BaseSheetExtension} - wrong size.");
				TaskProgress.PerformStep();
				TaskProgress.PerformStep();
				TaskProgress.PerformStep();
				TaskProgress.PerformStep();
				continue;
			}

			using var f = file.OpenRead();
			f.Read(basePlayerSheet.AsSpan(8 * CharacterGraphics.RowSize, 6 * CharacterGraphics.RowSize));
			TaskProgress.PerformStep();

			byte[] bpcomp = Compression.Compress(basePlayerSheet);

			TaskProgress.PerformStep();

			byte[] buildingOverworld = [..baseOverworldSheet];

			if (filename is not "toad") {
				CharacterGraphics.CopyOverworldTiles(basePlayerSheet, buildingOverworld);
			}

			byte[] owcomp = Compression.Compress(buildingOverworld);

			TaskProgress.PerformStep();

			byte[] gameovericon = CharacterGraphics.CreateGameOverIcon(basePlayerSheet);

			WoodsGraphicsStorage charStore = new();

			charStore.AddMany(bpcomp, owcomp, gameovericon);

			FileInfo wp = Program.PlayerGraphicsData.GetFile($"{filename}{WoodsGraphicsStorage.GraphicsContainerExtension}");
			using FileStream f1 = wp.Create();

			charStore.WriteToStream(f1);

			TaskProgress.PerformStep();
		}

		StatusLog.LogMessage("Finished creating compressed graphics.");
	}


	private void UpdateThemeOptions() {
		List<ThemeSelect> options = ThemeSelect.CreateOptions();

		ThemeSelectBox.BeginUpdate();

		ThemeSelectBox.DataSource = options;

		ThemeSelectBox.SelectedItem = options.FindMatchingSetting(Settings.Theme, BoardTheme.Vanilla);

		ThemeSelectBox.EndUpdate();

	}

	private void ReadPlayerSpritesXML() {
		// try to read the XML
		using var spritesStream = Program.SpritesXML.Open(FileMode.Open, FileAccess.ReadWrite);

		CharacterGraphics.ClearEntries();

		// read the XML
		XmlDocument characterXML = new();
		characterXML.Load(spritesStream);

		var playerNodes = characterXML.SelectNodes("/sprites/player/character");

		if (playerNodes is null) {
			StatusLog.LogMessage("No player nodes to make.");
			return;
		}

		UpdateStatusHard("Loading character graphics...");

		StartNewTask(playerNodes.Count);

		// make each sheet
		foreach (XmlNode pnode in playerNodes) {
			TaskProgress.PerformStep();

			if (pnode.Attributes?["name"]?.Value is not string pname) {
				StatusLog.LogMessage("Skipping unnamed character element.");
			} else if (pnode.Attributes?["filename"]?.Value is not string pgfx) {
				StatusLog.LogMessage($"Skipping missing filename on \"{pname}\" character node");
			} else {
				try {
					if (CharacterGraphics.CreateEntry(pname, pgfx, out string? guyerrors) is CharacterGraphics guy) {
						var palerrors = guy.AddPalettesFromNode(pnode);
						StatusLog.AddRange(palerrors);
					} else if (guyerrors is not null) {
						StatusLog.LogMessage(guyerrors);
					}
				} catch (Exception e) {
					StatusLog.LogMessage($"Error creating character: {e.Message}");
				}
			}

			TaskProgress.PerformStep();
		}

		TaskProgress.Visible = false;
	}


	private void RefreshCharacterSelection() {
		UpdateStatusHard("Fetching character graphics data...");

		ReadPlayerSpritesXML();

		PlayAsBox.BeginUpdate();

		CharacterOptions.Clear();

		PlayAsBox.DataSource = null;

		if (CharacterGraphics.AllSheets.Count > 0) {
			CharacterOptions.AddRange(CharacterGraphics.AllSheets);
			CharacterOptions.Add(RandomChoice);
		} else {
			StatusLog.LogMessage("No valid characters found.");
			CharacterOptions.Add("Toad");
		}

		PlayAsBox.DataSource = CharacterOptions;
		PlayAsBox.SelectedItem = CharacterOptions.FindMatchingSetting(Settings.Character, Character.Sarissa);


		PlayAsBox.EndUpdate();
	}

	private WoodsROMValidity TryROM(byte[] rom) {
		WoodsROMValidity status = WoodsROM.TestIfVanilla(rom);

		if (status is not WoodsROMValidity.AllGood) {
			SetVanillaROM(null);
		} else {
			SetVanillaROM(rom);
		}

		return status;
	}

	private void SetVanillaROM(byte[]? rom) {
		if (rom is null) {
			Vanilla = null;
		} else {
			Vanilla = new(rom);
		}

		bool haveROM = Vanilla is not null;

		NeedsRomBox.Enabled = haveROM;
		UploadARomButton.Visible = !haveROM;
	}


	private void UpdateStatus(string message) {
		StatusLog.LogMessage(message);
		LoggedMessage latest = StatusLog.Last();
		StatusInfoText.Text = latest.Message;
		StatusTime.Text = latest.Time.ToString("HH:mm:ss");
	}

	private void UpdateStatusHard(string message) {
		UpdateStatus(message);
		Refresh();
	}

	private void AllMonstersCheckbox_CheckedChanged(object sender, EventArgs e) {
		if (internalMonsterChange) return;

		internalMonsterChange = true;
		AllowScram.Checked = AllMonstersCheckbox.Checked;
		AllowSpud.Checked = AllMonstersCheckbox.Checked;
		AllowFuzz.Checked = AllMonstersCheckbox.Checked;
		AllowSpook.Checked = AllMonstersCheckbox.Checked;
		AllowSqueak.Checked = AllMonstersCheckbox.Checked;
		AllowBeaker.Checked = AllMonstersCheckbox.Checked;
		AllowDovo.Checked = AllMonstersCheckbox.Checked;
		internalMonsterChange = false;
	}

	private void AllowType_CheckedChanged(object sender, EventArgs e) {
		if (sender is not CheckBox cb) return;
		if (cb.Tag is not MonsterType mtype) return;

		AllowedTypes[mtype] = cb.Checked;

		if (internalMonsterChange) return;

		internalMonsterChange = true;

		if (AllowScram.Checked && AllowSpud.Checked &&
			AllowFuzz.Checked && AllowSpook.Checked &&
			AllowSqueak.Checked && AllowBeaker.Checked && AllowDovo.Checked) {
			AllMonstersCheckbox.Checked = true;
		} else {
			AllMonstersCheckbox.Checked = false;
		}

		internalMonsterChange = false;
	}

	private void SetOutputDirectory(string directoryName) {
		directoryName = directoryName.Trim();
		StatusLog.LogMessage($"Setting directory: \"{directoryName}\"");

		if (!Directory.Exists(directoryName)) {
			if (CurrentDirectoryExists()) {
				StatusLog.LogMessage("Invalid directory; sticking with current.");
				return;
			}

			OutputDirectoryButton.Text = string.Empty;
			StatusLog.LogMessage("Invalid directory; cancelling.");
			OpenOutputDirectoryButton.Enabled = false;
			return;
		}

		OutputDirectoryButton.Text = directoryName;
		Settings.OutputDirectory = directoryName;
		OpenOutputDirectoryButton.Enabled = true;
	}

	private bool CurrentDirectoryExists() {
		return Directory.Exists(OutputDirectoryButton.Text);
	}

	private bool DemandDirectory() {
		if (!CurrentDirectoryExists()) {
			MessageDialogs.ShowWarning(this,
				caption: "No directory set.",
				header: "You must set a directory.",
				details: "Please set a directory for output using the field at the top of the form."
			);

			return false;
		}
		return true;
	}

	private void GenerateButton_Click(object sender, EventArgs e) {
		if (SarissaLittleGirlBox.Checked) {
			UpdateStatus("Sarissa is a lady.");
			StatusLog.LogMessage("I will do everything in my power to defeat you.");

			TaskDialog.ShowDialog(this, MessageDialogs.SarissaWarning, TaskDialogStartupLocation.CenterOwner);
			SarissaLittleGirlBox.Checked = false;
			return;
		}

		if (!DemandDirectory()) {
			return;
		}

		int generationCount = 0;

		try {
			TaskStarted();

			Generate(out generationCount);

			TaskFinished();

			UpdateStatus($"Generated {SimplePluralS("seed", generationCount)} with the given settings.");
		} catch (Exception ee) {
			if (generationCount == 0) {
				UpdateStatus($"Failed before generating anything.");
			} else {
				UpdateStatus($"Generated {SimplePluralS("seed", generationCount)} before failing.");
			}

			TaskAborted();

			MessageDialogs.ShowException(this,
				caption: "D'oh!",
				header: "Something went wrong during generation.",
				exception: ee
			);
		}
	}

	/// <summary>
	/// Generate a randomized game
	/// </summary>
	private void Generate(out int generationCount) {
		generationCount = 0;

		var boardFiller = BoardFillBox.SelectedItem as BoardFiller ?? throw new WoodsException("Bad board fill");
		var colorFiller = ColorFillBox.SelectedItem as ColorFiller ?? throw new WoodsException("Bad color fill");
		var difficultySetter = DifficultyBox.SelectedItem as DifficultySelect ?? throw new WoodsException("Bad color fill");
		var rngAlgo = RNGBox.SelectedItem as RNGAlgorithm ?? throw new WoodsException("Bad rng algo");

		var nm = (int) GenCount.Value;

		StatusLog.LogMessage($"Starting generation of {SimplePluralS("seed", nm)}...");

		MonsterType[] allowedTypes = (
			from mtype in AllowedTypes
			where mtype.Value
			select mtype.Key
		).ToArray();

		int allowedCount = allowedTypes.Length;

		string outpath = Settings.OutputDirectory;

		StatusLog.LogMessage("Creating base ROM with patches...");
		WoodsROM baseROM = new(Vanilla!.Stream);

		// apply some base patches
		ROMPatch.BasePatches.ApplyTo(baseROM); // THIS MUST ALWAYS BE FIRST

		ROMPatch.Optimizations.ApplyTo(baseROM);

		// change all game types to the same set of pointers even though this app is for round mode
		baseROM.Write16(0x85F908, 0x8000, 0x8000, 0x8000, 0x8000);
		baseROM.Write16(0x85F912 + 1, 0x8000);
		baseROM.Write16(0x85F919 + 1, 0x8000);

		// lives
		byte lives = (byte) StartingLivesBox.SelectedIndex;
		bool infiniteLives = false;

		if (lives > 9) {
			baseROM.Write8(0x88CD7E, 0xAD); // DEC => LDA
			lives = 1;
			infiniteLives = true;
		}

		baseROM.Write8(0x81E804, lives);

		// coins per life
		// baseROM.write8(0x898D42, 50);

		// direct deposit
		if (DirectDepositBox.Checked) {
			ROMPatch.DirectDeposit.ApplyTo(baseROM);
		}

		// endurance mode
		if (EnduranceBox.Checked) {
			ROMPatch.Endurance.ApplyTo(baseROM);
		}

		bool simpleRounds = SimpleRoundsBox.Checked;

		string settingsName = string.Concat([
			boardFiller.Token,
			colorFiller.Token,
			difficultySetter.Token,
			rngAlgo.Token,
			simpleRounds ? "S" : "C",
			"L",
			infiniteLives ? "i" : lives
		]);

		string binaryOptions = string.Concat([
			DirectDepositBox.Checked ? "D" : "",
			EnduranceBox.Checked ? "E" : "",
			simpleRounds ? "S" : "",
		]);

		if (!string.IsNullOrWhiteSpace(binaryOptions)) {
			settingsName = $"{settingsName}-{binaryOptions}";
		}

		// clear every level
		foreach (var r in baseROM.AllRoundLevels) {
			r.Clear();
		}

		if (simpleRounds) {
			foreach (var round in baseROM.AllRounds) {
				round.LevelB = round.LevelA;
			}
		}

		baseROM.CommitChanges();

		ThemeSelect? selectedTheme = ThemeSelectBox.SelectedItem as ThemeSelect;
		SongSelect selectedSong = (SongSelectBox.SelectedItem as SongSelect) ?? SongSelect.RoundGame;


		CharacterGraphics? selguy = PlayAsBox.SelectedItem as CharacterGraphics;
		CharacterPalette? selpal = CharacterPaletteBox.SelectedItem as CharacterPalette;

		string? guystring = PlayAsBox.SelectedItem?.ToString();
		string? palstring = CharacterPaletteBox.SelectedItem?.ToString();




		StartNewTask(nm);

		for (; generationCount < nm; generationCount++) {
			WoodsROM rom = new(baseROM.Stream) {
				Path = Path.Join(outpath, $"woodsrando-{CommonRNG.Next():X8} ({settingsName}).sfc")
				//Path = Path.Join(outpath, $"woodsrando-test.sfc")
			};

			// See Patches/base.asm # Randomizer constants
			rom.Write16i(0x81E800, CommonRNG.NextInclusive(0x0001, 0xFFFF));
			rom.Write16i(0x81E802, CommonRNG.NextInclusive(0x0001, 0xFFFF));

			rom.Write8(0x81E806, (byte) (BTypeControls ? 1 : 0));

			//if (respect) {
			//	var enemyspeed = rom.AsSpan(0x80D2AD, 7 * 8);
			//	
			//	foreach (ref byte es in enemyspeed) {
			//		es = CommonRNG.NextByte(1, 5);
			//	}
			//
			//}

			byte warioSpeed = CommonRNG.NextByteInclusive(3, 8);
			rom.Write8(0x87916A, warioSpeed);
			rom.Write8(0x8791C1, warioSpeed);

			rngAlgo.ApplyTo(rom);

			ushort songval = selectedSong.GetValue();
			rom.Write16(0x81E808, songval);

			// Set character graphics
			CharacterGraphics? guy;

			// TODO move this to an Action<WoodsROM>?
			if (guystring is RandomChoice) {
				guy = CommonRNG.GetRandomElement(CharacterGraphics.AllSheets); // TODO this
			} else {
				guy = selguy;
			}

			if (guy is not null) {
				guy.ApplyToROM(rom);

				CharacterPalette pal;

				if (selpal is not null) {
					pal = selpal;
				} else if (guy.Palettes.Count > 0) {
					pal = palstring switch {
						RandomChoice => CommonRNG.GetRandomElement(guy.Palettes),
						DefaultChoice => guy.Palettes[0],
						_ => guy.Palettes[0]
					};
				} else {
					pal = CharacterPalette.GrayscaleDefault;
				}

				pal.ApplyToROM(rom);

				// remove big poses for everyone, even toad because compatability
				rom.Write8(0x88DF90, [0x2B]);
				rom.Write8(0x88DF92, [0x2B]);
				rom.Write8(0x88DF94, [0x2B]);
				rom.Write8(0x88DF96, [0x2B]);
				rom.Write8(0x88DF99, [0x2B]);
				rom.Write8(0x88DF9C, [0x2B]);
				rom.Write8(0x88DF9F, [0x2B]);
				rom.Write8(0x88DFA1, [0x2B]);
				rom.Write8(0x88DFA3, [0x2B]);
			}

			// apply theme
			selectedTheme?.ApplyTo(rom);

			// randomize every level
			foreach (var round in rom.AllRounds) {
				round.MonsterTypesBYKT = allowedTypes[CommonRNG.Next(allowedCount)];
				round.MonsterTypesGRWP = allowedTypes[CommonRNG.Next(allowedCount)];

				difficultySetter.ApplyTo(round);

				if (infiniteLives) {
					round.Gold = 0;
				}

				var level = rom.AllRoundLevels[round.LevelA];
				boardFiller.ApplyTo(level, round.RoundNumber);
				colorFiller.ApplyTo(level, round.RoundNumber);

				if (round.LevelA == round.LevelB) continue;
				level = rom.AllRoundLevels[round.LevelB];
				boardFiller.ApplyTo(level, round.RoundNumber);
				colorFiller.ApplyTo(level, round.RoundNumber);
			}

			// save randomized woods
			rom.CommitChanges();
			rom.Save();

			TaskProgress.PerformStep();
		}
	}

	private static readonly string[] Greetings = [
			"Hello!",
			"Sweet!",
			"Breakfast!",
			"Cool!",
			"Whoa!",
			"Woohoo!",
			"Fight!",
			"Oh!",
			"Yes!",
			"Groovy!",
			"Score!",
			"Winner!",
			"Yowwwwza!",
			"Ladies first.",
			"Let's see what you've got!",
			"The real run starts here.",
			"It only gets tougher.",
			"The next game will feature me!",
			"I've been waiting for you.",
			"Prepare your excuse for losing!",
			"I know all the mathematical possibilites.",
			"Did you see my skill with the A and B buttons?",
			"Behold!",
			"Everyone tells me I'm irresistible.",
			"I'm ready for your best shot.",
			"Bomb Time will be extended.",
		];

	private void RandomGreeting() {
		StatusInfoText.Text = CommonRNG.GetRandomElement(Greetings);
	}

	private void OutputDirectoryButton_MouseClick(object sender, MouseEventArgs e) {
		var folderBrowser = new FolderBrowserDialog() {
			ShowNewFolderButton = true,
		};

		if (folderBrowser.ShowDialog() != DialogResult.OK) return;

		SetOutputDirectory(folderBrowser.SelectedPath);
	}

	private void UploadROMButton_Click(object sender, EventArgs e) {
		MessageDialogs.OpenROM.FileName = $"{WoodsROM.PreferredFilename}.sfc";

		if (MessageDialogs.OpenROM.ShowDialog() is not DialogResult.OK) {
			return;
		}

		byte[] romData;

		try {
			using var fs = MessageDialogs.OpenROM.OpenFile();
			romData = fs.GetAsArray();
		} catch (Exception ee) {
			SetVanillaROM(null);

			MessageDialogs.ShowException(this,
				caption: "Bad file!",
				header: "Unable to read file.",
				exception: ee
			);

			return;
		}

		StatusLog.LogMessage("Validating selected ROM...");

		var status = TryROM(romData);

		if (status is not WoodsROMValidity.AllGood) {
			string reason;

			if (status.HasFlag(WoodsROMValidity.BadSize)) {
				reason = "The expected ROM size is 1MB (1,048,576 bytes).";
			} else if (status.HasFlag(WoodsROMValidity.NotEvenWoods)) {
				reason = "This appears to be a different game based on its title.";
			} else if (status.HasFlag(WoodsROMValidity.BadHash)) {
				reason = "Something is wrong with this ROM.";
			} else {
				reason = "Your ROM sucks!";
			}

			UpdateStatus($"Invalid ROM rejected: {reason}");

			MessageDialogs.ShowWarning(this,
				caption: "Invalid base ROM provided",
				header: $"Please provide a clean vanilla copy of {WoodsROM.PreferredFilename}.",
				details: reason
			);

			return;
		}

		DialogResult tryAgain = DialogResult.TryAgain;

		do {
			Exception? saveException = null;

			try {
				using var s = new IsolatedStorageFileStream(romname, FileMode.Create, FileAccess.Write, Program.Storage);
				s.SetLength(0);
				s.Write(romData);

				Vanilla = new(romData);
			} catch (Exception ex) {
				saveException = ex;
			}

			if (saveException is null) {
				break;
			}

			StatusLog.LogMessage($"Error saving ROM: {saveException.Message}");
			tryAgain = MessageDialogs.ShowTaskDialog(this, MessageDialogs.TryAgainPage);

			if (tryAgain is not DialogResult.TryAgain) {
				StatusLog.LogMessage("User gave up on saving ROM to storage.");
				return;
			}

			StatusLog.LogMessage("Trying again...");

		} while (tryAgain is DialogResult.TryAgain);

		UpdateStatus("Successfully saved vanilla ROM to storage.");

		TryScaryTasks(
			CreateVanillaPlayerSheets,
			CreateCharacterPreviewImages,
			CreateCompressedCharacterData,
			RefreshCharacterSelection
		);

	}

	private void ShowLogButton_Click(object sender, EventArgs e) {
		bool newvis = !LoggerPanel.Visible;

		if (newvis) {
			UpdateLogDisplay();
			ShowLogButton.BorderStyle = Border3DStyle.Sunken;
			ShowLogButton.BackColor = SystemColors.Control;
			LoggerPanel.BringToFront();
		} else {
			ShowLogButton.BorderStyle = Border3DStyle.Raised;
			ShowLogButton.BackColor = SystemColors.ButtonFace;
		}

		LoggerPanel.Visible = newvis;
		NeedsRomBox.Visible = !newvis;
		RomPanel.Visible = !newvis;
	}

	private void ClearLogButton_Click(object sender, EventArgs e) {
		StatusLog.Clear();
		StatusLog.LogMessage("Status log cleared.");

		UpdateLogDisplay();
	}

	private void UpdateLogDisplay() {
		LogBox.Text = string.Join("\r\n", StatusLog.Select(o => o.LongString));
	}

	private void WoodsFormClosing(object sender, FormClosingEventArgs e) {
		Settings.BoardFiller = BoardFillBox.SelectedItem?.ToString() ?? "???";
		Settings.Difficulty = DifficultyBox.SelectedItem?.ToString() ?? "???";
		Settings.ColorFiller = ColorFillBox.SelectedItem?.ToString() ?? "???";
		Settings.Character = PlayAsBox.SelectedItem?.ToString() ?? "Sarissa";
		Settings.RNGAlgorithm = RNGBox.SelectedItem?.ToString() ?? "???";
		Settings.Theme = ThemeSelectBox.SelectedItem?.ToString() ?? "???";

		Settings.DirectDeposit = DirectDepositBox.Checked;
		Settings.SimpleRounds = SimpleRoundsBox.Checked;

		Settings.StartingLives = StartingLivesBox.SelectedIndex;
		Settings.EnduranceMode = EnduranceBox.Checked;

		Settings.ButtonSelect = BTypeControls;

		Settings.Save();
	}

	private void SarissaClick(object sender, EventArgs e) {
		UpdateStatus("Sarissa clicked.");
		RandomGreeting();
	}

	private void BonusWindowClick(object sender, EventArgs e) {
		// check for types that have a Tag property
		object? w = sender switch {
			ToolStripItem a => a.Tag,
			Control a       => a.Tag,
			_               => null
		};

		if (w is Form f) {
			if (!f.Visible) {
				f.Location = Location + new Size(20, 20);
			}

			f.Show();
			f.Focus();
		}
	}

	private void GithubButton_Click(object sender, EventArgs e) {
		Process.Start("explorer.exe", "https://github.com/spannerisms/WariosWoodsRandomizer/releases/latest");
	}

	private void CopyVanilla() {
		if (Vanilla is null) {
			return;
		}

		if (!DemandDirectory()) {
			return;
		}

		string outdir = Settings.OutputDirectory;
		string path = Path.Join(outdir, $"{WoodsROM.PreferredFilename}.sfc");
		int n = 0;

		while (File.Exists(path)) {
			if (++n == 100) {
				MessageDialogs.ShowWarning(this,
					caption: "Enough!",
					header: "You have plenty of woods.",
					details: "Look, gamer; I love Woods as much as anyone else, but you already seem to have 100 copies of vanilla Wario's Woods."
				);
				return;
			}
			path = Path.Join(outdir, $"{WoodsROM.PreferredFilename} ({n}).sfc");
		}

		try {
			using var fs = new FileStream(path, FileMode.Create);
			fs.Write(Vanilla.Stream);
		} catch (Exception ee) {
			MessageDialogs.ShowException(this,
				caption: "Problem",
				header: "Unable to save",
				exception: ee
			);
		}
	}

	private void OpenOutputDirectoryButton_Click(object sender, EventArgs e) {
		if (CurrentDirectoryExists()) {
			Process.Start("explorer.exe", OutputDirectoryButton.Text);
		}
	}


	private void PlayAsBox_SelectedIndexChanged(object sender, EventArgs e) {
		CharacterPaletteBox.BeginUpdate();

		if (PlayAsBox.SelectedItem is CharacterGraphics cg) {
			object[] selitems = [..cg.Palettes, RandomChoice];
			CharacterPaletteBox.DataSource = selitems;
			CharacterPaletteBox.Visible = true;
		} else if (PlayAsBox.SelectedItem is string sels && sels is RandomChoice) {
			CharacterPaletteBox.DataSource = RandomCharPalettes;
			CharacterPaletteBox.Visible = true;
		} else {
			CharacterPaletteBox.DataSource = null;
			CharacterPaletteBox.Visible = false;
		}
		CharacterPaletteBox.EndUpdate();
	}

	private void ButtonSelectButton_Click(object sender, EventArgs e) {
		SetControlType(!BTypeControls);
	}

	private void SetControlType(bool btype) {
		BTypeControls = btype;
		ButtonSelectButton.Text = btype ? "B-type" : "A-type";
	}

	private void RestyleForVersion() {
		if (UpdateShower.Compare is int comp) {
			switch (comp) {
				case < 0:
					VersionLabel.BackColor = Color.SpringGreen;
					VersionLabel.ForeColor = Color.Green;
					VersionLabel.Text = "Update!";
					break;

				case 0:
					VersionLabel.BackColor = Color.Silver;
					VersionLabel.ForeColor = Color.DimGray;
					VersionLabel.Text = UpdateAnnouncer.VersionString;
					break;

				case > 0:
					VersionLabel.BackColor = Program.Periwinkle;
					VersionLabel.ForeColor = Program.DarkPeriwinkle;
					VersionLabel.Text = "Beta";
					break;
			}
		} else {
			VersionLabel.BackColor = Color.Orange;
			VersionLabel.ForeColor = Color.Maroon;
			VersionLabel.Text = "Problem";
		}
	}

	private void VersionLabel_Click(object sender, EventArgs e) {
		int? lastCompare = UpdateShower.Compare;

		UpdateShower.ShowAndCheckForUpdates();

		if (lastCompare != UpdateShower.Compare) {
			RestyleForVersion();
		}
	}
}
