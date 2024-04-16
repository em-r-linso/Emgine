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

	/// <summary>
	///		Inserts a mouseable based on its DrawOrder using a binary search.
	/// </summary>
	/// <param name="mouseable"></param>
	public void AddMouseable(IMouseable mouseable)
	{
		var index = Mouseables.BinarySearch(mouseable, new MouseableComparer());
		if (index < 0)
		{
			index = ~index;
		}

		Mouseables.Insert(index, mouseable);
	}

	public void RemoveMouseable(IMouseable mouseable)
	{
		Mouseables.Remove(mouseable);
	}
}

/// <summary>
///		Sorts drawables based on their DrawOrder, with lower values coming first.
/// </summary>
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

/// <summary>
///		Sorts mouseables based on their DrawOrder, with higher values coming first.
/// </summary>
public class MouseableComparer : IComparer<IMouseable>
{
	public int Compare(IMouseable? x, IMouseable? y)
	{
		if (x == null || y == null)
		{
			return 0;
		}

		return y.MouseableArea.DrawOrder.CompareTo(x.MouseableArea.DrawOrder);
	}
}