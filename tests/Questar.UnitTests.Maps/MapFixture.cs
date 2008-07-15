/*******************************************************************************
 *  MapFixture.cs: Unit Tests for Map objects.
 *
 *  Copyright (C) 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Maps;

namespace Questar.UnitTests.Maps
{
    [TestFixture]
    public class MapFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void ThereAreOneHundredGridsInATenByTenMap ()
        {
            Map map = new Map (10, 10);

            int count = 0;

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    Assert.That (map[i, j], Is.Not.Null);
                    count++;
                }
            }

            Assert.That (count, Is.EqualTo (100));
        }
    }
}

