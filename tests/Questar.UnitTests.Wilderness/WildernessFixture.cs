[TestFixture]
public class WildernessFixture
{
    private Wilderness wilderness;

    [SetUp]
    public void SetUp ()
    {
        wilderness = new Wilderness ();
    }

    // These GridChanged events should be on Map...

    [Test]
    public void GridChangedEventsForNewlyAddedActor ()
    {
        EventList<Actor> actors = new EventList<Actor> ();
        wilderness.Actors = actors;

        bool got_event = false;
        wilderness.GridChanged += (sender, args) => {
            got_event = true;
            Assert.That (args.Grid, Is.EqualTo (MockActor.Dave.Location));
        };

        actors.Add (MockActor.Dave);

        Assert.That (got_event, Is.True);
    }

    [Test]
    public void GridChangedEventWhenActorsListIsChanged ()
    {
        EventList<Actor> old_list = new EventList<Actor> () {
            MockActor.Dave;
        };
        EventList<Actor> new_list = new EventList<Actor> ();

        bool got_event = false;
        wilderness.GridChanged += (sender, args) => {
            got_event = true;
            Assert.That (args.Grid, Is.EqualTo (MockActor.Dave.Location));
        };

        wilderness.Actors = old_list;
        Assert.That (got_event, Is.False);
        wilderness.Actors = new_list;
        Assert.That (got_event, Is.True);
    }

    [Test]
    public void WildernessSizeIs1000x1000 ()
    {
        Assert.That (wilderness.Width, Is.EqualTo (1000));
        Assert.That (wilderness.Height, Is.EqualTo (1000));
    }
}

