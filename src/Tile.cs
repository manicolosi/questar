using Cairo;
using Gdk;
using Rsvg;
using System;

namespace Questar.Gui
{
    public struct Tile : IDisposable
    {
        public readonly string Filename;
        public readonly Gdk.Pixbuf Pixbuf;
        //public readonly SvgSurface Surface;

        public Tile (string filename, double zoom)
        {
            Filename = filename;
            Pixbuf = Rsvg.Pixbuf.FromFileAtZoom (filename, zoom, zoom);
            //Surface = new SvgSurface (filename, zoom * 64, zoom * 64);
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

