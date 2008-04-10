/*******************************************************************************
 *  Inventory.cs: An Inventory manages a collection of items.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Helpers;
using Questar.Primitives;

namespace Questar.Items
{
    public class ItemEventArgs : EventArgs
    {
        public Item Item;
    }

    public class Inventory : ICollection<Item>
    {
        public event EventHandler<ItemEventArgs> Added;
        public event EventHandler<ItemEventArgs> Removed;

        private Actor owner;
        private List<Item> items = new List<Item> ();

        public Inventory (Actor owner)
        {
            this.owner = owner;
        }

        public Inventory () : this (null)
        {
        }

        public Actor Owner
        {
            get { return owner; }
        }

        public bool IsEmpty
        {
            get { return items.Count == 0; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add (Item item)
        {
            item.AssertNotNull ();
            items.AssertDoesNotContain (item);

            items.Add (item);

            RaiseItemEvent (Added, item);
        }

        public void Remove (Item item)
        {
            item.AssertNotNull ();
            items.AssertContains (item);


            items.Remove (item);
            RaiseItemEvent (Removed, item);
        }

        bool ICollection<Item>.Remove (Item item)
        {
            Remove (item);
            return true;
        }

        public bool Contains (Item item)
        {
            return items.Contains (item);
        }

        public void Clear ()
        {
            items.Each (item => Remove (item));
        }

        public void CopyTo (Item[] array, int index)
        {
            items.CopyTo (array, index);
        }

        public IEnumerator<Item> GetEnumerator ()
        {
            return items.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        private void RaiseItemEvent (EventHandler<ItemEventArgs> event_handler,
            Item item)
        {
            EventHelper.Raise (this, event_handler,
                delegate (ItemEventArgs args) {
                    args.Item = item;
                });
        }
    }
}

