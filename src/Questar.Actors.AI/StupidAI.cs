/*******************************************************************************
 *  StupidAI.cs: A stupid AI for testing.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;

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
            Console.WriteLine ("{0} sees {1}",
                base.Actor, args.Actor);
        }

        protected override void OnActorLostSight (object sender,
            ActorEventArgs args)
        {
            Console.WriteLine ("{0} does not see {1} anymore",
                base.Actor, args.Actor);
        }
    }
}

