/*******************************************************************************
 *  HitPointsWidget.cs: Displays a HitPoints object graphically.
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
using Questar.Gui.Animation;
using Questar.Helpers;

using Color = Cairo.Color;
using Point = Cairo.Point;
using Rectangle = Gdk.Rectangle;

namespace Questar.Gui.Widgets
{
    public class HitPointsWidget : DrawingArea
    {
        private HitPoints hit_points;
        private DoubleAnimation animation;
        private double angle;

        public HitPointsWidget (HitPoints hit_points)
        {
            HitPoints = hit_points;
            animation = null;
            angle = HPToRadians (hit_points);
        }

        public HitPoints HitPoints
        {
            get { return hit_points; }
            set {
                if (hit_points != null) {
                    hit_points.Changed -= HitPointsChanged;
                }

                hit_points = value;

                if (hit_points != null) {
                    hit_points.Changed += HitPointsChanged;
                }
            }
        }

        private double HPToRadians (HitPoints hp)
        {
            return (hp.AsDouble * 360) * (Math.PI / 180);
        }

        protected override bool OnExposeEvent (EventExpose args)
        {

            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                // Clip context to args.Area...
                // CreateChartPath (context);
                // StrokeAndFill (context);
                Rectangle alloc = base.Allocation;
                Point center = new Point (alloc.Width / 2, alloc.Height / 2);
                double radius = (Math.Min (alloc.Width, alloc.Height) - 10) / 2;

                context.Arc (center.X, center.Y, radius,
                    -angle + (-0.5 * Math.PI), -0.5 * Math.PI);
                context.LineTo (center.X, center.Y);
                context.ClosePath ();

                context.LineWidth = 4.0;
                context.Color = TangoColors.Chameleon3;
                context.StrokePreserve ();

                context.Color = TangoColors.Chameleon1;
                context.Fill ();
            }

            return true;
        }

        private void HitPointsChanged (object sender, HitPointsEventArgs args)
        {
            if (animation != null)
                animation.Stop ();

            animation = new DoubleAnimation (TimeSpan.FromSeconds (1.0));
            animation.StartValue = angle;
            animation.EndValue = HPToRadians (hit_points);
            animation.NewFrame = AnimationNewFrame;
            animation.Completed = AnimationCompleted;
            animation.Start ();
        }

        private void AnimationNewFrame (object sender, NewFrameEventArgs args)
        {
            angle = args.Value;
            base.QueueDraw ();
        }

        private void AnimationCompleted (object sender, EventArgs args)
        {
            animation = null;
        }
    }
}

