/*******************************************************************************
 *  TestWorld.cs: Unit Tests for World class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using GLib;
using NUnit.Framework;
using System;

using Questar.Base;
using Questar.UnitTests;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestWorld
    {
        private World world;
        MockActor actor1;
        MockActor actor2;

        private MainLoop loop;

        [SetUp]
        public void Setup ()
        {
            loop = new MainLoop ();

            actor1 = new MockActor ("Mock Actor 1", loop);
            actor2 = new MockActor ("Mock Actor 2", loop);

            world = World.Instance;
        }

        [TearDown]
        public void TearDown ()
        {
            Singleton<World>.RecreateForTesting ();
        }

        [Test]
        public void WorldCreation ()
        {
            Assert.AreEqual (0, world.Round);
            Assert.AreEqual (true, world.IsPaused);
        }

        [Test]
        public void NotPausedWhenStarted ()
        {
            world.AddActor (actor1);

            world.Start ();
            Assert.IsFalse (world.IsPaused);
        }

        [Test]
        [ExpectedException (typeof (InvalidOperationException))]
        public void ExceptionWhenStartingWithoutActors ()
        {
            world.Start ();
        }

        [Test]
        public void ActorTurnTaking ()
        {

            world.AddActor (actor1);
            world.AddActor (actor2);
            world.Start ();

            // Nobody should have taken a turn at this point.
            Assert.IsFalse (actor1.TookTurnInRound (0));
            Assert.IsFalse (actor2.TookTurnInRound (0));
            Assert.AreSame (actor1, world.CurrentActor);

            // Make actor1 take it's turn.
            actor1.MakeTurnReady ();
            loop.Run ();

            // Only actor1 should have taken a turn at this point.
            Assert.IsTrue (actor1.TookTurnInRound (0));
            Assert.IsFalse (actor2.TookTurnInRound (0));
            Assert.AreSame (actor2, world.CurrentActor);

            // Make actor2 take it's turn.
            actor2.MakeTurnReady ();
            loop.Run ();

            // Both actor should have taken a turn now.
            Assert.IsTrue (actor1.TookTurnInRound (0));
            Assert.IsTrue (actor2.TookTurnInRound (0));
            Assert.AreSame (actor1, world.CurrentActor);
        }

        [Test]
        public void RoundIncreasesAfterTakingTurns ()
        {
            world.AddActor (actor1);
            world.AddActor (actor2);
            world.Start ();

            Assert.AreEqual (0, world.Round);

            actor1.MakeTurnReady ();
            actor2.MakeTurnReady ();
            loop.Run ();

            Assert.AreEqual (1, world.Round);
        }
    }
}

