using System.Runtime.CompilerServices;

namespace WoodsRandomizer;

internal static class Randomization {
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int NextInclusive(this Random rng, int min, int max) {
		return rng.Next(min, max + 1);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte NextByte(this Random r, int min, int max) {
		return (byte) r.Next(min, max);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte NextByteInclusive(this Random r, int min, int max) {
		return (byte) r.Next(min, max + 1);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandomElement<T>(this Random r, IList<T> a) {
		return a[r.Next(0, a.Count)];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandomElementWhere<T>(this Random r, IList<T> a, Func<T, bool> predicate) {
		return a.Where(predicate).ElementAt(r.Next(0, a.Count));
	}
}
