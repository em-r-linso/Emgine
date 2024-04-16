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

	Vector2          WindowSize       { get; set; }
	string           WindowName       { get; }
	Camera           Camera           { get; set; } = null!;
	GameStateManager GameStateManager { get; }      = new();
	MouseManager     MouseManager     { get; set; } = null!;

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

		MouseManager = new(Camera, GameStateManager);
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

		MouseManager.HandleMouseEvents();
	}
}