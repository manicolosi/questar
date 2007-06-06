//
// Monster.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;
using System.Collections.Generic;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;

namespace Questar.Actors
{
    public class Monster : Actor
    {
        public Monster (string tile, Map map)
        {
            base.Tile = tile;
            base.Name = "No Name";
            base.Map = map;

            Point p;
            do {
                p = Point.GetRandom (map.Width, map.Height);
            }
            while (!base.CanMoveTo (p));

            base.Location = p;
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
            List<Direction> potential_moves = new List<Direction> ();

            foreach (Direction direction in Direction.Directions) {
                if (base.CanMoveTo (direction))
                    potential_moves.Add (direction);
            }

            int index = random.Next (potential_moves.Count);
            return new MoveAction (this, potential_moves[index]);
        }
    }
}

