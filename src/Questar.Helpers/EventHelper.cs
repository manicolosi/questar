/*******************************************************************************
 *  EventHelper.cs: Static utility methods that make dealing with events
 *  easier.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Helpers
{
    public static class EventHelper
    {
        public static void Raise<T> (this EventHandler<T> event_handler,
            object sender, Action<T> setup) where T: EventArgs, new ()
        {
            if (event_handler == null)
                return;

            T args = new T ();

            if (setup != null)
                setup (args);

            try {
                event_handler (sender, args);
            }
            catch (Exception ex) {
                throw new RaisedEventException (event_handler, sender, args, ex);
            }
        }

        public static void Raise (this EventHandler event_handler, object sender)
        {
            if (event_handler == null)
                return;

            try {
                event_handler (sender, EventArgs.Empty);
            }
            catch (Exception ex) {
                throw new RaisedEventException (event_handler, sender, null, ex);
            }
        }
    }
}

