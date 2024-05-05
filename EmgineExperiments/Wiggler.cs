using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class Wiggler : Shape, IUpdatable
{
	public Wiggler(IReadOnlyList<Vector2> points, Color? fillColor = null, Color? edgeColor = null, int drawOrder = 0, float wiggleLimit = 10, float wiggleSpeed = 100)
		: base(points, fillColor, edgeColor, drawOrder)
	{
		OriginalPoints = Points.ToArray();
		WiggleLimit = wiggleLimit;
		WiggleSpeed = wiggleSpeed;
		
		WiggleDeltas = new Vector2[OriginalPoints.Length];
		for (var pointIndex = 0; pointIndex < WiggleDeltas.Length; pointIndex++)
		{
			WiggleDeltas[pointIndex] = new(Random.NextSingle() * 2 - 1, Random.NextSingle() * 2 - 1);
		}
	}

	Vector2[]    OriginalPoints { get; }
	Vector2[]    WiggleDeltas   { get; }
	Random       Random         { get; } = new();
	public float WiggleLimit    { get; set; }
	public float WiggleSpeed    { get; set; }

	public void Update(float deltaTime)
	{
		
		for (var pointIndex = 0;
			 pointIndex < Points.Length - 1; /* no need to edit final point, because it will match first point */
			 pointIndex++)
		{
			
			var point = Points[pointIndex];
			// point = new(point.X += Raylib.GetRandomValue(-1, 1), point.Y += Raylib.GetRandomValue(-1, 1));
			point += WiggleDeltas[pointIndex] * deltaTime * WiggleSpeed;
			if (Vector2.Distance(point, OriginalPoints[pointIndex]) > WiggleLimit)
			{
				var direction = Vector2.Normalize(point - OriginalPoints[pointIndex]);
				point = OriginalPoints[pointIndex] + direction * WiggleLimit;
				// WiggleDeltas[pointIndex] = -WiggleDeltas[pointIndex];
				WiggleDeltas[pointIndex] = new(Random.NextSingle() * 2 - 1, Random.NextSingle() * 2 - 1);
			}

			Points[pointIndex] = point;
		}

		WeldEndpoints();
	}
}