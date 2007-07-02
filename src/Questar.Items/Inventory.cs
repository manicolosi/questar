/*******************************************************************************
 *  Inventory.cs: An Inventory manages a collection of items.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

using Questar.Base;

namespace Questar.Items
{
    public class Inventory
    {
        private List<Item> items;

        public Inventory ()
        {
            items = new List<Item> ();
        }

        public void Add (Item item)
        {
            items.Add (item);
        }

        public bool Contains (Item item)
        {
            return items.Contains (item);
        }
    }
}

