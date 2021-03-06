//
// HitPointsTest.cs: Tests for HitPoints class.
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
// Copyright (C) 2007
//

using NUnit.Framework;
using System;

using Questar.Actors;

namespace Questar.UnitTests
{
    [TestFixture]
    public class HitPointsTests
    {
        HitPoints hp;

        [SetUp]
        public void Setup ()
        {
            hp = new HitPoints (25, 30);
        }

        [Test]
        public void Creation ()
        {
            Assert.AreEqual (25, hp.Current);
            Assert.AreEqual (30, hp.Max);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void CreationWithBadArguments ()
        {
            hp = new HitPoints (10, 5);
        }

        [Test]
        public void HPToString ()
        {
            StringAssert.IsMatch ("25/30", hp.ToString ());
        }

        [Test]
        public void CurrentChangedEvent ()
        {
            bool changed = false;
            hp.Changed += delegate { changed = true; };
            hp.Current -= 5;
            Assert.AreEqual (20, hp.Current);
            Assert.IsTrue (changed);
        }

        [Test]
        public void MaxChangedEvent ()
        {
            bool changed = false;
            hp.Changed += delegate { changed = true; };
            hp.Max -= 5;
            Assert.AreEqual (25, hp.Max);
            Assert.IsTrue (changed);
        }

        [Test]
        public void Equality ()
        {
            Assert.IsTrue (new HitPoints (10, 15) == new HitPoints (10, 15));
            Assert.IsTrue (new HitPoints (10, 15) != new HitPoints (50, 85));
        }

        [Test]
        public void HitPointsEventArgs ()
        {
            HitPoints old = hp.Clone () as HitPoints;
            hp.Changed += delegate (object sender, HitPointsEventArgs args) {
                Assert.AreEqual (old, args.OldHitPoints);
            };
            hp.Current += 5;
        }

        [Test]
        public void IsEmpty ()
        {
            Assert.IsFalse (hp.IsEmpty);
            hp.Current = 0;
            Assert.IsTrue (hp.IsEmpty);
        }

        [Test]
        public void IsFull ()
        {
            Assert.IsFalse (hp.IsFull);
            hp.Current = hp.Max;
            Assert.IsTrue (hp.IsFull);
        }
    }
}

