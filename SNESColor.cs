using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WoodsRandomizer;

[DebuggerDisplay("value: {(ushort) this,h}")]
[StructLayout(LayoutKind.Explicit, Size = 2)]
public readonly struct SNESColor {
	// _internalshort only exists to give an easy comparison between colors
	// it is not to be used for arbitrary purposes, as it will be dependent
	// on the endianness of the architecture
	[FieldOffset(0)] private readonly ushort _internalshort;
	[FieldOffset(0)] private readonly byte low;
	[FieldOffset(1)] private readonly byte high;

	public SNESColor(int rgb) {
		int r = rgb & 0xF80000;
		int g = rgb & 0x00F800;
		int b = rgb & 0x0000F8;
		low = (byte) ((r >> 19) | (g >> 6));
		high = (byte) ((g >> 14) | (b >> 1));
	}

	public SNESColor(int r, int g, int b) {
		r &= 0xF8;
		g &= 0xF8;
		b &= 0xF8;
		low = (byte) ((r >> 3) | (g >> 1));
		high = (byte) ((g >> 6) | (b >> 1));
	}

	public SNESColor(ushort color) {
		low = (byte) color;
		high = (byte) (color >> 8);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Get8BitColorComponent(int val) => (val >> 2) | (val << 3);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int Get8BitColorComponent(byte val) => (val >> 2) | (val << 3);


	public Color ToColor() {
		int r = Get8BitColorComponent(low & 0x1F);
		int g = Get8BitColorComponent(((low >> 5) | (high << 3))  & 0x1F);
		int b = Get8BitColorComponent((high >> 2) & 0x1F);

		return Color.FromArgb(r,g,b);
	}

	public static int GetRGBFromSNES(int color) {
		int r = Get8BitColorComponent(color & 0x1F);
		int g = Get8BitColorComponent((color >> 5) & 0x1F);
		int b = Get8BitColorComponent((color >> 10) & 0x1F);

		return (r << 16) | (g << 8) | b;
	}

	public int ToRGB() {
		int r = Get8BitColorComponent(low & 0x1F);
		int g = Get8BitColorComponent(((low >> 5) | (high << 3))  & 0x1F);
		int b = Get8BitColorComponent((high >> 2) & 0x1F);

		return (r << 16) | (g << 8) | b;
	}

	public static int ClampRBG(int rgb) {
		int r = Get8BitColorComponent((rgb >> 27) & 0x1F);
		int g = Get8BitColorComponent((rgb >> 19) & 0x1F);
		int b = Get8BitColorComponent((rgb >> 3) & 0x1F);

		return (r << 16) | (g << 8) | b;
	}

	// casting
	public static implicit operator SNESColor(int a) => new(a);
	public static implicit operator int(SNESColor a) => a.low | (a.high << 8);

	public static implicit operator SNESColor(ushort a) => new(a);
	public static implicit operator ushort(SNESColor a) => (ushort) (a.low | (a.high << 8));


	// Equality checks
	public static bool operator ==(SNESColor a, SNESColor b) => a._internalshort == b._internalshort;
	public static bool operator !=(SNESColor a, SNESColor b) => a._internalshort != b._internalshort;
	public static bool operator ==(SNESColor a, int b) => (a.low | a.high << 8) == b;
	public static bool operator !=(SNESColor a, int b) => (a.low | a.high << 8) != b;
	public static bool operator ==(int a, SNESColor b) => a == (b.low | b.high << 8);
	public static bool operator !=(int a, SNESColor b) => a != (b.low | b.high << 8);


	/// <inheritdoc cref="object.Equals(object?)"/>
	public override bool Equals(object? obj) => obj switch {
		SNESColor a => this == a,
		int a => this == a,
		_ => false
	};

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() {
		return low | (high << 8);
	}
}
