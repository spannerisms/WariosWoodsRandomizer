using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml;

namespace WoodsRandomizer;

internal record CopyInstructions(int SourceTile, int TargetTile, bool Flipped);
internal class CharacterGraphics : IDisposable {

	public const int PlayerSheetSize = 0x2000;
	public const int OverworldSheetSize = 0x0C00;
	public const int CharacterSheetSize = 0x0C00;

	public const string BaseSheetExtension = ".4bpp";
	public const string CharacterPreviewExtension = ".preview.gif";

	public const int TileSize = 32;
	public const int RowSize = 16 * TileSize;
	public string Name { get; init; }

	private readonly byte[] GameGraphics;
	private readonly byte[] OverworldGraphics;
	private readonly byte[] GameOverIcon;

	public static readonly List<CharacterGraphics> AllSheets = [];


	public Bitmap? PreviewImage { get; private set; } = null;

	public List<CharacterPalette> Palettes { get; } = [];

	public bool IsToad { get; init; } = false;

	private CharacterGraphics(string name, WoodsGraphicsStorage container) {
		Name = name;
		GameGraphics = container[0];
		OverworldGraphics = container[1];
		GameOverIcon = container[2];
	}

	public void ApplyToROM(WoodsROM rom) {
		// Player graphics easily fit in vanilla space
		rom.Write8(Addresses.PlayerGraphics, GameGraphics);
		rom.Write8(Addresses.RoundOverworldSprites, OverworldGraphics);

		if (IsToad) return;

		rom.Write8(0x9FCDC0, GameOverIcon.AsSpan(0x00, 0x40));
		rom.Write8(0x9FCFC0, GameOverIcon.AsSpan(0x40, 0x40));

	}

	public override string ToString() => Name;



	public List<LoggedMessage> AddPalettesFromNode(XmlNode node) {
		List<LoggedMessage> ret = new();
		Palettes.Clear();

		// find the palettes
		var palnodes = node.SelectNodes("palettes/skin");

		if (palnodes is null) {
			ret.LogMessage("Unable to find skin nodes.");
			return ret;
		}

		foreach (XmlNode colnode in palnodes) {
			int[] colors = new int[7];
			bool good = true;

			for (int pi = 0; (pi < 7) && good; pi++) {
				var palcol = colnode.Attributes?[$"col{pi+1}"]?.Value ?? "";
				good = int.TryParse(palcol, System.Globalization.NumberStyles.HexNumber, null, out colors[pi]);
			}

			string palname = colnode.Attributes?["name"]?.Value ?? $"{Name} / skin {Palettes.Count}";

			if (good) {
				Palettes.Add(new(palname, colors));
			} else {
				ret.LogMessage($"Skipping bad skin: {palname}");
			}
		}

		return ret;
	}

	/************************************************************************************/

	public static FileInfo[] GetBaseCharacterSheetFiles() {
		return Program.PlayerGraphicsData.GetFiles($"*{BaseSheetExtension}");
	}

	internal static void ClearEntries() {
		var kill = AllSheets.Select(o => o as IDisposable);

		AllSheets.Clear();

		foreach (var dispose in kill) {
			dispose?.Dispose();
		}
	}

	internal static CharacterGraphics CreateEntry(string name, string filename, out string? errors) {
		// get the actual graphics
		WoodsGraphicsStorage guyfile;
		errors = null;

		FileInfo wp = Program.PlayerGraphicsData.GetFile($"{filename}{WoodsGraphicsStorage.GraphicsContainerExtension}");
		guyfile = WoodsGraphicsStorage.GetFromStream(wp);

		// no using or dispose; let the CharacterGraphics handle that
		Bitmap? charPreview = null;

		CharacterGraphics guy;

		guy = new(name, guyfile);

		try {
			FileInfo pi = Program.PlayerGraphicsData.GetFile($"{filename}{CharacterPreviewExtension}");
			using var previewStream =  pi.OpenRead();
			charPreview = new Bitmap(previewStream);

			guy.PreviewImage = charPreview;

		} catch (Exception e) {
			charPreview?.Dispose();
			errors = e.Message;
		}

		AllSheets.Add(guy);
		return guy;
	}




