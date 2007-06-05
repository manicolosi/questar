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
        Window window;

        public GladeDialog (string name)
        {
            glade = new Glade.XML (glade_resource, name);
            glade.Autoconnect (this);

            window = glade[name] as Window;
            dialog = window as Dialog;

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

        public Window Window
        {
            get { return window; }
        }

        protected virtual void OnResponse (ResponseType response)
        {
            if (response == ResponseType.Close ||
                response == ResponseType.DeleteEvent)
                dialog.Destroy ();
        }
    }
}

