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

        [Test]
        public void XAndYPropertiesAreSameAsStartingPoint ()
        {
            Rectangle rect = new Rectangle (1, 2, 3, 4);

            Assert.That (rect.X, Is.EqualTo (rect.Start.X));
            Assert.That (rect.Y, Is.EqualTo (rect.Start.Y));
        }

        [Test]
        public void EndingPointIsCorrect ()
        {
            Rectangle rect = new Rectangle (2, 2, 4, 4);

            Assert.That (rect.End, Is.EqualTo (new Point (5, 5)));
        }

        [Test]
        public void StringConversionIsInX11GeometryFormat ()
        {
            string output = new Rectangle (10, 20, 30, 40).ToString ();

            Assert.That (output, Is.EqualTo ("10x20+30+40"));
        }

        [Test]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void RectangleSizeCannotBeSmallerThanOneByOne ()
        {
            Rectangle a = new Rectangle (0, 0);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void NonIntersectingRectanglesNotAllowed ()
        {
            Rectangle a = new Rectangle (0, 0, 2, 2);
            Rectangle b = new Rectangle (2, 0, 2, 2);
            Rectangle c = a.IntersectionWith (b);
        }

        [Test]
        public void BothRectanglesMustContainEachPointOfTheirIntersection ()
        {
            Rectangle a = new Rectangle (1, 1, 5, 5);
            Rectangle b = new Rectangle (3, 3, 5, 5);
            Rectangle c = a.IntersectionWith (b);

            foreach (Point p in c) {
                Assert.That (a.Contains (p), Is.True);
                Assert.That (b.Contains (p), Is.True);
            }
        }

        [Test]
        public void EqualRectanglesIntersectionIsTheSame ()
        {
            Rectangle a = new Rectangle (1, 2, 3, 4);
            Rectangle b = new Rectangle (1, 2, 3, 4);
            Rectangle c = a.IntersectionWith (b);

            Assert.That (c, Is.EqualTo (new Rectangle (1, 2, 3, 4)));
        }

        [Test]
        public void IntersectionWithParameterOrderDoesntMatter ()
        {
            Rectangle a = new Rectangle (1, 1, 5, 5);
            Rectangle b = new Rectangle (3, 3, 5, 5);
            Rectangle c = a.IntersectionWith (b);
            Rectangle d = b.IntersectionWith (a);

            Assert.That (c, Is.EqualTo (d));
        }

        [Test]
        public void RectangleInsideOfAnotherIntersection ()
        {
            Rectangle a = new Rectangle (2, 2, 5, 5);
            Rectangle b = new Rectangle (3, 3, 3, 3);
            Rectangle c = a.IntersectionWith (b);

            Assert.That (c, Is.EqualTo (new Rectangle (3, 3, 3, 3)));
        }

        [Test]
        public void SameStartingPointIntersectionHasSmallestDimensions ()
        {
            Rectangle a = new Rectangle (10, 15);
            Rectangle b = new Rectangle (15, 10);
            Rectangle c = a.IntersectionWith (b);

            Assert.That (c, Is.EqualTo (new Rectangle (0, 0, 10, 10)));
        }

        [Test]
        public void SinglePointIntersection ()
        {
            Rectangle a = new Rectangle (0, 0, 2, 2);
            Rectangle b = new Rectangle (1, 1, 2, 2);
            Rectangle c = a.IntersectionWith (b);

            Assert.That (c, Is.EqualTo (new Rectangle (1, 1, 1, 1)));
        }

        [Test]
        public void EqualIntersection ()
        {
            Rectangle a = new Rectangle (1, 1, 5, 5);
            Rectangle b = new Rectangle (3, 3, 5, 5);
            Rectangle c = a.IntersectionWith (b);

            Assert.That (c, Is.EqualTo (new Rectangle (3, 3, 3, 3)));
        }
    }
}

