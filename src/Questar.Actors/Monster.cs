/*******************************************************************************
 *  AbstractActor.cs: Actors that are controlled by Questar.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors.AI;
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
            base.AI = new StupidAI (this);

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

        public override void TakeTurn ()
        {
            Action action = null;
            Location location = base.Location;

            // Possibly attack someone
            foreach (Actor actor in location.AdjacentActors) {
                if (IsHostile (actor))
                    action = new AttackAction (this, actor);
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

