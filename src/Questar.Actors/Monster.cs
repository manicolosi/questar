//
// Monster.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;
using Questar.Primitives;

namespace Questar.Actors
{
    public class Monster : Actor
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
                base.Location = Location.GetRandom (Game.Instance.World.Map);
            }
            while (!base.CanMoveTo (base.Location));

            base.OnCreation ();
        }

        public override bool IsTurnReady
        {
            get { return true; }
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

        public override IAction Action
        {
            get {
                IAction action = null;
                Location location = base.Location;

                // Possible attack someone
                foreach (Actor actor in location.AdjacentActors) {
                    if (IsHostile (actor))
                        action = new AttackAction (this, actor);
                }

                // Move towards the Hero
                if (action == null) {
                    Location target = Game.Instance.World.Hero.Location;
                    Direction direction = location.DirectionOf (target);

                    if (base.CanMoveTo (direction))
                        action = new MoveAction (this, direction);
                }

                // Move randomly
                if (action == null)
                    action = new RandomMoveAction (this);

                return action;
            }
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

