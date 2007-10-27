/*******************************************************************************
 *  MoveTowardsTargetAction.cs: An Action that can move an Actor targets
 *  a Target. Later this will take into account path finding.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System; 

using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class MoveTowardsTargetAction : AbstractAction, Action
    {
        private Entity entity_target;

        private MoveAction move_action;
        private Action fallback_action;

        public MoveTowardsTargetAction (Actor actor, Entity target) :
            base (actor)
        {
            entity_target = target;
            move_action = new MoveAction (actor);
            fallback_action = new RandomMoveAction (actor);
        }

        public override void Execute ()
        {
            Location loc = base.Actor.Location;
            Location target = entity_target.Location;

            move_action.Direction = loc.DirectionOf (target);

            Action action;

            if (base.Actor.CanMoveIn (move_action.Direction))
                action = move_action;
            else
                action = fallback_action;

            action.Execute ();
        }
    }
}

