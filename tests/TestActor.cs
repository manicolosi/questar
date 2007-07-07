/*******************************************************************************
 *  TestActor.cs: Unit Tests for Actor class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;
using System;

using Questar.Actors;
using Questar.Base;
using Questar.Primitives;
using Questar.MockObjects;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestActor
    {
        Actor actor;

        [SetUp]
        public void Setup ()
        {
            actor = new MockActor ("Tom");
        }

        [Test]
        public void Properties ()
        {
            Assert.AreEqual ("Tom", actor.Name);
            Assert.AreEqual (new HitPoints (0, 0), actor.HitPoints);
        }

        [Test]
        public void MovedEvent ()
        {
            bool changed = false;
            actor.Moved += delegate { changed = true; };
            actor.Move (new Point (1, 1));
            Assert.IsTrue (changed);
        }
    }
}

