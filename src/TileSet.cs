//
// TileSet.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Gdk;
using Rsvg;
using System;
using System.Collections.Generic;
using System.IO;

using Questar.Base;
using Questar.Configuration;

namespace Questar.Gui
{
    public class TileSet
    {
        private const int Smallest = 50;
        private const int Small    = 75;
        private const int Normal   = 100;
        private const int Large    = 125;
        private const int Largest  = 150;

        private const string tile_set_directory = "../tilesets";

        public static IEnumerable<string> AvailableTileSets
        {
            get {
                string [] directories =
                    Directory.GetDirectories (tile_set_directory);

                foreach (string directory in directories) {
                    yield return Path.GetFileName (directory);
                }
            }
        }

        private Dictionary<string, Tile> tiles =
            new Dictionary<string, Tile> ();

        private int width;
        private int height;
        private int zoom;

        private string name;

        public event EventHandler<EventArgs> ZoomChanged;

        public TileSet ()
        {
            zoom = UISchema.Zoom.Get ();
            name = UISchema.TileSet.Get ();

            LoadPixbufs ();

            UIActions.Instance["ZoomIn"].Activated += delegate {
                if (zoom != Largest) {
                    Zoom += 25;
                }
            };
            UIActions.Instance["ZoomOut"].Activated += delegate {
                if (zoom != Smallest) {
                    Zoom -= 25;
                }
            };
            UIActions.Instance["NormalSize"].Activated += delegate {
                if (zoom != Normal) {
                    Zoom = Normal;
                }
            };
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public double ZoomPercentage
        {
            get { return ((int) zoom) / 100.0; }
        }

        public string Name
        {
            get { return name; }
            set {
                name = value;
                UISchema.TileSet.Set (name);
            }
        }

        public Tile this[string key]
        {
            get {
                Tile value;

                if (!tiles.TryGetValue (key, out value))
                    throw new ArgumentOutOfRangeException ("key",
                        String.Format ("'{0}' is an invalid tile key.", key));

                return value;
            }
        }

        private int Zoom
        {
            get {
                return zoom;
            }
            set {
                zoom = value;

                LoadPixbufs ();

                Events.FireEvent (this, ZoomChanged);

                UIActions.Instance["ZoomIn"].Sensitive  =
                    zoom != Largest;
                UIActions.Instance["ZoomOut"].Sensitive =
                    zoom != Smallest;

                UISchema.Zoom.Set (zoom);
            }
        }

        private string TileSetDirectory
        {
            get { return Path.Combine (tile_set_directory, Name); }
        }

        private void LoadPixbufs ()
        {
            foreach (Tile tile in tiles.Values) {
                tile.Dispose ();
            }
            tiles.Clear ();

            if (!Directory.Exists (TileSetDirectory)) {
                Name = UISchema.TileSet.DefaultValue;
            }

            string [] files = Directory.GetFiles (TileSetDirectory, "*.svg");

            foreach (string file in files) {
                string key = Path.GetFileNameWithoutExtension (file);
                tiles.Add (key, new Tile (file, ZoomPercentage));
            }

            width = tiles["default"].Pixbuf.Width;
            height = tiles["default"].Pixbuf.Height;
        }
    }
}

