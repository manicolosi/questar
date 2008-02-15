/*******************************************************************************
 *  Location.cs: Represents where something is (map and position).
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Items;
using Questar.Maps;

namespace Questar.Primitives
{
    public class Location
    {
        public static Location GetRandom (Map map)
        {
            return new Location (map, Point.GetRandom (map.Width, map.Height));
        }

        public Map Map { get; private set; }
        public Point Position { get; private set; }

        public Location (Map map, Point position)
        {
            Console.WriteLine ("New Location: {0}", position);

            if (map == null)
                throw new ApplicationException ("Map is null");

            Map = map;
            Position = position;

            if (GridInformation == GridInformation.Invalid)
                throw new ApplicationException (
                    String.Format ("{0} is an invalid Location", this));
        }

        public Location (Map map, int x, int y) : this (map, new Point (x, y))
        {
        }

        public bool IsActorHere
        {
            get { return Actor != null; }
        }

        public Actor Actor
        {
            get { return Map[Position].Actor; }
        }

        public bool IsItemHere
        {
            get { return Item != null; }
        }

        public Item Item
        {
            get { return Map[Position].Item; }
        }

        public bool IsAdjacentTo (Location loc)
        {
            foreach (Location adj_loc in AdjacentLocations) {
                if (loc == adj_loc)
                    return true;
            }

            return false;
        }

        public bool IsClear
        {
            get { return GridInformation == GridInformation.Clear; }
        }

        public IEnumerable<Location> AdjacentLocations
        {
            get {
                Location loc;

                foreach (Direction direction in Direction.All) {
                    loc = direction.ApplyTo (this);
                    if (loc != null)
                        yield return loc;
                }
            }
        }

        public IEnumerable<Actor> AdjacentActors
        {
            get {
                foreach (Location loc in AdjacentLocations) {
                    if (loc.Actor != null)
                        yield return loc.Actor;
                }
            }
        }

        public IEnumerable<Location> LocationsInRadius (int radius)
        {
            // FIXME: Should be using a circle instead of a rectangle,
            // but this is quick and easy...
            int width = (radius * 2) + 1;
            Point start = new Point (
                this.Position.X - radius, this.Position.Y - radius);
            Rectangle rect = new Rectangle (start, width, width);

            Location loc;

            foreach (Point p in rect) {
                loc = new Location (Map, p);

                if (loc != null)
                    yield return loc;
            }
        }

        public IEnumerable<Actor> ActorsInRadius (int radius)
        {
            foreach (Location loc in LocationsInRadius (radius)) {
                if (loc.Actor != null)
                    yield return loc.Actor;
            }
        }

        public Direction DirectionOf (Location loc)
        {
            return Position.DirectionOf (loc.Position);
        }

        public GridInformation GridInformation
        {
            get { return Map.GetGridInformation (Position); }
        }

        public override string ToString ()
        {
            return String.Format ("{0}: {1}", Map, Position);
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            if (!base.Equals (o))
                return false;

            Location l = o as Location;
            if ((l != null) && (Map == l.Map) && (Position == l.Position))
                return true;

            return false;
        }

        public static bool operator == (Location a, Location b)
        {
            return Object.Equals (a, b);
        }

        public static bool operator != (Location a, Location b)
        {
            return !Object.Equals (a, b);
        }
    }
}

