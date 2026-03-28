global using System.Collections.Immutable;
global using System.Text;
global using WariosWoods;
global using static WoodsRandomizer.Utility;
global using Addresses = WariosWoods.Addresses;
using System.IO.IsolatedStorage;
using System.Reflection;

namespace WoodsRandomizer;

// dotnet publish -p:PublishProfile=Properties/PublishProfiles/FolderProfile.pubxml

internal static class Program {
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main() {
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();

		EnsureExistence(LocalData);
		EnsureExistence(BaseGraphicsData);
		EnsureExistence(PlayerGraphicsData);

		Application.Run(new WoodsRandoForm());
	}

	public static readonly string AppPath = Application.StartupPath;

	internal static readonly IsolatedStorageFile Storage = IsolatedStorageFile.GetUserStoreForAssembly();
	internal static readonly DirectoryInfo LocalData;
	internal static readonly DirectoryInfo BaseGraphicsData;
	internal static readonly DirectoryInfo PlayerGraphicsData;
	internal static readonly FileInfo BasePlayerSheet;
	internal static readonly FileInfo BaseOverworldSheet;
	internal static readonly FileInfo PalettesXML;
	internal static readonly FileInfo SpritesXML;

	static Program() {
		LocalData = new(Path.Join(AppPath, "data"));
		BaseGraphicsData = new(Path.Join(LocalData.FullName, "base"));
		PlayerGraphicsData = new(Path.Join(LocalData.FullName, "player"));
		PlayerGraphicsData = new(Path.Join(LocalData.FullName, "player"));

		BasePlayerSheet = new(Path.Join(BaseGraphicsData.FullName, "player.4bpp"));
		BaseOverworldSheet = new(Path.Join(BaseGraphicsData.FullName, "overworld.4bpp"));

		PalettesXML = LocalData.GetFile("palettes.xml");
		SpritesXML = LocalData.GetFile("sprites.xml");
	}


	private static readonly Assembly ass = typeof(Program).Assembly;

	public static readonly string Version = ass.GetName()?.Version?.ToString(3) ?? "?????";

	private static void EnsureExistence(DirectoryInfo dir) {
		if (!dir.Exists) {
			dir.Create();
		}
	}


	public static Stream? GetResourceStream(string name) {
		name = name.Replace('/', '.');
		return ass.GetManifestResourceStream($"WoodsRandomizer.{name}");
	}
}