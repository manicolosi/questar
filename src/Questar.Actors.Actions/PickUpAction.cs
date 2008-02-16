/*******************************************************************************
 *  PickUpAction.cs: Action that picks up an Item under an Actor.
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
    public class PickUpAction : AbstractAction
    {
        private Item item;

        public PickUpAction (Actor actor, Item item) : base (actor)
        {
            this.item = item;
        }

        public override void Execute ()
        {
            base.Actor.Location.AssertEqualTo (item.Location);

            item.Location = null;
            base.Actor.Inventory.Add (item);
        }
    }
}

