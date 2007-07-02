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

namespace Questar.Items
{
    public class Inventory : IEnumerable<Item>
    {
        private Actor owner;
        private List<Item> items;

        public Inventory (Actor owner)
        {
            this.owner = owner;

            items = new List<Item> ();
        }

        public Inventory () : this (null)
        {
        }

        public Actor Owner
        {
            get { return owner; }
        }

        public void Add (Item item)
        {
            if (item == null)
                throw new ArgumentNullException ("Item must not be null.");

            if (Contains (item))
                throw new ArgumentException ("An Item can't be added twice.");

            if (item.IsOwned && owner != null && item.Owner != owner)
                throw new ArgumentException (
                    "Item can't be added because it's owned by somebody else");

            if (owner != null)
                item.Owner = owner;

            items.Add (item);
        }

        public void Remove (Item item)
        {
            if (item == null)
                throw new ArgumentNullException ("Item must not be null.");

            if (!Contains (item))
                throw new ArgumentException ("Item is not in this Inventory.");

            item.Owner = null;

            items.Remove (item);
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
    }
}

