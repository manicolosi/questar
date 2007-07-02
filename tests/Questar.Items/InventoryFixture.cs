/*******************************************************************************
 *  InventoryFixture.cs: Unit Tests for Inventory objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class InventoryFixture
    {
        Inventory inventory;
        Item item1;
        Item item2;

        [SetUp]
        public void SetUp ()
        {
            inventory = new Inventory ();

            item1 = ItemFactory.Create ("HealLightWounds");
            item2 = ItemFactory.Create ("HealSeriousWounds");
        }

        [Test]
        public void AddOneItem ()
        {
            inventory.Add (item1);

            Assert.IsTrue (inventory.Contains (item1));
        }

        [Test]
        public void AddMultipleItems ()
        {
            inventory.Add (item1);
            inventory.Add (item2);

            Assert.IsTrue (inventory.Contains (item1));
            Assert.IsTrue (inventory.Contains (item2));
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
            inventory.Add (item1);
            inventory.Add (item1);
        }

        [Test]
        public void ContainsNonAddedItem ()
        {
            Assert.IsFalse (inventory.Contains (item1));
        }

        [Test]
        public void ContainsNull ()
        {
            Assert.IsFalse (inventory.Contains (null));
        }

        [Test]
        public void RemoveItem ()
        {
            inventory.Add (item1);
            inventory.Add (item2);

            inventory.Remove (item1);

            Assert.IsFalse (inventory.Contains (item1));
            Assert.IsTrue (inventory.Contains (item2));
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void RemoveNonAddedItem ()
        {
            inventory.Remove (item1);
        }

        [Test]
        [ExpectedException (typeof (ArgumentNullException))]
        public void RemoveNullItem ()
        {
            inventory.Remove (null);
        }

        [Test]
        public void IsIEnumerable ()
        {
            Assert.IsInstanceOfType (typeof (IEnumerable), inventory);
        }

        [Test]
        public void Foreach ()
        {
            inventory.Add (item1);
            inventory.Add (item2);

            List<Item> items = new List<Item> ();

            foreach (Item item in inventory)
                items.Add (item);

            Assert.AreEqual (2, items.Count);

            CollectionAssert.Contains (items, item1);
            CollectionAssert.Contains (items, item2);
        }
    }
}

