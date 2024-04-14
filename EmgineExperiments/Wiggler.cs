using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class Wiggler : Polygon, IUpdatable
{
	public Wiggler(IReadOnlyList<Vector2> points, Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0)
		: base(points, fillColor, edgeColor, drawOrder)
	{
		OriginalPoints = Points.ToArray();
	}

	Vector2[] OriginalPoints { get; }

	public void Update()
	{
		var maxDistance = 10;
		for (var pointIndex = 0;
			 pointIndex < Points.Length - 1; /* no need to edit final point, because it will match first point */
			 pointIndex++)
		{
			var point = Points[pointIndex];
			point = new(point.X += Raylib.GetRandomValue(-1, 1), point.Y += Raylib.GetRandomValue(-1, 1));
			if (Vector2.Distance(point, OriginalPoints[pointIndex]) > maxDistance)
			{
				var direction = Vector2.Normalize(point - OriginalPoints[pointIndex]);
				point = OriginalPoints[pointIndex] + direction * maxDistance;
			}

			Points[pointIndex] = point;
		}

		WeldEndpoints();
	}
}