/*******************************************************************************
 *  HitPointsChart.cs: Displays a HitPoints object graphically.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Cairo;
using Gdk;
using Gtk;
using System;

using Questar.Actors;
using Questar.Gui;
using Questar.Helpers;

using Color = Cairo.Color;
using Point = Cairo.Point;
using Rectangle = Gdk.Rectangle;

namespace Questar.Gui.Widgets
{
    public class HitPointsChart : DrawingArea
    {
        private HitPoints hit_points;
        private Animation<int> animation;
        private int angle;

        public HitPointsChart (HitPoints hit_points)
        {
            this.hit_points = hit_points;
            angle = HPToDegrees (hit_points);
        }

        public HitPoints HitPoints
        {
            get { return hit_points; }
        }

        private int HPToDegrees (HitPoints hp)
        {
            return (int) (hp.AsDouble * 360);
        }

        private void CreatePath (Context context)
        {
            Rectangle alloc = base.Allocation;
            Point center = new Point (alloc.Width / 2, alloc.Height / 2);
            double radius = (Math.Min (alloc.Width, alloc.Height) - 10) / 2;
            double angle_in_radians = angle * (Math.PI / 180);

            context.Arc (center.X, center.Y, radius,
                -angle_in_radians + (-0.5 * Math.PI), -0.5 * Math.PI);
            context.LineTo (center.X, center.Y);
            context.ClosePath ();
        }

        private void StrokeAndFill (Context context)
        {
            context.LineWidth = 4.0;
            context.Color = TangoColors.Chameleon3;
            context.StrokePreserve ();

            context.Color = TangoColors.Chameleon1;
            context.Fill ();
        }

        protected override bool OnExposeEvent (EventExpose args)
        {
            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                CairoHelper.Rectangle (context, args.Area);
                context.Clip ();

                CreatePath (context);
                StrokeAndFill (context);
            }

            return true;
        }

        protected override void OnRealized ()
        {
            base.OnRealized ();
            hit_points.Changed += HitPointsChanged;
        }

        protected override void OnUnrealized ()
        {
            base.OnUnrealized ();
            hit_points.Changed -= HitPointsChanged;
        }

        private void RemoveAnimation ()
        {
            if (animation.IsRunning)
                animation.Stop ();

            animation.NewFrame -= NewFrame;
            animation.Completed -= delegate { RemoveAnimation (); };
            animation = null;
        }

        private void HitPointsChanged (object sender, HitPointsEventArgs args)
        {
            if (animation != null)
                RemoveAnimation ();

            int start = angle;
            int end = HPToDegrees (hit_points);
            Console.WriteLine (start);
            Console.WriteLine (end + "\n");

            animation = new Animation<int> (TimeSpan.FromSeconds (1));
            animation.Transform = delegate (Animation<int> anim, int frame) {
                double difference = Math.Abs (start - end);
                double value_per_frame = difference / animation.TotalFrames;
                int new_val;

                if (start < end)
                    new_val = (int) (start + (value_per_frame * frame + 1));
                else
                    new_val = (int) (start - (value_per_frame * frame + 1));

                return new_val;
            };
            animation.NewFrame += NewFrame;
            animation.Completed += delegate { RemoveAnimation (); };
            animation.Start ();
        }

        private void NewFrame (object sender, NewFrameEventArgs<int> args)
        {
            angle = args.Data;
            base.QueueDraw ();
        }
    }
}

