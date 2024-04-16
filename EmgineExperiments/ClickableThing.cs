using EmgineCore;

namespace EmgineExperiments;

public class ClickableThing : IMouseable
{
	public ClickableThing(Shape mouseableArea)
	{
		MouseableArea = mouseableArea;
	}

	public Shape MouseableArea { get; set; }

	// hovering events
	public void OnMouseEnter()
	{
		Console.WriteLine("OnMouseEnter" + MouseableArea.FillColor);
	}

	public void OnMouseExit()
	{
		Console.WriteLine("OnMouseExit" + MouseableArea.FillColor);
	}

	// left clicking events
	public void OnLeftClick()
	{
		Console.WriteLine("OnLeftClick" + MouseableArea.FillColor);
	}

	public void OnLeftHold()
	{
		Console.WriteLine("OnLeftHold" + MouseableArea.FillColor);
	}

	public void OnLeftRelease()
	{
		Console.WriteLine("OnLeftRelease" + MouseableArea.FillColor);
	}

	// right clicking events
	public void OnRightClick()
	{
		Console.WriteLine("OnRightClick" + MouseableArea.FillColor);
	}

	public void OnRightHold()
	{
		Console.WriteLine("OnRightHold" + MouseableArea.FillColor);
	}

	public void OnRightRelease()
	{
		Console.WriteLine("OnRightRelease" + MouseableArea.FillColor);
	}
}