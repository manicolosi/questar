/*******************************************************************************
 *  GladeDialog.cs: Abstract base class for Questar's dialogs.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gtk;
using System;

namespace Questar.Gui
{
    public abstract class GladeDialog : GladeWindow
    {
        Dialog dialog;

        public GladeDialog (string name) : base (name)
        {
            dialog = base.Window as Dialog;

            if (dialog != null) {
                dialog.Response += delegate (object o, ResponseArgs args) {
                    OnResponse (args.ResponseId);
                };
            }
        }

        public Dialog Dialog
        {
            get { return dialog; }
        }

        protected virtual void OnResponse (ResponseType response)
        {
            if (response == ResponseType.Close ||
                response == ResponseType.DeleteEvent)
                dialog.Destroy ();
        }
    }
}

