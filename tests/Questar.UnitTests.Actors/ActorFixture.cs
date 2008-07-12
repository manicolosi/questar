/*******************************************************************************
 *  ActorFixture.cs: Unit Tests for Actor objects.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;

using Questar.Actors;
using Questar.Items;
using Questar.Maps;
using Questar.Maps.Generation;
using Questar.MockObjects;
using Questar.Primitives;

namespace Questar.UnitTests.Actors
{
    [TestFixture]
    public class ActorFixture
    {
        private Actor actor;
        private Actor actor2;

        [SetUp]
        public void SetUp ()
        {
            IMapGenerator map_gen = new StupidGenerator ();
            actor = new MockActor (new Location (map_gen.Generate (), 2, 2));
            actor2 = new MockActor (new Location (actor.Location.Map, 1, 2));
        }

        [Test]
        public void InventoryIsEmpty ()
        {
            Assert.IsTrue (actor.Inventory.IsEmpty);
        }

        [Test]
        public void IsRemovedFromMapWhenDead ()
        {
            Map map = actor.Location.Map;

            Assert.IsTrue (map.Contains (actor));
            actor.TakeDamage (actor2, 1000);
            Assert.IsFalse (map.Contains (actor));
        }
    }
}

