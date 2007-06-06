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
    public class StartDialog : GladeDialog
    {
        [Widget] private Button quit_button;

        public StartDialog () : base ("start_dialog")
        {
            SetupHandlers ();
            base.Window.ShowAll ();
        }

        private void SetupHandlers ()
        {
            base.Window.DeleteEvent += delegate {
                quit_button.Click ();
            };

            quit_button.Clicked += delegate {
                Game.Instance.Quit ();
            };
        }
    }
}
