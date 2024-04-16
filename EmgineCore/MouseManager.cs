using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class MouseManager
{
	public MouseManager(Camera camera, GameStateManager gameStateManager)
	{
		Camera           = camera;
		GameStateManager = gameStateManager;
	}

	IMouseable?      CurrentMouseHoverTarget       { get; set; }
	IMouseable?      CurrentMouseLeftButtonTarget  { get; set; }
	IMouseable?      CurrentMouseRightButtonTarget { get; set; }
	Camera           Camera                        { get; }
	GameStateManager GameStateManager              { get; }

	public void HandleMouseEvents()
	{
		if (Raylib.GetMouseDelta() != Vector2.Zero)
		{
			HandleMouseMove();
		}

		if (CurrentMouseHoverTarget != null)
		{
			if (CurrentMouseLeftButtonTarget == null && Raylib.IsMouseButtonPressed(MouseButton.Left))
			{
				HandleMouseButtonPressedLeft();
			}

			if (CurrentMouseRightButtonTarget == null && Raylib.IsMouseButtonPressed(MouseButton.Right))
			{
				HandleMouseButtonPressedRight();
			}
		}

		if (CurrentMouseLeftButtonTarget != null)
		{
			HandleMouseButtonHeldLeft();
		}

		if (CurrentMouseRightButtonTarget != null)
		{
			HandleMouseButtonHeldRight();
		}
	}

	void HandleMouseMove()
	{
		var mousePosition = Camera.GetScreenToWorld(Raylib.GetMousePosition());

		if (CurrentMouseHoverTarget != null)
		{
			if (!CurrentMouseHoverTarget.IsMouseOver(mousePosition))
			{
				CurrentMouseHoverTarget.OnMouseExit();
				CurrentMouseHoverTarget = null;
			}
		}

		foreach (var mouseable in GameStateManager.Mouseables)
		{
			var isMouseOver = mouseable.MouseableArea.Contains(mousePosition);

			if (isMouseOver)
			{
				if (CurrentMouseHoverTarget == mouseable) // still hovering over current target
				{
					break;
				}

				if (CurrentMouseHoverTarget != null) // hovering over something in front of current target
				{
					mouseable.OnMouseExit();
				}

				CurrentMouseHoverTarget = mouseable;
				mouseable.OnMouseEnter();

				break;
			}
		}
	}

	void HandleMouseButtonPressedLeft()
	{
		CurrentMouseLeftButtonTarget = CurrentMouseHoverTarget;
		CurrentMouseLeftButtonTarget?.OnLeftClick();
	}

	void HandleMouseButtonPressedRight()
	{
		CurrentMouseRightButtonTarget = CurrentMouseHoverTarget;
		CurrentMouseRightButtonTarget?.OnRightClick();
	}

	void HandleMouseButtonHeldLeft()
	{
		CurrentMouseLeftButtonTarget?.OnLeftHold();

		if (Raylib.IsMouseButtonReleased(MouseButton.Left))
		{
			CurrentMouseLeftButtonTarget?.OnLeftRelease();
			CurrentMouseLeftButtonTarget = null;
		}
	}

	void HandleMouseButtonHeldRight()
	{
		CurrentMouseRightButtonTarget?.OnRightHold();

		if (Raylib.IsMouseButtonReleased(MouseButton.Right))
		{
			CurrentMouseRightButtonTarget?.OnRightRelease();
			CurrentMouseRightButtonTarget = null;
		}
	}
}