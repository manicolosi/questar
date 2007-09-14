//
// World.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using GLib;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Core;
using Questar.Helpers;
using Questar.Maps;
using Questar.Primitives;

using Timeout = GLib.Timeout;

namespace Questar.Base
{
    public class WorldActorEventArgs : EventArgs
    {
        public Actor Actor;
    }

    public class WorldNewRoundEventArgs : EventArgs
    {
        public int Round;
    }

    public class World
    {
        public event EventHandler<WorldNewRoundEventArgs> NewRound;

        private TurnLoop turn_loop;
        private ITurnLoopDriver driver;

        private Actor hero;
        private Map map;

        public World ()
        {
            turn_loop = new TurnLoop ();
            turn_loop.NewRound += delegate {
                EventHelper.Raise<WorldNewRoundEventArgs> (this, NewRound, null);
            };
            driver = new IdleTurnLoopDriver (turn_loop);

            MonsterFactory.Instance.Created += OnMonsterCreated;
        }

        public void Start ()
        {
            driver.Start ();
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public int Round
        {
            get { return turn_loop.Round; }
        }

        public bool IsPaused
        {
            get { return !driver.IsRunning; }
            set {
                if (value)
                    driver.Stop ();
                else
                    driver.Start ();
            }
        }

        public Actor Hero
        {
            get { return hero; }
        }

        private void OnMonsterCreated (object sender, EntityCreatedEventArgs args)
        {
            Actor actor = (Actor) args.Entity;
            actor.Destroyed += OnActorDestroyed;

            // HACK
            if (actor is Hero)
                this.hero = actor;
        }

        private void OnActorDestroyed (object sender, EventArgs args)
        {
            // HACK
            if (sender is Hero)
                return;
        }

        //public Actor CurrentActor
        //{
            //get { return actors[turn_index]; }
        //}
    }
}

