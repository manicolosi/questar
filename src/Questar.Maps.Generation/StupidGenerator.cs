/*******************************************************************************
 *  StupidGenerator.cs: A stupid IMapGenerator capable of creating
 *  uninteresting maps with random terrain.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Maps;
using Questar.Primitives;

namespace Questar.Maps.Generation
{
    public class StupidGenerator : IMapGenerator
    {
        private Map map = new Map (64, 64);
        private TerrainManager terrain_manager = new TerrainManager ();

        public Map Generate ()
        {
            Fill ("grass");
            FillRandom (0.10, "flower");
            FillRandom (0.15, "tree");

            return map;
        }

        private void Fill (string terrain)
        {
            Rectangle rect = new Rectangle (map.Width, map.Height);
            FillArea (terrain, rect);
        }

        private void FillArea (string terrain, Rectangle rectangle)
        {
            foreach (Point p in rectangle)
                Put (p, terrain);
        }

        private void FillRandom (double percent, string terrain)
        {
            double amount = (map.Width * map.Height) * percent;

            for (int i = 0; i < amount; i++)
                PutRandom (terrain);
        }

        private void PutRandom (string terrain)
        {
            Point p = Point.GetRandom (map.Width, map.Height);
            Put (p, terrain);
        }

        private void Put (Point point, string terrain)
        {
            map[point].Terrain = terrain_manager[terrain];
        }
    }
}

