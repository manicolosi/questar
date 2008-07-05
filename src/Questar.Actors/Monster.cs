/*******************************************************************************
 *  AbstractActor.cs: Actors that are controlled by Questar.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors.AI;
using Questar.Actors.Actions;
using Questar.Core;
using Questar.Primitives;

using Action = Questar.Actors.Actions.Action;

namespace Questar.Actors
{
    public class Monster : AbstractActor
    {
        private string monster_id;
        private string prefix;

        public Monster (MonsterDefinition definition)
        {
            base.AI = new StupidAI (this);

            base.Name = definition.Name;
            base.Tile = definition.TileId;
            base.HitPoints = new HitPoints (definition.MaxHP);

            monster_id = definition.Id;
            prefix = definition.Prefix;

            do {
                base.Location = Location.GetRandom (Game.Instance.CurrentMap);
            }
            while (!base.CanMoveTo (base.Location));
        }

        public override bool IsTurnReady
        {
            get {
                return base.IsTurnReady;
            }
        }

        public string Id
        {
            get { return monster_id; }
        }

        public override string ToString ()
        {
            return String.Format ("{0} {1}", prefix, base.Name);
        }
    }
}

