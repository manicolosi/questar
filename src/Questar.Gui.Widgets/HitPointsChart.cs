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
        private DoubleAnimation animation;
        private double angle;

        public HitPointsChart (HitPoints hit_points)
        {
            this.hit_points = hit_points;
            this.animation = null;
            this.angle = HPToRadians (hit_points);
        }

        public HitPoints HitPoints
        {
            get { return hit_points; }
        }

        private double HPToRadians (HitPoints hp)
        {
            return (hp.AsDouble * 360) * (Math.PI / 180);
        }

        private void CreatePath (Context context)
        {
            Rectangle alloc = base.Allocation;
            Point center = new Point (alloc.Width / 2, alloc.Height / 2);
            double radius = (Math.Min (alloc.Width, alloc.Height) - 10) / 2;

            context.Arc (center.X, center.Y, radius,
                -angle + (-0.5 * Math.PI), -0.5 * Math.PI);
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

            animation.NewFrame -= AnimationNewFrame;
            animation.Completed -= delegate { RemoveAnimation (); };
            animation = null;
        }

        private void HitPointsChanged (object sender, HitPointsEventArgs args)
        {
            if (animation != null)
                RemoveAnimation ();

            animation = new DoubleAnimation (angle, HPToRadians (hit_points));
            animation.Duration = TimeSpan.FromSeconds (animation.Difference);
            animation.NewFrame += AnimationNewFrame;
            animation.Completed += delegate { RemoveAnimation (); };
            animation.Start ();
        }

        private void AnimationNewFrame (object sender, NewFrameEventArgs args)
        {
            angle = args.Value;
            base.QueueDraw ();
        }
    }
}

