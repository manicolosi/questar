/*******************************************************************************
 *  Tile.cs: A structure to store information about a tile.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;

using Cairo;
using Gdk;
using Rsvg;

using Pixbuf = Gdk.Pixbuf;

namespace Questar.Gui
{
    public struct Tile
    {
        private Handle handle;

        public int Width;
        public int Height;

        public Tile (string file_name, double zoom)
        {
            handle = new Handle (file_name);
            Width = handle.Dimensions.Width;
            Height = handle.Dimensions.Height;
        }

        public void Render (Context cr)
        {
            handle.RenderCairo (cr);
        }
    }
}

