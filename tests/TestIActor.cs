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

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestIActor
    {
        IActor actor;

        [SetUp]
        public void Setup ()
        {
            actor = new MockActor ("Tom");
        }

        [Test]
        public void Properties ()
        {
            Assert.AreEqual ("Tom", actor.Name);
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

