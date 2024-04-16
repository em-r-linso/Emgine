using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Game
{
	public Game(Vector2 windowSize, string windowName)
	{
		WindowSize = windowSize;
		WindowName = windowName;

		OnLoad();
	}

	Vector2          WindowSize                    { get; set; }
	string           WindowName                    { get; }
	Camera           Camera                        { get; set; } = new();
	GameStateManager GameStateManager              { get; }      = new();
	IMouseable?      CurrentMouseHoverTarget       { get; set; }
	IMouseable?      CurrentMouseLeftButtonTarget  { get; set; }
	IMouseable?      CurrentMouseRightButtonTarget { get; set; }

	public void Start(GameState initialState)
	{
		GameStateManager.AddState(initialState);

		while (!Raylib.WindowShouldClose())
		{
			HandleEvents();
			OnUpdate();
			OnDraw();
		}

		Raylib.CloseWindow();
	}

	void OnLoad()
	{
		Raylib.InitWindow((int)WindowSize.X, (int)WindowSize.Y, WindowName);
		Raylib.SetWindowState(ConfigFlags.ResizableWindow);
		Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));

		Camera = new();
		Camera.SetOffset(WindowSize / 2);
	}

	void OnDraw()
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.Black);

		Camera.Use();

		foreach (var drawable in GameStateManager.Drawables)
		{
			drawable.Draw();
		}

		Camera.End();

		Raylib.EndDrawing();
	}

	void OnUpdate()
	{
		foreach (var updatable in GameStateManager.Updatables)
		{
			updatable.Update();
		}
	}

	void OnResize()
	{
		WindowSize = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
		Camera.SetOffset(WindowSize / 2);
	}

	void HandleEvents()
	{
		if (Raylib.IsWindowResized())
		{
			OnResize();
		}

		HandleMouseEvents();
	}

	void HandleMouseEvents()
	{
		var mousePosition  = Camera.GetScreenToWorld(Raylib.GetMousePosition());
		var mouseLeftDown  = Raylib.IsMouseButtonPressed(MouseButton.Left);
		var mouseRightDown = Raylib.IsMouseButtonPressed(MouseButton.Right);
		var mouseLeftUp    = Raylib.IsMouseButtonReleased(MouseButton.Left);
		var mouseRightUp   = Raylib.IsMouseButtonReleased(MouseButton.Right);

		// mouse exit
		if (CurrentMouseHoverTarget != null)
		{
			if (!CurrentMouseHoverTarget.IsMouseOver(mousePosition))
			{
				CurrentMouseHoverTarget.OnMouseExit();
				CurrentMouseHoverTarget = null;
			}
		}

		// mouseover events (enter, click, exit if behind)
		foreach (var mouseable in GameStateManager.Mouseables)
		{
			var isMouseOver = mouseable.MouseableArea.Contains(mousePosition);

			if (isMouseOver)
			{
				// if this is already the current hover target, there's no need to do anything
				if (CurrentMouseHoverTarget == mouseable)
				{
					break;
				}
				
				// if the current hover target is behind this element, exit it
				if (CurrentMouseHoverTarget != null)
				{
					mouseable.OnMouseExit();
				}

				CurrentMouseHoverTarget = mouseable;
				mouseable.OnMouseEnter();

				if (mouseLeftDown)
				{
					mouseable.OnLeftClick();
				}

				if (mouseRightDown)
				{
					mouseable.OnRightClick();
				}

				break;
			}
		}

		// click
		if (CurrentMouseHoverTarget != null)
		{
			if (mouseLeftDown)
			{
				CurrentMouseLeftButtonTarget = CurrentMouseHoverTarget;
				CurrentMouseLeftButtonTarget.OnLeftClick();
			}

			if (mouseRightDown)
			{
				CurrentMouseRightButtonTarget = CurrentMouseHoverTarget;
				CurrentMouseRightButtonTarget.OnRightClick();
			}
		}

		// left hold and release
		if (CurrentMouseLeftButtonTarget != null)
		{
			CurrentMouseLeftButtonTarget.OnLeftHold();

			if (mouseLeftUp)
			{
				CurrentMouseLeftButtonTarget.OnLeftRelease();
				CurrentMouseLeftButtonTarget = null;
			}
		}

		// right hold and release
		if (CurrentMouseRightButtonTarget != null)
		{
			CurrentMouseRightButtonTarget.OnRightHold();

			if (mouseRightUp)
			{
				CurrentMouseRightButtonTarget.OnRightRelease();
				CurrentMouseRightButtonTarget = null;
			}
		}
	}
}