/*******************************************************************************
 *  DoubleAnimation.cs: Allows animating on double values.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using GLib;
using System;

using Questar.Helpers;

namespace Questar.Gui.Animation
{
    public class NewFrameEventArgs : EventArgs
    {
        public int Frame;
        public double Value;
    }

    public class DoubleAnimation
    {
        private const int fps = 30;
        private const uint frame_rate = 1000 / 30;

        private bool is_running;
        private int total_frames;
        private int current_frame;

        private TimeSpan duration;
        private double start;
        private double end;

        public EventHandler<NewFrameEventArgs> NewFrame;

        public DoubleAnimation (TimeSpan duration)
        {
            this.duration = duration;
        }

        public double StartValue
        {
            get { return start; }
            set { start = value; }
        }

        public double EndValue
        {
            get { return end; }
            set { end = value; }
        }

        public void Start ()
        {
            is_running = true;
            current_frame = 0;
            total_frames = (int) (fps * duration.TotalSeconds);

            double difference = Math.Abs (start - end);
            double value_per_frame = difference / total_frames;
            bool is_increasing = start < end;

            Timeout.Add (frame_rate, delegate {
                if (!is_running)
                    return false;

                // TODO: Add Completed event
                if (current_frame >= total_frames)
                    return false;

                EventHelper.Raise (this, NewFrame,
                    delegate (NewFrameEventArgs args) {
                        args.Frame = current_frame;
                        if (is_increasing)
                            args.Value = start +
                                (value_per_frame * (current_frame + 1));
                        else
                            args.Value = start -
                                (value_per_frame * (current_frame + 1));
                    });

                current_frame++;

                return true;
            });
        }
    }
}

