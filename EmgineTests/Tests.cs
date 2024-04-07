using NUnit.Framework;
using Raylib_cs;
using EmgineCore;

namespace EmgineTests;

[TestFixture]
public class Tests
{
	[Test]
	public void FontReferenceCountTracksReferences()
	{
		// Arrange
		Raylib.InitWindow(100, 100, "Test");
		var typeface = TextManager.Typeface.MontserratRegular;
		var fontSize = 20;

		// Act
		var text1  = new Text("Test text", typeface: typeface, fontSize: fontSize);
		var count1 = TextManager.FontReferenceCounts[(typeface, fontSize)];
		var text2  = new Text("Test text 2", typeface: typeface, fontSize: fontSize);
		var count2 = TextManager.FontReferenceCounts[(typeface, fontSize)];
		text1.Dispose();
		var count3 = TextManager.FontReferenceCounts[(typeface, fontSize)];
		text2.Dispose();

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(count1,                                                            Is.EqualTo(1));
			Assert.That(count2,                                                            Is.EqualTo(2));
			Assert.That(count3,                                                            Is.EqualTo(1));
			Assert.That(TextManager.FontReferenceCounts.ContainsKey((typeface, fontSize)), Is.False);
		});
	}

	[Test]
	public void FontLoadsAndUnloads()
	{
		// Arrange
		Raylib.InitWindow(100, 100, "Test");
		var typeface = TextManager.Typeface.MontserratRegular;
		var fontSize = 20;

		// Act
		var text          = new Text("Test text", typeface: typeface, fontSize: fontSize);
		var fontIsLoaded1 = TextManager.LoadedFonts.ContainsKey((typeface, fontSize));
		text.Dispose();
		var fontIsLoaded2 = TextManager.LoadedFonts.ContainsKey((typeface, fontSize));

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(fontIsLoaded1, Is.True);
			Assert.That(fontIsLoaded2, Is.False);
		});
	}
}