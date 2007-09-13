/*******************************************************************************
 *  AbstractActor.cs: An abstract implementation of IActor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Actors
{
    public abstract class AbstractActor : IActor
    {
        public event EventHandler<EventArgs> Died;

        private HitPoints hit_points;
        private Inventory inventory;

        public HitPoints HitPoints
        {
            get { return hit_points; }
            protected set { hit_points = value; }
        }

        public Inventory Inventory
        {
            get {
                if (inventory == null)
                    inventory = new Inventory (this);

                return inventory;
            }
            protected set { inventory = value; }
        }

        public abstract Action Action { get; }

        protected virtual bool IsAlive
        {
            get { return HitPoints.Current >= 0; }
        }

        protected virtual bool IsDead
        {
            get { return !IsAlive; }
        }

        public abstract bool IsTurnReady { get; }

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

        public void Move (Location new_location)
        {
            base.Location = new_location;
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

