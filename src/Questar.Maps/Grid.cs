/*******************************************************************************
 *  Grid.cs: Represents a single cell of a Map.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Items;

namespace Questar.Maps
{
    public class Grid
    {
        private Terrain terrain;
        private Actor actor;
        private Item item;

        public Grid ()
        {
        }

        public Terrain Terrain
        {
            get { return terrain; }
            set { terrain = value; }
        }

        public bool IsTerrainBlocking
        {
            get { return terrain != null ? terrain.IsBlocking : false; }
        }

        public Actor Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public IEnumerable<string> Tiles
        {
            get {
                foreach (string tile in Terrain.Tiles)
                    yield return tile;

                if (Item != null)
                    yield return Item.Tile;
                if (Actor != null)
                    yield return Actor.Tile;
            }
        }
    }
}

