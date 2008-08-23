/*******************************************************************************
 *  WorldView.cs: Widget to show the current state of a World.
 *
 *  Copyright (C) 2007, 2008
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
using Questar.Core;
using Questar.Helpers;
using Questar.Maps;
using Questar.Primitives;

using Color = Cairo.Color;
using Key = Gdk.Key;
using Point = Questar.Primitives.Point;
using Rectangle = Questar.Primitives.Rectangle;

namespace Questar.Gui
{
    public class WorldView : DrawingArea
    {
        private Actor center;
        private Map map;

        private TileSet tileset = new TileSet ();
        private List<Point> queued_grids = new List<Point> ();

        private bool grid_lines;
        private bool recenter = false;
        private bool highlight = false;
        private Point highlight_grid;

        private Color fill_color;
        private Color hilight_color1;
        private Color hilight_color2;
        private Color grid_line_color;

        private int offset_x, offset_y;
        private int poffset_x, poffset_y;

        public WorldView (Actor center)
        {
            this.center = center;
            map = center.Location.Map;

            SetupHandlers ();

            base.Events = EventMask.ButtonPressMask;
            base.CanFocus = true;
        }

        public WorldView () : this (Game.Instance.Hero)
        {
        }

        protected override bool OnButtonPressEvent (EventButton args)
        {
            base.GrabFocus ();

            if (args.Button != 3)
                return false;

            Point grid = WindowCoordsToGridPoint ( (int) args.X, (int) args.Y);
            if (map.GetGridInformation (grid) == GridInformation.Invalid)
                return false;

            highlight_grid = grid;
            UIActions.Instance.WorldViewContextMenu.Popup ();

            return true;
        }

        protected override bool OnKeyPressEvent (EventKey args)
        {
            string action = null;

            // All this is really ugly and doesn't really belong here.
            switch (args.Key) {
                case Key.KP_Up:
                case Key.Up:
                    action = "MoveNorth"; break;
                case Key.KP_Down:
                case Key.Down:
                    action = "MoveSouth"; break;
                case Key.KP_Left:
                case Key.Left:
                    action = "MoveWest"; break;
                case Key.KP_Right:
                case Key.Right:
                    action = "MoveEast"; break;
                case Key.KP_Home:
                    action = "MoveNorthWest"; break;
                case Key.KP_Page_Up:
                    action = "MoveNorthEast"; break;
                case Key.KP_End:
                    action = "MoveSouthWest"; break;
                case Key.KP_Page_Down:
                    action = "MoveSouthEast"; break;
                case Key.KP_Begin:
                    action = "DoNothing"; break;
                case Key.KP_Delete:
                    action = "PickUp"; break;
                default:
                    break;
            }
            if (action != null) {
                UIActions.Instance[action].Activate ();
                return true;
            }
            return false;
        }

        protected override bool OnExposeEvent (EventExpose args)
        {
            //DateTime start = DateTime.Now;

            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                Gdk.Rectangle rect = args.Area;
                context.Rectangle (rect.X, rect.Y, rect.Width, rect.Height);
                context.Clip ();

                DrawMap (context);
            }

            //Console.WriteLine ((DateTime.Now - start).Milliseconds + "ms");

            return true;
        }

        protected override void OnRealized ()
        {
            base.OnRealized ();
            base.GrabFocus ();
        }

        protected override void OnSizeRequested (ref Requisition requisition)
        {
            requisition.Width  = 640;
            requisition.Height = 480;
        }

        protected override void OnStyleSet (Style previous)
        {
            fill_color = ColorHelper.BlendColors (0.75,
                Style.Background (StateType.Normal),
                Style.Foreground (StateType.Normal));

            grid_line_color = ColorHelper.FromGdkColor (
                Style.Foreground (StateType.Normal), 0.2);

            hilight_color1 = ColorHelper.FromGdkColor (
                Style.Background (StateType.Selected), 0.5);
            hilight_color2 = ColorHelper.FromGdkColor (
                Style.Background (StateType.Selected));
        }

        private void SetupHandlers ()
        {
            Menu context_menu = UIActions.Instance.WorldViewContextMenu;
            context_menu.Hidden += delegate { highlight = false; };
            context_menu.Shown  += delegate { highlight = true; };

            tileset.TileSetChanged += delegate { base.QueueDraw (); };

            ConfigurationClient.SyncToggleAction ("ShowGridLines",
                UISchema.DrawGridLines, (action, entry) => {
                    grid_lines = action.Active;
                    base.QueueDraw ();
                });

            Game.Instance.TurnLoop.NewRound += NewRoundHandler;
            center.Location.Map.GridChanged += GridChangedHandler;
            center.LocationChanged += CenterLocationChangedHandler;
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

            Point center_grid = center.Location.Position;
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
                tileset[tile].Pixbuf, 0, 0, x, y,
                -1, -1, Gdk.RgbDither.None, 0, 0);
        }

        private void DrawGrid (Context context, Point grid)
        {
            if (map.GetGridInformation (grid) == GridInformation.Invalid)
                return;

            int x, y;
            GridPointToWindowCoords (grid, out x, out y);

            foreach (string tile in map[grid].Tiles)
                DrawTile (tile, x, y);

            if (grid_lines) {
                context.Color = grid_line_color;
                context.LineWidth = tileset.ZoomPercentage;
                context.Rectangle (x, y, tileset.Width, tileset.Height);
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
            Rectangle visible = new Rectangle (offset_x, offset_y,
                tiles_wide + 1, tiles_high + 1);

            foreach (Point grid in visible)
                DrawGrid (context, grid);

            if (highlight) {
                int x, y;
                GridPointToWindowCoords (highlight_grid, out x, out y);
                context.Rectangle (x, y, tileset.Width, tileset.Height);
                context.LineWidth = tileset.ZoomPercentage * 2;
                context.Color = hilight_color1;
                context.FillPreserve ();
                context.Color = hilight_color2;
                context.Stroke ();
            }
        }

        private void NewRoundHandler (object sender, NewRoundEventArgs args)
        {
            if (recenter) {
                recenter = false;
                base.QueueDraw ();
            }
            else {
                foreach (Point grid in queued_grids)
                    QueueDrawGrid (grid);
            }

            queued_grids.Clear ();
        }

        private void GridChangedHandler (object sender, MapGridChangedEventArgs args)
        {
            queued_grids.Add (args.Grid);
        }

        private void CenterLocationChangedHandler (object sender,
            LocationChangedEventArgs args)
        {
            map = args.NewLocation.Map;
            recenter = true;
        }
    }
}

