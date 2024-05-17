using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Text : IDisposable, IDrawable
{
	public Text(string                content,
				Vector2?              position  = null,
				TextManager.Typeface? typeface  = null,
				int                   fontSize  = 20,
				Color?                fontColor = null,
				int                   spacing   = 1,
				int                   wrapWidth = -1,
				int                   drawOrder = 0)
	{
		Content   = content;
		Position  = position ?? new(0, 0);
		Typeface  = typeface ?? TextManager.DEFAULT_TYPEFACE;
		FontSize  = fontSize;
		FontColor = fontColor;
		Spacing   = spacing;
		WrapWidth = wrapWidth;
		DrawOrder = drawOrder;

		Font = TextManager.GetOrLoadFont(Typeface, FontSize);

		if (WrapWidth == -1)
		{
			WrapWidth = (int)Raylib.MeasureTextEx(Font, Content, Font.BaseSize, Spacing).X;
			Lines.Add(Content);
		}
		else
		{
			WrapLines();
		}
	}

	string               Content    { get; }
	public Vector2       Position   { get; set; }
	TextManager.Typeface Typeface   { get; }
	int                  FontSize   { get; }
	public Color?        FontColor  { get; set; }
	int                  Spacing    { get; }
	public int           WrapWidth  { get; }
	Font                 Font       { get; }
	float                LineHeight { get; set; }
	List<string>         Lines      { get; } = [];

	public void Dispose()
	{
		TextManager.DereferenceFont(Typeface, FontSize);
		GC.SuppressFinalize(this);
	}

	public int   DrawOrder    { get; set; }
	public float CameraWeight { get; set; }

	public void Draw()
	{
		var linePosition = Position;
		foreach (var line in Lines)
		{
			Raylib.DrawTextEx(Font, line, linePosition, Font.BaseSize, Spacing, FontColor ?? Color.Black);
			linePosition.Y += LineHeight;
		}
	}

	void WrapLines()
	{
		var spaceSize  = Raylib.MeasureTextEx(Font, " ", Font.BaseSize, Spacing);
		var spaceWidth = spaceSize.X;
		LineHeight = spaceSize.Y;

		var words     = Content.Split(' ');
		var lineWidth = 0f;

		foreach (var word in words)
		{
			var wordWidth = Raylib.MeasureTextEx(Font, word, Font.BaseSize, Spacing).X;

			if (Lines.Count == 0 || lineWidth + wordWidth > WrapWidth) // first word or line would be too long
			{
				Lines.Add(word);
				lineWidth = wordWidth;

				continue;
			}

			Lines[^1] += $" {word}";
			lineWidth += spaceWidth + wordWidth;
		}
	}

	~Text()
	{
		Dispose();
	}
}