//
// Events.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;

namespace Questar.Base
{
    public static class Events
    {
        public delegate void SetupEventArgsHandler<T> (T args);

        public static void FireEvent<T> (object sender,
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

        public static void FireEvent (object sender,
            EventHandler<EventArgs> event_handler)
        {
            FireEvent<EventArgs> (sender, event_handler, null);
        }
    }
}
