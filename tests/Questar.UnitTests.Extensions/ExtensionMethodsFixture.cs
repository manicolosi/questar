/*******************************************************************************
 *  ExtensionMethodsFixture.cs: Unit Tests for ExtensionMethods extension
 *  methods.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Extensions;

namespace Questar.UnitTests.Extensions
{
    [TestFixture]
    public class ExtensionMethodsFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void ClampReturnsHighWhenSelfGreaterThanIt ()
        {
            int result = 5.Clamp (1, 3);

            Assert.That (result, Is.EqualTo (3));
        }

        [Test]
        public void ClampReturnsLowWhenSelfLessThanIt ()
        {
            int result = 5.Clamp (7, 10);

            Assert.That (result, Is.EqualTo (7));
        }

        [Test]
        public void ClampReturnsSelfWhenSelfIsBetweenHighAndLow ()
        {
            int result = 5.Clamp (1, 10);

            Assert.That (result, Is.EqualTo (5));
        }

        [Test]
        public void ClampWorksWithDifferentIComparableTypes ()
        {
            Assert.That ('c'.Clamp ('b', 'd'), Is.EqualTo ('c'));
            Assert.That ('a'.Clamp ('b', 'd'), Is.EqualTo ('b'));
            Assert.That ('e'.Clamp ('b', 'd'), Is.EqualTo ('d'));

            Assert.That (3.0.Clamp (2.0, 4.0), Is.EqualTo (3.0));
            Assert.That (1.0.Clamp (2.0, 4.0), Is.EqualTo (2.0));
            Assert.That (5.0.Clamp (2.0, 4.0), Is.EqualTo (4.0));

            Assert.That ("computer".Clamp ("b", "d"), Is.EqualTo ("computer"));
            Assert.That ("appletea".Clamp ("b", "d"), Is.EqualTo ("b"));
            Assert.That ("elephant".Clamp ("b", "d"), Is.EqualTo ("d"));
        }
    }
}

