/*******************************************************************************
 *  TurnLoop.cs: Keeps tracks of Actor's turns and game rounds.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Helpers;
using Questar.Primitives;

namespace Questar.Core
{
    public class NewRoundEventArgs : System.EventArgs
    {
        public int Round;
    }

    public class TurnLoop
    {
        public event EventHandler<NewRoundEventArgs> NewRound;

        private List<Actor> actors = new List<Actor> ();
        private int round = -1;
        private int turn_index = 10000;

        public TurnLoop ()
        {
            // This event will replaced with something more generic.
            MonsterFactory.Instance.Created += ActorCreatedHandler;
        }

        public int Round
        {
            get { return round; }
            private set {
                round = value;

                EventHelper.Raise (this, NewRound,
                    delegate (NewRoundEventArgs args) {
                        args.Round = round;
                    });
            }
        }

        public Actor CurrentActor
        {
            get { return actors[turn_index]; }
        }

        // Returns false when the ITurnLoopDriver should stop.
        public bool NextTurn ()
        {
            if (turn_index >= actors.Count) {
                turn_index = 0;
                Round++;
            }

            if (actors.Count == 0)
                return false;

            if (!CurrentActor.IsTurnReady)
                return false;

            CurrentActor.TakeTurn ();
            turn_index++;

            return true;
        }

        private void ActorCreatedHandler (object sender, EntityCreatedEventArgs args)
        {
            Actor actor = (Actor) args.Entity;
            actor.Destroyed += ActorDestroyedHandler;

            // HACK: Get the hero in the first position.
            if (actor is Hero)
                actors.Insert (0, actor);
            else
                actors.Add (actor);
        }

        private void ActorDestroyedHandler (object sender, EventArgs args)
        {
            actors.Remove ((Actor) sender);
        }
    }
}

