/*******************************************************************************
 *  PickupMessageWidget.cs: A widget that allows the player to pickup an
 *  Item the Hero is standing on.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gdk;
using Gtk;
using System;

using Questar.Actors;
using Questar.Core;
using Questar.Helpers;

using Item = Questar.Items.Item;

namespace Questar.Gui
{
    public class PickupMessageWidget : EventBox
    {
        private bool really_show = false;
        private bool set_background = true;

        [Glade.Widget] private Container pickup_message;
        [Glade.Widget] private Label label;

        public PickupMessageWidget ()
        {
            Glade.XML glade = new Glade.XML ("questar.glade", "pickup_message");
            glade.Autoconnect (this);

            base.Add (pickup_message);

            SetupHandlers ();
        }

        protected override void OnStyleSet (Style previous)
        {
            if (set_background == true) {
                set_background = false;
                base.ModifyBg (StateType.Normal, base.Style.Background (StateType.Selected));
                label.ModifyFg (StateType.Normal, base.Style.Foreground (StateType.Selected));
            }
            set_background = true;
        }

        private void SetupHandlers ()
        {
            TurnLoop turn_loop = Game.Instance.TurnLoop;
            Actor hero = Game.Instance.Hero;

            turn_loop.NewRound += delegate {
                Item item = hero.Location.Item;
                if (item != null) {
                    label.Markup = String.Format (
                        "There is a <b>{0}</b> here. Pick it up?",
                        item);

                    really_show = true;
                    base.Show ();
                }
                else {
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

