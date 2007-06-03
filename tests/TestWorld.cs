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

        public static GLib.MainLoop Loop;

        [SetUp]
        public void SetupWorldTests ()
        {
            Loop = new GLib.MainLoop ();

            actor1 = new MockActor ("Mock Actor 1");
            actor2 = new MockActor ("Mock Actor 2");

            World.RecreateInstanceForTesting ();
            world = World.Instance;
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
            Loop.Run ();

            // Only actor1 should have taken a turn at this point.
            Assert.IsTrue (actor1.TookTurnInRound (0));
            Assert.IsFalse (actor2.TookTurnInRound (0));
            Assert.AreSame (actor2, world.CurrentActor);

            // Make actor2 take it's turn.
            actor2.MakeTurnReady ();
            Loop.Run ();

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
            Loop.Run ();

            Assert.AreEqual (1, world.Round);
        }
    }
}

