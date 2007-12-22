/*******************************************************************************
 *  Direction.cs: Represents the eight directions.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Extensions;
using Questar.Maps;

namespace Questar.Primitives
{
    public struct Direction
    {
        public static readonly Direction None =
            new Direction ("none", 0, 0);
        public static readonly Direction North =
            new Direction ("north", 0, -1);
        public static readonly Direction NorthEast =
            new Direction ("northeast", 1, -1);
        public static readonly Direction East =
            new Direction ("east", 1, 0);
        public static readonly Direction SouthEast =
            new Direction ("southeast", 1, 1);
        public static readonly Direction South =
            new Direction ("south", 0, 1);
        public static readonly Direction SouthWest =
            new Direction ("southwest", -1, 1);
        public static readonly Direction West =
            new Direction ("west", -1, 0);
        public static readonly Direction NorthWest =
            new Direction ("northwest", -1, -1);

        public static IEnumerable<Direction> All
        {
            get {
                yield return North;
                yield return NorthEast;
                yield return East;
                yield return SouthEast;
                yield return South;
                yield return SouthWest;
                yield return West;
                yield return NorthWest;
            }
        }

        public static IEnumerable<Direction> AllIncludingNone
        {
            get {
                yield return Direction.None;

                foreach (Direction d in All)
                    yield return d;
            }
        }

        public static Direction GetWithDeltas (int dx, int dy)
        {
            dx.CheckInRange ("dx", -1, 1);
            dy.CheckInRange ("dy", -1, 1);

            return Direction.AllIncludingNone
                .Where (d => d.DeltaX == dx && d.DeltaY == dy)
                .First ();
        }

        private static Random random;

        public static Direction GetRandom ()
        {
            if (random == null)
                random = new Random ();

            return GetRandom (random);
        }

        public static Direction GetRandom (Random random)
        {
            return All.Random (random);
        }

        private readonly string name;
        private readonly int dx;
        private readonly int dy;

        private Direction (string name, int dx, int dy)
        {
            dx.CheckInRange ("dx", -1, 1);
            dy.CheckInRange ("dy", -1, 1);

            this.name = name;
            this.dx = dx;
            this.dy = dy;
        }

        public int DeltaX
        {
            get { return dx; }
        }

        public int DeltaY
        {
            get { return dy; }
        }

        public string Name
        {
            get { return name; }
        }

        public Point ApplyTo (Point p)
        {
            return new Point (p.X + DeltaX, p.Y + DeltaY);
        }

        public Location ApplyTo (Location loc)
        {
            Point p = ApplyTo (loc.Point);
            if (loc.Map.GetGridInformation (p) == GridInformation.Invalid)
                return new NullLocation ();

            return new MapLocation (loc.Map, p);
        }

        public override string ToString ()
        {
            return name;
        }
    }
}

