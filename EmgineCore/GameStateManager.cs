namespace EmgineCore;

public class GameStateManager
{
	List<GameState> ActiveStates { get; } = new();
	List<GameState> FrozenStates { get; } = new();

	public List<IDrawable>  Drawables  { get; } = new();
	public List<IUpdatable> Updatables { get; } = new();
	public List<IMouseable> Mouseables { get; } = new();

	public void AddState(GameState state)
	{
		state.Parent = this;
		ActiveStates.Add(state);
		state.Enter();
	}

	public void RemoveState(GameState state)
	{
		ActiveStates.Remove(state);
		state.Exit();
	}

	public void FreezeState(GameState state)
	{
		ActiveStates.Remove(state);
		FrozenStates.Add(state);
		state.Freeze();
	}

	public void UnfreezeState(GameState state)
	{
		FrozenStates.Remove(state);
		ActiveStates.Add(state);
		state.Unfreeze();
	}

	/// <summary>
	///     Inserts a drawable based on its DrawOrder using a binary search.
	/// </summary>
	public void AddDrawable(IDrawable drawable)
	{
		var index = Drawables.BinarySearch(drawable, new DrawableComparer());
		if (index < 0)
		{
			index = ~index;
		}

		Drawables.Insert(index, drawable);
	}

	public void RemoveDrawable(IDrawable drawable)
	{
		Drawables.Remove(drawable);
	}

	public void AddUpdatable(IUpdatable updatable)
	{
		Updatables.Add(updatable);
	}

	public void RemoveUpdatable(IUpdatable updatable)
	{
		Updatables.Remove(updatable);
	}

	public void AddMouseable(IMouseable mouseable)
	{
		Mouseables.Add(mouseable);
	}

	public void RemoveMouseable(IMouseable mouseable)
	{
		Mouseables.Remove(mouseable);
	}
}

public class DrawableComparer : IComparer<IDrawable>
{
	public int Compare(IDrawable? x, IDrawable? y)
	{
		if (x == null || y == null)
		{
			return 0;
		}

		return x.DrawOrder.CompareTo(y.DrawOrder);
	}
}