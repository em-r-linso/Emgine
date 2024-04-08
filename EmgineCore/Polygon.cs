using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Polygon : Shape
{
	public Polygon(IReadOnlyList<Vector2> points, Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0)
		: base(fillColor, edgeColor, drawOrder)
	{
		Points = points.Append(points[0]).ToArray();
	}

	protected Vector2[] Points { get; set; }

	protected override void DrawFill()
	{
		Raylib.DrawTriangleFan(Points, Points.Length, FillColor!.Value);
	}

	protected override void DrawEdge()
	{
		Raylib.DrawLineStrip(Points, Points.Length, EdgeColor!.Value);
	}

	/// <summary>
	///     Ensures that the polygon is closed by welding the last point to the first point.
	///     This should be called whenever the points are modified.
	/// </summary>
	protected void WeldEndpoints()
	{
		Points[^1] = Points[0];
	}
}