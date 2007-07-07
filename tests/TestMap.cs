/*******************************************************************************
 *  TestMap.cs: Unit Tests for Map class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;
using System;

using Questar.Actors;
using Questar.Base;
using Questar.Maps;
using Questar.MockObjects;
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
        public void AdjacentActors ()
        {
            MockActor a1 = new MockActor ();
            MockActor a2 = new MockActor ();
            a1.Move (new Point (3, 3));
            a2.Move (new Point (3, 5));
            a1.Create ();
            a2.Create ();

            int count = 0;
            foreach (Actor actor in map.GetAdjacentActors (new Point (3, 4))) {
                count++;
                Assert.IsTrue (actor == a1 || actor == a2);
            }

            Assert.AreEqual (2, count);
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

