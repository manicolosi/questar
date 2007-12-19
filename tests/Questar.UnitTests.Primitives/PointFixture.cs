/*******************************************************************************
 *  PointFixture.cs: Unit Tests for Point values.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Extensions;
using Questar.Primitives;

namespace Questar.UnitTests.Primitives
{
    [TestFixture]
    public class PointFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void DirectionOfReturnsCorrectDirection ()
        {
            Point p = new Point (5, 5);

            Assert.That (p.DirectionOf (new Point (5, 5)), Is.EqualTo (Direction.None));
            Assert.That (p.DirectionOf (new Point (5, 4)), Is.EqualTo (Direction.North));
            Assert.That (p.DirectionOf (new Point (6, 4)), Is.EqualTo (Direction.NorthEast));
            Assert.That (p.DirectionOf (new Point (6, 5)), Is.EqualTo (Direction.East));
            Assert.That (p.DirectionOf (new Point (6, 6)), Is.EqualTo (Direction.SouthEast));
            Assert.That (p.DirectionOf (new Point (5, 6)), Is.EqualTo (Direction.South));
            Assert.That (p.DirectionOf (new Point (4, 6)), Is.EqualTo (Direction.SouthWest));
            Assert.That (p.DirectionOf (new Point (4, 5)), Is.EqualTo (Direction.West));
            Assert.That (p.DirectionOf (new Point (4, 4)), Is.EqualTo (Direction.NorthWest));
        }
    }
}

