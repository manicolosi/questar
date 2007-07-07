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
        public MockItem (Point location)
        {
            base.Location = location;
        }

        public override void Use (Actor target)
        {
        }
    }
}

