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
    public abstract class GladeDialog
    {
        private const string glade_resource = "questar.glade";

        Glade.XML glade;
        Dialog dialog;

        public GladeDialog (string name)
        {
            glade = new Glade.XML (glade_resource, name);
            glade.Autoconnect (this);

            dialog = glade[name] as Dialog;
            dialog.Response += delegate (object o, ResponseArgs args) {
                OnResponse (args.ResponseId);
            };
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

