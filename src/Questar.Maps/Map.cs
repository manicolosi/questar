/*******************************************************************************
 *  Map.cs: Basically a 2d collection of Grids.
 *
 *  Copyright (C) 2007, 2008
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
                this[actor.Location.Position].Actor = actor;
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

            this[old_loc.Position].Actor = null;
            FireGridChanged (old_loc.Position);

            this[new_loc.Position].Actor = actor;
            FireGridChanged (new_loc.Position);
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

            if (old_loc != null) {
                this[old_loc.Position].Item = null;
                FireGridChanged (old_loc.Position);
            }

            this[new_loc.Position].Item = item;
            FireGridChanged (new_loc.Position);
        }

        private void FireGridChanged (Point point)
        {
            EventHelper.Raise (this, GridChanged,
                delegate (MapGridChangedEventArgs grid_args) {
                    grid_args.Grid = point;
                });
        }

        public GridInformation GetGridInformation (Point grid)
        {
            GridInformation info = GridInformation.Clear;

            Rectangle rect = new Rectangle (width, height);
            if (!rect.Contains (grid))
                info = GridInformation.Invalid;

            else if (this[grid].Terrain.IsBlocking)
                info = GridInformation.BlockingTerrain;

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

