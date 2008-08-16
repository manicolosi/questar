/*******************************************************************************
 *  PickupMessageWidget.cs: A widget that allows the player to pickup an
 *  Item the Hero is standing on.
 *
 *  Copyright (C) 2007, 2008
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
        private Item item = null;
        private bool set_background = true;

        [Glade.Widget] private Container pickup_message;
        [Glade.Widget] private Label label;
        [Glade.Widget] private Button no_button;
        [Glade.Widget] private Button yes_button;

        public PickupMessageWidget ()
        {
            Build ();
            SetupHandlers ();
        }

        protected override void OnShown ()
        {
            label.Markup = String.Format (
                "There is a <b>{0}</b> here. Pick it up?", item);
            base.OnShown ();
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

        private void Build ()
        {
            Glade.XML glade = new Glade.XML ("questar.glade", "pickup_message");
            glade.Autoconnect (this);

            base.Add (pickup_message);

            // FIXME: Setting this in the glade file doesn't seem to
            // work.
            no_button.CanFocus = false;
            yes_button.CanFocus = false;
        }

        private void SetupHandlers ()
        {
            no_button.Clicked += (object sender, EventArgs args) => {
                base.Hide ();
            };

            yes_button.Clicked += (object sender, EventArgs args) => {
                UIActions.Instance["PickUp"].Activate ();
            };

            TurnLoop turn_loop = Game.Instance.TurnLoop;
            Actor hero = Game.Instance.Hero;

            turn_loop.NewRound += (object sender, NewRoundEventArgs args) => {
                item = hero.Location.Item;
                base.Visible = item != null;
            };
        }
    }
}

