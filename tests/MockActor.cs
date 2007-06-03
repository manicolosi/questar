using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;

namespace Questar.UnitTests
{
    public class MockActor : Actor
    {
        bool is_turn_ready = false;
        Dictionary<int, bool> turns = new Dictionary<int, bool> ();

        public MockActor (string name)
        {
            base.Name = name;
            base.HitPoints = new HitPoints (0, 0);
            base.Tile = "Blah";
            base.Location = new Point (0, 0);
            base.Map = new Map ();
        }

        public override bool IsTurnReady
        {
            get { return is_turn_ready; }
        }

        public override IAction Action
        {
            get {
                if (!is_turn_ready)
                    throw new InvalidOperationException (
                        "An action is unavailable, check IsTurnReady first");

                is_turn_ready = false;
                turns.Add (World.Instance.Round, true);

                TestWorld.Loop.Quit ();

                return new DoNothingAction (this);
            }
        }

        public void MakeTurnReady ()
        {
            is_turn_ready = true;
            World.Instance.IsPaused = false;
        }

        public bool TookTurnInRound (int round)
        {
            return turns.ContainsKey (round);
        }
    }
}

