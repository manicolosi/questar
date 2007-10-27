/*******************************************************************************
 *  ActorEventArgs.cs: Various EventArgs derivatives for Actor events.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Actors
{
    public class ActorEventArgs : EventArgs
    {
        public Actor Actor;
    }

    public class ActorSightedEventArgs : ActorEventArgs
    {
    }

    public class ActorLostSightEventArgs : ActorEventArgs
    {
    }
}

