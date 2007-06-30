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
    public class RandomMoveAction : IAction
    {
        private Actor actor;
        private Random random = new Random ();

        public RandomMoveAction (Actor actor)
        {
            this.actor = actor;
        }

        public void Execute ()
        {
            List<Direction> potentials = new List<Direction> ();

            foreach (Direction direction in Direction.Directions) {
                if (actor.CanMoveTo (direction))
                    potentials.Add (direction);
            }

            if (potentials.Count == 0) {
                IAction do_nothing = new DoNothingAction (actor);
                do_nothing.Execute ();
            }
            else {
                int index = random.Next (potentials.Count);
                actor.Move (potentials[index].ApplyToPoint (actor.Location));
            }
        }
    }
}