	private static readonly CopyInstructions[] ForGameOverIcon = [
		new(SourceTile: 0xCD, TargetTile: 0x00, Flipped: true),
		new(SourceTile: 0xCD, TargetTile: 0x01, Flipped: false),
		new(SourceTile: 0xDD, TargetTile: 0x02, Flipped: true),
		new(SourceTile: 0xDD, TargetTile: 0x03, Flipped: false),
	];

	private static readonly CopyInstructions[] ForOverworldSheet = [
		new(SourceTile: 0x80, TargetTile: 0x05, Flipped: true), // head 1
		new(SourceTile: 0x81, TargetTile: 0x04, Flipped: true),
		new(SourceTile: 0x90, TargetTile: 0x15, Flipped: true),
		new(SourceTile: 0x91, TargetTile: 0x14, Flipped: true),
		new(SourceTile: 0x92, TargetTile: 0x07, Flipped: true), // legs 1-1
		new(SourceTile: 0x93, TargetTile: 0x06, Flipped: true),
		new(SourceTile: 0x86, TargetTile: 0x09, Flipped: true), // legs 1-2
		new(SourceTile: 0x87, TargetTile: 0x08, Flipped: true),
		new(SourceTile: 0x96, TargetTile: 0x17, Flipped: true), // legs 1-3
		new(SourceTile: 0x97, TargetTile: 0x16, Flipped: true),
		new(SourceTile: 0x80, TargetTile: 0x01, Flipped: true), // head 2
		new(SourceTile: 0x81, TargetTile: 0x00, Flipped: true),
		new(SourceTile: 0x90, TargetTile: 0x11, Flipped: true),
		new(SourceTile: 0x91, TargetTile: 0x10, Flipped: true),
		new(SourceTile: 0xAD, TargetTile: 0x21, Flipped: true), // legs 2
		new(SourceTile: 0xAE, TargetTile: 0x20, Flipped: true),
		new(SourceTile: 0x80, TargetTile: 0x02, Flipped: false), // head 3
		new(SourceTile: 0x81, TargetTile: 0x03, Flipped: false),
		new(SourceTile: 0x90, TargetTile: 0x12, Flipped: false),
		new(SourceTile: 0x91, TargetTile: 0x13, Flipped: false),
		new(SourceTile: 0xAD, TargetTile: 0x22, Flipped: false), // legs 3
		new(SourceTile: 0xAE, TargetTile: 0x23, Flipped: false),
	];

	internal static void CopyOverworldTiles(byte[] sourceSheet, byte[] targetSheet) {
		CopyTiles(sourceSheet, targetSheet, ForOverworldSheet);

		// clear toad's little hand graphic
		ClearTiles(targetSheet, (0x24, 1));
	}

	internal static byte[] CreateGameOverIcon(byte[] sourceSheet) {
		byte[] ret = new byte[32 * 4];

		CopyTiles(sourceSheet, ret, ForGameOverIcon);

		TransformAllTiles(ret, OverworldFill);

		return ret;
	}


	private static void CopyTiles(byte[] sourceSheet, byte[] targetSheet, CopyInstructions[] instructions) {
		foreach (CopyInstructions instr in instructions) {
			int sourceTile = instr.SourceTile * 32;
			int targetTile = instr.TargetTile * 32;

			if (!instr.Flipped) {
				Array.Copy(sourceSheet, sourceTile, targetSheet, targetTile, 32);
				continue;
			}

			for (int i = 0; i < 32; i++) {
				byte b = sourceSheet[sourceTile++];
				targetSheet[targetTile++] = (byte) ((b * 0x0202020202UL & 0x010884422010UL) % 1023); // flip order of bits; found this online
			}
		}
	}


