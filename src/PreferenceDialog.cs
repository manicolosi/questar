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
            PopulateTileSetComboBox ();
            PopulateZoomComboBox ();
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
                zoom = zoom.Remove (zoom.Length - 1);
                UISchema.Zoom.Value = (ZoomSetting) Int32.Parse (zoom);
            };
        }

        private void PopulateTileSetComboBox ()
        {
            ListStore model = new ListStore (typeof (string));
            tile_set_combobox.Model = model;

            foreach (string tile_set in TileSet.AvailableTileSets) {
                TreeIter iter = model.AppendValues (tile_set);

                if (tile_set == UISchema.TileSet.Value)
                    tile_set_combobox.SetActiveIter (iter);

            }

            CellRendererText text_cr = new CellRendererText ();
            tile_set_combobox.PackStart (text_cr, false);
            tile_set_combobox.AddAttribute (text_cr, "text", 0);
        }

        private void PopulateZoomComboBox ()
        {
            ListStore model = new ListStore (typeof (string));
            zoom_combobox.Model = model;

            Type type = typeof (ZoomSetting);
            foreach (ZoomSetting zoom_level in Enum.GetValues (type)) {
                string zoom_percent = String.Format ("{0}%", (int) zoom_level);
                TreeIter iter = model.AppendValues (zoom_percent);

                if (zoom_level == UISchema.Zoom.Value)
                    zoom_combobox.SetActiveIter (iter);
            }

            CellRendererText text_cr = new CellRendererText ();
            zoom_combobox.PackStart (text_cr, false);
            zoom_combobox.AddAttribute (text_cr, "text", 0);
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

