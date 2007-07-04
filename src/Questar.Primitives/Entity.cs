/*******************************************************************************
 *  Entity.cs: This is the base class for Item and Actor objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Maps;

namespace Questar.Primitives
{
    public abstract class Entity
    {
        private string tile;
        private string name;
        private string description;
        private Point location;
        private Map map;

        public virtual string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        public virtual string Description
        {
            get { return description; }
            protected set { description = value; }
        }

        public virtual string Tile
        {
            get { return tile; }
            protected set { tile = value; }
        }

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

        public override string ToString ()
        {
            return name;
        }
    }
}

