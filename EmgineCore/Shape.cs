﻿using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

/// <summary>
///     Represents a shape that can be drawn.
///     Shapes have a fill color and/or edge color; if neither is provided, the shape will not be drawn.
/// </summary>
public class Shape : IDrawable
{
	public Color? EdgeColor;
	public Color? FillColor;

	public Shape(IReadOnlyList<Vector2> points,
				 Color?                 fillColor    = null,
				 Color?                 edgeColor    = null,
				 int                    drawOrder    = 0,
				 float                  cameraWeight = 1)
	{
		FillColor    = fillColor;
		EdgeColor    = edgeColor;
		DrawOrder    = drawOrder;
		CameraWeight = cameraWeight;

		Points = points.Append(points[0]).ToArray();
	}

	protected Vector2[] Points       { get; }
	public    int       DrawOrder    { get; set; }
	public    float     CameraWeight { get; set; }

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

	void DrawFill()
	{
		Raylib.DrawTriangleFan(Points, Points.Length, FillColor!.Value);
	}

	void DrawEdge()
	{
		Raylib.DrawLineStrip(Points, Points.Length, EdgeColor!.Value);
	}

	public bool Contains(Vector2 testPoint)
	{
		WeldEndpoints();

		var intersections = 0;

		for (var edge = 0; edge < Points.Length - 1; edge++)
		{
			var point1 = Points[edge];
			var point2 = Points[edge + 1];

			// We will imagine an infinite horizontal line at the height of our testPoint.

			// If the edge is horizontal, this is a lot simpler.
			if (point1.Y == point2.Y)
			{
				return point1.Y == testPoint.Y;
			}

			// Our imaginary line must be vertically between this edge's two points for it to intersect.
			if (point1.Y > testPoint.Y == point2.Y > testPoint.Y)
			{
				continue;
			}

			// Now that we know the test line will intersect, we need to find the X value of the intersection.
			float intersectionX;

			// If the edge is vertical, the intersection is at the edge's X value.
			if (point1.X == point2.X)
			{
				intersectionX = point1.X;
			}

			// If the edge is not vertical, we will calculate the intersection using the line's equation.
			// Linear equation:	y = mx + b
			// Solve for x:		x = (y - b) / m
			else
			{
				var m = (point2.Y - point1.Y) / (point2.X - point1.X);
				var b = point1.Y - m * point1.X;
				var y = testPoint.Y;
				intersectionX = (y - b) / m;
			}

			// We only want to count intersections that are to the right of the test point.
			if (intersectionX > testPoint.X)
			{
				intersections++;
			}
		}

		// If the number of intersections is odd, the test point is inside the polygon.
		return intersections % 2 == 1;
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