/*******************************************************************************
 *  WorldView.cs: Widget to show the current state of a World.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Cairo;
using Gdk;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Base;
using Questar.Configuration;
using Questar.Maps;

using Color = Cairo.Color;
using Point = Questar.Base.Point;
using Rectangle = Gdk.Rectangle;

namespace Questar.Gui
{
    public class WorldView : DrawingArea
    {
        private World world;
        private IActor center;

        private TileSet tileset = new TileSet ();
        private List<Point> queued_grids = new List<Point> ();

        private bool grid_lines;
        private bool hero_moved = false;
        private bool highlight = false;
        private Point highlight_grid;

        private Color fill_color;
        private Color hilight_color;
        private Color grid_line_color;

        private int offset_x, offset_y;
        private int poffset_x, poffset_y;

        public WorldView (World world, IActor center)
        {
            SetWorld (world);
            SetCenter (center);

            SetupHandlers ();

            base.Events = EventMask.ButtonPressMask;
            base.CanFocus = true;
        }

        public WorldView (World world) : this (world, world.Hero)
        {
        }

        public World World
        {
            get { return world; }
            set {
                SetWorld (value);
            }
        }

        public IActor Center
        {
            get { return center; }
            set {
                SetCenter (value);
            }
        }

        protected override bool OnButtonPressEvent (EventButton button)
        {
            if (button.Button != 3)
                return false;

            Point grid = WindowCoordsToGridPoint (
                (int) button.X, (int) button.Y);
            if (world.Map.GetGridInformation (grid) == GridInformation.Invalid)
                return false;

            highlight_grid = grid;
            UIActions.Instance.WorldViewContextMenu.Popup ();

            return true;
        }

        protected override bool OnExposeEvent (EventExpose args)
        {
            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                Rectangle rect = args.Area;
                context.Rectangle (rect.X, rect.Y, rect.Width, rect.Height);
                context.Clip ();

                DrawMap (context);
            }
            return true;
        }

        protected override void OnSizeRequested (ref Requisition requisition)
        {
            requisition.Width  = 640;
            requisition.Height = 480;
        }

        protected override void OnStyleSet (Style previous)
        {
            fill_color = CairoColorExtensions.BlendColors (0.75,
                Style.Background (StateType.Normal),
                Style.Foreground (StateType.Normal));

            grid_line_color = CairoColorExtensions.FromGdkColor (
                Style.Foreground (StateType.Normal), 0.2);

            hilight_color = CairoColorExtensions.FromGdkColor (
                Style.Background (StateType.Selected), 0.5);
        }

        private void SetWorld (World world)
        {
            this.world = world;

            world.NewRound += delegate {
                if (hero_moved) {
                    hero_moved = false;
                    base.QueueDraw ();
                }
                else {
                    foreach (Point grid in queued_grids)
                        QueueDrawGrid (grid);
                }
                queued_grids.Clear ();
            };

            world.Hero.Moved += delegate { hero_moved = true; };

            world.Map.GridChanged += delegate (object sender,
                MapGridChangedEventArgs args) {
                    queued_grids.Add (args.Grid);
            };
        }

        private void SetCenter (IActor center)
        {
            this.center = center;
        }

        private void SetupHandlers ()
        {
            Menu context_menu = UIActions.Instance.WorldViewContextMenu;
            context_menu.Hidden += delegate { highlight = false; };
            context_menu.Shown  += delegate { highlight = true; };

            tileset.TileSetChanged += delegate { base.QueueDraw (); };

            ConfigurationClient.SyncToggleAction ("ShowGridLines",
                UISchema.DrawGridLines, delegate (ToggleAction action,
                    SchemaEntry<bool> entry) {
                    grid_lines = action.Active;
                    base.QueueDraw ();
                });
        }

        private void GridPointToWindowCoords (Point p, out int x, out int y)
        {
            x = (p.X - offset_x) * tileset.Width - poffset_x;
            y = (p.Y - offset_y) * tileset.Height - poffset_y;
        }

        private Point WindowCoordsToGridPoint (int x, int y)
        {
            return new Point (
                (x + poffset_x) / tileset.Width + offset_x,
                (y + poffset_y) / tileset.Height + offset_y);
        }

        private void QueueDrawGrid (Point grid)
        {
            int x, y;
            GridPointToWindowCoords (grid, out x, out y);
            base.QueueDrawArea (x, y, tileset.Width, tileset.Height);
        }

        private void SetOffsets (int visible_wide, int visible_high)
        {
            int w = base.Allocation.Width;
            int h = base.Allocation.Height;

            Point center_grid = center.Location;
            offset_x = center_grid.X - (visible_wide / 2);
            offset_y = center_grid.Y - (visible_high / 2);

            poffset_x = w % tileset.Width;
            if (poffset_x != 0)
                poffset_x = ((w / tileset.Width + 1) * tileset.Width - w) / 2;
            if (visible_wide % 2 == 0)
                poffset_x += tileset.Width / 2;

            poffset_y = h % tileset.Height;
            if (poffset_y != 0)
                poffset_y = ((h / tileset.Height + 1) * tileset.Height - h) / 2;
            if (visible_high % 2 == 0)
                poffset_y += tileset.Height / 2;
        }

        private void DrawTile (string tile, int x, int y)
        {
            base.GdkWindow.DrawPixbuf (base.Style.BlackGC,
                tileset[tile].Pixbuf, 0, 0,
                x * tileset.Width - poffset_x,
                y * tileset.Height - poffset_y,
                -1, -1, Gdk.RgbDither.None, 0, 0);
        }

        private void DrawGrid (Context context, int x, int y)
        {
            Point grid = new Point (offset_x + x, offset_y + y);
            if (world.Map.GetGridInformation (grid) == GridInformation.Invalid)
                return;

            foreach (string tile in world.Map[grid].Tiles)
                DrawTile (tile, x, y);

            if (grid_lines) {
                context.Color = grid_line_color;
                context.LineWidth = tileset.ZoomPercentage;
                context.Rectangle (
                    x * tileset.Width - poffset_x,
                    y * tileset.Height - poffset_y,
                    tileset.Width, tileset.Height);
                context.Stroke ();
            }
        }

        private void DrawMap (Context context)
        {
            int w = base.Allocation.Width;
            int h = base.Allocation.Height;

            context.Color = fill_color;
            context.Rectangle (0, 0, w, h);
            context.Fill ();

            int tiles_wide = (w / tileset.Width) +
                ((w % tileset.Width) > 0 ? 1 : 0);
            int tiles_high = (h / tileset.Height) +
                ((h % tileset.Height) > 0 ? 1 : 0);

            SetOffsets (tiles_wide, tiles_high);

            for (int tx = 0; tx <= tiles_wide; tx++)
                for (int ty = 0; ty <= tiles_high; ty++)
                    DrawGrid (context, tx, ty);

            if (highlight) {
                int x, y;
                GridPointToWindowCoords (highlight_grid, out x, out y);
                context.Color = hilight_color;
                context.Rectangle (x, y, tileset.Width, tileset.Height);
                context.Fill ();
            }
        }
    }
}

