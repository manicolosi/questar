/*******************************************************************************
 *  StupidAI.cs: A stupid AI for testing.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Actors.Actions;

namespace Questar.Actors.AI
{
    public class StupidAI : ArtificialIntelligence
    {
        public StupidAI (Actor actor) : base (actor)
        {
        }

        protected override void OnActorSighted (object sender,
            ActorEventArgs args)
        {
            base.Action = new MoveTowardsTargetAction (base.Actor, args.Actor);
        }

        protected override void OnActorLostSight (object sender,
            ActorEventArgs args)
        {
            base.Action = new DoNothingAction (base.Actor);
        }
    }
}