	public static void CreateVanillaPlayerSheets(WoodsROM rom) {
		// create global data
		byte[] BaseCharacterSheet = rom.Decompress(Addresses.PlayerGraphics);
		byte[] OverworldSheet = rom.Decompress(Addresses.RoundOverworldSprites);

		// clear up some stuff for compression optimization
		ClearTiles(BaseCharacterSheet, (0x2A, 1), (0x2E, 2), (0x3E, 2), (0x60, 32));
		ClearTiles(OverworldSheet, (0x0A, 1), (0x19, 1), (0x25, 3), (0x30, 9), (0x46, 10), (0x56, 10));


		using var f1 = Program.BasePlayerSheet.Open(FileMode.Create);
		f1.Write(BaseCharacterSheet);

		using var f2 = Program.BaseOverworldSheet.Open(FileMode.Create);
		f2.Write(OverworldSheet);

		foreach (Character c in Character.ListOf) {
			byte[] charSheet = rom.Decompress(c.GraphicsPointer);
			var spritePlayerData = charSheet.AsSpan(8 * RowSize, 6 * RowSize);

			// clear out bitplane 4 to use first 7 colors (unless toad)
			// also skip the black pixel
			if (c != Character.Toad) {
				TransformAllTiles(spritePlayerData, CharacterClamp);
			}

			CreateCharacterData(c.Name.Replace(' ', '-'), spritePlayerData);
		}
	}

	private static void ClearTiles(byte[] sheet, params (int basetile, int tilecount)[] tiles) {
		foreach (var (baseTile, tileCount) in tiles) {
			sheet.AsSpan(baseTile * TileSize, tileCount * TileSize).Clear();
		}
	}


	private static readonly Color PreviewNullColor = Color.FromArgb(200, 200, 200);

	internal static void CreateCharacterPreview(string filename, FileInfo file) {
		CreateCharacterPreview(filename, file.GetAsArray());
	}


	internal static void CreateCharacterPreview(string filename, ReadOnlySpan<byte> data) {
		const int width = 8 * 16;
		const int height = 6 * 8;

		// create an array and pin it for fast drawing
		byte[] pixels = new byte[width * height]; // pixels to edit
		GCHandle arrp = GCHandle.Alloc(pixels, GCHandleType.Pinned);
		nint ptr = Marshal.UnsafeAddrOfPinnedArrayElement(pixels, 0);

		using Bitmap export = new Bitmap(width, height, width, PixelFormat.Format8bppIndexed, ptr);

		for (int i = 0; i < width * height; i++) {
			pixels[i] = 0;
		}

		int picrow = 0;
		int piccol = 0;

		for (int i = 0; i < data.Length; i += 32) {
			var g = GetIndexedTile(data.Slice(i, 32));
			for (int rr = 0; rr < 8; rr++) {
				for (int cc = 0; cc < 8; cc++) {
					pixels[(width * (rr + picrow)) + (piccol + cc)] = g[rr, cc];
				}
			}
			piccol += 8;
			if (piccol == width) {
				piccol = 0;
				picrow += 8;
			}

		}

		ColorPalette cp = export.Palette;
		for (int i = 0; i < 256; i++) {
			cp.Entries[i] = PreviewNullColor;
		}

		for (int i = 1, ci = 255; i < 8; i++, ci -= 20) {
			cp.Entries[i] = Color.FromArgb(ci, ci, ci);
		}

		cp.Entries[15] = Color.FromArgb(33, 33, 33);
		export.Palette = cp;

		using FileStream wd40 = Program.PlayerGraphicsData.GetFile($"{filename}{CharacterPreviewExtension}").Open(FileMode.Create);

		export.Save(wd40, ImageFormat.Gif);

		arrp.Free();
	}

	internal static void CreateCharacterData(string filename, ReadOnlySpan<byte> data) {
		using FileStream w = Program.PlayerGraphicsData.GetFile($"{filename}{BaseSheetExtension}").Open(FileMode.Create);
		w.Write(data);
	}

	private static byte[,] GetIndexedTile(ReadOnlySpan<byte> tile) {
		byte[,] modTiles = new byte[8,8];

		// decode tile
		int bat = 0;
		foreach (var (row, plane) in snesplaner) {
			byte pixel = tile[bat++];

			if ((pixel & 0b10000000) is not 0) modTiles[row, 0] |= plane;
			if ((pixel & 0b01000000) is not 0) modTiles[row, 1] |= plane;
			if ((pixel & 0b00100000) is not 0) modTiles[row, 2] |= plane;
			if ((pixel & 0b00010000) is not 0) modTiles[row, 3] |= plane;
			if ((pixel & 0b00001000) is not 0) modTiles[row, 4] |= plane;
			if ((pixel & 0b00000100) is not 0) modTiles[row, 5] |= plane;
			if ((pixel & 0b00000010) is not 0) modTiles[row, 6] |= plane;
			if ((pixel & 0b00000001) is not 0) modTiles[row, 7] |= plane;
		}

		return modTiles;
	}

