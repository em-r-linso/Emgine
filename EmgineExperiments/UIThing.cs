using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class UIThing : IMouseable
{
	public UIThing(
		IReadOnlyList<Vector2> points,
		Color?                 fillColorNormal   = null,
		Color?                 edgeColorNormal   = null,
		Color?                 fillColorHover    = null,
		Color?                 edgeColorHover    = null,
		int                    drawOrder         = 0,
		float                  cameraWeight      = 1,
		int                    wiggleLimitNormal = 5,
		int                    wiggleSpeedNormal = 20,
		int                    wiggleLimitHover  = 12,
		int                    wiggleSpeedHover  = 100)
	{
		WiggleLimitNormal = wiggleLimitNormal;
		WiggleSpeedNormal = wiggleSpeedNormal;
		WiggleLimitHover  = wiggleLimitHover;
		WiggleSpeedHover  = wiggleSpeedHover;
		FillColorNormal   = fillColorNormal;
		EdgeColorNormal   = edgeColorNormal;
		FillColorHover    = fillColorHover;
		EdgeColorHover    = edgeColorHover;

		// MouseableArea = new(points, drawOrder: drawOrder, cameraWeight: cameraWeight);
		MouseableArea = new(points.ToArray(), Color.Gold, Color.Red, drawOrder: drawOrder, cameraWeight: cameraWeight);
		VisualArea = new(points.ToArray(),
						 FillColorNormal,
						 EdgeColorNormal,
						 drawOrder,
						 wiggleLimitNormal,
						 wiggleSpeedNormal,
						 cameraWeight);
		
	}

	public Wiggler VisualArea { get; set; }

	int    WiggleLimitNormal { get; }
	int    WiggleSpeedNormal { get; }
	int    WiggleLimitHover  { get; }
	int    WiggleSpeedHover  { get; }
	Color? FillColorNormal   { get; }
	Color? EdgeColorNormal   { get; }
	Color? FillColorHover    { get; }
	Color? EdgeColorHover    { get; }

	public Shape MouseableArea { get; set; }

	public void OnMouseEnter()
	{
		VisualArea.FillColor   = FillColorHover;
		VisualArea.EdgeColor   = EdgeColorHover;
		VisualArea.WiggleSpeed = WiggleSpeedHover;
		VisualArea.WiggleLimit = WiggleLimitHover;
		Raylib.SetMouseCursor(MouseCursor.PointingHand);
	}

	public void OnMouseExit()
	{
		VisualArea.FillColor   = FillColorNormal;
		VisualArea.EdgeColor   = EdgeColorNormal;
		VisualArea.WiggleSpeed = WiggleSpeedNormal;
		VisualArea.WiggleLimit = WiggleLimitNormal;
		Raylib.SetMouseCursor(MouseCursor.Default);
	}
}