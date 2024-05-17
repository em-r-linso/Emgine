using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class Wiggler : Shape, IUpdatable
{
	/// <summary>
	///     A shape whose vertices wiggles around its original points.
	/// </summary>
	/// <param name="points"></param>
	/// <param name="position"></param>
	/// <param name="fillColor"></param>
	/// <param name="edgeColor"></param>
	/// <param name="drawOrder"></param>
	/// <param name="wiggleLimit"></param>
	/// <param name="wiggleSpeed"></param>
	/// <param name="wiggleVariance">
	///     Percentage up to 1.0 that dictates how intense each wiggle will be. At 0%, all wiggles are
	///     at full speed. At 100%, wiggles can get too slow.
	/// </param>
	/// <param name="cameraWeight"></param>
	public Wiggler(IReadOnlyList<Vector2> points,
				   Vector2?               position       = null,
				   Color?                 fillColor      = null,
				   Color?                 edgeColor      = null,
				   int                    drawOrder      = 0,
				   float                  wiggleLimit    = 10,
				   float                  wiggleSpeed    = 100,
				   float                  wiggleVariance = 0.8f,
				   float                  cameraWeight   = 1)
		: base(points, position, fillColor, edgeColor, drawOrder, cameraWeight)
	{
		OriginalPoints = Points.ToArray();
		WiggleLimit    = wiggleLimit;
		WiggleSpeed    = wiggleSpeed;
		WiggleVariance = wiggleVariance;

		WiggleDeltas = new Vector2[OriginalPoints.Length];
		for (var pointIndex = 0; pointIndex < WiggleDeltas.Length; pointIndex++)
		{
			WiggleDeltas[pointIndex] = new(Random.SignedFloatInRange(1 - WiggleVariance),
										   Random.SignedFloatInRange(1 - WiggleVariance));
		}
	}

	Vector2[]    OriginalPoints { get; }
	Vector2[]    WiggleDeltas   { get; }
	Random       Random         { get; } = new();
	public float WiggleLimit    { get; set; }
	public float WiggleSpeed    { get; set; }
	public float WiggleVariance { get; set; }

	public void Update(float deltaTime)
	{
		for (var pointIndex = 0;
			 pointIndex < Points.Length - 1; /* no need to edit final point, because it will match first point */
			 pointIndex++)
		{
			var point = Points[pointIndex];
			point += WiggleDeltas[pointIndex] * deltaTime * WiggleSpeed;
			if (Vector2.Distance(point, OriginalPoints[pointIndex]) > WiggleLimit)
			{
				var direction = Vector2.Normalize(point - OriginalPoints[pointIndex]);
				point = OriginalPoints[pointIndex] + direction * WiggleLimit;
				WiggleDeltas[pointIndex] = new(Random.SignedFloatInRange(1 - WiggleVariance),
											   Random.SignedFloatInRange(1 - WiggleVariance));
			}

			Points[pointIndex] = point;
		}

		WeldEndpoints();
	}
}