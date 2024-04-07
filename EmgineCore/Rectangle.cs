using Raylib_cs;
using Color = Raylib_cs.Color;

namespace EmgineCore;

public class Rectangle : Shape
{
	public Rectangle(int x, int y, int width, int height, Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0)
		: base(fillColor, edgeColor, drawOrder)
	{
		Rect = new(x, y, width, height);
	}

	public Rectangle(Raylib_cs.Rectangle rect, Color? fillColor = null, Color? edgeColor = null)
		: base(fillColor, edgeColor)
	{
		Rect = rect;
	}

	Raylib_cs.Rectangle Rect { get; }

	protected override void DrawFill()
	{
		Raylib.DrawRectangle((int)Rect.X, (int)Rect.Y, (int)Rect.Width, (int)Rect.Height, FillColor!.Value);
	}

	protected override void DrawEdge()
	{
		Raylib.DrawRectangleLines((int)Rect.X, (int)Rect.Y, (int)Rect.Width, (int)Rect.Height, EdgeColor!.Value);
	}
}