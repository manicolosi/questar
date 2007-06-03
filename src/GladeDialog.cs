//
// GladeDialog.cs: Base class for dialogs.
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

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
        }

        public Dialog Dialog
        {
            get { return dialog; }
        }

        public ResponseType Run ()
        {
            ResponseType response;

            do {
                response = (ResponseType) dialog.Run ();

                // FIXME: Handler help response.
            } while (response != ResponseType.Close &&
                response != ResponseType.DeleteEvent);

            return response;
        }

        public void Destroy ()
        {
            dialog.Destroy ();
        }
    }
}

