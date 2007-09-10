/*******************************************************************************
 *  PickUpAction.cs: Action that picks up an Item under an Actor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Items;
using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class PickUpAction : AbstractAction, Action
    {
        private Item item;

        public PickUpAction (Actor actor, Item item) : base (actor)
        {
            this.item = item;
        }

        public void Execute ()
        {
            if (item.Location is ActorLocation)
                throw new ImpossibleActionException (
                    "Item belongs to another Actor.");

            if (item.Location != base.Actor.Location)
                throw new ImpossibleActionException (
                    "Item is not under Actor.");

            base.Actor.Take (item);
        }
    }
}

