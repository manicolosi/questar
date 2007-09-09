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

        public Map Map
        {
            get { return actor.Location.Map; }
        }

        public Point Point
        {
            get { return actor.Location.Point; }
        }

        public Actor Actor
        {
            get { return actor; }
        }

        public Item Item
        {
            // Maybe Inventory should be an Item and this could return
            // that.
            get { return null; }
        }

        public bool IsAdjacentTo (Location loc)
        {
            return actor.Location.IsAdjacentTo (loc);
        }

        public bool IsClear
        {
            get { return true; }
        }

        public IEnumerable<MapLocation> AdjacentLocations
        {
            get { return actor.Location.AdjacentLocations; }
        }

        public IEnumerable<Actor> AdjacentActors
        {
            get { return actor.Location.AdjacentActors; }
        }

        public Direction DirectionOf (Location loc)
        {
            return actor.Location.DirectionOf (loc);
        }

        public GridInformation GridInformation
        {
            get { return actor.Location.GridInformation; }
        }

        public override string ToString ()
        {
            return String.Format ("On {0}", actor);
        }

        public override int GetHashCode ()
        {
            return ToString ().GetHashCode ();
        }

        public override bool Equals (object o)
        {
            ActorLocation loc = o as ActorLocation;

            if (this.actor == loc.actor)
                return true;

            return false;
        }

        public static bool operator == (ActorLocation a, ActorLocation b)
        {
            return a.Equals (b);
        }

        public static bool operator != (ActorLocation a, ActorLocation b)
        {
            return !(a == b);
        }
    }
}

