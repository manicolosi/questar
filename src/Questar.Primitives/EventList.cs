/*******************************************************************************
 *  EventList.cs: A list that raises events when an item is added or
 *  removed for it.
 *
 *  Copyright (C) 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

using Questar.Extensions;
using Questar.Helpers;

namespace Questar.Primitives
{
    public class EventListEventArgs<T> : EventArgs
    {
        public T Item;
    }

    public class EventList<T> : ICollection<T>
    {
        private List<T> inner_list;

        public event EventHandler<EventListEventArgs<T>> Added;
        public event EventHandler<EventListEventArgs<T>> Removed;

        public EventList ()
        {
            inner_list = new List<T> ();
        }

        public int Count
        {
            get { return inner_list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add (T item)
        {
            Added.Raise (this, args => { args.Item = item; });

            inner_list.Add (item);
        }

        public bool Remove (T item)
        {
            if (Contains (item)) {
                Removed.Raise (this, args => { args.Item = item; });
            }

            return inner_list.Remove (item);
        }

        public void Clear ()
        {
            inner_list.ForEach (item => {
                Removed.Raise (this, args => { args.Item = item; });
            });

            inner_list.Clear ();
        }

        public bool Contains (T item)
        {
            return inner_list.Contains (item);
        }

        public void CopyTo (T[] array, int index)
        {
            inner_list.CopyTo (array, index);
        }

        public IEnumerator<T> GetEnumerator ()
        {
            return inner_list.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }
    }
}

