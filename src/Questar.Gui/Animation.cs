/*******************************************************************************
 *  Animation.cs: Allows other objects to change over time.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using GLib;
using System;

using Questar.Helpers;

namespace Questar.Gui
{
    public class NewFrameEventArgs<T> : EventArgs
    {
        public int Frame;
        public T Data;
    }

    public class Animation<T>
    {
        private const int fps = 30;

        private bool is_running;
        private T data;
        private TimeSpan duration;

        public EventHandler<NewFrameEventArgs<T>> NewFrame;
        public EventHandler Completed;

        public Animation (TimeSpan duration)
        {
            Duration = duration;
        }

        public Animation ()
        {
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set {
                if (is_running)
                    throw new ApplicationException (
                        "Can't change duration while running.");

                duration = value;
            }
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public bool IsRunning
        {
            get { return is_running; }
        }

        public void Stop ()
        {
            is_running = false;
        }

        public void Start ()
        {
            is_running = true;

            int current_frame = 0;
            int total_frames = (int) (fps * duration.TotalSeconds);

            uint frame_rate = 1000 / fps;

            Timeout.Add (frame_rate, delegate {
                if (!is_running)
                    return false;

                if (current_frame >= total_frames) {
                    EventHelper.Raise (this, Completed);
                    return false;
                }

                EventHelper.Raise (this, NewFrame,
                    delegate (NewFrameEventArgs<T> args) {
                        args.Frame = current_frame;
                        args.Data = data;
                    });

                current_frame++;

                return true;
            });
        }
    }
}

