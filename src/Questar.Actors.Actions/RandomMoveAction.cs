/*******************************************************************************
 *  RandomMoveAction.cs: Moves an Actor in a random direction
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System; 
using System.Collections.Generic;

using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class RandomMoveAction : AbstractAction
    {
        public RandomMoveAction (Actor actor) : base (actor)
        {
        }

        public override void Execute ()
        {
            List<Direction> potentials = GetPotentialMoves ();
            Action action = CreateMoveAction (potentials);

            if (action == null)
                action = new DoNothingAction (Actor);

            action.Execute ();
        }

        private Action CreateMoveAction (List<Direction> potentials)
        {
            if (potentials.Count != 0)
                return new MoveAction (Actor, RandomDirection (potentials));

            return null;
        }

        private List<Direction> GetPotentialMoves ()
        {
            List<Direction> potentials = new List<Direction> ();

            foreach (Direction direction in Direction.Directions) {
                if (Actor.CanMoveIn (direction))
                    potentials.Add (direction);
            }

            return potentials;
        }

        private Direction RandomDirection (List<Direction> directions)
        {
            Random random = new Random ();
            int index = random.Next (directions.Count);

            return directions[index];
        }
    }
}

