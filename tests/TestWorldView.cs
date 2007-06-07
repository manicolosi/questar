/*******************************************************************************
 *  TestWorldView.cs: Unit Tests for WorldView widget.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Actors;
using Questar.Base;
using Questar.Gui;
using Questar.Maps;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestWorldView
    {
        [Test]
        public void Creation ()
        {
            World world = new World ();
            world.Map = new Map ();
            IActor center = new MockActor ();
            WorldView world_view = new WorldView (world, center);

            Assert.AreSame (world, world_view.World);
            Assert.AreSame (center, world_view.Center);
        }
    }
}

