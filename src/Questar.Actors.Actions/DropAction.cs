/*******************************************************************************
 *  DropAction.cs: Action that drops an Item under an Actor.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Extensions;
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
            base.Actor.Inventory.AssertContains (item);

            Actor.Inventory.Remove (item);
            item.Location = Actor.Location;
        }
    }
}

