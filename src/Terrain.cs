//
// Terrain.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;
using System.Collections.Generic;

namespace Questar.Maps
{
    public class Terrain
    {
        private string name;
        private string description;
        private List<string> tiles;
        private bool is_blocking;

        public Terrain ()
        {
            this.tiles = new List<string> ();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsBlocking
        {
            get { return is_blocking; }
            set { is_blocking = value; }
        }

        public bool IsValid
        {
            get {
                return ((name != null) && (description != null) &&
                    (tiles.Count != 0));
            }
        }

        public IList<string> Tiles
        {
            get { return tiles; }
        }
    }
}

