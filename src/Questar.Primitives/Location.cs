/*******************************************************************************
 *  Location.cs: Represents an Entity's location.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Maps;

namespace Questar.Primitives
{
    public class Location
    {
        public static Location GetRandom (Map map)
        {
            return new Location (map, Point.GetRandom (map.Width, map.Height));
        }

        private Map map;
        private Point point;

        public Location (Map m, Point p)
        {
            map = m;
            point = p;
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public IEnumerable<Location> AdjacentLocations
        {
            get {
                foreach (Direction direction in Direction.Directions)
                    yield return new Location (Map, direction.ApplyTo (Point));
            }
        }

        public IEnumerable<Actor> AdjacentActors
        {
            get {
                foreach (Location location in AdjacentLocations) {
                    if (location.Actor != null)
                        yield return location.Actor;
                }
            }
        }

        public bool IsAdjacentTo (Location loc)
        {
            foreach (Location adjacent_loc in AdjacentLocations) {
                if (loc == adjacent_loc)
                    return true;
            }

            return false;
        }

        public Direction DirectionOf (Location loc)
        {
            return point.DirectionOf (loc.Point);
        }

        public GridInformation GridInformation
        {
            get { return map.GetGridInformation (point); }
        }

        public bool IsClear
        {
            get { return GridInformation == GridInformation.Clear; }
        }

        public bool IsInvalid
        {
            get { return GridInformation == GridInformation.Invalid; }
        }

        public Actor Actor
        {
            get {
                if (IsInvalid)
                    return null;

                return map[point].Actor;
            }
        }

        public override string ToString ()
        {
            return String.Format ("{0}: {1}", map, point);
        }

        public override bool Equals (object o)
        {
            return this == (Location) o;
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public static bool operator == (Location a, Location b)
        {
            if (a.map == b.map && a.Point == b.Point)
                return true;

            return false;
        }

        public static bool operator != (Location a, Location b)
        {
            return !(a == b);
        }
    }
}

