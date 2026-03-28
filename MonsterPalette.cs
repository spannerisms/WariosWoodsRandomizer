using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WoodsRandomizer;

internal class MonsterPalette {
	public string Name { get; init; } = string.Empty;
	public ShadedColor Body { get; }
	public ShadedColor Gloss { get; }
	public ShadedColor Blush { get; }

	public ShadedColor SubOutline { get; init; } = ShadedColor.VanillaSuboutline;

	public ObjectColor? PreferredColor { get; init; } = null;

	public MonsterPalette(ShadedColor body, ShadedColor gloss, ShadedColor blush) {
		Body = body;
		Gloss = gloss;
		Blush = blush;
	}

	public MonsterPalette(string name, ShadedColor body, ShadedColor gloss, ShadedColor blush) : this(body, gloss, blush) {
		Name = name;
	}

	public static MonsterPalette FromBytes(ReadOnlySpan<byte> data) {
		ReadOnlySpan<SNESColor> palette = MemoryMarshal.Cast<byte, SNESColor>(data);

		if (palette.Length < 7) throw new WoodsException("Not enough data for palette reading.");

		ShadedColor body = ShadedColor.FromSNES(palette[0], palette[1]);
		ShadedColor gloss = ShadedColor.FromSNES(palette[2], palette[3]);
		ShadedColor blush = ShadedColor.FromSNES(palette[5], palette[4]);
		ShadedColor suboutline = ShadedColor.FromSNES(palette[6], palette[6]);

		return new(body, gloss, blush) { SubOutline = suboutline };
	}



	public override string ToString() => Name;

	public static readonly MonsterPalette VanillaRed = new(name: "Vanilla Red", body: ShadedColor.VanillaRedBody, gloss: ShadedColor.VanillaRedGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Red,
	};

	public static readonly MonsterPalette VanillaGreen = new(name: "Vanilla Green", body: ShadedColor.VanillaGreenBody, gloss: ShadedColor.VanillaGreenGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Green,
	};

	public static readonly MonsterPalette VanillaBlue = new(name: "Vanilla Blue", body: ShadedColor.VanillaBlueBody, gloss: ShadedColor.VanillaBlueGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Blue,
	};

	public static readonly MonsterPalette VanillaYellow = new(name: "Vanilla Yellow", body: ShadedColor.VanillaYellowBody, gloss: ShadedColor.VanillaYellowGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Yellow,
	};

	public static readonly MonsterPalette VanillaWhite = new(name: "Vanilla White", body: ShadedColor.VanillaWhiteBody, gloss: ShadedColor.VanillaWhiteGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.White,
	};

	public static readonly MonsterPalette VanillaBlack = new(name: "Vanilla Black", body: ShadedColor.VanillaBlackBody, gloss: ShadedColor.VanillaBlackGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Black,
	};

	public static readonly MonsterPalette VanillaPink = new(name: "Vanilla Pink", body: ShadedColor.VanillaPinkBody, gloss: ShadedColor.VanillaPinkGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Pink,
	};

	public static readonly MonsterPalette VanillaAzure = new(name: "Vanilla Azure", body: ShadedColor.VanillaAzureBody, gloss: ShadedColor.VanillaAzureGloss, blush: ShadedColor.VanillaMonsterBlush) {
		PreferredColor = ObjectColor.Azure,
	};


	public void ApplyToImage(Image image) {
		ColorPalette ppal = image.Palette;

		if (ppal.Entries.Length < 15) return;

		ppal.Entries[2] = Body.Light;
		ppal.Entries[3] = Body.Dark;
		ppal.Entries[4] = Gloss.Light;
		ppal.Entries[5] = Gloss.Dark;
		ppal.Entries[6] = Blush.Dark;
		ppal.Entries[7] = Blush.Light;
		ppal.Entries[8] = SubOutline.Light;
		image.Palette = ppal;
	}


	public void WriteToRomAt(WoodsROM rom, ReadOnlySpan<int> addresses) {
		foreach (int address in addresses) {
			Span<SNESColor> palette = MemoryMarshal.Cast<byte, SNESColor>(rom.AsSpan(address, 16));

			palette[0] = Body.LightData;
			palette[1] = Body.DarkData;

			palette[2] = Gloss.LightData;
			palette[3] = Gloss.DarkData;

			palette[5] = Blush.LightData;
			palette[4] = Blush.DarkData;

			palette[6] = SubOutline.LightData;

		}
	}

	public override bool Equals(object? obj) => obj switch {
		MonsterPalette sc => Equals(sc),
		_ => false
	};

	public override int GetHashCode() {
		return $"{Body.GetHashCode()}{Gloss.GetHashCode()}{Blush.GetHashCode()}{SubOutline.GetHashCode()}".GetHashCode();
	}


	public bool Equals(MonsterPalette compare) {
		return Body.Equals(compare.Body) && Blush.Equals(compare.Blush) && Gloss.Equals(compare.Gloss) && (SubOutline.LightData == compare.SubOutline.LightData);
	}
}