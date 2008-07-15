/*******************************************************************************
 *  Map.cs: Basically a 2d collection of Grids.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Extensions;
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
        private Grid [,] grids;
        private Rectangle area;

        public event EventHandler<MapGridChangedEventArgs> GridChanged;

        public Map (int width, int height)
        {
            grids = new Grid[width, height];

            area = new Rectangle (width, height);

            foreach (Point p in area) {
                grids[p.X, p.Y] = new Grid ();
            }

            ItemFactory.Instance.Created += OnItemCreated;
            MonsterFactory.Instance.Created += OnActorCreated;
        }

        public int Width
        {
            get { return area.Width; }
        }

        public int Height
        {
            get { return area.Height; }
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

        public GridInformation GetGridInformation (Point grid)
        {
            GridInformation info = GridInformation.Clear;

            if (!area.Contains (grid)) {
                info = GridInformation.Invalid;
            }
            else if (this[grid].IsTerrainBlocking) {
                info = GridInformation.BlockingTerrain;
            }
            else if (this[grid].Actor != null) {
                info = GridInformation.Occupied;
            }

            return info;
        }

        public bool Contains (Actor actor)
        {
            return area.Any (p => this[p].Actor == actor);
        }

        private void OnActorCreated (object sender, EntityCreatedEventArgs args)
        {
            Actor actor = (Actor) args.Entity;

            actor.LocationChanged += OnActorLocationChanged;
            actor.Destroyed += OnActorDestroyed;

            if (actor.Location.Map == this) {
                this[actor.Location.Position].Actor = actor;
            }
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

            if (new_loc != null) {
                this[new_loc.Position].Actor = actor;
                FireGridChanged (new_loc.Position);
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

            if (old_loc != null) {
                this[old_loc.Position].Item = null;
                FireGridChanged (old_loc.Position);
            }

            if (new_loc != null) {
                this[new_loc.Position].Item = item;
                FireGridChanged (new_loc.Position);
            }
        }

        private void FireGridChanged (Point point)
        {
            EventHelper.Raise (this, GridChanged,
                delegate (MapGridChangedEventArgs grid_args) {
                    grid_args.Grid = point;
                });
        }
    }
}

