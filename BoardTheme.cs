namespace WoodsRandomizer;

internal class BoardTheme {
	public MonsterPalette RedPalette { get; }
	public MonsterPalette YellowPalette { get; }
	public MonsterPalette GreenPalette { get; }
	public MonsterPalette BluePalette { get; }
	public MonsterPalette PinkPalette { get; }
	public MonsterPalette AzurePalette { get; }
	public MonsterPalette BlackPalette { get; }
	public MonsterPalette WhitePalette { get; }

	public string Name { get; init; } = string.Empty;

	public BoardTheme(MonsterPalette red, MonsterPalette yellow, MonsterPalette green, MonsterPalette blue, MonsterPalette pink, MonsterPalette azure, MonsterPalette black, MonsterPalette white) {
		RedPalette = red;
		YellowPalette = yellow;
		GreenPalette = green;
		BluePalette = blue;
		PinkPalette = pink;
		AzurePalette = azure;
		BlackPalette = black;
		WhitePalette = white;
	}

	public BoardTheme(string name, MonsterPalette red, MonsterPalette yellow, MonsterPalette green, MonsterPalette blue, MonsterPalette pink, MonsterPalette azure, MonsterPalette black, MonsterPalette white)
		: this(red, yellow, green, blue, pink, azure, black, white) {
		Name = name;
	}


	public ThemeSelect AsThemeSelect() => new(Name, ApplyTo);

	public override string ToString() => Name;

	public void ApplyTo(WoodsROM rom) {
		GreenPalette.WriteToRomAt(rom, Addresses.GreenPaletteAddresses);
		BluePalette.WriteToRomAt(rom, Addresses.BluePaletteAddresses);
		RedPalette.WriteToRomAt(rom, Addresses.RedPaletteAddresses);
		YellowPalette.WriteToRomAt(rom, Addresses.YellowPaletteAddresses);
		WhitePalette.WriteToRomAt(rom, Addresses.WhitePaletteAddresses);
		BlackPalette.WriteToRomAt(rom, Addresses.BlackPaletteAddresses);
		PinkPalette.WriteToRomAt(rom, Addresses.PinkPaletteAddresses);
		AzurePalette.WriteToRomAt(rom, Addresses.AzurePaletteAddresses);
	}


	public static BoardTheme FromROM(byte[] rom) {
		return new(
			red: _palget(Addresses.RedPaletteAddresses),
			yellow: _palget(Addresses.YellowPaletteAddresses),
			green: _palget(Addresses.GreenPaletteAddresses),
			blue: _palget(Addresses.BluePaletteAddresses),
			pink: _palget(Addresses.PinkPaletteAddresses),
			azure: _palget(Addresses.AzurePaletteAddresses),
			black: _palget(Addresses.BlackPaletteAddresses),
			white: _palget(Addresses.WhitePaletteAddresses)
		);

		MonsterPalette _palget(ReadOnlySpan<int> start) => MonsterPalette.FromBytes(new ReadOnlySpan<byte>(rom, SNESToPC(start[0]), 14));
	}


	public bool ThemesEqual(BoardTheme compare) {
		return
			RedPalette.Equals(compare.RedPalette) &&
			YellowPalette.Equals(compare.YellowPalette) &&
			GreenPalette.Equals(compare.GreenPalette) &&
			BluePalette.Equals(compare.BluePalette) &&
			PinkPalette.Equals(compare.PinkPalette) &&
			AzurePalette.Equals(compare.AzurePalette) &&
			BlackPalette.Equals(compare.BlackPalette) &&
			WhitePalette.Equals(compare.WhitePalette);
	}

	public static readonly BoardTheme Vanilla = new(
		red: MonsterPalette.VanillaRed,
		yellow: MonsterPalette.VanillaYellow,
		green: MonsterPalette.VanillaGreen,
		blue: MonsterPalette.VanillaBlue,
		pink: MonsterPalette.VanillaPink,
		azure: MonsterPalette.VanillaAzure,
		black: MonsterPalette.VanillaBlack,
		white: MonsterPalette.VanillaWhite){
		Name = "Vanilla"
	};
}