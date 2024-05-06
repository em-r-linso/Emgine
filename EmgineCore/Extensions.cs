namespace EmgineCore;

public static class Extensions
{
	public static float SignedFloatInRange(this Random random, float min = 0, float max = 1)
	{
		var value = random.NextSingle() * (max - min) + min;
		var sign  = random.NextSingle() > 0.5 ? 1 : -1;

		// Console.WriteLine($"min: {min} | max: {max} | value: {value} | sign: {sign}");

		return sign * value;
	}
}