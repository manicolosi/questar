//
// MapView.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Cairo;
using Gdk;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Base;
using Questar.Configuration;
using Questar.Maps;

using Point = Questar.Base.Point;
using GC = Gdk.GC;
using Color = Cairo.Color;

namespace Questar.Gui
{
    public class MapView : Gtk.DrawingArea
    {
        private TileSet tileset;
        private Map map;

        private bool show_grid_lines;
        private bool show_highlight = false;
        private Point highlight_grid;

        private int offset_x;
        private int offset_y;

        private int poffset_x;
        private int poffset_y;

        private List<Point> redraw_grids = new List<Point> ();
        private bool hero_moved;

        Color fill_color;

        Context context;

        public MapView (Map map)
        {
            this.map = map;

            tileset = new TileSet ();

            base.Events = EventMask.ButtonPressMask;
            base.CanFocus = true;

            SetupHandlers ();
        }

        private void SetupHandlers ()
        {
            tileset.TileSetChanged += delegate {
                base.QueueDraw ();
            };

            ConfigurationClient.SyncToggleAction ("ShowGridLines",
                UISchema.DrawGridLines,
                delegate (ToggleAction action, SchemaEntry<bool> entry) {
                    show_grid_lines = action.Active;
                    base.QueueDraw ();
                });

            map.GridChanged += delegate (object sender,
                MapGridChangedEventArgs args) {
                redraw_grids.Add (args.Grid);
            };

            World.Instance.NewRound += delegate {
                if (hero_moved) {
                    base.QueueDraw ();
                    hero_moved = false;
                }
                else {
                    foreach (Point grid in redraw_grids)
                        QueueDrawGrid (grid);
                }
                redraw_grids.Clear ();
            };

            World.Instance.Hero.Moved += delegate {
                hero_moved = true;
            };
        }

        private void QueueDrawGrid (Point p)
        {
            int x, y;
            GridPointToWindowCoords (p, out x, out y);
            base.QueueDrawArea (x, y, tileset.Width, tileset.Height);
        }

        protected override bool OnButtonPressEvent (Gdk.EventButton button)
        {
            if (button.Button != 3)
                return false;

            Point grid = WindowCoordsToGridPoint (button.X, button.Y);
            if (map.GetGridInformation (grid) == GridInformation.Invalid)
                return false;

            highlight_grid = grid;
            show_highlight = true;
            UIActions.Instance.MapViewMenu.Popup ();

            return true;
        }

        private void GridPointToWindowCoords (Point p, out int x, out int y)
        {
            x = (p.X - offset_x) * tileset.Width - poffset_x;
            y = (p.Y - offset_y) * tileset.Height - poffset_y;
        }

        private Point WindowCoordsToGridPoint (double x, double y)
        {
            return new Point (
                (int) (x + poffset_x) / tileset.Width + offset_x,
                (int) (y + poffset_y) / tileset.Height + offset_y);
        }

        protected override void OnSizeRequested
            (ref Gtk.Requisition requisition)
        {
            requisition.Width  = 640;
            requisition.Height = 480;
        }

        protected override void OnStyleSet (Style previous)
        {
            fill_color = CairoUtilities.BlendColors (
                Style.Background (StateType.Normal),
                Style.Foreground (StateType.Normal), 0.75);
        }

        protected override bool OnExposeEvent (Gdk.EventExpose args)
        {
            //DateTime start = DateTime.Now;

            using (context = CairoHelper.Create (base.GdkWindow))
            {
                Gdk.Rectangle rect = args.Area;
                context.Rectangle (rect.X, rect.Y, rect.Width, rect.Height);
                context.Clip ();

                DrawMap ();
            }

            //Console.WriteLine ((DateTime.Now - start).Milliseconds + "ms");
            return true;
        }

        private void CenterAroundHero (int visible_wide, int visible_high)
        {
            int w = base.Allocation.Width;
            int h = base.Allocation.Height;

            Point hero = World.Instance.Hero.Location;
            offset_x = hero.X - (visible_wide / 2);
            offset_y = hero.Y - (visible_high / 2);

            poffset_x = w % tileset.Width;
            if (poffset_x != 0)
                poffset_x = ((w / tileset.Width + 1) *
                    tileset.Width - w) / 2;
            if (visible_wide % 2 == 0)
                poffset_x += tileset.Width / 2;

            poffset_y = h % tileset.Height;
            if (poffset_y != 0)
                poffset_y = ((h / tileset.Height + 1) *
                    tileset.Height - h) / 2;
            if (visible_high % 2 == 0)
                poffset_y += tileset.Height / 2;
        }

        private void DrawMap ()
        {
            int w = base.Allocation.Width;
            int h = base.Allocation.Height;

            int tiles_wide = (w / tileset.Width) +
                ((w % tileset.Width) > 0 ? 1 : 0);
            int tiles_high = (h / tileset.Height) +
                ((h % tileset.Height) > 0 ? 1 : 0);

            context.Color = fill_color;
            context.Rectangle (0, 0, w, h);
            context.Fill ();

            CenterAroundHero (tiles_wide, tiles_high);

            for (int tx = 0; tx <= tiles_wide; tx++)
                for (int ty = 0; ty <= tiles_high; ty++)
                    DrawGrid (tx, ty);

            if (show_highlight) {
                Console.WriteLine ("Exposing");
                context.Color = new Color (1, 0, 0);

                int x, y;
                GridPointToWindowCoords (highlight_grid, out x, out y);
                context.LineWidth = tileset.ZoomPercentage * 4;
                context.Rectangle (x, y, tileset.Width, tileset.Height);
                context.Stroke ();
            }
        }

        private void DrawGrid (int x, int y)
        {
            Point grid = new Point (offset_x + x, offset_y + y);
            if (map.GetGridInformation (grid) == GridInformation.Invalid)
                return;

            foreach (string tile in map[grid].Tiles)
                DrawTile (tile, x, y);

            if (show_grid_lines) {
                CairoHelper.SetSourceColor (context,
                    base.Style.Foreground (StateType.Normal));

                context.LineWidth = tileset.ZoomPercentage;
                context.Rectangle (
                    x * tileset.Width - poffset_x,
                    y * tileset.Height - poffset_y,
                    tileset.Width, tileset.Height);
                context.Stroke ();
            }
        }

        private void DrawTile (string tile, int x, int y)
        {
            base.GdkWindow.DrawPixbuf (base.Style.BlackGC,
                tileset[tile].Pixbuf, 0, 0,
                x * tileset.Width - poffset_x,
                y * tileset.Height - poffset_y,
                -1, -1, Gdk.RgbDither.None, 0, 0);
        }
    }
}

