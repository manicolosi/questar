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

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFixture
    {
        private Item item;

        [SetUp]
        public void SetUp ()
        {
            item = ItemFactory.Create ("HealLightWounds");
        }

        [Test]
        public void IsAnEntity ()
        {
            Assert.IsInstanceOfType (typeof (Entity), item);
        }

        [Test]
        public void NotOwned ()
        {
            Assert.IsFalse (item.IsOwned);
        }

        [Test]
        public void Owned ()
        {
            Actor owner = new MockActor ();

            item.Owner = owner;

            Assert.IsTrue (item.IsOwned);
            Assert.AreSame (owner, item.Owner);
        }
    }
}

