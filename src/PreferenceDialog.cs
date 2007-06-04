/*******************************************************************************
 *  PreferenceDialog.cs: Provides a dialog that allows the user to
 *  customize Questar.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gnome;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Configuration;

namespace Questar.Gui
{
    public class PreferenceDialog : GladeDialog
    {
        [Glade.Widget] private ComboBox tile_set_combobox;

        public PreferenceDialog () : base ("preference_dialog")
        {
            SetupHandlers ();
            PopulateTileSetComboBox ();
        }

        private void SetupHandlers ()
        {
            tile_set_combobox.Changed += delegate {
                TreeIter iter;
                tile_set_combobox.GetActiveIter (out iter);
                UISchema.TileSet.Value =
                    (string) tile_set_combobox.Model.GetValue (iter, 0);
            };
        }

        private void PopulateTileSetComboBox ()
        {
            ListStore model = new ListStore (typeof (string));
            tile_set_combobox.Model = model;

            TreeIter iter;
            foreach (string tile_set in TileSet.AvailableTileSets) {
                iter = model.AppendValues (tile_set);

                if (tile_set == UISchema.TileSet.Value)
                    tile_set_combobox.SetActiveIter (iter);

            }

            CellRendererText text_cr = new CellRendererText ();
            tile_set_combobox.PackStart (text_cr, false);
            tile_set_combobox.AddAttribute (text_cr, "text", 0);
        }

        protected override void OnResponse (ResponseType response)
        {
            base.OnResponse (response);

            if (response == ResponseType.Help)
                Help.DisplayUriOnScreen ("ghelp:questar/preferences",
                    base.Dialog.Screen);
        }
    }
}

