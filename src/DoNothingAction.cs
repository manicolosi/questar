using System;

namespace Questar.Actors
{
    public class DoNothingAction : IAction
    {
        public DoNothingAction (IActionable actor)
        {
        }

        public void Execute ()
        {
        }
    }
}

