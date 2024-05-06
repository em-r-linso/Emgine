using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class UIThing : IMouseable
{
	public UIThing(
		string                 content,
		Vector2? padding = null,
		Vector2?               position          = null,
		TextManager.Typeface?  typeface          = null,
		int                    fontSize          = 20,
		Color?                 color             = null,
		int                    spacing           = 1,
		int                    wrapWidth         = -1,
		Color?                 fillColorNormal   = null,
		Color?                 edgeColorNormal   = null,
		Color?                 fillColorHover    = null,
		Color?                 edgeColorHover    = null,
		int                    drawOrder         = 0,
		float                  cameraWeight      = 1,
		int                    wiggleLimitNormal = 3,
		int                    wiggleSpeedNormal = 25,
		int                    wiggleLimitHover  = 9,
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
		
		position ??= new(0, 0);
		Text = new(content, position, typeface, fontSize, color, spacing, wrapWidth, drawOrder);
		padding ??= new(0, 0);
		var points = new Vector2[]
		{
			new(position.Value.X - padding.Value.X, position.Value.Y + padding.Value.Y + fontSize),             // bottom left
			new(position.Value.X + padding.Value.X + wrapWidth, position.Value.Y+padding.Value.Y + fontSize), // bottom right
			new(position.Value.X + padding.Value.X + wrapWidth, position.Value.Y -padding.Value.Y), // top right
			new(position.Value.X  - padding.Value.X, position.Value.Y -padding.Value.Y)              // top left
		};

		MouseableArea = new(points, drawOrder: drawOrder - 1, cameraWeight: cameraWeight);
		// MouseableArea = new(points.ToArray(), Color.Gold, Color.Red, drawOrder, cameraWeight);
		VisualArea = new(points.ToArray(),
						 FillColorNormal,
						 EdgeColorNormal,
						 drawOrder - 1,
						 wiggleLimitNormal,
						 wiggleSpeedNormal,
						 cameraWeight);
	}

	public Wiggler VisualArea { get; set; }
	public Text    Text       { get; set; }

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