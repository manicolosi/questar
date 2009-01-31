// hello
public abstract class Map
{
    protected Map (EventList<Actor> actors)
    {
        actors.Added   += (list, args) => AddActor (args.Item);
        actors.Removed += (list, args) => RemoveActor (args.Item);
        actors.ForEach (a => AddActor (a));
    }

    public abstract Grid this [Point p];

    private void AddActor (Actor a)
    {
        a.Moved += OnActorMovedHandler;
    }

    private void RemoveActor (Actor a)
    {
        a.Moved -= OnActorMovedHandler;
    }
}

