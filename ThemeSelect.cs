namespace WoodsRandomizer;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

internal record ThemeSelect(string Name, Action<WoodsROM> Algo) {
	public override string ToString() => Name;

	public void ApplyTo(WoodsROM rom) => Algo(rom);

	private static readonly MonsterPalette[] shuffleable = [.. ThemesHandler.NamedPalettes];

	public static ImmutableArray<ThemeSelect> ListOfFunctions { get; }

	static ThemeSelect() {
		ListOfFunctions = [];
	}


	public static List<ThemeSelect> CreateOptions() {
		return [
			.. ThemesHandler.NamedThemes.Select(o => o.AsThemeSelect()),
			Random, Hodgepodge, Shifted
		];
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)] private static int GetRandomHigh() => CommonRNG.Next(160, 256);
	[MethodImpl(MethodImplOptions.AggressiveInlining)] private static int GetRandomMed() => CommonRNG.Next(100, 150);
	[MethodImpl(MethodImplOptions.AggressiveInlining)] private static int GetRandomLow() => CommonRNG.Next(0, 41);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static ShadedColor GetRandomFace() {
		return ShadedColor.GetAutoShadedFace(CommonRNG.Next(230, 256), CommonRNG.Next(170, 220), CommonRNG.Next(170, 220));
	}


	private static ShadedColor GetRandomGray(int min, int max) {
		int gray = CommonRNG.Next(min, max);
		return ShadedColor.GetAutoShadedColor(gray, gray, gray);
	}


	// hues
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomRed() => ShadedColor.GetAutoShadedColor(red: GetRandomHigh(), green: GetRandomLow(), blue: GetRandomLow());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomYellow() => ShadedColor.GetAutoShadedColor(red: GetRandomHigh(), green: GetRandomHigh(), blue: GetRandomLow());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomBlue() => ShadedColor.GetAutoShadedColor(red: GetRandomLow(), green: GetRandomLow(), blue: GetRandomHigh());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomPink() => ShadedColor.GetAutoShadedColor(red: GetRandomHigh(), green: GetRandomMed(), blue: GetRandomHigh());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomGreen() => ShadedColor.GetAutoShadedColor(red: GetRandomLow(), green: GetRandomHigh(), blue: GetRandomLow());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomWhite() => GetRandomGray(200, 256);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static ShadedColor GetRandomBlack() => GetRandomGray(70, 130);




	/****************************
	 * themes
	*****************************/

	/// <summary>
	/// Picks a random existing theme
	/// </summary>
	public static readonly ThemeSelect Random = new($"{SPLAT}Random",
		rom => ThemesHandler.NamedThemes[CommonRNG.Next(ThemesHandler.NamedThemes.Count)].ApplyTo(rom));

	/// <summary>
	/// Creates a new theme from random colors
	/// </summary>
	public static readonly ThemeSelect Hodgepodge = new($"{SPLAT}Hodgepodge", rom => {
		CommonRNG.Shuffle(shuffleable);

		BoardTheme hp = new(
			red: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Red),
			yellow: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Yellow),
			green: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Green),
			blue: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Blue),
			pink: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Pink),
			azure: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Azure),
			black: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.Black),
			white: ThemesHandler.NamedPalettes.GetRandomElementWhere(o => o.PreferredColor == ObjectColor.White)
		);
		hp.ApplyTo(rom);
	});

	/// <summary>
	/// Creates a new theme from random colors
	/// </summary>
	public static readonly ThemeSelect Potpourri = new($"{SPLAT}Potpourri", rom => {
		CommonRNG.Shuffle(shuffleable);

		BoardTheme hp = new(shuffleable[0], shuffleable[1], shuffleable[2], shuffleable[3], shuffleable[4], shuffleable[5], shuffleable[6], shuffleable[7]);
		hp.ApplyTo(rom);
	});


	public static readonly ThemeSelect Shifted = new($"{SPLAT}Shifted", rom => GetShiftedTheme().ApplyTo(rom));


	internal static BoardTheme GetShiftedTheme() {
		MonsterPalette redpal = new(
			body: GetRandomRed(),
			gloss: GetRandomYellow(),
			blush: GetRandomFace()
		);

		MonsterPalette yellowpal = new(
			body: GetRandomYellow(),
			gloss: GetRandomRed(),
			blush: GetRandomFace()
		);

		MonsterPalette greenpal = new(
			body: GetRandomGreen(),
			gloss: ShadedColor.GetAutoShadedColor(red: GetRandomHigh(), green: GetRandomHigh(), blue: GetRandomMed()),
			blush: GetRandomFace()
		);

		MonsterPalette bluepal = new(
			body: GetRandomBlue(),
			gloss: GetRandomWhite(),
			blush: GetRandomFace()
		);

		MonsterPalette pinkpal = new(
			body: GetRandomPink(),
			gloss: GetRandomYellow(),
			blush: GetRandomFace()
		);

		MonsterPalette azurepal = new(
			body: ShadedColor.GetAutoShadedColor(red: GetRandomLow(), green: GetRandomHigh(), blue: GetRandomHigh()),
			gloss: GetRandomBlue(),
			blush: GetRandomFace()
		);

		MonsterPalette blackpal = new(
			body: GetRandomBlack(),
			gloss: GetRandomYellow(),
			blush: GetRandomFace()
		);

		MonsterPalette whitepal = new(
			body: GetRandomWhite(),
			gloss: GetRandomBlue(),
			blush: GetRandomFace()
		);

		return new(redpal, yellowpal, greenpal, bluepal, pinkpal, azurepal, blackpal, whitepal);
	}





}
