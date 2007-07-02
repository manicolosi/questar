/*******************************************************************************
 *  InventoryFixture.cs: Unit Tests for Inventory objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;

using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class InventoryFixture
    {
        Inventory inventory;

        [SetUp]
        public void SetUp ()
        {
            inventory = new Inventory ();
        }

        [Test]
        public void AddOneItem ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");
            inventory.Add (item);

            Assert.IsTrue (inventory.Contains (item));
        }

        [Test]
        [ExpectedException (typeof (ArgumentNullException))]
        public void AddNull ()
        {
            inventory.Add (null);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void AddTwice ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");

            inventory.Add (item);
            inventory.Add (item);
        }

        [Test]
        public void ContainsNonAddedItem ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");

            Assert.IsFalse (inventory.Contains (item));
        }

        [Test]
        public void ContainsNull ()
        {
            Assert.IsFalse (inventory.Contains (null));
        }
    }
}

