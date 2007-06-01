using System;

using Questar.Actors.Actions;
using Questar.Base;
using Questar.Gui;
using Questar.Maps;

namespace Questar.Actors
{
    public class Hero : Actor
    {
        private IAction action = null;
        private DateTime last_action;

        public Hero (Map map)
        {
            base.Tile = "hero";
            base.Name = "Hero";
            base.Map = map;

            Point p;
            do {
                p = Point.GetRandom (map.Width, map.Height);
            }
            while (!base.CanMoveTo (p));
            base.Location = p;

            SetupHandlers ();
        }

        public override bool IsTurnReady
        {
            get {
                return action != null;
            }
        }

        public override IAction Action
        {
            get {
                IAction action = this.action;
                this.action = null;
                return action;
            }
        }

        private void SetupHandlers ()
        {
            // TODO: Should the Hero be a regular Actor that has a PlayerAI?
            // There definitely needs to be some sort of ActorFactory thing.
            //
            // An alternative to all this is to have the MainWindow send all keypresses
            // to the Hero and see if a key is found in a table of actions.
            last_action = DateTime.Now;

            UIActions.Instance["MoveNorth"].Activated += delegate {
                CreateMoveAction (Direction.North);
            };
            UIActions.Instance["MoveSouth"].Activated += delegate {
                CreateMoveAction (Direction.South);
            };
            UIActions.Instance["MoveWest"].Activated += delegate {
                CreateMoveAction (Direction.West);
            };
            UIActions.Instance["MoveEast"].Activated += delegate {
                CreateMoveAction (Direction.East);
            };
            UIActions.Instance["MoveNorthEast"].Activated += delegate {
                CreateMoveAction (Direction.NorthEast);
            };
            UIActions.Instance["MoveSouthEast"].Activated += delegate {
                CreateMoveAction (Direction.SouthEast);
            };
            UIActions.Instance["MoveNorthWest"].Activated += delegate {
                CreateMoveAction (Direction.NorthWest);
            };
            UIActions.Instance["MoveSouthWest"].Activated += delegate {
                CreateMoveAction (Direction.SouthWest);
            };

            UIActions.Instance["DoNothing"].Activated += delegate {
                AddAction (new DoNothingAction (this));
            };
        }

        private void CreateMoveAction (Direction direction)
        {
            GridInformation info = base.Map.GetGridInformation (
                direction.ApplyToPoint (base.Location));

            switch (info)
            {
                case GridInformation.Invalid:
                    Messages.Instance.Add ("You can't go that way!");
                    break;

                case GridInformation.BlockingTerrain:
                    Messages.Instance.Add ("There is something in the way.");
                    break;

                case GridInformation.Occupied:
                    Messages.Instance.Add ("Someone is in the way.");
                    break;

                default:
                    AddAction (new MoveAction (this, direction));
                    break;
            }
        }

        private void AddAction (IAction action)
        {
            if ((int) (DateTime.Now - last_action).Milliseconds < 100)
                return;

            if (this.action != null)
                return;

            this.action = action;
            last_action = DateTime.Now;

            World.Instance.IsPaused = false;
        }
    }
}

