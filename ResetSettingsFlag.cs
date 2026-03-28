
namespace WoodsRandomizer;

[Flags]
internal enum ResetSettingsFlag {
	None = 0,
	CopyVanillaROM = 1,
	ReloadVanillaPlayerGraphics = 2,
	RecompressBaseSprites = 4,
	RecreatePreviewImages = 8,
}
