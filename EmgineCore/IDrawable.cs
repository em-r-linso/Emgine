namespace EmgineCore;

public interface IDrawable
{
	public int  DrawOrder { get; set; }
	public float CameraWeight { get; set; }
	
	public void Draw();
}