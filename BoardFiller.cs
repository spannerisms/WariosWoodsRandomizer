namespace WoodsRandomizer;

internal class BoardFiller {
	public string Name { get; }
	public string Token { get; }

	private readonly Action<Level, int> Filler;
	private BoardFiller(string name, string token, Action<Level, int> filler) {
		Name = name;
		Filler = filler;
		Token = $"B{token}";
	}

	public void ApplyTo(Level level, int round) => Filler(level, round);
	public override string ToString() => Name;

	public static ImmutableArray<BoardFiller> ListOf { get; }

	static BoardFiller() {
		ListOf = [Progressive, Easy, Medium, Hard, Expert, FullFill, SimpleShuffle, AdvancedShuffle];
	}

	public static readonly BoardFiller Progressive = new("Progressive", "p", (level, round) => {
		int maxHeight = GetProgressiveTallestColumn(round);

		maxHeight += CommonRNG.NextInclusive(-1, 2);
		maxHeight = int.Clamp(maxHeight, 1, 11);

		// get random number of monsters to use
		// TODO probably needs tweaking
		int at = (int) ((-0.003878 * round * round) + (0.7032 * round) + 1.46);
		int bt = (int) ((-0.002698 * round * round) + (0.9732 * round) + 12.46);

		at = int.Max(at, 5);
		bt = int.Min(bt, 65);

		if (at > bt) {
			(at, bt) = (bt, at);
		}


		int allot = CommonRNG.NextInclusive(at, bt);

		BuildCompleteBoard(level, allot, maxHeight);
	});


	public static readonly BoardFiller SimpleShuffle = new("Simple shuffle", "s", (level, round) => {
		var columns = level.GetColumns();
		CommonRNG.Shuffle(columns);
		PreventImpossibleGames(columns);
		BuildFromColumns(level, columns);
	});

	public static readonly BoardFiller AdvancedShuffle = new("Advanced shuffle", "v", (level, round) => {
		BuildCompleteBoard(level, level.GetEntityCount(), level.GetRowCount());
	});

	public static readonly BoardFiller Easy = new("Easy", "e", (level, round) => {
		int allot = CommonRNG.NextInclusive(20, 30);
		int maxHeight = CommonRNG.NextInclusive(5, 8);
		BuildCompleteBoard(level, allot, maxHeight);
	});

	public static readonly BoardFiller Medium = new("Medium", "m", (level, round) => {
		int allot = CommonRNG.NextInclusive(30, 40);
		int maxHeight = CommonRNG.NextInclusive(7, 9);
		BuildCompleteBoard(level, allot, maxHeight);
	});

	public static readonly BoardFiller Hard = new("Hard", "h", (level, round) => {
		int allot = CommonRNG.NextInclusive(40, 50);
		int maxHeight = CommonRNG.NextInclusive(6, 11);
		BuildCompleteBoard(level, allot, maxHeight);
	});

	public static readonly BoardFiller Expert = new("Expert", "x", (level, round) => {
		int allot = CommonRNG.NextInclusive(55, 65);
		int maxHeight = CommonRNG.NextInclusive(9, 11);
		BuildCompleteBoard(level, allot, maxHeight);
	});

	public static readonly BoardFiller FullFill = new("Full fill", "f", (level, round) => {
		int st = CommonRNG.NextInclusive(8, 9);
		int[] columns = [st, st, st, st, st, st, st];
		BuildFromColumns(level, columns);
	});

	private static void BuildCompleteBoard(Level level, int count, int maxHeight) {
		var columns = BuildRandomBoard(count, maxHeight);

		CommonRNG.Shuffle(columns);
		PreventImpossibleGames(columns);
		BuildFromColumns(level, columns);
	}

	private static int[] BuildRandomBoard(int count, int maxHeight) {
		// fill the board one column at a time
		var columns = new int[7];
		int col = 0;

		maxHeight = int.Max((count + 6) / 7, maxHeight);

		for (int i = 0; i < count; i++) {
			columns[col++]++;
			if (col == 7) {
				col = 0;
			}
		}

		// randomly resize columns
		for (int i = 0; i < 100; i++) {
			int colA = CommonRNG.Next(0, 7);
			int colB = CommonRNG.Next(0, 7);

			if (colA == colB) continue;

			if (columns[colA] >= maxHeight) {
				continue;
			}

			if (columns[colB] == 0) {
				continue;
			}

			columns[colA]++;
			columns[colB]--;
		}

		return columns;
	}

	private static void BuildFromColumns(Level level, int[] columns) {
		for (int c = 0; c < 7; c++) {
			for (int r = 11 - columns[c]; r < 11; r++) {
				level[r, c].EntityType = ObjectType.Monster;
			}
		}
	}

	private static void PreventImpossibleGames(int[] columns) {
		if (columns[3] == 11) {
			int swap;
			if (columns[4] < 10) {
				swap = 4;
			} else if (columns[2] < 10) {
				swap = 2;
			} else {
				int lowest = columns.Min();
				swap = Array.FindIndex(columns, n => n == lowest);
			}
			(columns[3], columns[swap]) = (columns[swap], columns[3]);
		} else if (columns[3] == 10) {
			if (columns[4] >= 10 && columns[2] >= 10) {
				int lowest = columns.Min();
				int swap = Array.FindIndex(columns, n => n == lowest);
				(columns[3], columns[swap]) = (columns[swap], columns[3]);
			}
		}
	}

	// TODO probably needs tweaking, maybe an equation
	private static int GetProgressiveTallestColumn(int round) => round switch {
		< 5 => 1,
		< 10 => 2,
		< 15 => 3,
		< 20 => 4,
		< 30 => 5,
		< 40 => 6,
		< 50 => 7,
		< 60 => 8,
		< 70 => 9,
		< 85 => 10,
		< 95 => 11,
		_ => 11,
	};
}
