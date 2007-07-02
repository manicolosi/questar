/*******************************************************************************
 *  ItemFactoryFixture.cs: Unit Tests for ItemFactory.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Items;

namespace Questar.UnitTests.Items
{
    [TestFixture]
    public class ItemFactoryFixture
    {
        private ItemFactory factory;

        [SetUp]
        public void SetUp ()
        {
            factory = ItemFactory.Instance;
        }

        [Test]
        public void Singleton ()
        {
            Assert.AreSame (factory, ItemFactory.Instance);
        }
    }
}

