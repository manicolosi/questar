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

            Game.Instance.World.AddActor (this);
        }

        public override bool IsTurnReady
        {
            get { return true; }
        }

        public override IAction Action
        {
            get {
                Point target = Game.Instance.World.Hero.Location;
                Direction direction = base.Location.DirectionOf (target);

                if (base.CanMoveTo (direction))
                    return new MoveAction (this, direction);
                else
                    return CreateRandomMoveAction ();
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

