using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Polygon : Shape
{
	public Polygon(IReadOnlyList<Vector2> points, Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0)
		: base(fillColor, edgeColor, drawOrder)
	{
		Points = points.Append(points[0]).ToArray(); // first point added to end to close polygon
	}

	Vector2[] Points { get; }

	protected override void DrawFill()
	{
		Raylib.DrawTriangleFan(Points, Points.Length, FillColor!.Value);
	}

	protected override void DrawEdge()
	{
		Raylib.DrawLineStrip(Points, Points.Length, EdgeColor!.Value);
	}
}