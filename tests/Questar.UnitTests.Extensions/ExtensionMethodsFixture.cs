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
        private int [] source;

        [SetUp]
        public void SetUp ()
        {
            source = new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        [Test]
        public void SelectWorksAsItShould ()
        {
            double [] result = new [] { 0.0, 1.5, 3.0, 4.5, 6.0,
                                        7.5, 9.0, 10.5, 12, 13.5 };

            Assert.That (source.Select (i => i * 1.5), Is.EquivalentTo (result));
        }

        [Test]
        public void AnyReturnsTrueWhenPredicateIsTrueForAnItemInSource ()
        {
          Assert.That (source.Any (i => i == 5), Is.True);
        }

        [Test]
        public void AnyReturnsFalseWhenPredicateIsFalseForAllItemsInSource ()
        {
          Assert.That (source.Any (i => i == 15), Is.False);
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

        [Test]
        public void WhereReturnsSameListIfPredicateAlwaysReturnsTrue ()
        {
            var result = source.Where (i => true);

            Assert.That (result, Is.EquivalentTo (source));
        }

        [Test]
        public void WhereReturnsEmptyListIfPredicateAlwaysReturnsFalse ()
        {
            var result = source.Where (i => false);

            Assert.That (result, Is.Empty);
        }

        [Test]
        public void WhereReturnsEvenIntsWithThisPredicate ()
        {
            var result = source.Where (i => i % 2 == 0);

            Assert.That (result, Is.EquivalentTo (new [] {0, 2, 4, 6, 8}));
        }
    }
}
