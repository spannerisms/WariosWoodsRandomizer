using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WoodsRandomizer;
public class CharacterPalette {
	public string Name { get; }

	private readonly SNESColor[] data = new SNESColor[7];

	public CharacterPalette(string name, int[] colors) {
		Name = name;
		data = colors.Select(c => new SNESColor(c)).ToArray();
	}

	public override string ToString() => Name;

	public void ApplyToROM(WoodsROM rom) {
		data.CopyTo(GetOne(Addresses.PlayerPalette));
		data.CopyTo(GetOne(Addresses.RoundOverworldPlayerPalette));
		data.CopyTo(GetOne(0x84870B));
		data.CopyTo(GetOne(0x84A4BB));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		Span<SNESColor> GetOne(int address) => MemoryMarshal.Cast<byte, SNESColor>(rom.AsSpan(address, 14));
	}

	public void ApplyToImage(Image image) {
		ColorPalette ppal = image.Palette;

		if (ppal.Entries.Length < 15) return;

		for (int i = 0; i < 7; i++) {
			ppal.Entries[i + 1] = data[i].ToColor();
		}

		image.Palette = ppal;
	}


	public static readonly CharacterPalette GrayscaleDefault = new("Grayscale", [0xFFFFFF, 0xCCCCCC, 0xAAAAAA, 0x888888, 0x777777, 0x999999]);
}
