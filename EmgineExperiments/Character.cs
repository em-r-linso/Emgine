using System.Numerics;
using EmgineCore;
using Raylib_cs;

namespace EmgineExperiments;

public class Character : UIThing, IUpdatable
{
	public Character(string                content,
					 Vector2?              padding              = null,
					 Vector2?              position             = null,
					 TextManager.Typeface? typeface             = null,
					 int                   fontSize             = 20,
					 Color?                fontColorNormal      = null,
					 Color?                fontColorHover       = null,
					 int                   spacing              = 1,
					 int                   wrapWidth            = -1,
					 Color?                fillColorNormal      = null,
					 Color?                edgeColorNormal      = null,
					 Color?                fillColorHover       = null,
					 Color?                edgeColorHover       = null,
					 int                   drawOrder            = 0,
					 float                 cameraWeight         = 1,
					 int                   wiggleLimitNormal    = 3,
					 int                   wiggleSpeedNormal    = 15,
					 int                   wiggleLimitHover     = 9,
					 int                   wiggleSpeedHover     = 70,
					 float                 wiggleVarianceNormal = 0.8f,
					 float                 wiggleVarianceHover  = 0.2f)
		: base(content,
			   padding,
			   position,
			   typeface,
			   fontSize,
			   fontColorNormal,
			   fontColorHover,
			   spacing,
			   wrapWidth,
			   fillColorNormal,
			   edgeColorNormal,
			   fillColorHover,
			   edgeColorHover,
			   drawOrder,
			   cameraWeight,
			   wiggleLimitNormal,
			   wiggleSpeedNormal,
			   wiggleLimitHover,
			   wiggleSpeedHover,
			   wiggleVarianceNormal,
			   wiggleVarianceHover)
	{
	}

	public void Update(float deltaTime)
	{
		Position += new Vector2(100, 1);
	}
}