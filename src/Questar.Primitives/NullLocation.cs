/*******************************************************************************
 *  NullLocation.cs: A Location that represents an invalid or null
 *  Location.
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
    public class NullLocation : Location
    {
        public NullLocation ()
        {
        }

        public Map Map
        {
            get { return null; }
        }

        public Point Point
        {
            get { return Point.Zero; }
        }

        public Actor Actor
        {
            get { return null; }
        }

        public Item Item
        {
            get { return null; }
        }

        public bool IsAdjacentTo (Location loc)
        {
            return false;
        }

        public bool IsClear
        {
            get { return false; }
        }

        public IEnumerable<MapLocation> AdjacentLocations
        {
            get { return null; }
        }

        public IEnumerable<Actor> AdjacentActors
        {
            get { return null; }
        }

        public Direction DirectionOf (Location loc)
        {
            return Direction.None;
        }

        public GridInformation GridInformation
        {
            get { return GridInformation.Invalid; }
        }
    }
}

