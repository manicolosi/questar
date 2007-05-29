using GLib;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Maps;

namespace Questar.Base
{
    public class WorldActorAddedEventArgs : EventArgs
    {
        public IActor Actor;
    }

    public class WorldNewRoundEventArgs : EventArgs
    {
        public int Round;
    }

    public class World
    {
        private static World instance;

        public static World Instance
        {
            get {
                if (instance == null)
                    instance = new World ();

                return instance;
            }
        }

        private List<IActor> actors = new List<IActor> ();
        private IActor hero;
        private Map map;

        private int turn_index = 0;
        private int round;
        private bool is_paused;

        public event EventHandler<WorldActorAddedEventArgs> ActorAdded;
        public event EventHandler<WorldNewRoundEventArgs> NewRound;

        private World ()
        {
        }

        public void Start ()
        {
            Round = 0;
            IsPaused = false;
        }

        public IActor Hero
        {
            get { return hero; }
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

                Events.FireEvent<WorldNewRoundEventArgs> (this, NewRound,
                    delegate (WorldNewRoundEventArgs args) {
                        args.Round = Round;
                    });
            }
        }

        public bool IsPaused
        {
            get { return is_paused; }
            set {
                is_paused = value;

                if (!is_paused) {
                    if (actors.Count == 0)
                        throw new InvalidOperationException (
                            "Actors must be added to the World before starting or unpausing it.");

                    Idle.Add (NextTurn);
                }
            }
        }

        public IActor CurrentActor
        {
            get { return actors[turn_index]; }
        }

        public void AddActor (IActor actor)
        {
            Hero hero = actor as Hero;
            if (hero != null)
                this.hero = hero;

            actors.Add (actor);

            Events.FireEvent<WorldActorAddedEventArgs> (this, ActorAdded,
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
            }

            return true;
        }
    }
}

