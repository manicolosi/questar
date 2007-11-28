/*******************************************************************************
 *  DirectionFixture.cs: Unit Tests for Direction objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;

using Questar.Primitives;

namespace Questar.UnitTests.Primitives
{
    [TestFixture]
    public class DirectionFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void EightDirections ()
        {
            // FIXME: This would be prettier with the Count Linq
            // extension method.
            Assert.AreEqual (8, new List<Direction> (Direction.Directions).Count);
        }

        [Test]
        public void DirectionsDoesNotContainNoneDirection ()
        {
            // FIXME: It'd be nice if this worked on IEnumerable<T>
            // types instead of just ICollection<T> types.
            CollectionAssert.DoesNotContain ( new List<Direction> (Direction.Directions), Direction.None);
        }
    }
}

