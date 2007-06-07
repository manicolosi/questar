/*******************************************************************************
 *  TestPoint.cs: Unit Tests for Point struct.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Base;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestPoint
    {
        [Test]
        public void Creation ()
        {
            Point p = new Point (30, 20);
            Assert.AreEqual (30, p.X);
            Assert.AreEqual (20, p.Y);
        }

        [Test]
        public void CreationNoParameters ()
        {
            Point p = new Point ();
            Assert.AreEqual (0, p.X);
            Assert.AreEqual (0, p.Y);
        }

        [Test]
        public void ValueType ()
        {
            Point p = new Point ();
            Point b = p;
            b.X = 30;
            Assert.AreEqual (0, p.X);
        }
    }
}

