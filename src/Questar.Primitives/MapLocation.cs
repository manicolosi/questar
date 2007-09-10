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

        public MapLocation (Map map, Point point)
        {
            if (map == null)
                throw new ApplicationException ("Map is null");

            this.map = map;
            this.point = point;

            if (GridInformation == GridInformation.Invalid)
                throw new ApplicationException (
                    String.Format ("{0} is an invalid MapLocation", this));

        }

        public MapLocation (Location location) :
            this (location.Map, location.Point)
        {
        }

        public override Map Map
        {
            get { return map; }
        }

        public override Point Point
        {
            get { return point; }
        }

        public override Actor Actor
        {
            get { return map[point].Actor; }
        }

        public override Item Item
        {
            get { return map[point].Item; }
        }

        public override bool IsAdjacentTo (Location loc)
        {
            foreach (MapLocation adj_loc in AdjacentLocations) {
                if ((MapLocation) loc == adj_loc)
                    return true;
            }

            return false;
        }

        public override bool IsClear
        {
            get { return GridInformation == GridInformation.Clear; }
        }

        public override IEnumerable<Location> AdjacentLocations
        {
            get {
                Location adj_loc;

                foreach (Direction direction in Direction.Directions) {
                    adj_loc = direction.ApplyTo (this);
                    if (!(adj_loc is NullLocation))
                        yield return adj_loc;
                }
            }
        }

        public override IEnumerable<Actor> AdjacentActors
        {
            get {
                foreach (Location location in AdjacentLocations) {
                    if (location.Actor != null)
                        yield return location.Actor;
                }
            }
        }

        public override Direction DirectionOf (Location loc)
        {
            return point.DirectionOf (loc.Point);
        }

        public override GridInformation GridInformation
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
            if (!base.Equals (o))
                return false;

            MapLocation loc = o as MapLocation;
            if ((loc != null) && (Map == loc.Map) && (Point == loc.Point))
                return true;

            return false;
        }
    }
}

