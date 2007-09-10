//
// MoveAction.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System; 

using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class MoveAction : AbstractAction, Action
    {
        private Direction direction;

        public MoveAction (Actor actor, Direction direction) : base (actor)
        {
            this.direction = direction;
        }

        public void Execute ()
        {
            Actor.Move (direction.ApplyTo (Actor.Location));
        }
    }
}

