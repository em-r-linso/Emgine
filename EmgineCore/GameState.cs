namespace EmgineCore;

public abstract class GameState
{	
	public GameStateManager Parent     { get; set; } = null!;
	List<IDrawable>           Drawables  { get; }      = new();
	List<IUpdatable>        Updatables { get; }      = new();

	/// <summary>
	/// Called when the state is first entered.
	/// Can be extended with OnEnter().
	/// </summary>
	public void Enter()
	{
		OnEnter();
	}
	
	/// <summary>
	/// Called when the state is finally exited.
	/// Removes all drawables and updatables.
	/// Can be extended with OnExit().
	/// </summary>
	public void Exit()
	{
		foreach (var drawable in Drawables)
		{
			RemoveDrawable(drawable);
		}

		foreach (var updatable in Updatables)
		{
			RemoveUpdatable(updatable);
		}
		
		OnExit();
	}

	/// <summary>
	/// Called when the state is frozen.
	/// Removes all drawables and updatables from the state manager (but saves them for later).
	/// Can be extended with OnFreeze().
	/// </summary>
	public void Freeze()
	{
		foreach (var drawable in Drawables)
		{
			Parent.RemoveDrawable(drawable);
		}

		foreach (var updatable in Updatables)
		{
			Parent.RemoveUpdatable(updatable);
		}
		
		OnFreeze();
	}

	/// <summary>
	/// Called when the state is unfrozen.
	/// Re-adds drawables and updatables to the state manager.
	/// Can be extended with OnUnfreeze().
	/// </summary>
	public void Unfreeze()
	{
		foreach (var drawable in Drawables)
		{
			Parent.AddDrawable(drawable);
		}

		foreach (var updatable in Updatables)
		{
			Parent.AddUpdatable(updatable);
		}
		
		OnUnfreeze();
	}

	protected virtual void OnEnter()
	{
	}
	
	protected virtual void OnExit()
	{
	}

	protected virtual void OnFreeze()
	{
	}

	protected virtual void OnUnfreeze()
	{
	}

	public void AddDrawable(IDrawable drawable)
	{
		Drawables.Add(drawable);
		Parent.AddDrawable(drawable);
	}
	
	public void RemoveDrawable(IDrawable drawable)
	{
		Drawables.Remove(drawable);
		Parent.RemoveDrawable(drawable);
	}

	public void AddUpdatable(IUpdatable updatable)
	{
		Updatables.Add(updatable);
		Parent.AddUpdatable(updatable);
	}
	
	public void RemoveUpdatable(IUpdatable updatable)
	{
		Updatables.Remove(updatable);
		Parent.RemoveUpdatable(updatable);
	}
}