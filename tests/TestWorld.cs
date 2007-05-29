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

        [SetUp]
        public void SetupWorldTests ()
        {
            actor1 = new MockActor ("Mock Actor 1");
            actor2 = new MockActor ("Mock Actor 2");

            world = World.Instance;
        }

        [Test]
        public void WorldCreation ()
        {
            Assert.AreEqual (0, world.Round);
            Assert.AreEqual (true, world.IsPaused);
        }

        [Test]
        public void StillPausedWhenStarted ()
        {
            world.Start ();
            Assert.AreEqual (0, world.Round);
            Assert.AreEqual (true, world.IsPaused);
        }

        [Test]
        public void ActorTurnTaking ()
        {
            world.AddActor (actor1);
            world.AddActor (actor2);
            world.Start ();

            // Nobody should have taken a turn at this point.
            Assert.AreEqual (false, actor1.TookTurnInRound (0));
            Assert.AreEqual (false, actor2.TookTurnInRound (0));
            Assert.AreSame (actor1, world.CurrentActor);

            // Make actor1 take it's turn.
            actor1.MakeTurnReady ();
            Assert.AreEqual (true, actor1.TookTurnInRound (0));
            Assert.AreEqual (false, actor2.TookTurnInRound (0));
            Assert.AreSame (actor2, world.CurrentActor);

            // Make actor2 take it's turn. world.Round must now be 1.
            actor2.MakeTurnReady ();
            Assert.AreEqual (true, actor1.TookTurnInRound (0));
            Assert.AreEqual (true, actor2.TookTurnInRound (0));
            Assert.AreSame (actor2, world.CurrentActor);
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

            Assert.AreEqual (1, world.Round);
        }

        [Test]
        [ExpectedException (typeof (InvalidOperationException))]
        public void ExceptionWhenStartingWithoutActors ()
        {
            world.Start ();
        }
    }
}

