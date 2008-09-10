/*******************************************************************************
 *  Rectangle.cs: Represents a rectangular region of a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Questar.Primitives
{
    // NOTE: This is meant to only represent a rectanglular region of a
    // Map. Gdk.Rectangle should be used to represent areas within the
    // GUI or Cairo.Point should be used to represent areas within a
    // Cairo.Context.
    public struct Rectangle : IEnumerable<Point>
    {
        private const string out_of_range_message = "Must be greater than 0.";

        private Point start;
        private int width, height;

        public Rectangle (Point start, int width, int height)
        {
            Start = start;
            Width = width;
            Height = height;
        }

        public Rectangle (int width, int height) :
            this (Point.Zero, width, height)
        {
        }

        public Rectangle (int x, int y, int width, int height) :
            this (new Point (x, y), width, height)
        {
        }

        public Point Start
        {
            get { return start; }
            private set { start = value; }
        }

        public int X
        {
            get { return Start.X; }
        }

        public int Y
        {
            get { return Start.Y; }
        }

        public Point End
        {
            get { return new Point (X + Width - 1, Y + Height - 1); }
        }

        public int Width
        {
            get { return width; }
            private set {
                if (value < 1) {
                    throw new ArgumentOutOfRangeException ("Width", value, out_of_range_message);
                }

                width = value;
            }
        }

        public int Height
        {
            get { return height; }
            private set {
                if (value < 1) {
                    throw new ArgumentOutOfRangeException ("Height", value, out_of_range_message);
                }

                height = value;
            }
        }

        public Rectangle IntersectionWith (Rectangle other)
        {
            int x = Math.Max (X, other.X);
            int y = Math.Max (Y, other.Y);

            int w = Math.Min (End.X, other.End.X) - x + 1;
            int h = Math.Min (End.Y, other.End.Y) - y + 1;

            Rectangle rect;

            try {
                rect = new Rectangle (x, y, w, h);
            }
            catch (ArgumentOutOfRangeException e) {
                throw new ArgumentException (
                    "Rectangles do not intersect.", "other", e);
            }

            return rect;
        }

        public bool Contains (Point p)
        {
            return (p.X >= start.X) && (p.Y >= start.Y) &&
                (p.X < start.X + width) && (p.Y < start.Y + height);
        }

        public IEnumerator<Point> GetEnumerator ()
        {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    yield return (new Point (x, y) + start);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        public override string ToString ()
        {
            return String.Format ("{0}x{1}+{2}+{3}",
                start.X, start.Y, width, height);
        }
    }
}

