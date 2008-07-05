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

        private bool IsHostile (Actor target)
        {
            // Always hostile towards the Hero.
            if (target is Hero)
                return true;

            return false;
        }

        protected override void OnActorSighted (object sender,
            ActorEventArgs args)
        {
            base.Action = new MoveTowardsTargetAction (base.Actor, args.Actor);
            Console.WriteLine ("{0} sees {1}",
                base.Actor, args.Actor);
        }

        protected override void OnActorLostSight (object sender,
            ActorEventArgs args)
        {
            base.Action = new DoNothingAction (base.Actor);
            Console.WriteLine ("{0} does not see {1} anymore",
                base.Actor, args.Actor);
        }

        protected override void OnActorAdjacent (object sender,
            ActorEventArgs args)
        {
            Actor target = args.Actor;
            if (IsHostile (target)) {
                // Create AttackAction...
            }
            base.Action = new DoNothingAction (base.Actor);
        }
    }
}

