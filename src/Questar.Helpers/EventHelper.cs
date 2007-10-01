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
        public delegate void SetupEventArgsHandler<T> (T args);

        public static void Raise<T> (object sender,
            EventHandler<T> event_handler,
            SetupEventArgsHandler<T> setup_handler) where T: EventArgs, new ()
        {
            if (event_handler != null) {
                T args = new T ();

                if (setup_handler != null)
                    setup_handler (args);

                event_handler (sender, args);
            }
        }

        public static void Raise (object sender, EventHandler event_handler)
        {
            if (event_handler != null)
                event_handler (sender, EventArgs.Empty);
        }
    }
}

