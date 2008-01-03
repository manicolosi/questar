/*******************************************************************************
 *  CharacterDialog.cs: Shows various information about the Hero.
 *
 *  Copyright (C) 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gtk;
using System;

using Questar.Core;

namespace Questar.Gui
{
    public class CharacterDialog : GladeDialog
    {
        [Widget] private Container inventory_container;

        public CharacterDialog () : base ("character_dialog")
        {
            inventory_container.Add (
                new InventoryView (Game.Instance.Hero.Inventory));

            base.Dialog.ShowAll ();
        }
    }
}
