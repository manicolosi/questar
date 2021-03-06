/*******************************************************************************
 *  Actor.cs: An interface implemented by Actors.
 *
 *  Copyright (C) 2007, 2009
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

using Questar.Actors.Actions;
using Questar.Items;
using Questar.Primitives;

namespace Questar.Actors
{
    public interface Actor : Entity
    {
        event EventHandler<ActorEventArgs> ActorSighted;
        event EventHandler<ActorEventArgs> ActorLostSight;

        HitPoints HitPoints { get; }
        Inventory Inventory { get; }

        void TakeTurn ();

        bool IsAlive { get; }
        bool IsDead { get; }
        bool IsTurnReady { get; }
        bool IsAdjacentTo (Actor target);
        bool CanMoveTo (Location location);
        bool CanMoveIn (Direction direction);

        IEnumerable<Location> FieldOfView { get; }

        void Move (Location new_location);
        int GetAttackDamage (Actor target);
        void TakeDamage (Actor attacker, int damage);
    }
}

