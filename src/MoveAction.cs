using System; 

using Questar.Base;

namespace Questar.Actors
{
    public class MoveAction : IAction
    {
        private Direction direction;
        private IActionable actor;

        public MoveAction (IActionable actor, Direction direction)
        {
            this.actor = actor;
            this.direction = direction;
        }

        public void Execute ()
        {
            actor.Move (direction.ApplyToPoint (actor.Location));
        }

        public Direction Direction
        {
            get { return direction; }
        }
    }
}

