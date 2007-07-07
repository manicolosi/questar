/*******************************************************************************
 *  PickUpAction.cs: Action that picks up an Item under an Actor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Items;

namespace Questar.Actors.Actions
{
    public class PickUpAction : IAction
    {
        private Actor actor;
        private Item item;

        public PickUpAction (Actor actor, Item item)
        {
            this.actor = actor;
            this.item = item;
        }

        public void Execute ()
        {
            if (item.IsOwned)
                throw new ImpossibleActionException
                    ("Item is already owned.");

            if (item.Map != actor.Map || item.Location != actor.Location)
                throw new ImpossibleActionException
                    ("Item is not under Actor.");

            actor.Inventory.Add (item);
        }
    }
}

