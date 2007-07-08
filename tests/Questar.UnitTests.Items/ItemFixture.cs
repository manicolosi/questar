/*******************************************************************************
 *  ItemFixture.cs: Unit Tests for Item objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;

using Questar.Actors;
using Questar.Primitives;
using Questar.Items;
using Questar.MockObjects;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFixture
    {
        private Actor owner;
        private Item item;

        [SetUp]
        public void SetUp ()
        {
            owner = new MockActor ();

            item = new MockItem ();
            item.Owner = owner;
        }

        [Test]
        public void IsAnEntity ()
        {
            Assert.IsInstanceOfType (typeof (Entity), item);
        }

        [Test]
        public void NotOwned ()
        {
            item.Owner = null;

            Assert.IsFalse (item.IsOwned);
        }

        [Test]
        public void Owned ()
        {
            Assert.IsTrue (item.IsOwned);
            Assert.AreSame (owner, item.Owner);
        }

        [Test]
        public void IsICloneable ()
        {
            Assert.IsInstanceOfType (typeof (ICloneable), item);
        }

        [Test]
        public void ClonedItem ()
        {
            Item clone = item.Clone ();

            Assert.AreNotSame (item, clone);
            Assert.AreSame (item.Owner, clone.Owner);
        }

        [Test]
        public void UseIt ()
        {
            item.Use (owner);

            Assert.AreEqual (1, ((MockItem) item).UsedCount);
        }
    }
}

