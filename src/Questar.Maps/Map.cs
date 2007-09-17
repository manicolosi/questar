/*******************************************************************************
 *  Map.cs: Basically a 2d collection of Grids.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
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
        private int width;
        private int height;

        private Grid [,] grids;

        public event EventHandler<MapGridChangedEventArgs> GridChanged;

        public Map (int width, int height)
        {
            this.width = width;
            this.height = height;

            grids = new Grid [width, height];

            ItemFactory.Instance.Created += OnItemCreated;
            MonsterFactory.Instance.Created += OnActorCreated;
        }

        private void OnActorCreated (object sender, EntityCreatedEventArgs args)
        {
            Actor actor = (Actor) args.Entity;

            actor.LocationChanged += OnActorLocationChanged;
            actor.Destroyed += OnActorDestroyed;

            if (actor.Location.Map == this)
                this[actor.Location.Point].Actor = actor;
        }

        private void OnActorDestroyed (object sender, EventArgs args)
        {
            Actor actor = (Actor) sender;
            actor.LocationChanged -= OnActorLocationChanged;
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

        private void OnItemCreated (object sender, EntityCreatedEventArgs args)
        {
            Item item = (Item) args.Entity;
            item.LocationChanged += OnItemLocationChanged;
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
            set { grids[x, y] = value; }
        }

        public Grid this[Point p]
        {
            get { return this[p.X, p.Y]; }
            set { this[p.X, p.Y] = value; }
        }
    }
}

