/*******************************************************************************
 *  FactoryLocation.cs: Create Locations.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Maps;

namespace Questar.Primitives
{
    public static class LocationFactory
    {
        public static Location Create (Map map, Point p)
        {
            Location loc;

            if (map.GetGridInformation (p) == GridInformation.Invalid)
                loc = new NullLocation ();
            else
                loc = new MapLocation (map, p);

            return loc;
        }

        public static Location Create (Actor actor)
        {
            return new ActorLocation (actor);
        }

        public static Location CreateRandom (Map map)
        {
            return new MapLocation (
                map, Point.GetRandom (map.Width, map.Height));
        }

    }
}

