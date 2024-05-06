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
			var deltaTime = Raylib.GetFrameTime();
			
			HandleEvents();
			OnUpdate(deltaTime);
			OnDraw();
		}

		Raylib.CloseWindow();
	}

	void OnLoad()
	{
		Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
		Raylib.SetConfigFlags(ConfigFlags.VSyncHint);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
		Raylib.InitWindow((int)WindowSize.X, (int)WindowSize.Y, WindowName);
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

	void OnUpdate(float deltaTime)
	{
		foreach (var updatable in GameStateManager.Updatables)
		{
			updatable.Update(deltaTime);
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