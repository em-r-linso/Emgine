using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class ClickableThing : IMouseable
{
	public ClickableThing(Shape mouseableArea)
	{
		MouseableArea = mouseableArea;
	}

	public Shape MouseableArea { get; set; }
	public bool  Hovered       { get; set; }

	public void OnMouseEnter()
	{
		MouseableArea.FillColor = Color.Red;
	}

	public void OnMouseExit()
	{
		MouseableArea.FillColor = Color.Green;
	}

	public void OnLeftClick()
	{
		MouseableArea.EdgeColor = Color.Blue;
	}
	
	public void OnLeftRelease()
	{
		MouseableArea.EdgeColor = Color.White;
	}
	
	public void OnRightClick()
	{
		MouseableArea.EdgeColor = Color.Purple;
	}

	public void OnRightRelease()
	{
		MouseableArea.EdgeColor = Color.White;
	}
}