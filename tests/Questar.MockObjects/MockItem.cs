/*******************************************************************************
 *  MockItem.cs: Mock object that implements the Item abstract class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Questar.Actors;
using Questar.Items;
using Questar.Primitives;

namespace Questar.MockObjects
{
    public class MockItem : Item
    {
        private int count = 0;

        public MockItem (Point location)
        {
            base.Location = location;
        }

        public MockItem () : this (new Point (0, 0))
        {
        }

        public override void Use (Actor target)
        {
            count++;
        }

        public int UsedCount
        {
            get { return count; }
        }
    }
}

