using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Game
{
	Vector2         WindowSize { get; set; }
	string 		WindowName { get; set; }
	Camera          Camera     { get; set; } = new();
	List<IDrawable> Drawables  { get; }      = new();
	List<IUpdatable> Updatables { get; }      = new();

	public Game(Vector2 windowSize, string windowName)
	{
		WindowSize = windowSize;
		WindowName = windowName;
		
		OnLoad();
	}

	public void Start()
	{
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

		foreach (var drawable in Drawables)
		{
			drawable.Draw();
		}

		Camera.End();

		Raylib.EndDrawing();
	}

	void OnUpdate()
	{
		foreach (var updatable in Updatables)
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
	}

	public void AddDrawable(IDrawable drawable)
	{
		Drawables.Add(drawable);
		Drawables.Sort((a, b) => a.DrawOrder.CompareTo(b.DrawOrder));
	}

	public void AddUpdatable(IUpdatable updatable)
	{
		Updatables.Add(updatable);
	}
}