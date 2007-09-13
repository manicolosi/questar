/*******************************************************************************
 *  AbstractActor.cs: Actors that are controlled by the user.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Gui;
using Questar.Helpers;
using Questar.Items;
using Questar.Maps;
using Questar.Primitives;

namespace Questar.Actors
{
    public class Hero : AbstractActor
    {
        private Action action = null;
        private DateTime last_action;

        public Hero ()
        {
            base.Tile = "hero";
            base.Name = "Hero";
            base.HitPoints = new HitPoints (100, 400);

            do {
                base.Location = MapLocation.GetRandom (Game.Instance.World.Map);
            }
            while (!base.CanMoveTo (base.Location));

            SetupHandlers ();

            MonsterFactory.Instance.FireTheCreationEventHack (this);
        }

        public override bool IsTurnReady
        {
            get {
                return action != null;
            }
        }

        public override Action Action
        {
            get {
                Action current_action = action;
                action = null;
                return current_action;
            }
        }

        private void SetupHandlers ()
        {
            // TODO: Should the Hero be a regular Actor that has a PlayerAI?
            // There definitely needs to be some sort of ActorFactory thing.

            // An alternative to all this is to have the MainWindow send
            // all keypresses to the Hero and see if a key is found in a
            // table of actions.
            last_action = DateTime.Now;

            UIActions.Instance["MoveNorth"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.North));
            };
            UIActions.Instance["MoveSouth"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.South));
            };
            UIActions.Instance["MoveWest"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.West));
            };
            UIActions.Instance["MoveEast"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.East));
            };
            UIActions.Instance["MoveNorthEast"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.NorthEast));
            };
            UIActions.Instance["MoveSouthEast"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.SouthEast));
            };
            UIActions.Instance["MoveNorthWest"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.NorthWest));
            };
            UIActions.Instance["MoveSouthWest"].Activated += delegate {
                AddAction (CreateMoveAction (Direction.SouthWest));
            };

            UIActions.Instance["DoNothing"].Activated += delegate {
                AddAction (new DoNothingAction (this));
            };

            UIActions.Instance["PickUp"].Activated += delegate {
                AddAction (CreatePickUpAction ());
            };
        }

        private Action CreatePickUpAction ()
        {
            Item item = base.Location.Item;

            if (item == null) {
                Messages.Instance.Add ("Nothing here to pick up!");
                return null;
            }

            return new PickUpAction (this, item);
        }

        private Action CreateMoveAction (Direction direction)
        {
            Action action = null;
            Location loc = direction.ApplyTo (base.Location);
            GridInformation info = loc.GridInformation;

            switch (info)
            {
                case GridInformation.Invalid:
                    Messages.Instance.Add ("You can't go that way!");
                    break;

                case GridInformation.BlockingTerrain:
                    Messages.Instance.Add ("There is something in the way.");
                    break;

                case GridInformation.Occupied:
                    AddAction (new AttackAction (this, loc.Actor));
                    break;

                default:
                    action = new MoveAction (this, direction);
                    break;
            }

            return action;
        }

        public void AddAction (Action action)
        {
            if (action == null)
                return;

            if ((int) (DateTime.Now - last_action).Milliseconds < 100)
                return;

            if (this.action != null)
                return;

            this.action = action;
            last_action = DateTime.Now;

            Game.Instance.World.IsPaused = false;
        }

        protected override void OnDeath ()
        {
            base.HitPoints = new HitPoints (100);
            base.OnDeath ();
        }

        public override string ToString ()
        {
            return "you";
        }
    }
}

