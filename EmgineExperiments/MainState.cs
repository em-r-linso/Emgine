using EmgineCore;
using Raylib_cs;
using Rectangle = EmgineCore.Rectangle;

namespace EmgineExperiments;

public class MainState : GameState
{
	protected override void OnEnter()
	{
		// var triangle = new Polygon(new Vector2[]
		// 						   {
		// 							   new(-500, 0),
		// 							   new(0, 0),
		// 							   new(0, -500),
		// 						   },
		// 						   Color.Red,
		// 						   Color.Red);
		// AddDrawable(triangle);

		var rectangle      = new Rectangle(0, 0, 50, 50, Color.Green, Color.White);
		var clickableThing = new ClickableThing(rectangle);
		AddDrawable(clickableThing.MouseableArea);
		AddMouseable(clickableThing);

		// var wiggler = new Wiggler(new Vector2[]
		// 						  {
		// 							  new(-100, 100), // bottom left
		// 							  new(100, 100), // bottom right
		// 							  new(100, -100), // top right
		// 							  new(-100, -100), // top left
		// 						  },
		// 						  Color.Blue,
		// 						  Color.Blue,
		// 						  -100);
		// AddDrawable(wiggler);
		// AddUpdatable(wiggler);

		// var text = new Text("Hello, world!", new(-100,-100), color: Color.White, drawOrder: 100);
		// AddDrawable(text);
	}
}