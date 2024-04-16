using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

/// <summary>
///     Represents a shape that can be drawn.
///     Shapes have a fill color and/or edge color; if neither is provided, the shape will not be drawn.
/// </summary>
public abstract class Shape : IDrawable
{
	public Color? EdgeColor;
	public Color? FillColor;

	protected Shape(Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0)
	{
		FillColor = fillColor;
		EdgeColor = edgeColor;
		DrawOrder = drawOrder;
	}

	public int   DrawOrder    { get; set; }
	public float CameraWeight { get; set; }

	public void Draw()
	{
		if (FillColor.HasValue)
		{
			DrawFill();
		}

		if (EdgeColor.HasValue)
		{
			DrawEdge();
		}
	}

	protected abstract void DrawFill();
	protected abstract void DrawEdge();
	public abstract    bool Contains(Vector2 point);
}