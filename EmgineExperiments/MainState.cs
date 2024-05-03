using System.Numerics;
using EmgineCore;
using Raylib_cs;
using Rectangle = EmgineCore.Rectangle;

namespace EmgineExperiments;

public class MainState : GameState
{
	protected override void OnEnter()
	{
		var triangle = new Polygon(new Vector2[]
								   {
									   new(-500, 0),
									   new(0, 0),
									   new(0, -500)
								   },
								   Color.Blue,
								   Color.White);
		// AddDrawable(triangle);

		var clickableThingA = new ClickableThing(new Rectangle(0, 0, 50, 50, Color.Green, Color.White, 100));
		AddDrawable(clickableThingA.MouseableArea);
		AddMouseable(clickableThingA);
		// var clickableThingB = new ClickableThing(new Rectangle(-25, -25, 50, 50, Color.Blue, Color.White, 10));
		var clickableThingB = new ClickableThing(triangle);
		AddDrawable(clickableThingB.MouseableArea);
		AddMouseable(clickableThingB);

		var wiggler = new Wiggler(new Vector2[]
								  {
									  new(-100, 100), // bottom left
									  new(100, 100), // bottom right
									  new(100, -100), // top right
									  new(-100, -100), // top left
								  },
								  Color.Blue,
								  Color.Blue,
								  -100);
		AddDrawable(wiggler);
		AddUpdatable(wiggler);

		// var text = new Text("Hello, world!", new(-100,-100), color: Color.White, drawOrder: 100);
		// AddDrawable(text);
	}
}