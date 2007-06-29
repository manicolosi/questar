//
// Monster.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;
using System.Collections.Generic;

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
            base.Map = Game.Instance.World.Map;
            base.HitPoints = new HitPoints (definition.MaxHP);

            monster_id = definition.Id;
            prefix = definition.Prefix;

            Point p;
            do {
                p = Point.GetRandom (base.Map.Width, base.Map.Height);
            }
            while (!base.CanMoveTo (p));
            base.Location = p;

            base.OnCreation ();
        }

        public override bool IsTurnReady
        {
            get { return true; }
        }

        // FIXME: This note means this isn't clear. First see if there
        // is another Actor adjacent to us. If there is and the other
        // Actor is the Hero or a Monster of a different type, attack
        // it. If there's nobody to attack, go towards the Hero if
        // possible otherwise move randomly.
        public override IAction Action
        {
            get {
                IAction action = null;
                Map map = base.Map;
                Point location = base.Location;

                foreach (Direction direction in Direction.Directions) {
                    Point p = direction.ApplyToPoint (location);
                    GridInformation info = map.GetGridInformation (p);

                    if (info == GridInformation.Occupied) {
                        Actor actor = map[p].Actor;
                        Monster monster = actor as Monster;

                        if ((monster != null && monster.Id != Id) ||
                            actor is Hero)
                            action = new AttackAction (this, actor);
                    }
                }

                if (action == null) {
                    Point target = Game.Instance.World.Hero.Location;
                    Direction direction = location.DirectionOf (target);

                    if (base.CanMoveTo (direction))
                        action = new MoveAction (this, direction);
                    else
                        action = CreateRandomMoveAction ();
                }

                return action;
            }
        }

        private Random random = new Random ();

        private IAction CreateRandomMoveAction ()
        {
            List<Direction> potentials = new List<Direction> ();

            foreach (Direction direction in Direction.Directions) {
                if (base.CanMoveTo (direction))
                    potentials.Add (direction);
            }

            if (potentials.Count == 0)
                return new DoNothingAction (this);

            int index = random.Next (potentials.Count);
            return new MoveAction (this, potentials[index]);
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

