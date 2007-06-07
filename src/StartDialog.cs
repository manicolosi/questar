/*******************************************************************************
 *  StartDialog.cs: Shown when Questar starts and allows the user to
 *  open an already saved game or start a new one.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gtk;
using System;

using Questar.Base;

namespace Questar.Gui
{
    public class StartDialog : GladeWindow
    {
        private class ConfirmDeleteMessageDialog : MessageDialog
        {
            private const string Format = "<big><b>Are you sure " +
                "you want to permanently delete \"{0}\"?</b></big>";
            private const string Secondary = "If you delete a saved " +
                "game, it is permanently lost.";

            public ConfirmDeleteMessageDialog (Window parent) :
                base (parent, DialogFlags.DestroyWithParent,
                    MessageType.Warning, ButtonsType.None, "")
            {
                string game_text = "Mark - Level 34";

                base.UseMarkup = true;
                base.Text = String.Format (Format, game_text);
                base.SecondaryText = Secondary;

                base.AddActionWidget (new Button (Stock.Cancel),
                    ResponseType.Cancel);
                base.AddActionWidget (new Button (Stock.Delete),
                    ResponseType.Apply);
            }
        }

        [Widget] private Button quit_button;
        [Widget] private Button new_button;
        [Widget] private Button delete_button;

        public StartDialog () : base ("start_dialog")
        {
            SetupHandlers ();
            base.Window.ShowAll ();
        }

        private void SetupHandlers ()
        {
            base.Window.DeleteEvent += delegate { Game.Instance.Quit (); };
            quit_button.Clicked += delegate { Game.Instance.Quit (); };
            new_button.Clicked += delegate { base.Window.Destroy (); };

            delete_button.Clicked += delegate {
                MessageDialog warning =
                    new ConfirmDeleteMessageDialog (base.Window);
                ResponseType response = (ResponseType) warning.Run ();
                if (response == ResponseType.Apply)
                    Console.WriteLine ("Deleted");

                warning.Destroy ();
            };
        }
    }
}

