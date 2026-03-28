namespace WoodsRandomizer;

internal class ColorFiller {
	public string Name { get; }
	public string Token { get; }
	private readonly Action<Level, int> Filler;
	private ColorFiller(string name, string token, Action<Level, int> filler) {
		Name = name;
		Filler = filler;
		Token = $"C{token}";
	}

	public void ApplyTo(Level level, int round) => Filler(level, round);

	public override string ToString() => Name;

	public static ImmutableArray<ColorFiller> ListOf { get; }
	static ColorFiller() {
		ListOf = [Progressive, AllColors, Random, Chaotic];
	}

	public static readonly ColorFiller Progressive = new("Progressive", "p", (level, round) => {
		(int maxColor, int minColor) = round switch {
			< 5  => (1, 2),
			< 10 => (2, 3),
			< 20 => (2, 4),
			< 25 => (3, 4),
			< 30 => (3, 5),
			< 40 => (4, 5),
			< 50 => (4, 6),
			< 55 => (5, 6),
			< 65 => (5, 7),
			< 70 => (6, 7),
			< 85 => (6, 8),
			< 90 => (7, 8),
			_    => (8, 8)
		};

		int colorsUsed = CommonRNG.Next(minColor, maxColor+1);
		ShuffleLevelColors(level, colorsUsed);
	});

	public static readonly ColorFiller AllColors = new("All colors", "a", (level, round) => {
		ShuffleLevelColors(level, 8);
	});

	public static readonly ColorFiller Random = new("Random", "r", (level, round) => {
		int maxColor = CommonRNG.Next(6, 8);

		ShuffleLevelColors(level, maxColor);
	});

	public static readonly ColorFiller Chaotic = new("Chaotic", "c", (level, round) => {
		int maxColor = CommonRNG.Next(1, 8);

		ShuffleLevelColors(level, maxColor);
	});

	private static void ShuffleLevelColors(Level level, int maxColors) {
		ObjectColor[] ret = Enum.GetValues<ObjectColor>();
		CommonRNG.Shuffle(ret);

		level.ForAllObjects(o => o.Color = ret[CommonRNG.Next(0, maxColors)]);
	}
}
