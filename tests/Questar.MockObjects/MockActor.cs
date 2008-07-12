/*******************************************************************************
 *  MockActor.cs: Mock object that implements the Actor abstract class.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Actors.Actions;
using Questar.Base;
using Questar.Primitives;

namespace Questar.MockObjects
{
    public class MockActor : AbstractActor
    {
        public MockActor (Location location)
        {
            base.Tile = "mock";
            base.Name = "Mock Actor";
            base.HitPoints = new HitPoints (100, 100);

            base.Location = location;

            MonsterFactory.Instance.FireTheCreationEventHack (this);
        }
    }
}

