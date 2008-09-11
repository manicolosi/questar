/*******************************************************************************
 *  EventListFixture.cs: Unit Tests for EventList<T> objects.
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
    public class EventListFixture
    {
        EventList<string> list;
        string item = "Test Item";

        [SetUp]
        public void SetUp ()
        {
            list = new EventList<string> ();
        }

        [Test]
        public void CountIsInitiallyZero ()
        {
            Assert.That (list.Count, Is.EqualTo (0));
        }

        [Test]
        public void CountIsThreeAfterAddingThreeItems ()
        {
            list.Add ("test1");
            list.Add ("test2");
            list.Add ("test3");

            Assert.That (list.Count, Is.EqualTo (3));
        }

        [Test]
        public void EventListsAreNotReadOnly ()
        {
            Assert.That (list.IsReadOnly, Is.False);
        }

        [Test]
        public void AfterAddingListContainsItem ()
        {
            Assert.That (list.Contains (item), Is.False);
            list.Add (item);
            Assert.That (list.Contains (item), Is.True);
        }

        [Test]
        public void AfterRemovingListDoesntContainItem ()
        {
            list.Add (item);

            Assert.That (list.Contains (item), Is.True);
            list.Remove (item);
            Assert.That (list.Contains (item), Is.False);
        }

        [Test]
        public void WhenAddingANewItemAddedEventIsRaised ()
        {
            bool got_event = false;

            list.Added += (sender, args) => { got_event = true; };
            list.Add (item);

            Assert.That (got_event, Is.True);
        }

        [Test]
        public void WhenRemovingAnItemRemovedEventIsRaised ()
        {
            list.Add (item);
            bool got_event = false;

            list.Removed += (sender, args) => { got_event = true; };
            list.Remove (item);

            Assert.That (got_event, Is.True);
        }

        [Test]
        public void NoRemovedEventWhenListDoesntContainItem ()
        {
            bool got_event = false;

            list.Removed += (sender, args) => { got_event = true; };
            list.Remove (item);

            Assert.That (got_event, Is.False);
        }

        [Test]
        public void EventArgsItemPropertyIsTheAddedOrRemovedItem ()
        {
            list.Added += (sender, args) => {
                Assert.That (args.Item, Is.EqualTo (item));
            };

            list.Removed += (sender, args) => {
                Assert.That (args.Item, Is.EqualTo (item));
            };

            list.Add (item);
            list.Remove (item);
        }

        [Test]
        public void ListIsEmtpyAfteringClearingIt ()
        {
            list.Add ("Test 1");
            list.Add ("Test 2");
            list.Add ("Test 3");

            list.Clear ();

            Assert.That (list, Is.Empty);
        }

        [Test]
        public void ImplementsIEnumerableAndFunctions ()
        {
            list.Add ("Test 1");
            list.Add ("Test 2");
            list.Add ("Test 3");

            int count = 0;
            list.Each (item => { count++; });

            Assert.That (count, Is.EqualTo (3));
        }

        [Test]
        public void RemovedEventGetsRaisedForEachItemWhenCleared ()
        {
            list.Add ("Test 1");
            list.Add ("Test 2");
            list.Add ("Test 3");

            int removed_count = 0;

            list.Removed += (sender, item) => { removed_count++; };
            list.Clear ();

            Assert.That (removed_count, Is.EqualTo (3));
        }
    }
}

