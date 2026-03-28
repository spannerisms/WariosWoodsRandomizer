namespace WoodsRandomizer;

internal class DifficultySelect {
	public string Name { get; }
	public string Token { get; }
	private readonly Action<Round> Setter;
	private DifficultySelect(string name, string token, Action<Round> setter) {
		Name = name;
		Setter = setter;
		Token = $"D{token}";
	}

	public override string ToString() => Name;

	public void ApplyTo(Round round) => Setter(round);

	public static ImmutableArray<DifficultySelect> ListOf { get; }
	static DifficultySelect() {
		ListOf = [Progressive, Easy, Medium, Hard, Expert, Chaotic];
	}

	public static readonly DifficultySelect Progressive = new("Progressive", "p", round => {
		round.Aggro = (byte) (0x0F - (int.Max(round.RoundNumber, 99) / 8));
		round.Gold = (byte) int.Max(5, (((round.RoundNumber / 10) + 1) * 5) + CommonRNG.Next(-5,5));
		round.Speed = (byte) int.Min((int) (CommonRNG.Next(0x00, 0x10) + (round.RoundNumber * 2.3F)), 0xF8);
		round.CoinsLost = (byte) (round.Gold / 4);
	});

	public static readonly DifficultySelect Easy = new("Easy", "e", round => {
		round.Aggro = (byte) CommonRNG.Next(0x0B, 0x0F);
		round.Gold = (byte) int.Max(5, (((round.RoundNumber / 10) + 1) * 5) + CommonRNG.Next(-5,5));
		round.Speed = (byte) CommonRNG.Next(0x22, 0x44);
		round.CoinsLost = (byte) (round.Gold / 6);
	});

	public static readonly DifficultySelect Medium = new("Medium", "m", round => {
		round.Aggro = (byte) CommonRNG.Next(0x07, 0x0C);
		round.Gold = (byte) int.Max(5, (((round.RoundNumber / 8) + 1) * 5) + CommonRNG.Next(-5,5));
		round.Speed = (byte) CommonRNG.Next(0x44, 0x99);
		round.CoinsLost = (byte) (round.Gold / 5);
	});

	public static readonly DifficultySelect Hard = new("Hard", "h", round => {
		round.Aggro = (byte) CommonRNG.Next(0x04, 0x08);
		round.Gold = (byte) CommonRNG.Next(20, 30);
		round.Speed = (byte) CommonRNG.Next(0xC0, 0xF0);
		round.CoinsLost = (byte) CommonRNG.Next(6, 9);
	});

	public static readonly DifficultySelect Expert = new("Expert", "x", round => {
		round.Aggro = (byte) CommonRNG.Next(0x03, 0x05);
		round.Gold = (byte) CommonRNG.Next(20, 40);
		round.Speed = (byte) CommonRNG.Next(0xF8, 0xFF);
		round.CoinsLost = (byte) CommonRNG.Next(7, 10);
	});

	public static readonly DifficultySelect Chaotic = new("Chaotic", "c", round => {
		round.Aggro = (byte) CommonRNG.Next(0x02, 0x0F);
		round.Gold = (byte) CommonRNG.Next(0, 99);
		round.Speed = (byte) CommonRNG.Next(0x33, 0xFF);
		round.CoinsLost = (byte) CommonRNG.Next(0, 10);
	});

}