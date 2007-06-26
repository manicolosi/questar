//
// MoveAction.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System; 

using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class MoveAction : IAction
    {
        private Direction direction;
        private Actor actor;

        public MoveAction (Actor actor, Direction direction)
        {
            this.actor = actor;
            this.direction = direction;
        }

        public void Execute ()
        {
            actor.Move (direction.ApplyToPoint (actor.Location));
        }
    }
}

