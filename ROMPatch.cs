namespace WoodsRandomizer;

internal class ROMPatch {
	// Patches
	public static readonly ROMPatch BasePatches = new("basepatches");
	public static readonly ROMPatch Optimizations = new("optimizations");
	public static readonly ROMPatch DirectDeposit = new("directdeposit");
	public static readonly ROMPatch Endurance = new("endurance");

	// Class
	private readonly byte[] payload;

	public bool Valid { get; } = true;

	private static readonly byte[] NullIPS = [.. "PATCHEOF"u8];

	internal ROMPatch(string name) {
		using var v = Program.GetResourceStream($"Patches/IPS/{name}.ips");

		if (v is not null) {
			payload = new byte[v.Length];
			v.Read(payload, 0, payload.Length);

			if (!ValidateIPS(payload)) {
				payload = NullIPS;
			}
		} else {
			payload = NullIPS;
		}

		Valid = payload != NullIPS;
	}

	public void ApplyTo(WoodsROM rom) {
		int read = 5;
		while (true) {
			byte a = payload[read++];
			byte b = payload[read++];
			byte c = payload[read++];

			if (a == 'E' && b == 'O' && c == 'F') break;
			int offset = c | (b << 8) | (a << 16);

			int size = (payload[read++] << 8) | payload[read++];

			if (size == 0) { // for RLE
				size = (payload[read++] << 8) | payload[read++];
				rom.Stream.AsSpan(offset, size).Fill(payload[read++]);
				continue;
			}

			Array.Copy(payload, read, rom.Stream, offset, size);

			read += size;
		}
	}

	public static bool ValidateIPS(byte[] patch) {
		ArraySegment<byte> t = new(patch, 0, 5);
		string header = new([.. t.Select(o => (char) o)]);

		if (header is not "PATCH") return false;

		int read = 5;
		int end = patch.Length;
		while (true) {
			if ((read + 3) > end) {
				return false;
			}

			byte a = patch[read++];
			byte b = patch[read++];
			byte c = patch[read++];

			if (a == 'E' && b == 'O' && c == 'F') {
				break;
			};

			if ((read + 2) > end) {
				return false;
			}

			int offset = c | (b << 8) | (a << 16);

			int size = (patch[read++] << 8) | patch[read++];

			if (size == 0) { // for RLE
				if ((read + 2) > end) {
					return false;
				}
				continue;
			}
			read += size;
		}

		return true;
	}
}
