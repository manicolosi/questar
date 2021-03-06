/*******************************************************************************
 *  AbstractActor.cs: An abstract implementation of Actor.
 *
 *  Copyright (C) 2007, 2009
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Helpers;
using Questar.Items;
using Questar.Primitives;

using Action = Questar.Actors.Actions.Action;

namespace Questar.Actors
{
    public abstract class AbstractActor : AbstractEntity, Actor
    {
        public event EventHandler<ActorEventArgs> ActorSighted;
        public event EventHandler<ActorEventArgs> ActorLostSight;

        private HitPoints hit_points;
        private Inventory inventory;
        private Action action;

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

        protected Action Action {
            get { return action; }
            set { action = value; }
        }

        public virtual void TakeTurn ()
        {
            Action.Execute ();
        }
        public virtual bool IsAlive
        {
            get { return HitPoints.Current >= 0; }
        }

        public virtual bool IsDead
        {
            get { return !IsAlive; }
        }

        public virtual bool IsTurnReady {
            get {
                if (IsDead)
                    throw new ApplicationException (
                        String.Format ("{0} is dead.", this));

                return true;
            }
        }

        public bool IsAdjacentTo (Actor target)
        {
            return base.Location.IsAdjacentTo (target.Location);
        }

        public bool CanMoveTo (Location loc)
        {
            return loc == null ? false : loc.IsClear;
        }

        public bool CanMoveIn (Direction direction)
        {
            return CanMoveTo (direction.ApplyTo (base.Location));
        }

        public IEnumerable<Location> FieldOfView
        {
            get {
                return Location.LocationsInRadius (5);
            }
        }

        public void Move (Location new_location)
        {
            Location = new_location;
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

        protected virtual void OnAttacked (Actor attacker, int damage)
        {
            //EventHelper.Raise (this, Attacked);
        }

        protected virtual void OnDeath ()
        {
            OnDestroyed ();
        }
    }
}

