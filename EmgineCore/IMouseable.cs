namespace EmgineCore;

public interface IMouseable
{
	public Shape MouseableArea { get; set; }
	bool         Hovered       { get; set; }

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

	public void OnLeftRelease()
	{
	}

	// right clicking events
	public void OnRightClick()
	{
	}

	public void OnRightRelease()
	{
	}
}