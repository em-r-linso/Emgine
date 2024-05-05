using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class MainState : GameState
{
	protected override void OnEnter()
	{
		// var triangle = new Polygon(new Vector2[]
		// 						   {
		// 							   new(-500, 0),
		// 							   new(0, 0),
		// 							   new(0, -500)
		// 						   },
		// 						   Color.Blue,
		// 						   Color.White);
		// var clickableThingA = new ClickableThing(new Rectangle(0, 0, 50, 50, Color.Green, Color.White, 100));
		// AddDrawable(clickableThingA.MouseableArea);
		// AddMouseable(clickableThingA);
		// var clickableThingB = new ClickableThing(triangle);
		// AddDrawable(clickableThingB.MouseableArea);
		// AddMouseable(clickableThingB);

		var uiThing1 = new UIThing(
								   new Wiggler(new Vector2[]
											   {
												   new(-10, 30),  // bottom left
												   new(120, 30),  // bottom right
												   new(120, -10), // top right
												   new(-10, -10)  // top left
											   },
											   Color.SkyBlue,
											   drawOrder: -100,
											   wiggleLimit: 5,
											   wiggleSpeed: 20),
								   new("Continue", new(0, 0), color: Color.White, drawOrder: 100));
		AddDrawable(uiThing1.MouseableArea);
		AddDrawable(uiThing1.Text);
		AddUpdatable((Wiggler)uiThing1.MouseableArea);
		AddMouseable(uiThing1);

		var uiThing2 = new UIThing(
								   new Wiggler(new Vector2[]
											   {
												   new(-10, 30  + 50), // bottom left
												   new(120, 30  + 50), // bottom right
												   new(120, -10 + 50), // top right
												   new(-10, -10 + 50)  // top left
											   },
											   Color.SkyBlue,
											   drawOrder: -100,
											   wiggleLimit: 5,
											   wiggleSpeed: 20),
								   new("New Game", new(0, 0 + 50), color: Color.White, drawOrder: 100));
		AddDrawable(uiThing2.MouseableArea);
		AddDrawable(uiThing2.Text);
		AddUpdatable((Wiggler)uiThing2.MouseableArea);
		AddMouseable(uiThing2);

		var uiThing3 = new UIThing(
								   new Wiggler(new Vector2[]
											   {
												   new(-10, 30  + 100), // bottom left
												   new(120, 30  + 100), // bottom right
												   new(120, -10 + 100), // top right
												   new(-10, -10 + 100)  // top left
											   },
											   Color.SkyBlue,
											   drawOrder: -100,
											   wiggleLimit: 5,
											   wiggleSpeed: 20),
								   new("Options", new(0, 0 + 100), color: Color.White, drawOrder: 100));
		AddDrawable(uiThing3.MouseableArea);
		AddDrawable(uiThing3.Text);
		AddUpdatable((Wiggler)uiThing3.MouseableArea);
		AddMouseable(uiThing3);
	}
}