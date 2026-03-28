using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WoodsRandomizer;


// subclasses
internal record LoggedMessage(string Message) {
	public DateTime Time { get; } = DateTime.Now;

	public string LongString => $"{Time:HH:mm:ss:ff} | {Message}";
}

internal static partial class Utility {
	/// <summary>
	/// Character that indicates an option runs a function.
	/// </summary>
	public const string SPLAT = "\u2756";

	public static readonly Random CommonRNG = new();


	public static LoggedMessage LogMessage(this List<LoggedMessage> list, string message) {
		LoggedMessage add = new(message);
		list.Add(add);
		return add;
	}




	public static T GetRandomElement<T>(this IList<T> a) {
		return a[CommonRNG.Next(0, a.Count)];
	}

	public static T GetRandomElementWhere<T>(this IList<T> a, Func<T, bool> predicate) {
		return a.Where(predicate).ElementAt(CommonRNG.Next(0, a.Count));
	}

	internal static readonly OpenFileDialog OpenROM = new() {
		Filter = "SNES ROM file|*.sfc; *.smc",
		Title = "Select a ROM file",
	};

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int SNESToPC(int a) {
		return a & 0x7FFF | (a & 0x7F0000) >> 1;
	}

	public static FileInfo GetFile(this DirectoryInfo directory, string filename) {
		return new FileInfo(Path.Join(directory.FullName, filename));
	}

	public static byte[] GetAsArray(this FileInfo file) {
		using FileStream f = file.OpenRead();
		byte[] ret = f.GetAsArray();
		f.Dispose();
		return ret;
	}

	public static byte[] GetAsArray(this Stream stream) {
		byte[] ret = new byte[stream.Length];
		stream.Read(ret);
		return ret;
	}

	public static string SimplePluralS(int count) {
		if (count == 1) return "";
		return "s";
	}

	public static byte NextByte(this Random r, int min, int max) {
		return (byte) r.Next(min, max);
	}

	//[GeneratedRegex(@"(?<=^)[A-F\d]{6}$", RegexOptions.Compiled)]
	//internal static partial Regex HexColorMatchRegex();

	internal static bool IsExplicitColor(string? value, out int color) {
		if (value is null || value.Length is not 6) {
			color = -1;
			return false;
		}

		return int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out color);

		//Match m = HexColorMatchRegex().Match(value);
		//
		//if (m.Success) {
		//	return int.TryParse(m.Value, System.Globalization.NumberStyles.HexNumber, null, out color);
		//}

		//color = -1;
		//return false;
	}





	public static object FindMatchingSetting(this IEnumerable<object> list, string setting, object defolt) {
		return list.FirstOrDefault(o => o.ToString() == setting, defolt);
	}
}
