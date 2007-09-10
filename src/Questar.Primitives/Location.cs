/*******************************************************************************
 *  Location.cs: Represents an Entity's location.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Items;
using Questar.Maps;

namespace Questar.Primitives
{
    public abstract class Location
    {
        public abstract Map Map { get; }
        public abstract Point Point { get; }
        public abstract Actor Actor { get; }
        public abstract Item Item { get; }

        public abstract bool IsAdjacentTo (Location loc);
        public abstract bool IsClear { get; }

        public abstract IEnumerable<MapLocation> AdjacentLocations { get; }
        public abstract IEnumerable<Actor> AdjacentActors { get; }

        public abstract Direction DirectionOf (Location loc);

        public abstract GridInformation GridInformation { get; }

        public override bool Equals (Object o)
        {
            if ((o == null) || base.GetType () != o.GetType ())
                return false;

            return true;
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public static bool operator == (Location a, Location b)
        {
            return a.Equals (b);
        }

        public static bool operator != (Location a, Location b)
        {
            return !a.Equals (b);
        }
    }
}

