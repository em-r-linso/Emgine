using Raylib_cs;


namespace EmgineCore;

public class TextManager
{
	public enum Typeface
	{
		MontserratRegular, MontserratBold
	}

	const        string   FONT_DIRECTORY   = "Assets/Fonts/";
	const        string   FONT_EXTENSION   = ".ttf";
	public const Typeface DEFAULT_TYPEFACE = Typeface.MontserratRegular;

	static Dictionary<Typeface, string> TypefacePaths { get; } = new()
	{
		{ Typeface.MontserratRegular, "Montserrat-Regular" },
		{ Typeface.MontserratBold, "Montserrat-Bold" }
	};

	public static Dictionary<(Typeface, int), Font> LoadedFonts         { get; } = new();
	public static Dictionary<(Typeface, int), int>  FontReferenceCounts { get; } = new();

	public static Font GetOrLoadFont(Typeface typeface, int fontSize)
	{
		var key = (typeface, fontSize);

		if (LoadedFonts.TryGetValue(key, out var value))
		{
			FontReferenceCounts[key]++;

			return value;
		}

		var path = $"{FONT_DIRECTORY}{TypefacePaths[typeface]}{FONT_EXTENSION}";
		var font = Raylib.LoadFontEx(path, fontSize, null, 0);
		LoadedFonts.Add(key, font);
		FontReferenceCounts.Add(key, 1);

		return font;
	}

	public static void DereferenceFont(Typeface typeface, int fontSize)
	{
		var key = (typeface, fontSize);

		if (!FontReferenceCounts.TryGetValue(key, out var count))
		{
			return;
		}

		if (--count > 0)
		{
			FontReferenceCounts[key] = count;

			return;
		}

		Raylib.UnloadFont(LoadedFonts[key]);
		LoadedFonts.Remove(key);
		FontReferenceCounts.Remove(key);
	}
}