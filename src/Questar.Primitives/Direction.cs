/*******************************************************************************
 *  Direction.cs: Represents the eight directions.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

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

        private static List<Direction> directions =
            new List<Direction> (Directions);

        public static IEnumerable<Direction> Directions
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

        public static Direction GetRandom ()
        {
            Random random = new Random ();
            int index = random.Next (directions.Count);

            return directions[index];
        }

        private string name;
        private int x;
        private int y;

        private Direction (string name, int x, int y)
        {
            if (x > 1 || x < -1 || y > 1 || y < -1)
                throw new ArgumentException (
                    "Arguments x and y must be between -1 and 1.");

            this.name = name;
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Point ApplyToPoint (Point p)
        {
            return new Point (p.X + X, p.Y + Y);
        }

        public override string ToString ()
        {
            return name;
        }
    }
}

