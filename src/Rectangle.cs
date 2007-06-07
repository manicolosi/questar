/*******************************************************************************
 *  Rectangle.cs: Represents a rectangular region of a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Questar.Base
{
    // NOTE: This is meant to only represent a rectanglular region of a
    // Map. Gdk.Rectangle should be used to represent areas within the
    // GUI or Cairo.Point should be used to represent areas within a
    // Cairo.Context.
    public struct Rectangle : IEnumerable<Point>
    {
        private Point start;
        private int width, height;

        public Rectangle (Point start, int width, int height)
        {
            this.start = start;
            this.width = width;
            this.height = height;
        }

        public Rectangle (int width, int height) :
            this (new Point (), width, height)
        {
        }

        public Rectangle (int x, int y, int width, int height) :
            this (new Point (x, y), width, height)
        {
        }

        public Point Start
        {
            get { return start; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public IEnumerator<Point> GetEnumerator ()
        {
            return new Enumerator (this);
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return new Enumerator (this);
        }

        private struct Enumerator : IEnumerator<Point>
        {
            Rectangle rectangle;
            Point current;
            Point end;

            public Enumerator (Rectangle rect)
            {
                rectangle = rect;
                current = rectangle.Start;
                end = new Point (
                    rect.Start.X + rect.Width - 1,
                    rect.Start.Y + rect.Height - 1);
                Reset ();
            }

            public Point Current
            {
                get { return current; }
            }

            object IEnumerator.Current
            {
                get { return current; }
            }

            public bool MoveNext ()
            {
                current.X++;

                if (current.X > end.X) {
                    current.X = rectangle.Start.X;
                    current.Y++;
                }

                if (current.Y > end.Y)
                    return false;

                return true;
            }

            public void Reset ()
            {
                current = rectangle.Start;
                current.X--;
            }

            public void Dispose ()
            {
            }
        }
    }
}

