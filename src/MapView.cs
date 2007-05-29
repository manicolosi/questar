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
            tileset.ZoomChanged += delegate {
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

        private List<Point> redraw_grids = new List<Point> ();
        private bool hero_moved;

        private void QueueDrawGrid (Point p)
        {
            p.X -= offset_x;
            p.Y -= offset_y;

            base.QueueDrawArea (p.X * tileset.Width, p.Y * tileset.Height,
                tileset.Width, tileset.Height);
        }

        protected override bool OnButtonPressEvent (Gdk.EventButton button)
        {
            bool handled = (button.Button == 3);

            if (handled) {
                Menu menu = UIActions.Instance.GetWidget ("/MapViewPopup")
                    as Menu;
                menu.Popup ();
            }
            return handled;
        }

        protected override void OnSizeRequested
            (ref Gtk.Requisition requisition)
        {
            requisition.Width  = tileset.Width * 9;
            requisition.Height = tileset.Height * 7;
        }

        Color fill_color;

        protected override void OnStyleSet (Style previous)
        {
            fill_color = CairoUtilities.BlendColors (
                Style.Background (StateType.Normal),
                Style.Foreground (StateType.Normal), 0.75);
        }

        Context context;

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

        private int offset_x;
        private int offset_y;

        private int poffset_x;
        private int poffset_y;

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
            //ImageSurface image = tileset[tile].Surface;
            //image.Show (context, 32, 32);

            //CairoHelper.SetSourcePixbuf (context, tileset[tile].Pixbuf,
            //    0, 0);
            //context.Rectangle (x * tileset.Width, y * tileset.Height,
            //    tileset.Width, tileset.Height);
            //context.Fill ();

            base.GdkWindow.DrawPixbuf (base.Style.BlackGC,
                tileset[tile].Pixbuf, 0, 0,
                x * tileset.Width - poffset_x,
                y * tileset.Height - poffset_y,
                -1, -1, Gdk.RgbDither.None, 0, 0);
        }
    }
}