	private delegate void PixelTransform(ref byte val);

	internal static void OverworldFill(ref byte val) {
		if (val is 0) {
			val = 15;
		}
	}

	internal static void CharacterClamp(ref byte val) {
		if (val is > 7 and < 15) {
			val -= 7;
		}
	}

	private static readonly (int row, byte plane)[] snesplaner = [
		(0, 0b0001), (0, 0b0010), (1, 0b0001), (1, 0b0010), (2, 0b0001), (2, 0b0010), (3, 0b0001), (3, 0b0010),
		(4, 0b0001), (4, 0b0010), (5, 0b0001), (5, 0b0010), (6, 0b0001), (6, 0b0010), (7, 0b0001), (7, 0b0010),
		(0, 0b0100), (0, 0b1000), (1, 0b0100), (1, 0b1000), (2, 0b0100), (2, 0b1000), (3, 0b0100), (3, 0b1000),
		(4, 0b0100), (4, 0b1000), (5, 0b0100), (5, 0b1000), (6, 0b0100), (6, 0b1000), (7, 0b0100), (7, 0b1000),
	];

	private static void TransformTileColors(Span<byte> tile, PixelTransform func) {
		byte[,] modTiles = new byte[8,8];

		// decode tile
		int bat = 0;
		foreach (var (row, plane) in snesplaner) {
			byte pixel = tile[bat++];

			if ((pixel & 0b10000000) is not 0) modTiles[row, 0] |= plane;
			if ((pixel & 0b01000000) is not 0) modTiles[row, 1] |= plane;
			if ((pixel & 0b00100000) is not 0) modTiles[row, 2] |= plane;
			if ((pixel & 0b00010000) is not 0) modTiles[row, 3] |= plane;
			if ((pixel & 0b00001000) is not 0) modTiles[row, 4] |= plane;
			if ((pixel & 0b00000100) is not 0) modTiles[row, 5] |= plane;
			if ((pixel & 0b00000010) is not 0) modTiles[row, 6] |= plane;
			if ((pixel & 0b00000001) is not 0) modTiles[row, 7] |= plane;
		}

		// apply transformation
		for (int rp = 0; rp < 8; rp++) {
			for (int cp = 0; cp < 8; cp++) {
				func(ref modTiles[rp, cp]);
			}
		}

		// convert back
		int cb = 0;
		foreach (var (row, plane) in snesplaner) {
			ref byte b = ref tile[cb++];
			b = 0;
			if ((modTiles[row, 0] & plane) is not 0) b |= 0b10000000;
			if ((modTiles[row, 1] & plane) is not 0) b |= 0b01000000;
			if ((modTiles[row, 2] & plane) is not 0) b |= 0b00100000;
			if ((modTiles[row, 3] & plane) is not 0) b |= 0b00010000;
			if ((modTiles[row, 4] & plane) is not 0) b |= 0b00001000;
			if ((modTiles[row, 5] & plane) is not 0) b |= 0b00000100;
			if ((modTiles[row, 6] & plane) is not 0) b |= 0b00000010;
			if ((modTiles[row, 7] & plane) is not 0) b |= 0b00000001;
		}
	}

	private static void TransformAllTiles(Span<byte> sheet, PixelTransform transform) {
		for (int i = 0; i < sheet.Length; i += 32) {
			TransformTileColors(sheet.Slice(i, 32), transform);
		}
	}

	private bool disposedValue;
	protected virtual void Dispose(bool disposing) {
		if (!disposedValue) {
			if (disposing) {
				PreviewImage?.Dispose();
			}

			disposedValue = true;
		}
	}

	void IDisposable.Dispose() {
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
