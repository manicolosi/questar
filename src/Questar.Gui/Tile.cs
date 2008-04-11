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
    public struct Tile : IDisposable
    {
        public readonly string FileName;
        public readonly Pixbuf Pixbuf;
        //public readonly SvgSurface Surface;

        public Tile (string file_name, double zoom)
        {
            FileName = file_name;

            Handle handle = new Handle (file_name);
            Pixbuf = handle.Pixbuf;

            //Surface = new RsvgSurface (filename);
        }

        public void Dispose ()
        {
            // FIXME: This should work, but doesn't.
            //Pixbuf.Dispose ();

#pragma warning disable 0612
            Pixbuf.Unref ();
#pragma warning restore 0612
        }
    }
}

