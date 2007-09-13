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
    public class PickUpAction : AbstractAction
    {
        private Item item;

        public PickUpAction (Actor actor, Item item) : base (actor)
        {
            this.item = item;
        }

        public override void Execute ()
        {
            if (!(item.Location is MapLocation))
                throw new ImpossibleActionException (
                    "Item is not on a MapLocation.");

            if (item.Location != Actor.Location)
                throw new ImpossibleActionException (
                    "Item is not under Actor.");

            Actor.Inventory.Add (item);
        }
    }
}

