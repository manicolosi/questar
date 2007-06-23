/*******************************************************************************
 *  PreferenceDialog.cs: Provides a dialog that allows the user to
 *  customize Questar.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gnome;
using Gtk;
using System;
using System.Collections.Generic;

using Questar.Configuration;

namespace Questar.Gui
{
    public class PreferenceDialog : GladeDialog
    {
        [Widget] private ComboBox tile_set_combobox;
        [Widget] private ComboBox zoom_combobox;

        public PreferenceDialog () : base ("preference_dialog")
        {
            SetupHandlers ();
            PopulateComboBox (zoom_combobox, UISchema.Zoom.Value.ToString (),
                Enum.GetNames (typeof (ZoomSetting)));
            PopulateComboBox (tile_set_combobox, UISchema.TileSet.Value,
                TileSet.AvailableTileSets);

            base.Dialog.ShowAll ();
        }

        private void SetupHandlers ()
        {
            tile_set_combobox.Changed += delegate {
                TreeIter iter;
                tile_set_combobox.GetActiveIter (out iter);
                UISchema.TileSet.Value =
                    (string) tile_set_combobox.Model.GetValue (iter, 0);
            };

            zoom_combobox.Changed += delegate {
                TreeIter iter;
                zoom_combobox.GetActiveIter (out iter);
                string zoom = zoom_combobox.Model.GetValue (iter, 0) as string;
                UISchema.Zoom.Value = (ZoomSetting) Enum.Parse (
                    typeof (ZoomSetting), zoom);
            };

            UISchema.Zoom.Changed += delegate { zoom_combobox.Model.Foreach (
                delegate (TreeModel model, TreePath path, TreeIter iter) {
                    string model_value = model.GetValue (iter, 0) as string;
                    if (UISchema.Zoom.Value.ToString () != model_value) {
                        return false;
                    }
                    zoom_combobox.SetActiveIter (iter);
                    return true;
                });
            };
        }

        private void PopulateComboBox (ComboBox combobox, string active,
            IEnumerable<string> enumerable)
        {
            ListStore model = new ListStore (typeof (string));
            combobox.Model = model;

            foreach (string s in enumerable) {
                TreeIter iter = model.AppendValues (s);
                if (s == active)
                    combobox.SetActiveIter (iter);
            }

            CellRendererText cr = new CellRendererText ();
            combobox.PackStart (cr, false);
            combobox.AddAttribute (cr, "text", 0);
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

