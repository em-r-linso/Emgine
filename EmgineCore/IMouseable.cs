using System.Numerics;

namespace EmgineCore;

public interface IMouseable
{
	Shape MouseableArea { get; set; }

	public bool IsMouseOver(Vector2 mousePosition)
	{
		return MouseableArea.Contains(mousePosition);
	}

	// hovering events
	public void OnMouseEnter()
	{
	}

	public void OnMouseExit()
	{
	}

	// left clicking events
	public void OnLeftClick()
	{
	}

	public void OnLeftHold()
	{
	}

	public void OnLeftRelease()
	{
	}

	// right clicking events
	public void OnRightClick()
	{
	}

	public void OnRightHold()
	{
	}

	public void OnRightRelease()
	{
	}
}