using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;

namespace Questar.Actors
{
    public abstract class Actor : IActor, IActionable
    {
        private string name;
        private string tile;
        private Map map;
        private Point location;

        public event EventHandler<ActorMovedEventArgs> Moved;

        public virtual string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        public virtual string Tile
        {
            get { return tile; }
            protected set { tile = value; }
        }

        public virtual Point Location
        {
            get { return (Point) location.Clone (); }
            protected set { location = value; }
        }

        public virtual Map Map
        {
            get { return map; }
            protected set { map = value; }
        }

        public abstract bool IsTurnReady { get; }
        public abstract IAction Action { get; }

        public void Move (Point p)
        {
            Point old = Location;
            Location = p;

            Events.FireEvent<ActorMovedEventArgs> (this, Moved,
                delegate (ActorMovedEventArgs args) {
                    args.OldLocation = old;
                });
        }


        protected bool CanMoveTo (Direction direction)
        {
            return CanMoveTo (direction.ApplyToPoint (Location));
        }

        protected bool CanMoveTo (Point p)
        {
            return Map.GetGridInformation (p) == GridInformation.Clear;
        }
    }
}

