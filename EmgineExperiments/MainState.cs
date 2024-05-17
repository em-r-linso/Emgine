using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class MainState : GameState
{
	protected override void OnEnter()
	{
		string[] uiElements = ["Test", "Test2", "Test3"];
		for (var i = 0; i < uiElements.Length; i++)
		{
			var uiThing = new UIThing(
									  uiElements[i],
									  typeface: TextManager.Typeface.MontserratBold,
									  position: new(10 * i, 50 * i),
									  padding: new(20, 10),
									  wrapWidth: 100,
									  fillColorNormal: Color.SkyBlue,
									  fillColorHover: Color.Pink,
									  fontColorNormal: Color.DarkGray,
									  fontColorHover: Color.Black
									 );
			AddMouseable(uiThing);
			AddDrawable(uiThing.VisualArea);
			AddDrawable(uiThing.Text);
			AddUpdatable(uiThing.VisualArea);
		}

		var character = new Character(
									  "a",
									  typeface: TextManager.Typeface.MontserratBold,
									  position: new(-100, -100),
									  padding: new(20, 10),
									  wrapWidth: -1,
									  fillColorNormal: Color.Lime,
									  fillColorHover: Color.Pink,
									  fontColorNormal: Color.DarkGray,
									  fontColorHover: Color.Black
									 );
		AddMouseable(character);
		AddDrawable(character.VisualArea);
		AddDrawable(character.Text);
		AddUpdatable(character.VisualArea);
		AddUpdatable(character);
	}
}