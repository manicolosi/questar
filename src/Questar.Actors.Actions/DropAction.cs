/*******************************************************************************
 *  DropAction.cs: Action that drops an Item under an Actor.
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
    public class DropAction : AbstractAction
    {
        private Item item;

        public DropAction (Actor actor, Item item) : base (actor)
        {
            this.item = item;
        }

        public override void Execute ()
        {
            if (!(item.Location is ActorLocation))
                throw new ImpossibleActionException (
                    "Item is not on a ActorLocation.");

            if (item.Location.Actor != Actor)
                throw new ImpossibleActionException (
                    "Item doesn't belong to Actor.");

            Actor.Inventory.Remove (item);
            item.Location = Actor.Location;
        }
    }
}

