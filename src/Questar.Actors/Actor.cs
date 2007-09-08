//
// Actor.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Helpers;
using Questar.Items;
using Questar.Primitives;

namespace Questar.Actors
{
    public class ActorMovedEventArgs : EventArgs
    {
        public Location OldLocation;
    }

    public abstract class Actor : Entity
    {
        public static event EventHandler<EventArgs> Created;
        public event EventHandler<ActorMovedEventArgs> Moved;
        public event EventHandler<EventArgs> Died;

        private HitPoints hit_points;
        private Inventory inventory = new Inventory ();

        public abstract bool IsTurnReady { get; }
        public abstract IAction Action { get; }

        public HitPoints HitPoints
        {
            get { return hit_points; }
            protected set { hit_points = value; }
        }

        public Inventory Inventory
        {
            get { return inventory; }
            protected set { inventory = value; }
        }

        public void Move (Location new_loc)
        {
            Location old_loc = base.Location;
            base.Location = new_loc;

            EventHelper.Raise<ActorMovedEventArgs> (this, Moved,
                delegate (ActorMovedEventArgs args) {
                    args.OldLocation = old_loc;
                });
        }

        public int GetAttackDamage (Actor target)
        {
            return 5;
        }

        public void TakeDamage (Actor attacker, int damage)
        {
            HitPoints.Current -= damage;
            OnAttacked (attacker, damage);

            if (IsDead)
                OnDeath ();
        }

        public bool IsAdjacentTo (Actor target)
        {
            return base.Location.IsAdjacentTo (target.Location);
        }

        public bool CanMoveTo (Direction direction)
        {
            return CanMoveTo (direction.ApplyTo (base.Location));
        }

        public bool CanMoveTo (Location loc)
        {
            return loc.IsClear;
        }

        protected virtual bool IsAlive
        {
            get { return HitPoints.Current >= 0; }
        }

        protected virtual bool IsDead
        {
            get { return !IsAlive; }
        }

        protected virtual void OnCreation ()
        {
            EventHelper.Raise (this, Created);
        }

        protected virtual void OnAttacked (Actor attacker, int damage)
        {
            string attacker_name = StringHelper.SentenceCapitalize (attacker);
            Messages.Instance.Add ("{0} attack {1}.", attacker_name, this);

            //EventHelper.Raise (this, Attacked);
        }

        protected virtual void OnDeath ()
        {
            string attacker_name = StringHelper.SentenceCapitalize (this);
            Messages.Instance.Add ("{0} has died.", attacker_name);

            EventHelper.Raise (this, Died);
        }
    }
}

