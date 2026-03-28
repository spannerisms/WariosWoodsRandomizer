namespace WoodsRandomizer;
internal class ShadedColor {
	public string Name { get; init; } = string.Empty;

	public Color Light { get; }
	public Color Dark { get; }

	public SNESColor LightData { get; }
	public SNESColor DarkData { get; }

	public ShadedColor(int light, int dark) {
		LightData = new(light);
		DarkData = new(dark);

		Light = LightData.ToColor();
		Dark = DarkData.ToColor();
	}



	public ShadedColor(string name, int light, int dark) : this(light, dark) {
		Name = name;
	}

	public ShadedColor(int lr, int lg, int lb, int dr, int dg, int db) {
		LightData = new(lr, lg, lb);
		DarkData = new(dr, dg, db);

		Light = LightData.ToColor();
		Dark = DarkData.ToColor();
	}


	const float scale = 1F / 1.77F;
	public static ShadedColor GetAutoShadedColor(int red, int green, int blue) {
		int dr = (int) (scale * red);
		int dg = (int) (scale * green);
		int db = (int) (scale * blue);

		return new(red, green, blue, dr, dg, db);
	}

	public static ShadedColor GetAutoShadedFace(int red, int green, int blue) {
		int dg = (int) (scale * green);
		int db = (int) (scale * blue);

		return new(red, green, blue, red, dg, db);
	}

	public override bool Equals(object? obj) => obj switch {
		ShadedColor sc => Equals(sc),
		_ => false
	};

	public override int GetHashCode() {
		return (((int) LightData) << 16) | (((int) DarkData) << 16);
	}


	public bool Equals(ShadedColor compare) {
		return (LightData == compare.LightData) && (DarkData == compare.DarkData);
	}


	public static ShadedColor FromSNES(int light, int dark) => new(SNESColor.GetRGBFromSNES(light), SNESColor.GetRGBFromSNES(dark));
	public static ShadedColor FromSNES(SNESColor light, SNESColor dark) => new(light.ToRGB(), dark.ToRGB());



	public static readonly ShadedColor VanillaMonsterBlush = new(name: "Vanilla Monster Blush", light: 0xFFD6D6, dark: 0xFF7B7B);
	public static readonly ShadedColor VanillaSuboutline = new(name: "Vanilla Suboutline", light: 0x4A0000, dark: 0x200000);
	public static readonly ShadedColor VanillaRedBody = new(name: "Vanilla Red Body", light: 0xFF3129, dark: 0x942929);
	public static readonly ShadedColor VanillaRedGloss = new(name: "Vanilla Red Gloss", light: 0xFFDE00, dark: 0x7B8400);
	public static readonly ShadedColor VanillaYellowBody = new(name: "Vanilla Yellow Body", light: 0xEFEF21, dark: 0x7B8400);
	public static readonly ShadedColor VanillaYellowGloss = new(name: "Vanilla Yellow Gloss", light: 0xFF3131, dark: 0x942929);
	public static readonly ShadedColor VanillaGreenBody = new(name: "Vanilla Green Body", light: 0x42CE00, dark: 0x296B29);
	public static readonly ShadedColor VanillaGreenGloss = new(name: "Vanilla Green Gloss", light: 0xFFFF8C, dark: 0x7B844A);
	public static readonly ShadedColor VanillaBlueBody = new(name: "Vanilla Blue Body", light: 0x0084FF, dark: 0x003994);
	public static readonly ShadedColor VanillaBlueGloss = new(name: "Vanilla Blue Gloss", light: 0xFFFFFF, dark: 0x00BDF7);
	public static readonly ShadedColor VanillaPinkBody = new(name: "Vanilla Pink Body", light: 0xFF84FF, dark: 0xAD427B);
	public static readonly ShadedColor VanillaPinkGloss = new(name: "Vanilla Pink Gloss", light: 0xFFFF00, dark: 0x7B8400);
	public static readonly ShadedColor VanillaAzureBody = new(name: "Vanilla Azure Body", light: 0x42D6F7, dark: 0x318C8C);
	public static readonly ShadedColor VanillaAzureGloss = new(name: "Vanilla Azure Gloss", light: 0x0084FF, dark: 0x003994);
	public static readonly ShadedColor VanillaBlackBody = new(name: "Vanilla Black Body", light: 0x848484, dark: 0x525252);
	public static readonly ShadedColor VanillaBlackGloss = new(name: "Vanilla Black Gloss", light: 0xFFCE00, dark: 0x9C8400);
	public static readonly ShadedColor VanillaWhiteBody = new(name: "Vanilla White Body", light: 0xEFEFEF, dark: 0xA5A5A5);
	public static readonly ShadedColor VanillaWhiteGloss = new(name: "Vanilla White Gloss", light: 0x73B5FF, dark: 0x004AA5);
}