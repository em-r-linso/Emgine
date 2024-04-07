using System.Numerics;
using Raylib_cs;

namespace EmgineCore;

public class Camera
{
	Camera2D Camera2D { get; set; } = new()
	{
		Offset   = new(),
		Target   = new(),
		Rotation = new(),
		Zoom     = 1
	};

	public void SetOffset(Vector2 offset)
	{
		var newCamera = Camera2D;
		newCamera.Offset = offset;
		Camera2D         = newCamera;
	}

	public void Use()
	{
		Raylib.BeginMode2D(Camera2D);
	}

	public void End()
	{
		Raylib.EndMode2D();
	}
}