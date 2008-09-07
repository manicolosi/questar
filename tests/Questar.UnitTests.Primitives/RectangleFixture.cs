/*******************************************************************************
 *  RectangleFixture.cs: Unit Tests for Rectangle values.
 *
 *  Copyright (C) 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Extensions;
using Questar.Primitives;

namespace Questar.UnitTests.Primitives
{
    [TestFixture]
    public class RectangleFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void DefaultStartingPointIsZero ()
        {
            Rectangle rect = new Rectangle (5, 5);

            Assert.That (rect.Start, Is.EqualTo (Point.Zero));
        }

        [Test]
        public void OneByOneRectangleHasOnePoint ()
        {
            Rectangle rect = new Rectangle (1, 1);

            Assert.That (rect.Count (), Is.EqualTo (1));
        }

        [Test]
        public void FourByFourRectangleHasSixteenPoints ()
        {
            Rectangle rect = new Rectangle (4, 4);

            Assert.That (rect.Count (), Is.EqualTo (16));
        }
    }
}

