using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Base;

namespace Questar.UnitTests
{
    public class MockActor : Actor
    {
        bool is_turn_ready = false;
        Dictionary<int, bool> turns = new Dictionary<int, bool> ();

        public MockActor (string name)
        {
            base.Name = name;
            base.Tile = "Blah";
            base.Location = new Point (0, 0);
            base.Map = null;
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
            return turns[round];
        }
    }
}

