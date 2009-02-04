/*******************************************************************************
 *  Monster.cs: An actor that are controlled by Questar.
 *
 *  Copyright (C) 2007, 2008, 2009
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

using Questar.Actors.Actions;
using Questar.Core;
using Questar.Primitives;

using Action = Questar.Actors.Actions.Action;

namespace Questar.Actors
{
    public class Monster : AbstractActor
    {
        private string monster_id;
        private string prefix;

        public Monster (MonsterDefinition definition)
        {
            base.Name = definition.Name;
            base.Tile = definition.TileId;
            base.HitPoints = new HitPoints (definition.MaxHP);

            monster_id = definition.Id;
            prefix = definition.Prefix;

            do {
                base.Location = Location.GetRandom (Game.Instance.CurrentMap);
            }
            while (!base.CanMoveTo (base.Location));
        }

        public override bool IsTurnReady
        {
            get {
                return base.IsTurnReady;
            }
        }

        private bool IsHostile (Actor actor)
        {
            // Always hostile towards the Hero.
            if (actor is Hero)
                return true;

            // We're also hostile to Monsters that have a different Id
            // than this one.
            Monster monster = actor as Monster;
            if (monster != null && monster.Id != Id)
                return true;

            return false;
        }

        public IEnumerable<Actor> AdjacentHostiles
        {
            get {
                return Location.AdjacentActors.Where (a => IsHostile (a));
            }
        }
        public override void TakeTurn ()
        {
            Action action = null;
            Location location = base.Location;

            // Possibly attack someone
            Actor enemy = AdjacentHostiles.FirstOrDefault ();
            if (enemy != null) {
                action = new AttackAction (this, enemy);
            }

            // Move towards the Hero
            if (action == null) {
                Location target = Game.Instance.Hero.Location;
                Direction direction = location.DirectionOf (target);

                if (base.CanMoveIn (direction))
                    action = new MoveAction (this, direction);
            }

            // Move randomly
            if (action == null)
                action = new RandomMoveAction (this);

            base.Action = action;
            base.TakeTurn ();
        }

        public string Id
        {
            get { return monster_id; }
        }

        public override string ToString ()
        {
            return String.Format ("{0} {1}", prefix, base.Name);
        }
    }
}

