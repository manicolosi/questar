/*******************************************************************************
 *  AttackAction.cs: Action that melee attacks another Actor.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Primitives;

namespace Questar.Actors.Actions
{
    public class AttackAction : IAction
    {
        private Actor attacker;
        private Actor target;

        public AttackAction (Actor attacker, Actor target)
        {
            this.attacker = attacker;
            this.target = target;
        }

        public void Execute ()
        {
            CheckAdjacency ();

            int damage = attacker.GetAttackDamage (target);
            target.TakeDamage (attacker, damage);
        }

        public void CheckAdjacency ()
        {
            bool found = false;

            foreach (Direction direction in Direction.Directions) {
                Point p = direction.ApplyToPoint (attacker.Location);
                if (attacker.Map[p].Actor == target)
                    found = true;
            }

            if (!found)
                throw new ApplicationException (
                    "Target is not adjacent to attacker.");
        }
    }
}

