/*******************************************************************************
 *  PickupMessageWidget.cs: A widget that allows the player to pickup an
 *  Item the Hero is standing on.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using System;

using Questar.Actors;
using Questar.Core;

namespace Questar.Gui
{
    public class PickupMessageWidget : VBox
    {
        private bool really_show = false;

        public PickupMessageWidget ()
        {
            BuildWidget ();
            SetupHandlers ();
        }

        private void BuildWidget ()
        {
            base.Add (new Label ("Blah"));
        }

        private void SetupHandlers ()
        {
            TurnLoop turn_loop = Game.Instance.TurnLoop;
            Actor hero = Game.Instance.Hero;

            turn_loop.NewRound += delegate {
                if (hero.Location.Item != null) {
                    really_show = true;
                    base.Show ();
                }
                else
                {
                    really_show = false;
                    base.Hide ();
                }
            };
        }

        protected override void OnShown ()
        {
            if (really_show)
                base.OnShown ();
        }
    }
}

