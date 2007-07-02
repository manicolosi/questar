/*******************************************************************************
 *  InventoryFixture.cs: Unit Tests for Inventory objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class InventoryFixture
    {
        [Test]
        public void AddOneItem ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");

            Inventory inventory = new Inventory ();
            inventory.Add (item);

            Assert.IsTrue (inventory.Contains (item));
        }
    }
}

