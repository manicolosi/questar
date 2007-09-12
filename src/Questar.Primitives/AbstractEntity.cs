/*******************************************************************************
 *  AbstractEntity.cs: This is an implementation of Entity that
 *  subclasses can use.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Helpers;
using Questar.Maps;

namespace Questar.Primitives
{
    public abstract class AbstractEntity : Entity
    {
        public event EventHandler<LocationChangedEventArgs> LocationChanged;

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
            get {
                if (location == null)
                    location = new NullLocation ();

                return location;
            }
            set {
                Location old_loc = Location;
                location = value;

                FireLocationChanged (old_loc, location);
            }
        }

        public override string ToString ()
        {
            return name;
        }

        private void FireLocationChanged (Location old_loc, Location new_loc)
        {
            EventHelper.Raise<LocationChangedEventArgs> (this, LocationChanged,
                delegate (LocationChangedEventArgs args) {
                    args.OldLocation = old_loc;
                    args.NewLocation = new_loc;
                });
        }
    }
}

