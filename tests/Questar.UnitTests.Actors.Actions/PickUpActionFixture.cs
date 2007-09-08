/*******************************************************************************
 *  PickUpActionFixture.cs: Unit Tests for PickUpAction objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;

using Questar.Actors;
using Questar.Actors.Actions;
using Questar.Items;
using Questar.MockObjects;
using Questar.Primitives;

namespace Questar.UnitTests.Actors.Actions
{
    [TestFixture]
    public class PickUpActionFixture
    {
        private Actor actor;
        private Item item;

        [SetUp]
        public void SetUp ()
        {
            Location location = new Location (null, new Point (5, 5));

            actor = new MockActor (location);
            item = new MockItem (location);
        }

        [Test]
        public void PickUp ()
        {
            Assert.IsFalse (actor.Inventory.Contains (item));

            new PickUpAction (actor, item).Execute ();

            Assert.IsTrue (actor.Inventory.Contains (item));
        }

        [Test]
        [ExpectedException (typeof (ImpossibleActionException))]
        public void PickUpOwnedItem ()
        {
            item.Owner = new MockActor ();
            new PickUpAction (actor, item).Execute ();
        }

        [Test]
        [ExpectedException (typeof (ImpossibleActionException))]
        public void DifferentLocation ()
        {
            actor.Move (new Location (null, new Point (50, 50)));
            new PickUpAction (actor, item).Execute ();
        }
    }
}

