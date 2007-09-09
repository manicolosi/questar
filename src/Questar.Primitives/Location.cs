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
    public interface Location
    {
        Map Map { get; }
        Point Point { get; }
        Actor Actor { get; }
        Item Item { get; }

        bool IsAdjacentTo (Location loc);
        bool IsClear { get; }

        IEnumerable<MapLocation> AdjacentLocations { get; }
        IEnumerable<Actor> AdjacentActors { get; }

        Direction DirectionOf (Location loc);

        GridInformation GridInformation { get; }
    }
}

