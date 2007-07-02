/*******************************************************************************
 *  Item.cs: An item is an in game object that an Actor can carry around
 *  and use. Examples are a sword or a potion that recovers HP.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Actors;
using Questar.Primitives;

namespace Questar.Items
{
    public abstract class Item : Entity
    {
        private Actor owner = null;

        public Actor Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public bool IsOwned
        {
            get { return owner != null; }
        }
    }
}

