/*******************************************************************************
 *  ItemFactoryFixture.cs: Unit Tests for ItemFactory.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;
using System;

using Questar.Items;
using Questar.Primitives;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFactoryFixture
    {
        ItemFactory factory;

        [SetUp]
        public void SetUp ()
        {
            factory = ItemFactory.Instance;
        }

        [Test]
        public void CreatePotion ()
        {
            Item item = factory.Create ("HealLight");
            Assert.IsNotNull (item);
        }

        [Test]
        [ExpectedException (typeof (ApplicationException))]
        public void InvalidItemId ()
        {
            factory.Create ("InvalidItem");
        }

        [Test]
        [ExpectedException (typeof (ArgumentNullException))]
        public void NullItemId ()
        {
            factory.Create (null);
        }

        [Test]
        public void CreatedEvent ()
        {
            bool got_event = false;

            factory.Created += delegate {
                got_event = true;
            };

            factory.Create ("HealLight");
            Assert.IsTrue (got_event);
        }
    }
}

