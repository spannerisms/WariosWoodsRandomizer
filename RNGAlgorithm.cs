namespace WoodsRandomizer;

internal class RNGAlgorithm {
	public string Name { get; }
	public string Token { get; }

	private readonly Action<WoodsROM> Patcher;

	private readonly ROMPatch? Payload = null;

	private RNGAlgorithm(string name, string token, string? patch, Action<WoodsROM> patcher) {
		Name = name;
		Patcher = patcher;
		Token = $"R{token}";

		if (patch is not null) {
			Payload = new(patch);
		}
	}

	public void ApplyTo(WoodsROM rom) {
		Payload?.ApplyTo(rom);
		Patcher(rom);
	}

	public override string ToString() => Name;

	public static ImmutableArray<RNGAlgorithm> ListOf { get; }
	static RNGAlgorithm() {
		ListOf = [Vanilla, Remixed, Floppy, Shifty, Zelda3];
	}

	public static readonly RNGAlgorithm Vanilla = new("Vanilla", "v", null, DoNothing);

	public static readonly RNGAlgorithm Remixed = new("Remixed", "r", null, rom => {
		CommonRNG.NextBytes(rom.AsSpan(Addresses.RNGTable, 256));
	});

	private static void DoNothing(WoodsROM rom) { }

	public static readonly RNGAlgorithm Floppy = new("Floppy", "f", "rng-floppy", DoNothing);
	public static readonly RNGAlgorithm Shifty = new("Shifty", "x", "rng-shifty", DoNothing);

	public static readonly RNGAlgorithm Zelda3 = new("ALTTP", "z", "rng-zelda3", DoNothing);
}
