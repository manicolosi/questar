/*******************************************************************************
 *  ActorFixture.cs: Unit Tests for Actor objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;

using Questar.Actors;
using Questar.Items;
using Questar.MockObjects;

namespace Questar.UnitTests.Actors
{
    [TestFixture]
    public class ActorFixture
    {
        private Actor actor;

        [SetUp]
        public void SetUp ()
        {
            actor = new MockActor ();
        }

        [Test]
        public void EmptyInventory ()
        {
            Assert.IsTrue (actor.Inventory.IsEmpty);
        }
    }
}

