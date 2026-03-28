using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace WoodsRandomizer;

internal static class ThemesHandler {
	public static readonly List<string> PaletteStatusLog = [];

	public static List<BoardTheme> NamedThemes { get; } = [];

	internal static List<MonsterPalette> NamedPalettes { get; } = [];
	internal static List<ShadedColor> NamedColors { get; } = [];


	static ThemesHandler() {
		ReadThemesXML();
	}


	internal static StringBuilder CreateXMLForTheme(BoardTheme theme) {
		StringBuilder ret = new();

		ret.AppendLine("\t\t<theme name=\"Untitled\">");

		CreatePaletteNode("red", theme.RedPalette);
		CreatePaletteNode("yellow", theme.YellowPalette);
		CreatePaletteNode("green", theme.GreenPalette);
		CreatePaletteNode("blue", theme.BluePalette);
		CreatePaletteNode("pink", theme.PinkPalette);
		CreatePaletteNode("azure", theme.AzurePalette);
		CreatePaletteNode("black", theme.BlackPalette);
		CreatePaletteNode("white", theme.WhitePalette);

		ret.AppendLine("\t\t</theme>");

		return ret;

		void CreatePaletteNode(string colorname, MonsterPalette palette) {
			foreach (MonsterPalette otherPalette in NamedPalettes) {
				if (otherPalette.Equals(palette)) {
					ret.AppendLine($"\t\t\t<{colorname} type=\"namedpalette\" palettename=\"{otherPalette.Name}\" />");
					return;
				}
			}

			ret.AppendLine($"\t\t\t<{colorname} type=\"childpalette\" />");
			CreateColorPartNode("body", palette.Body);
			CreateColorPartNode("gloss", palette.Gloss);
			CreateColorPartNode("blush", palette.Blush);

			if (palette.SubOutline.LightData != ShadedColor.VanillaSuboutline.LightData) {
				ret.AppendLine($"\t\t\t\t<suboutline type=\"inlinecolor\" colorname=\"{palette.SubOutline.LightData.ToRGB():X6}\" />");
			}

			ret.AppendLine($"\t\t\t</{colorname}>");


			void CreateColorPartNode(string partname, ShadedColor color) {
				foreach (ShadedColor otherColor in NamedColors) {
					if (otherColor.Equals(color)) {
						ret.AppendLine($"\t\t\t\t<{partname} type=\"namedcolor\" colorname=\"{otherColor.Name}\" />");
						return;
					}
				}

				ret.AppendLine($"\t\t\t\t<{partname} type=\"inlinecolor\" light=\"#{color.LightData.ToRGB():X6}\" dark=\"#{color.DarkData.ToRGB():X6}\" />");
			}
		}
	}

