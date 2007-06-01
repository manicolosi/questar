//
// UIActions.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Gtk;
using System;
using System.Collections.Generic;

using Questar.Configuration;

namespace Questar.Gui
{
    public class PreferenceDialog
    {
        Dialog dialog;
        Glade.XML glade;

        [Glade.Widget] private ComboBox tile_set_combobox;

        public PreferenceDialog ()
        {
            SetupGlade ();
            SetupHandlers ();
            PopulateTileSetComboBox ();
        }

        public ResponseType Run ()
        {
            return (ResponseType) dialog.Run ();
        }

        private void SetupGlade ()
        {
            string name = "preference_dialog";

            glade = new Glade.XML ("questar.glade", name);
            glade.Autoconnect (this);

            dialog = glade[name] as Dialog;
        }

        private void SetupHandlers ()
        {
            tile_set_combobox.Changed += delegate {
                TreeIter iter;
                tile_set_combobox.GetActiveIter (out iter);
                Console.WriteLine (tile_set_combobox.Model.GetValue (iter, 0));
            };
        }

        private void PopulateTileSetComboBox ()
        {
            ListStore model = new ListStore (typeof (string));
            tile_set_combobox.Model = model;

            TreeIter iter;
            foreach (string tile_set in TileSet.AvailableTileSets) {
                iter = model.AppendValues (tile_set);

                if (tile_set == UISchema.TileSet.Get ())
                    tile_set_combobox.SetActiveIter (iter);

            }

            CellRendererText text_cr = new CellRendererText ();
            tile_set_combobox.PackStart (text_cr, false);
            tile_set_combobox.AddAttribute (text_cr, "text", 0);
        }
    }
}

