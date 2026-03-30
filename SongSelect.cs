using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WoodsRandomizer;

internal class SongSelect {
	public string Name { get; }
	private readonly ushort Value;

	public Func<ushort> GetValue { get; }

	private SongSelect(ushort value, string name) {
		Name = name;
		Value = value;
		GetValue = () => value;
	}

	private SongSelect(ushort value, string name, Func<ushort> func) {
		Name = name;
		Value = value;
		GetValue = func;
	}

	public override string ToString() => Name;

	public static readonly ImmutableArray<SongSelect> ListOf;

	static SongSelect() {
		ListOf = [ RoundGame, WarioTime, TimeRace, VSCOM, Lesson, MainMenu, GameOver, Credits ];
	}

	public static readonly SongSelect RoundGame = new(0x0001, "Round game");
	public static readonly SongSelect WarioTime = new(0x0002, "Wario time");
	public static readonly SongSelect TimeRace  = new(0x0005, "Time race");
	public static readonly SongSelect VSCOM     = new(0x0006, "VS COM");
	public static readonly SongSelect Lesson    = new(0x0012, "Lesson");
	public static readonly SongSelect MainMenu  = new(0x0014, "Main menu");
	public static readonly SongSelect GameOver  = new(0x0019, "Game over");
	public static readonly SongSelect Credits   = new(0x0017, "Credits");

	public static readonly SongSelect Muted     = new(0x0030, "Muted");
	public static readonly SongSelect Random    = new(0x0000, $"{SPLAT}Random", () => CommonRNG.GetRandomElement(ListOf).Value);

}
