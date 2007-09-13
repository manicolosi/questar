/*******************************************************************************
 *  Map.cs: Basically a 2d collection of Grids.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Base;
using Questar.Helpers;
using Questar.Items;
using Questar.Primitives;

namespace Questar.Maps
{
    public class MapGridChangedEventArgs : EventArgs
    {
        public Point Grid;
    }

    public class Map
    {
        private int width  = 48;
        private int height = 48;

        private Grid [,] grids;

        private TerrainManager terrain_manager;

        public event EventHandler<MapGridChangedEventArgs> GridChanged;

        public Map ()
        {
            terrain_manager = new TerrainManager ();

            grids = new Grid [width, height];

            FillRandom ("tree", "grass", "flower", "grass", "grass",
                "grass", "grass", "flower", "grass", "grass",
                "grass", "grass", "flower", "grass", "grass",
                "grass", "grass", "flower", "grass", "grass",
                "grass", "grass", "flower", "grass", "grass");

            ItemFactory.Instance.Created += OnItemCreated;

            if (Game.Instance.World != null) {
                Game.Instance.World.ActorAdded += delegate (object sender,
                    WorldActorEventArgs args) {
                    Actor actor = args.Actor;
                    Point grid = actor.Location.Point;
                    this[grid].Actor = actor;
                    actor.LocationChanged += OnActorLocationChanged;
                };

                Game.Instance.World.ActorRemoved += delegate (object sender,
                    WorldActorEventArgs args) {
                    Actor actor = args.Actor;
                    Point grid = actor.Location.Point;
                    this[grid].Actor = null;
                    actor.LocationChanged -= OnActorLocationChanged;

                    FireGridChanged (grid);
                };
            }
        }

        private void OnItemCreated (object sender, EntityCreatedEventArgs args)
        {
            Item item = (Item) args.Entity;
            item.LocationChanged += OnItemLocationChanged;
        }

        private void OnActorLocationChanged (object sender,
            LocationChangedEventArgs args)
        {
            Actor actor = (Actor) sender;
            Location new_loc = args.NewLocation;
            Location old_loc = args.OldLocation;

            if (IsLocationValid (old_loc)) {
                this[old_loc.Point].Actor = null;
                FireGridChanged (old_loc.Point);
            }

            if (IsLocationValid (new_loc)) {
                this[new_loc.Point].Actor = actor;
                FireGridChanged (new_loc.Point);
            }
        }

        private void OnItemLocationChanged (object sender,
            LocationChangedEventArgs args)
        {
            Item item = (Item) sender;
            Location new_loc = args.NewLocation;
            Location old_loc = args.OldLocation;

            if (IsLocationValid (old_loc)) {
                this[old_loc.Point].Item = null;
                FireGridChanged (old_loc.Point);
            }

            if (IsLocationValid (new_loc)) {
                this[new_loc.Point].Item = item;
                FireGridChanged (new_loc.Point);
            }
        }

        private bool IsLocationValid (Location location)
        {
            if (location is MapLocation) {
                if (location.Map != this)
                    throw new NotImplementedException ("Multiple Maps");

                return true;
            }

            return false;
        }

        private void FireGridChanged (Point point)
        {
            EventHelper.Raise<MapGridChangedEventArgs> (this, GridChanged,
                delegate (MapGridChangedEventArgs grid_args) {
                    grid_args.Grid = point;
                });
        }

        // Dummy method
        private void FillRandom (params string [] terrain_ids)
        {
            Random random = new Random ();

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    int i = random.Next (terrain_ids.Length);
                    Terrain terrain = terrain_manager[terrain_ids[i]];

                    this[x, y] = new Grid (terrain);
                }
            }
        }

        public void FillArea (string terrain_id, Rectangle rectangle)
        {
            foreach (Point p in rectangle) {
                Terrain terrain = terrain_manager[terrain_id];
                this[p] = new Grid (terrain);
            }
        }

        public GridInformation GetGridInformation (Point grid)
        {
            GridInformation info = GridInformation.Clear;

            int x = grid.X;
            int y = grid.Y;

            if ((x < 0) || (x >= width) || (y < 0) || (y >= height))
                info = GridInformation.Invalid;

            else if (this[grid].Terrain.IsBlocking) {
                info = GridInformation.BlockingTerrain;
            }

            else if (this[grid].Actor != null)
                info = GridInformation.Occupied;

            return info;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Grid this[int x, int y]
        {
            get { return grids[x, y]; }
            private set { grids[x, y] = value; }
        }

        public Grid this[Point p]
        {
            get { return this[p.X, p.Y]; }
            private set { this[p.X, p.Y] = value; }
        }
    }
}

