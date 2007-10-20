/*******************************************************************************
 *  AbstractActor.cs: An abstract implementation of Actor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Helpers;
using Questar.Items;
using Questar.Primitives;

namespace Questar.Actors
{
    public abstract class AbstractActor : AbstractEntity, Actor
    {
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
            return loc.IsClear;
        }

        public bool CanMoveIn (Direction direction)
        {
            return CanMoveTo (direction.ApplyTo (base.Location));
        }

        List<Actor> visible_actors = new List<Actor> ();

        public void Move (Location new_location)
        {
            Location = new_location;
            List<Actor> new_visible_actors = new List<Actor> ();

            foreach (Actor actor in Location.ActorsInRadius (5)) {
                if (actor == this)
                    continue;

                new_visible_actors.Add (actor);
            }

            foreach (Actor actor in new_visible_actors) {
                if (visible_actors.Contains (actor))
                    continue;

                // sighted...
                Console.WriteLine ("{0} sights {1}", this, actor);
            }

            foreach (Actor actor in visible_actors) {
                if (new_visible_actors.Contains (actor))
                    continue;

                // lost sight...
                Console.WriteLine ("{0} lost sight of {1}", this, actor);
            }

            visible_actors = new_visible_actors;
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
            string attacker_name = StringHelper.SentenceCapitalize (attacker);
            Messages.Instance.Add ("{0} attack {1}.", attacker_name, this);

            //EventHelper.Raise (this, Attacked);
        }

        protected virtual void OnDeath ()
        {
            string attacker_name = StringHelper.SentenceCapitalize (this);
            Messages.Instance.Add ("{0} has died.", attacker_name);

            OnDestroyed ();
        }
    }
}

