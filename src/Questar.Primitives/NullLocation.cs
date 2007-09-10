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

        public override Map Map
        {
            get { return null; }
        }

        public override Point Point
        {
            get { return Point.Zero; }
        }

        public override Actor Actor
        {
            get { return null; }
        }

        public override Item Item
        {
            get { return null; }
        }

        public override bool IsAdjacentTo (Location loc)
        {
            return false;
        }

        public override bool IsClear
        {
            get { return false; }
        }

        public override IEnumerable<MapLocation> AdjacentLocations
        {
            get { return null; }
        }

        public override IEnumerable<Actor> AdjacentActors
        {
            get { return null; }
        }

        public override Direction DirectionOf (Location loc)
        {
            return Direction.None;
        }

        public override GridInformation GridInformation
        {
            get { return GridInformation.Invalid; }
        }
    }
}

