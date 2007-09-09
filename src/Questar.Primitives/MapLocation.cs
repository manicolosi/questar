/*******************************************************************************
 *  MapLocation.cs: A Location used when an Entity is on a Map.
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
    public class MapLocation : Location
    {
        public static Location GetRandom (Map map)
        {
            return new MapLocation (
                map, Point.GetRandom (map.Width, map.Height));
        }

        private Map map;
        private Point point;

        public MapLocation (Map m, Point p)
        {
            map = m;
            point = p;

            if (GridInformation == GridInformation.Invalid)
                throw new ApplicationException (
                    String.Format ("{0} is an invalid MapLocation", this));

        }

        public Map Map
        {
            get { return map; }
        }

        public Point Point
        {
            get { return point; }
        }

        public Actor Actor
        {
            get { return map[point].Actor; }
        }

        public Item Item
        {
            get { return map[point].Item; }
        }

        public bool IsAdjacentTo (Location loc)
        {
            foreach (MapLocation adj_loc in AdjacentLocations) {
                if ((MapLocation) loc == adj_loc)
                    return true;
            }

            return false;
        }

        public bool IsClear
        {
            get { return GridInformation == GridInformation.Clear; }
        }

        public IEnumerable<MapLocation> AdjacentLocations
        {
            get {
                Location adj_loc;

                foreach (Direction direction in Direction.Directions) {
                    adj_loc = direction.ApplyTo (this);
                    if (!(adj_loc is NullLocation))
                        yield return adj_loc as MapLocation;
                }
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

        public Direction DirectionOf (Location loc)
        {
            return point.DirectionOf (loc.Point);
        }

        public GridInformation GridInformation
        {
            get { return map.GetGridInformation (point); }
        }

        public override string ToString ()
        {
            return String.Format ("{0}: {1}", map, point);
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            MapLocation loc = o as MapLocation;

            if (this.map == loc.map && this.Point == loc.Point)
                return true;

            return false;
        }

        public static bool operator == (MapLocation a, MapLocation b)
        {
            return a.Equals (b);
        }

        public static bool operator != (MapLocation a, MapLocation b)
        {
            return !(a == b);
        }
    }
}