	internal static void ReadThemesXML() {
		DateTime startPaletteParse = DateTime.Now;

		NamedThemes.Clear();
		NamedThemes.Add(BoardTheme.Vanilla);

		// look for the file
		using var palFile = Program.PalettesXML.OpenRead();

		var palXml = new XmlDocument();

		// make sure it's valid XML
		if (palFile is not null) {
			try {
				palXml.Load(palFile);
			} catch (XmlException e) {
				PaletteStatusLog.Add(e.Message);
			}
		} else {
			PaletteStatusLog.Add("The palettes.xml settings file could not be found!");
		}

		if (PaletteStatusLog.Count > 0) {
			return;
		}

		// Add vanilla colors
		NamedColors.Add(ShadedColor.VanillaMonsterBlush);
		NamedColors.Add(ShadedColor.VanillaRedBody);
		NamedColors.Add(ShadedColor.VanillaRedGloss);
		NamedColors.Add(ShadedColor.VanillaGreenBody);
		NamedColors.Add(ShadedColor.VanillaGreenGloss);
		NamedColors.Add(ShadedColor.VanillaBlueBody);
		NamedColors.Add(ShadedColor.VanillaBlueGloss);
		NamedColors.Add(ShadedColor.VanillaYellowBody);
		NamedColors.Add(ShadedColor.VanillaYellowGloss);
		NamedColors.Add(ShadedColor.VanillaWhiteBody);
		NamedColors.Add(ShadedColor.VanillaWhiteGloss);
		NamedColors.Add(ShadedColor.VanillaBlackBody);
		NamedColors.Add(ShadedColor.VanillaBlackGloss);
		NamedColors.Add(ShadedColor.VanillaPinkBody);
		NamedColors.Add(ShadedColor.VanillaPinkGloss);
		NamedColors.Add(ShadedColor.VanillaAzureBody);
		NamedColors.Add(ShadedColor.VanillaAzureGloss);

		// Add vanilla palettes
		NamedPalettes.Add(MonsterPalette.VanillaRed);
		NamedPalettes.Add(MonsterPalette.VanillaYellow);
		NamedPalettes.Add(MonsterPalette.VanillaGreen);
		NamedPalettes.Add(MonsterPalette.VanillaBlue);
		NamedPalettes.Add(MonsterPalette.VanillaPink);
		NamedPalettes.Add(MonsterPalette.VanillaAzure);
		NamedPalettes.Add(MonsterPalette.VanillaBlack);
		NamedPalettes.Add(MonsterPalette.VanillaWhite);

		// Add the vanilla theme

		// first parse out every color
		var colorNodes = palXml.SelectNodes("//data/colors/color");
		if (colorNodes is null) {
			PaletteStatusLog.Add("palettes.xml does not contain a colors node with relevant children!");
		} else {
			foreach (XmlNode colorNode in colorNodes) {
				if (colorNode.NodeType is XmlNodeType.Element) {
					var colorToAdd = ParseNamedColorNode(colorNode);

					if (colorToAdd is null) {
						PaletteStatusLog.Add("Skipping malformatted color");
						continue;
					}

					if (NamedColors.Any(otherColor => otherColor.Name == colorToAdd.Name)) {
						PaletteStatusLog.Add($"Skipping duplicate named color: {colorToAdd.Name}");
						continue;
					}

					NamedColors.Add(colorToAdd);
				}
			}
		}

		// now parse out the palettes
		var paletteNodes = palXml.SelectNodes("//data/palettes/palette");

		if (paletteNodes is null) {
			PaletteStatusLog.Add("palettes.xml does not contain a palettes node with relevant children!");
		} else {
			foreach (XmlNode palNode in paletteNodes) {
				if (palNode.NodeType is XmlNodeType.Element) {
					var paletteToAdd = ParseNamedPaletteNode(palNode);

					if (paletteToAdd is null) {
						PaletteStatusLog.Add("Skipping malformatted palette");
						continue;
					}

					if (NamedPalettes.Any(otherPalette => otherPalette.Name == paletteToAdd.Name)) {
						PaletteStatusLog.Add($"Skipping duplicate named palette: {paletteToAdd.Name}");
						continue;
					}

					NamedPalettes.Add(paletteToAdd);
				}
			}
		}

		// create every palette set
		var setNodes = palXml.SelectNodes("//data/themes/theme");

		if (setNodes is null) {
			PaletteStatusLog.Add("palettes.xml does not contain a themes node with relevant children!");
		} else {
			foreach (XmlNode setNode in setNodes) {
				if (setNode.NodeType is XmlNodeType.Element) {
					var thisTheme = ParseThemeNode(setNode);

					if (thisTheme is null) {
						var name = setNode.Attributes!["name"]?.Value;

						if (name is null) {
							PaletteStatusLog.Add($"Skipping malformatted theme");
						} else {
							PaletteStatusLog.Add($"Skipping malformatted theme: {name}");
						}

						continue;
					}


					if (NamedThemes.Any(o => o.Name == thisTheme.Name)) {
						PaletteStatusLog.Add($"Skipping duplicate named theme: {thisTheme.Name}");
						continue;
					}

					NamedThemes.Add(thisTheme);
				}
			}
		}

		TimeSpan diff = DateTime.Now - startPaletteParse;
		PaletteStatusLog.Add($"Palette reading completed in {diff.TotalSeconds:F3}s");


		/* *******************************************
		 *********************************************
		 *** local methods
		 *********************************************
		 ******************************************* */
		static BoardTheme? ParseThemeNode(XmlNode themenode) {
			bool good = true;

			var redNode = TryToGetNode("red");
			var yellowNode = TryToGetNode("yellow");
			var greenNode = TryToGetNode("green");
			var blueNode = TryToGetNode("blue");
			var pinkNode = TryToGetNode("pink");
			var azureNode = TryToGetNode("azure");
			var blackNode = TryToGetNode("black");
			var whiteNode = TryToGetNode("white");

			if (!good) {
				return null;
			}

			MonsterPalette? red = TryToParseNode(redNode!);
			MonsterPalette? yellow = TryToParseNode(yellowNode!);
			MonsterPalette? green = TryToParseNode(greenNode!);
			MonsterPalette? blue = TryToParseNode(blueNode!);
			MonsterPalette? pink = TryToParseNode(pinkNode!);
			MonsterPalette? azure = TryToParseNode(azureNode!);
			MonsterPalette? black = TryToParseNode(blackNode!);
			MonsterPalette? white = TryToParseNode(whiteNode!);

			if (!good) {
				return null;
			}

			if (themenode.Attributes!["name"]?.Value is not string name) {
				PaletteStatusLog.Add($"Theme is missing a name");
				name = $"<badname>{DateTime.Now.Ticks & 0xFFFFFF:X6}";
			}

			return new(red!, yellow!, green!, blue!, pink!, azure!, black!, white!) {
				Name = name
			};

			XmlNode? TryToGetNode(string nodeName) {
				var nodeGuy = themenode.SelectSingleNode(nodeName);
				if (nodeGuy is null) {
					PaletteStatusLog.Add($"Unable to find {nodeName} in palette node");
					good = false;
				}
				return nodeGuy;
			}

			MonsterPalette? TryToParseNode(XmlNode theNode) {
				var retPal = ParsePossiblyNamedPaletteNode(theNode);

				if (retPal is null) {
					good = false;
					PaletteStatusLog.Add($"Unable to parse <{theNode.Name}> in theme");
				}

				return retPal;
			}
		}

		static MonsterPalette? ParsePossiblyNamedPaletteNode(XmlNode palnode) {
			XmlAttributeCollection attr = palnode.Attributes!;

			MonsterPalette? retpal;

			switch (attr["type"]?.Value) {
				case "namedpalette":
					var palettename = attr["palettename"]?.Value;

					if (palettename is null) {
						PaletteStatusLog.Add("No color name given for namedpalette node");
						return null;
					}

					retpal = NamedPalettes.FirstOrDefault(op => op?.Name == palettename, null);

					if (retpal is null) {
						PaletteStatusLog.Add($"Unable to find named palette: \"{palettename}\"");
					}

					return retpal;

				case "childpalette":
					return ParsePaletteNode(palnode);
			}

			PaletteStatusLog.Add($"Unable to determine format of palette node: \"{palnode.OuterXml}\"");
			return null;
		}

		static MonsterPalette? ParsePaletteNode(XmlNode palnode) {
			if (!ParsePaletteAttributes(palnode, out ShadedColor? body, out ShadedColor? gloss, out ShadedColor? blush, out ShadedColor suboutline)) {
				return null;
			}

			if (body is null || gloss is null || blush is null) {
				return null;
			}

			return new(body, gloss, blush) {
				SubOutline = suboutline,
			};
		}

		static MonsterPalette? ParseNamedPaletteNode(XmlNode palnode) {
			if (!ParsePaletteAttributes(palnode, out ShadedColor? body, out ShadedColor? gloss, out ShadedColor? blush, out ShadedColor suboutline)) {
				return null;
			}

			if (body is null || gloss is null || blush is null) {
				return null;
			}

			if (palnode?.Attributes?["name"]?.Value is not string name) {
				PaletteStatusLog.Add("Missing \"name\" attribute on palette node");
				return null;
			}

			if (palnode?.Attributes?["color"]?.Value is not string prefcolor) {
				PaletteStatusLog.Add("Missing \"color\" attribute on palette node");
				prefcolor = "any";
			}

			return new(name, body, gloss, blush) {
				SubOutline = suboutline,
				PreferredColor = ObjectColors.GetColorFromName(prefcolor)
			};
		}

		static bool ParsePaletteAttributes(XmlNode palnode, [NotNullWhen(true)] out ShadedColor? body, [NotNullWhen(true)] out ShadedColor? gloss, [NotNullWhen(true)] out ShadedColor? blush, out ShadedColor suboutline) {
			body = null;
			gloss = null;
			blush = null;
			suboutline = ShadedColor.VanillaSuboutline;

			bool good = true;

			var bodyNode = TryToGetNode("body");
			var glossNode = TryToGetNode("gloss");
			var blushNode = TryToGetNode("blush");

			if (!good) {
				return false;
			}

			body = ParsePossiblyNamedColorNode(bodyNode!);
			gloss = ParsePossiblyNamedColorNode(glossNode!);
			blush = ParsePossiblyNamedColorNode(blushNode!);

			var suboutlineNode = palnode.SelectSingleNode("suboutline");

			if (suboutlineNode?.Attributes?["color"]?.Value is string subcolor) {
				if (IsExplicitColor(subcolor, out int suboutlinehex)) {
					suboutline = new(suboutlinehex, suboutlinehex);
				}
			}

			if (body is null || gloss is null || blush is null) {
				return false;
			}

			return true;

			XmlNode? TryToGetNode(string nodeName) {
				var nodeGuy = palnode.SelectSingleNode(nodeName);
				if (nodeGuy is null) {
					PaletteStatusLog.Add($"Unable to find {nodeName} in palette node");
					good = false;
				}
				return nodeGuy;
			}
		}

		static ShadedColor? ParsePossiblyNamedColorNode(XmlNode colnode) {
			XmlAttributeCollection attr = colnode.Attributes!;
			ShadedColor? retcol;

			switch (attr["type"]?.Value) {
				case "namedcolor":
					var colorname = attr["colorname"]?.Value;
					if (colorname is null) {
						PaletteStatusLog.Add("No color name given for namedcolor node");
						return null;
					}

					retcol = NamedColors.FirstOrDefault(oc => oc?.Name == colorname, null);

					if (retcol is null) {
						PaletteStatusLog.Add($"Unable to find named color: \"{colorname}\"");
					}

					return retcol;

				case "inlinecolor":
					return ParseColorNode(colnode);

			}

			PaletteStatusLog.Add($"Unable to determine format of color node: \"{colnode.OuterXml}\"");
			return null;
		}

		static ShadedColor? ParseColorNode(XmlNode colnode) {
			var attr = colnode.Attributes;

			if (attr is null) {
				return null;
			}

			if (!ParseColorAttributes(attr, out int lite, out int dark)) {
				return null;
			}

			return new(lite, dark);
		}

		static ShadedColor? ParseNamedColorNode(XmlNode colnode) {
			var attr = colnode.Attributes;

			if (attr is null) {
				return null;
			}

			if (!ParseColorAttributes(attr, out int lite, out int dark)) {
				return null;
			}

			if (attr["name"]?.Value is not string name) {
				PaletteStatusLog.Add("Missing \"name\" attribute on color node");
				return null;
			}

			return new(name, lite, dark);
		}


		static bool ParseColorAttributes(XmlAttributeCollection attr, out int lite, out int dark) {
			var lightAttribute = attr["light"]?.Value;

			lite = -1;
			dark = -1;

			if (lightAttribute is null) {
				PaletteStatusLog.Add("Missing \"light\" attribute on color node");
				return false;
			}

			var darkAttribute = attr["dark"]?.Value;

			if (darkAttribute is null) {
				PaletteStatusLog.Add("Missing \"dark\" attribute on color node");
				return false;
			}

			if (!IsExplicitColor(lightAttribute, out lite)) {
				PaletteStatusLog.Add($"Unable to parse \"light\" attribute as color: \"{lightAttribute}\"");
				return false;
			}

			if (!IsExplicitColor(darkAttribute, out dark)) {
				PaletteStatusLog.Add($"Unable to parse \"dark\" attribute as color: \"{darkAttribute}\"");
				return false;
			}

			return true;
		}
	}
}
