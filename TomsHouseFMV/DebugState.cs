using System.Numerics;
using EmgineCore;
using Raylib_cs;
using Rectangle = EmgineCore.Rectangle;

namespace TomsHouseFMV;

public class DebugState : GameState
{
	protected override void OnEnter()
	{
		AddDrawable(new Text("Hello world!", new(10, 10), color: Color.White, drawOrder: 100));
		AddDrawable(new Text("Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet.",
							 new(10, 50),
							 TextManager.Typeface.MontserratBold,
							 wrapWidth: 200,
							 color: Color.Red,
							 drawOrder: 100));
		Vector2[] vertices =
		{
			new(-10, -10),
			new(-10, 10),
			new(10, 10),
			new(10, -10)
		};
		for (var i = 0; i < vertices.Length; i++)
		{
			vertices[i] -= new Vector2(100, 100);
		}

		var wiggler = new Wiggler(vertices, Color.Blue, Color.White);
		AddDrawable(new Rectangle(0, 0, 220, 100, Color.Green));
		AddDrawable(wiggler);
		AddUpdatable(wiggler);
	}
}