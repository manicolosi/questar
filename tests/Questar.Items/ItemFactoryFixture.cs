/*******************************************************************************
 *  ItemFactoryFixture.cs: Unit Tests for ItemFactory.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;
using System;

using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFactoryFixture
    {
        [Test]
        public void CreateHealthPotion ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");
            Assert.IsInstanceOfType (typeof (HealthPotion), item);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void InvalidItem ()
        {
            Item item = ItemFactory.Create ("InvalidItem");
        }
    }
}

