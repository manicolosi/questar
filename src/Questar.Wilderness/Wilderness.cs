using System;

using Questar.Maps;
using Questar.Actors;
using Questar.Extensions;
using Questar.Primitives;
using Questar.Helpers;

namespace Questar.Wilderness
{
    public class Wilderness
    {
        public event EventHandler<MapGridChangedEventArgs> GridChanged;

        private const int RegionsWide  = 10;
        private const int RegionsHigh  = 10;

        private const int RegionWidth  = 100;
        private const int RegionHeight = 100;

        private Map[,] regions;

        public Wilderness ()
        {
            regions = new Map[RegionsWide, RegionsHigh];
        }

        public int Width
        {
            get { return RegionsWide * RegionWidth; }
        }

        public int Height
        {
            get { return RegionsHigh * RegionHeight; }
        }

        public Grid this[int x, int y]
        {
            get {
                //Assert x >= 0 AND x < RegionsWide * RegionWidth

                int rx = x / RegionWidth;
                int ry = y / RegionHeight;

                int gx = x % RegionWidth;
                int gy = y % RegionHeight;

                Map region = regions[rx, ry];
                if (region == null) {
                    throw new ApplicationException ("Region is not loaded!");
                    //throw new RegionNotLoadedException ();
                }

                return region[gx, gy];
            }
        }
    }
}

