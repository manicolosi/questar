/*******************************************************************************
 *  DrinkAction.cs: Action that drinks an Item.
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
    public class DrinkAction : AbstractAction
    {
        private IDrinkable item;

        public DrinkAction (Actor actor, IDrinkable item) : base (actor)
        {
            this.item = item;
        }

        public override void Execute ()
        {
            if (!(item.Location is ActorLocation))
                throw new ImpossibleActionException (
                    "Item is not on an ActorLocation.");

            if (item.Location.Actor != Actor)
                throw new ImpossibleActionException (
                    "Item doesn't belong this Actor.");

            Actor.Inventory.Remove (item);
            item.Drink (Actor);
            //item.Destroy ();
        }
    }
}

