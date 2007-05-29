using System;

namespace Questar.Actors.Actions
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

