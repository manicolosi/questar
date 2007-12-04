/*******************************************************************************
 *  Point.cs: Represents a single point in a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Primitives
{
    // NOTE: This is meant to only represent grids inside a Map.
    // Gdk.Point should be used to represent points within the GUI or
    // Cairo.Point should be used to represent points within a
    // Cairo.Context.
    public struct Point
    {
        public static Point Zero
        {
            get { return new Point (0, 0); }
        }

        public int X;
        public int Y;

        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        public Direction DirectionOf (Point target)
        {
            Point delta = target - this;

            delta.X = Math.Max (Math.Min (delta.X, 1), -1);
            delta.Y = Math.Max (Math.Min (delta.Y, 1), -1);

            foreach (Direction direction in Direction.All) {
                if (direction.DeltaX == delta.X && direction.DeltaY == delta.Y)
                    return direction;
            }

            return Direction.None;
        }

        private static Random random;
        public static Point GetRandom (int width, int height)
        {
            if (random == null)
                random = new Random ();
            return new Point (random.Next (width), random.Next (height));
        }

        public override string ToString ()
        {
            return String.Format ("({0},{1})", X, Y);
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            return this == (Point) o;
        }

        public static bool operator == (Point a, Point b)
        {
            if (a.X != b.X || a.Y != b.Y)
                return false;

            return true;
        }

        public static bool operator != (Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator - (Point a, Point b)
        {
            return new Point (a.X - b.X, a.Y - b.Y);
        }

        public static Point operator + (Point a, Point b)
        {
            return new Point (a.X + b.X, a.Y + b.Y);
        }
    }
}

