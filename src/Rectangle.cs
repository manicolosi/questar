/*******************************************************************************
 *  Rectangle.cs: Represents a rectangular region of a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Base
{
    // NOTE: This is meant to only represent a rectanglular region of a
    // Map. Gdk.Rectangle should be used to represent areas within the
    // GUI or Cairo.Point should be used to represent areas within a
    // Cairo.Context.
    public struct Rectangle
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
            this (Point.Zero, width, height)
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
    }
}

