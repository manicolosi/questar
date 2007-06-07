/*******************************************************************************
 *  TestRectangle.cs: Unit Tests for Rectangle struct.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Base;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestRectangle
    {
        [Test]
        public void Creation ()
        {
            Rectangle r = new Rectangle (new Point (15, 10), 30, 20);
            Assert.AreEqual (30, r.Width);
            Assert.AreEqual (20, r.Height);
            Assert.AreEqual (15, r.Start.X);
            Assert.AreEqual (10, r.Start.Y);
        }

        [Test]
        public void CreationNoStart ()
        {
            Rectangle r = new Rectangle (5, 8);
            Assert.AreEqual (new Point (), r.Start);
        }
    }
}

