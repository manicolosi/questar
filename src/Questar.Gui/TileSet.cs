/*******************************************************************************
 *  TileSet.cs: Manages a collection of Tile objects.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gdk;
using Rsvg;
using System;
using System.Collections.Generic;
using System.IO;

using Questar.Helpers;
using Questar.Configuration;

using EventHelper = Questar.Helpers.EventHelper;

namespace Questar.Gui
{
    public enum ZoomSetting
    {
        Smallest = 50,
        Small    = 75,
        Normal   = 100,
        Large    = 125,
        Largest  = 150
    }

    public class TileSet
    {
        private const string tile_set_directory = "../tilesets";

        public static IEnumerable<string> AvailableTileSets
        {
            get {
                string [] directories =
                    Directory.GetDirectories (tile_set_directory);

                foreach (string directory in directories)
                    yield return Path.GetFileName (directory);
            }
        }

        private Dictionary<string, Tile> tiles =
            new Dictionary<string, Tile> ();

        private int width;
        private int height;
        private ZoomSetting zoom;

        private string name;

        public event EventHandler TileSetChanged;

        public TileSet ()
        {
            zoom = UISchema.Zoom.Value;
            name = UISchema.TileSet.Value;

            SetupHandlers ();
            LoadPixbufs ();
        }

        private void SetupHandlers ()
        {
            // Schema Notify Events
            UISchema.TileSet.Changed += delegate {
                name = UISchema.TileSet.Value;
                LoadPixbufs ();
                EventHelper.Raise (this, TileSetChanged);
            };
            UISchema.Zoom.Changed += delegate {
                zoom = UISchema.Zoom.Value;
                LoadPixbufs ();
                EventHelper.Raise (this, TileSetChanged);
            };

            // UI Action Events
            UIActions.Instance["ZoomIn"].Activated += delegate {
                if (zoom != ZoomSetting.Largest) {
                    Zoom += 25;
                }
            };
            UIActions.Instance["ZoomOut"].Activated += delegate {
                if (zoom != ZoomSetting.Smallest) {
                    Zoom -= 25;
                }
            };
            UIActions.Instance["NormalSize"].Activated += delegate {
                if (zoom != ZoomSetting.Normal) {
                    Zoom = ZoomSetting.Normal;
                }
            };
        }

        public int Width
        {
            get { return tiles["default"].Width; }
        }

        public int Height
        {
            get { return tiles["default"].Width; }
        }

        public double ZoomPercentage
        {
            get { return ((int) zoom) / 100.0; }
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

        private ZoomSetting Zoom
        {
            get {
                return zoom;
            }
            set {
                zoom = value;
                UISchema.Zoom.Value = zoom;
            }
        }

        private string Name
        {
            get { return name; }
            set {
                name = value;
                UISchema.TileSet.Value = name;
            }
        }

        private string TileSetDirectory
        {
            get { return Path.Combine (tile_set_directory, Name); }
        }

        private void LoadPixbufs ()
        {
            UIActions.Instance["ZoomIn"].Sensitive  =
                zoom != ZoomSetting.Largest;
            UIActions.Instance["ZoomOut"].Sensitive =
                zoom != ZoomSetting.Smallest;

            tiles.Clear ();

            if (!Directory.Exists (TileSetDirectory)) {
                Name = UISchema.TileSet.DefaultValue;
            }

            string [] files = Directory.GetFiles (TileSetDirectory, "*.svg");

            foreach (string file in files) {
                string key = Path.GetFileNameWithoutExtension (file);
                tiles.Add (key, new Tile (file, ZoomPercentage));
            }
        }
    }
}

