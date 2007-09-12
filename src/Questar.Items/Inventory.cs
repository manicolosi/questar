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

    public class Inventory : IEnumerable<Item>
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

        public void Add (Item item)
        {
            if (item == null)
                throw new ArgumentNullException ("Item must not be null.");

            if (Contains (item))
                throw new ArgumentException ("An Item can't be added twice.");

            item.Location = new ActorLocation (owner);
            items.Add (item);

            FireEvent (Added, item);
        }

        public void Remove (Item item)
        {
            if (item == null)
                throw new ArgumentNullException ("Item must not be null.");

            if (!Contains (item))
                throw new ArgumentException ("Item is not in this Inventory.");

            items.Remove (item);

            FireEvent (Removed, item);
        }

        public bool Contains (Item item)
        {
            return items.Contains (item);
        }

        public IEnumerator<Item> GetEnumerator ()
        {
            return items.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        private void FireEvent (EventHandler<ItemEventArgs> event_handler, Item item)
        {
            EventHelper.Raise<ItemEventArgs> (this, event_handler,
                delegate (ItemEventArgs args) {
                    args.Item = item;
                });
        }
    }
}

