//
// Grid.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;
using System.Collections.Generic;

using Questar.Actors;

namespace Questar.Maps
{
    public class Grid
    {
        private Terrain terrain;
        private IActor actor;

        public Grid (Terrain terrain)
        {
            this.terrain = terrain;
        }

        public Terrain Terrain
        {
            get { return terrain; }
            set { terrain = value; }
        }

        public IActor Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        public IEnumerable<string> Tiles
        {
            get {
                foreach (string tile in Terrain.Tiles)
                    yield return tile;

                if (Actor != null)
                    yield return Actor.Tile;
            }
        }
    }
}

