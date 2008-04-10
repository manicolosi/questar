/*******************************************************************************
 *  ArtificialIntelligence.cs: Controls an Actor's Actions.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Actors.Actions;

using Action = Questar.Actors.Actions.Action;

namespace Questar.Actors.AI
{
    public abstract class ArtificialIntelligence
    {
        private Actor actor;
        private Action action;

        public ArtificialIntelligence (Actor actor)
        {
            this.actor = actor;

            actor.ActorSighted += OnActorSighted;
            actor.ActorLostSight += OnActorLostSight;
        }

        public Actor Actor
        {
            get { return actor; }
        }

        public Action Action
        {
            get {
                if (action == null)
                    action = new DoNothingAction (actor);

                return action;
            }
            protected set { action = value; }
        }

        protected abstract void OnActorSighted (object sender,
            ActorEventArgs args);

        protected abstract void OnActorLostSight (object sender,
            ActorEventArgs args);
    }
}

