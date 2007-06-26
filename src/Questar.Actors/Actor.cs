//
// Actor.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Helpers;
using Questar.Maps;
using Questar.Primitives;

namespace Questar.Actors
{
    public class ActorMovedEventArgs : EventArgs
    {
        public Point OldLocation;
    }

    public abstract class Actor
    {
        private string name;
        private HitPoints hit_points;
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

        public virtual HitPoints HitPoints
        {
            get { return hit_points; }
            protected set { hit_points = value; }
        }

        public virtual Point Location
        {
            get { return location; }
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

            EventHelper.Raise<ActorMovedEventArgs> (this, Moved,
                delegate (ActorMovedEventArgs args) {
                    args.OldLocation = old;
                });
        }

        public int GetAttackDamage (Actor target)
        {
            return 5;
        }

        public void TakeDamage (Actor attacker, int damage)
        {
            string attacker_name = FirstLetterUpper (attacker.ToString ());
            string message = String.Format ("{0} attack {1} for {2}.",
                attacker_name, this, damage);
            Messages.Instance.Add (message);

            this.HitPoints.Current -= damage;
        }

        protected bool CanMoveTo (Direction direction)
        {
            return CanMoveTo (direction.ApplyToPoint (Location));
        }

        protected bool CanMoveTo (Point p)
        {
            return Map.GetGridInformation (p) == GridInformation.Clear;
        }

        public override string ToString ()
        {
            return name;
        }

        private string FirstLetterUpper (string str)
        {
            string first_letter = str.Substring (0, 1).ToUpper ();
            return str.Remove (0, 1).Insert (0, first_letter);
        }
    }
}

