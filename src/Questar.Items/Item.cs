/*******************************************************************************
 *  Item.cs: An item is an in game object that an Actor can carry around
 *  and use. Examples are a sword or a potion that recovers HP.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Maps;
using Questar.Primitives;

namespace Questar.Items
{
    public abstract class Item : IEntity
    {
        private Point location;
        private Map map;
        private string tile;

        public virtual Point Location
        {
            get { return location; }
            protected set { location = value; }
        }

        public virtual Map Map
        {
            get { return map; }
            protected set { map = value; }
        }

        public virtual string Tile
        {
            get { return tile; }
            protected set { tile = value; }
        }
    }
}

