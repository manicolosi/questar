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

        [Test]
        public void IEnumerable ()
        {
            Rectangle rect = new Rectangle (10, 10);
            bool [] points = new bool [rect.Width * rect.Height];
            int i = 0;
            foreach (Point p in rect) {
                Assert.IsTrue (p.X >= rect.Start.X);
                Assert.IsTrue (p.Y >= rect.Start.Y);
                Assert.IsTrue (p.X < rect.Width);
                Assert.IsTrue (p.Y < rect.Height);
                points[i] = true;
                i++;
            }

            foreach (bool p in points)
                Assert.IsTrue (p, p.ToString ());
        }
    }
}

