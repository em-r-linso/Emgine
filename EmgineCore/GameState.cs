namespace EmgineCore;

public abstract class GameState
{
	public List<IDrawable>  Drawables  { get; } = new();
	public List<IUpdatable> Updatables { get; } = new();

	public abstract void Enter();
	public abstract void Exit();

	public void AddDrawable(IDrawable drawable)
	{
		Drawables.Add(drawable);
	}

	public void AddUpdatable(IUpdatable updatable)
	{
		Updatables.Add(updatable);
	}
}