using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class UIThing : IMouseable
{
	public UIThing(Shape mouseableArea, Text text)
	{
		MouseableArea = mouseableArea;
		Text          = text;
	}

	public Text         Text          { get; set; }
	public Shape MouseableArea { get; set; }

	public void OnMouseEnter()
	{
		MouseableArea.FillColor              = Color.Pink;
		((Wiggler)MouseableArea).WiggleSpeed = 100;
		((Wiggler)MouseableArea).WiggleLimit = 12;
		Raylib.SetMouseCursor(MouseCursor.PointingHand);
	}
	
	public void OnMouseExit()
	{
		MouseableArea.FillColor              = Color.SkyBlue;
		((Wiggler)MouseableArea).WiggleSpeed = 20;
		((Wiggler)MouseableArea).WiggleLimit = 5;
		Raylib.SetMouseCursor(MouseCursor.Default);
	}
}