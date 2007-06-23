/*******************************************************************************
 *  TestActor.cs: Unit Tests for Actor class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;
using System;

using Questar.Base;
using Questar.Maps;
using Questar.Primitives;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestMap
    {
        Map map;

        [SetUp]
        public void Setup ()
        {
            World world = new World ();
            Game.Instance.World = world;

            world.Map = new Map ();
            map = world.Map;
        }

        [Test]
        public void FillArea ()
        {
            int w = map.Width;
            int h = map.Height;
            map.FillArea ("flower", new Rectangle (0, 0, w, h));

            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    Assert.AreEqual ("flower", map[i, j].Terrain.Id);
                }
            }
        }
    }
}

