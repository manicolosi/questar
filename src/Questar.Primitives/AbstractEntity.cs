/*******************************************************************************
 *  AbstractEntity.cs: This is an implementation of Entity that
 *  subclasses can use.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Maps;

namespace Questar.Primitives
{
    public abstract class AbstractEntity : Entity
    {
        private string tile;
        private string name;
        private string description;
        private Location location;

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

        public virtual Location Location
        {
            get { return location; }
            set { location = value; }
        }

        public override string ToString ()
        {
            return name;
        }
    }
}

