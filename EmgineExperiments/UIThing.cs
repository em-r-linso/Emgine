using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class UIThing : IMouseable
{
	Vector2 _position;

	public UIThing(
		string                content,
		Vector2?              padding              = null,
		Vector2?              position             = null,
		TextManager.Typeface? typeface             = null,
		int                   fontSize             = 20,
		Color?                fontColorNormal      = null,
		Color?                fontColorHover       = null,
		int                   spacing              = 1,
		int                   wrapWidth            = -1,
		Color?                fillColorNormal      = null,
		Color?                edgeColorNormal      = null,
		Color?                fillColorHover       = null,
		Color?                edgeColorHover       = null,
		int                   drawOrder            = 0,
		float                 cameraWeight         = 1,
		int                   wiggleLimitNormal    = 3,
		int                   wiggleSpeedNormal    = 15,
		int                   wiggleLimitHover     = 9,
		int                   wiggleSpeedHover     = 70,
		float                 wiggleVarianceNormal = 0.8f,
		float                 wiggleVarianceHover  = 0.2f)
	{
		WiggleLimitNormal    = wiggleLimitNormal;
		WiggleSpeedNormal    = wiggleSpeedNormal;
		WiggleLimitHover     = wiggleLimitHover;
		WiggleSpeedHover     = wiggleSpeedHover;
		WiggleVarianceNormal = wiggleVarianceNormal;
		WiggleVarianceHover  = wiggleVarianceHover;
		FillColorNormal      = fillColorNormal;
		EdgeColorNormal      = edgeColorNormal;
		FillColorHover       = fillColorHover;
		EdgeColorHover       = edgeColorHover;
		FontColorNormal      = fontColorNormal;
		FontColorHover       = fontColorHover;

		padding ??= new(0, 0);
		var points = new Vector2[]
		{
			new(0 - padding.Value.X, 0 + padding.Value.Y + fontSize), // bottom left
			new(0 + padding.Value.X + wrapWidth,
				0 + padding.Value.Y + fontSize),                          // bottom right
			new(0 + padding.Value.X    + wrapWidth, 0 - padding.Value.Y), // top right
			new(0 - padding.Value.X, 0 - padding.Value.Y)                 // top left
		};
		MouseableArea = new(points, drawOrder: drawOrder - 1, cameraWeight: cameraWeight);
		VisualArea = new(points.ToArray(),
						 Position,
						 FillColorNormal,
						 EdgeColorNormal,
						 drawOrder - 1,
						 wiggleLimitNormal,
						 wiggleSpeedNormal,
						 wiggleVarianceNormal,
						 cameraWeight);
		Text = new(content, Position, typeface, fontSize, FontColorNormal, spacing, wrapWidth, drawOrder);
		
		Position = position ?? new(0, 0);
	}

	public Wiggler VisualArea { get; set; }
	public Text    Text       { get; set; }

	int    WiggleLimitNormal    { get; }
	int    WiggleSpeedNormal    { get; }
	int    WiggleLimitHover     { get; }
	int    WiggleSpeedHover     { get; }
	float  WiggleVarianceNormal { get; }
	float  WiggleVarianceHover  { get; }
	Color? FillColorNormal      { get; }
	Color? EdgeColorNormal      { get; }
	Color? FontColorNormal      { get; }
	Color? FillColorHover       { get; }
	Color? EdgeColorHover       { get; }
	Color? FontColorHover       { get; }

	protected Vector2 Position
	{
		get => _position;
		set
		{
			_position              = value;
			MouseableArea.Position = value;
			VisualArea.Position    = value;
			Text.Position          = value;
		}
	}

	public Shape MouseableArea { get; set; }

	public void OnMouseEnter()
	{
		VisualArea.FillColor      = FillColorHover;
		VisualArea.EdgeColor      = EdgeColorHover;
		VisualArea.WiggleSpeed    = WiggleSpeedHover;
		VisualArea.WiggleLimit    = WiggleLimitHover;
		VisualArea.WiggleVariance = WiggleVarianceHover;

		Text.FontColor = FontColorHover;

		Raylib.SetMouseCursor(MouseCursor.PointingHand);
	}

	public void OnMouseExit()
	{
		VisualArea.FillColor      = FillColorNormal;
		VisualArea.EdgeColor      = EdgeColorNormal;
		VisualArea.WiggleSpeed    = WiggleSpeedNormal;
		VisualArea.WiggleLimit    = WiggleLimitNormal;
		VisualArea.WiggleVariance = WiggleVarianceNormal;

		Text.FontColor = FontColorNormal;

		Raylib.SetMouseCursor(MouseCursor.Default);
	}
}