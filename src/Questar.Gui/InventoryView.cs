/*******************************************************************************
 *  InventoryView.cs: A subclass of TreeView that displays an Inventory.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using System;

using Questar.Actors;
using Questar.Actors.Actions;
using Questar.Core;
using Questar.Items;

using Item = Questar.Items.Item;

namespace Questar.Gui
{
    public class InventoryView : TreeView
    {
        private Inventory inventory;
        private ListStore store;

        public InventoryView (Inventory inventory)
        {
            this.inventory = inventory;

            SetupHandlers ();
            BuildView ();
            BuildStore ();

            base.ShowAll ();
        }

        private void SetupHandlers ()
        {
            inventory.Added += OnAddedOrRemoved;
            inventory.Removed += OnAddedOrRemoved;
        }

        private void BuildView ()
        {
            base.HeadersVisible = false;

            // Name Column
            TreeViewColumn name = new TreeViewColumn ();
            name.Title = "Name";
            name.Expand = true;

            CellRendererText cell = new CellRendererText ();
            name.PackStart (cell, true);
            name.SetCellDataFunc (cell, new CellLayoutDataFunc (RenderNameCell));

            base.AppendColumn (name);
        }

        private void BuildStore ()
        {
            if (store == null)
                store = new ListStore (typeof (Item));

            store.Clear ();

            foreach (Item item in inventory)
                store.AppendValues (item);

            base.Model = store;
        }

        private void OnAddedOrRemoved (object sender, ItemEventArgs args)
        {
            BuildStore ();
        }

        private void RenderNameCell (CellLayout layout, CellRenderer cell,
            TreeModel model, TreeIter iter)
        {
            Item item = (Item) model.GetValue (iter, 0);
            ((CellRendererText) cell).Text = item.Name;
        }

        protected override void OnRowActivated (TreePath path, TreeViewColumn column)
        {
            // HACK: How could this be better?
            Hero hero = (Hero) Game.Instance.Hero;

            if (inventory != hero.Inventory)
                return;

            TreeIter iter = TreeIter.Zero;
            store.GetIter (out iter, path);

            Item item = (Item) store.GetValue (iter, 0);

            //hero.AddAction (new DropAction (hero, item));
            hero.AddAction (new DrinkAction (hero, (IDrinkable) item));
        }
    }
}

