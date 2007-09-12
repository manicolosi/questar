/*******************************************************************************
 *  ActorLocation.cs: A Location used when an Entity is on another
 *  Actor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Items;
using Questar.Maps;

namespace Questar.Primitives
{
    public class ActorLocation : Location
    {
        private Actor actor;

        public ActorLocation (Actor actor)
        {
            if (actor == null)
                throw new ApplicationException ("Actor is null");

            this.actor = actor;
        }

        public override Map Map
        {
            get { return actor.Location.Map; }
        }

        public override Point Point
        {
            get { return actor.Location.Point; }
        }

        public override Actor Actor
        {
            get { return actor; }
        }

        public override Item Item
        {
            // Maybe Inventory should be an Item and this could return
            // that.
            get { return null; }
        }

        public override bool IsAdjacentTo (Location loc)
        {
            return actor.Location.IsAdjacentTo (loc);
        }

        public override bool IsClear
        {
            get { return true; }
        }

        public override IEnumerable<Location> AdjacentLocations
        {
            get { return actor.Location.AdjacentLocations; }
        }

        public override IEnumerable<Actor> AdjacentActors
        {
            get { return actor.Location.AdjacentActors; }
        }

        public override Direction DirectionOf (Location loc)
        {
            return actor.Location.DirectionOf (loc);
        }

        public override GridInformation GridInformation
        {
            get { return actor.Location.GridInformation; }
        }

        public override string ToString ()
        {
            return actor.Name;
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            if (!base.Equals (o))
                return false;

            ActorLocation loc = o as ActorLocation;
            if ((loc != null) && (Actor == loc.Actor))
                return true;

            return false;
        }
    }
}

