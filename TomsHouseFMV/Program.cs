using System.Numerics;
using EmgineCore;
using Raylib_cs;

var game = new Game(new(800, 600), "Tom's House");

game.AddDrawable(new Text("Hello world!", new(10, 10), color: Color.White, drawOrder: 100));
game.AddDrawable(new Text("Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet.",
					   new(10, 50),
					   TextManager.Typeface.MontserratBold,
					   wrapWidth: 200,
					   color: Color.Red,
					   drawOrder: 100));
Vector2[] vertices =
{
	new(-10, 0),
	new(-5, 10),
	new(5, 10),
	new(100, 0),
	new(7, -10),
	new(-5, -10),
	new(-10, 0)
};
game.AddDrawable(new Polygon(vertices, Color.Blue, Color.White));
game.AddDrawable(new EmgineCore.Rectangle(0, 0, 220, 100, Color.Green));

game.Start();