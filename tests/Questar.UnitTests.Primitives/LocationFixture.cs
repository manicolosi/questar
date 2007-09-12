/*******************************************************************************
 *  ActorFixture.cs: Unit Tests for Location objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;

using Questar.Maps;
using Questar.MockObjects;
using Questar.Primitives;

namespace Questar.UnitTests.Primitives
{
    [TestFixture]
    public class LocationFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void NullLocationEquality ()
        {
            Location loc1 = new NullLocation ();
            Location loc2 = new NullLocation ();

            Assert.IsTrue (loc1.Equals (loc2));
            Assert.IsTrue (loc1 == loc2);
            Assert.IsFalse (loc1 != loc2);
        }

        [Test]
        public void MapLocationEquality ()
        {
            Location loc1 = new MapLocation (new Map (), Point.Zero);
            Location loc2 = new MapLocation (loc1);

            Assert.IsTrue (loc1.Equals (loc2));
            Assert.IsTrue (loc1 == loc2);
            Assert.IsFalse (loc1 != loc2);
        }

        [Test]
        public void MapLocationInequality ()
        {
            Location loc1 = new MapLocation (new Map (), new Point (1, 1));
            Location loc2 = new MapLocation (new Map (), new Point (1, 2));

            Assert.IsFalse (loc1.Equals (loc2));
            Assert.IsFalse (loc1 == loc2);
            Assert.IsTrue (loc1 != loc2);
        }

        [Test]
        public void ActorLocationEquality ()
        {
            Location loc1 = new ActorLocation (new MockActor ());
            Location loc2 = new ActorLocation (loc1.Actor);

            Assert.IsTrue (loc1.Equals (loc2));
            Assert.IsTrue (loc1 == loc2);
            Assert.IsFalse (loc1 != loc2);
        }

        [Test]
        public void ActorLocationInequality ()
        {
            Location loc1 = new ActorLocation (new MockActor ());
            Location loc2 = new ActorLocation (new MockActor ());

            Assert.IsFalse (loc1.Equals (loc2));
            Assert.IsFalse (loc1 == loc2);
            Assert.IsTrue (loc1 != loc2);
        }

        [Test]
        public void NullLocationsEquality ()
        {
            Location loc1 = null;
            Location loc2 = null;

            Assert.AreEqual (loc1, loc2);
            Assert.IsTrue (loc1 == loc2);
        }

        [Test]
        public void AdjacentLocationsFromMapLocation ()
        {
            Location loc = new MapLocation (new Map (), new Point (5, 5));
            List<Location> adj_locs =
                new List<Location> (loc.AdjacentLocations);

            Assert.AreEqual (8, adj_locs.Count);

            CollectionAssert.Contains (adj_locs,
                Direction.North.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.NorthEast.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.East.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.SouthEast.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.South.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.SouthWest.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.West.ApplyTo (loc));
            CollectionAssert.Contains (adj_locs,
                Direction.NorthWest.ApplyTo (loc));
        }
    }
}

