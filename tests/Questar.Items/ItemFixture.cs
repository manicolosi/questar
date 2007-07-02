/*******************************************************************************
 *  ItemFixture.cs: Unit Tests for Item objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Primitives;
using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFixture
    {
        [Test]
        public void IsAnEntity ()
        {
            Item item = ItemFactory.Create ("HealLightWounds");
            Assert.IsInstanceOfType (typeof (Entity), item);
        }
    }
}

