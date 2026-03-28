namespace WoodsRandomizer;

internal class WoodsGraphicsStorage {
	public const string GraphicsContainerExtension = ".wgs";

	private const string Signature = "WOODSGFX";

	private const int HeaderSize = 32;
	private const int EntrySize = 8;

	public byte[] this[int i] => ItemList[i];

	public int ItemCount => ItemList.Count;

	// format:
	// 32 byte header
	// "WOODSGFX" - 8 bytes
	// <size>     - 4 byte LE int, number of items
	//            - reserved
	// 
	// payload:
	// <loc>, <size>  4 byte LE ints, pointer and size for each item
	// <size> - 4 byte LE size of name
	// <name> - null terminated


	private readonly List<byte[]> ItemList = [];
	public WoodsGraphicsStorage() {

	}

	public void Add(byte[] item) {
		ItemList.Add(item);
	}

	public void AddMany(params byte[][] items) {
		ItemList.AddRange(items);
	}

	public void AddRange(IEnumerable<byte[]> items) {
		ItemList.AddRange(items);
	}

	public static WoodsGraphicsStorage GetFromStream(FileInfo f) {
		using var s = f.OpenRead();

		try {
			return GetFromStream(s);
		} catch {
			s.Dispose();
			throw;
		}
	}


	public static WoodsGraphicsStorage GetFromStream(Stream s) {
		if (s.Length < HeaderSize) {
			throw new FileFormatException("Too short to be a storage file");
		}

		byte[] signatureBytes = new byte[8];
		s.Read(signatureBytes);
		char[] nameCheck = Encoding.UTF8.GetChars(signatureBytes);

		if (new string(nameCheck) != Signature) {
			throw new FileFormatException("Invalid header");
		};

		int count = ReadInt();

		WoodsGraphicsStorage ret = new();

		for (int i = 0; i < count; i++) {
			s.Position = HeaderSize + (EntrySize * i);
			int pos = ReadInt();
			int size = ReadInt();
			byte[] item = new byte[size];
			s.Position = pos;
			int readCount = s.Read(item);

			if (readCount != size) {
				throw new FileFormatException("Container does not contain entire object");
			}

			ret.ItemList.Add(item);
		}

		return ret;

		int ReadInt() {
			if ((s.Position + 4) > s.Length) {
				throw new FileFormatException("File too short");
			}

			int a0 = s.ReadByte();
			int a1 = s.ReadByte();
			int a2 = s.ReadByte();
			int a3 = s.ReadByte();
			return a0 | (a1 << 8) | (a2 << 16) | (a3 << 24);
		}
	}



	public void WriteToStream(Stream s) {
		s.SetLength(0);

		s.Write("WOODSGFX"u8);

		WriteLE(ItemList.Count);

		s.Position = HeaderSize;
		int dataOffset = (int) s.Position + (ItemList.Count * EntrySize);

		// write item information
		foreach (byte[] item in ItemList) {
			WriteLE(dataOffset);
			WriteLE(item.Length);
			dataOffset += item.Length;
		}


		// write data
		foreach (byte[] item in ItemList) {
			s.Write(item);
		}

		void WriteLE(int i) {
			s.WriteByte((byte) i);
			s.WriteByte((byte) (i >> 8));
			s.WriteByte((byte) (i >> 16));
			s.WriteByte((byte) (i >> 24));
		}

	}
}
