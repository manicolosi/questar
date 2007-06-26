//
// World.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using GLib;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Helpers;
using Questar.Maps;

using Timeout = GLib.Timeout;

namespace Questar.Base
{
    public class WorldActorAddedEventArgs : EventArgs
    {
        public Actor Actor;
    }

    public class WorldNewRoundEventArgs : EventArgs
    {
        public int Round;
    }

    public class World
    {
        private List<Actor> actors = new List<Actor> ();
        private Actor hero;
        private Map map;

        private int turn_index = 0;
        private int round = 0;
        private bool is_paused = true;

        public event EventHandler<WorldActorAddedEventArgs> ActorAdded;
        public event EventHandler<WorldNewRoundEventArgs> NewRound;

        public World ()
        {
        }

        public void Start ()
        {
            IsPaused = false;
        }

        public Actor Hero
        {
            get { return hero; }
            set {
                this.hero = value;
                AddActor (hero);
            }
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public int Round
        {
            get { return round; }
            private set {
                round = value;

                EventHelper.Raise<WorldNewRoundEventArgs> (this, NewRound,
                    delegate (WorldNewRoundEventArgs args) {
                        args.Round = Round;
                    });
            }
        }

        public bool IsPaused
        {
            get { return is_paused; }
            set {
                if (!value) {
                    if (actors.Count == 0)
                        throw new InvalidOperationException (
                            "Actors must be added to the World before " +
                            "starting or unpausing it.");

                    Idle.Add (NextTurn);
                }

                is_paused = value;
            }
        }

        public Actor CurrentActor
        {
            get { return actors[turn_index]; }
        }

        public void AddActor (Actor actor)
        {
            Hero hero = actor as Hero;
            if (hero != null)
                actors.Insert (0, hero);
            else
                actors.Add (actor);

            EventHelper.Raise<WorldActorAddedEventArgs> (this, ActorAdded,
                delegate (WorldActorAddedEventArgs args) {
                    args.Actor = actor;
                });
        }

        private bool NextTurn ()
        {
            if (!CurrentActor.IsTurnReady) {
                IsPaused = true;
                return false;
            }

            CurrentActor.Action.Execute ();
            turn_index++;

            if (turn_index == actors.Count) {
                turn_index = 0;
                Round++;
                return false;
            }

            return true;
        }
    }
}
