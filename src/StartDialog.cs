/*******************************************************************************
 *  StartDialog.cs: Shown when Questar starts and allows the user to
 *  open an already saved game or start a new one.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using System;

namespace Questar.Gui
{
    public class StartDialog : GladeDialog
    {
        public StartDialog () : base ("start_dialog")
        {
            base.Dialog.ShowAll ();
        }
    }
}
