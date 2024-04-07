using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Text : IDisposable, IDrawable
{
	public Text(string                content,
				Vector2?              position  = null,
				TextManager.Typeface? typeface  = null,
				int                   fontSize  = 20,
				Color?                color     = null,
				int                   spacing   = 1,
				int                   wrapWidth = -1,
				int                   drawOrder = 0)
	{
		Content   = content;
		Position  = position ?? new(0, 0);
		Typeface  = typeface ?? TextManager.DEFAULT_TYPEFACE;
		FontSize  = fontSize;
		Color     = color ?? Color.Black;
		Spacing   = spacing;
		WrapWidth = wrapWidth;
		DrawOrder = drawOrder;

		Font = TextManager.GetOrLoadFont(Typeface, FontSize);

		if (WrapWidth == -1)
		{
			Lines.Add(Content);
		}
		else
		{
			WrapLines();
		}
	}

	string               Content    { get; }
	Vector2              Position   { get; }
	TextManager.Typeface Typeface   { get; }
	int                  FontSize   { get; }
	Color                Color      { get; }
	int                  Spacing    { get; }
	int                  WrapWidth  { get; }
	Font                 Font       { get; }
	float                LineHeight { get; set; }
	List<string>         Lines      { get; } = new();

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
			Raylib.DrawTextEx(Font, line, linePosition, Font.BaseSize, Spacing, Color);
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