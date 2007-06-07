/*******************************************************************************
 *  Point.cs: Represents a single point in a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Base
{
    // NOTE: This is meant to only represent grids inside a Map.
    // Gdk.Point should be used to represent points within the GUI or
    // Cairo.Point should be used to represent points within a
    // Cairo.Context.
    public struct Point : ICloneable
    {
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

            foreach (Direction direction in Direction.Directions) {
                if (direction.X == delta.X && direction.Y == delta.Y)
                    return direction;
            }

            return Direction.None;
        }

        public static Point GetRandom (int width, int height)
        {
            Random random = new Random ();
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

        public object Clone ()
        {
            return this.MemberwiseClone ();
        }
    }
}

