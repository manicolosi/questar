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

using Color = Cairo.Color;
using Point = Cairo.Point;
using Rectangle = Gdk.Rectangle;

namespace Questar.Gui.Widgets
{
    public class HitPointsWidget : DrawingArea
    {
        private HitPoints hit_points;

        public HitPointsWidget (HitPoints hit_points)
        {
            HitPoints = hit_points;
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

        private double HPToRadians ()
        {
            return (hit_points.AsDouble * 360) * (Math.PI / 180);
        }

        protected override bool OnExposeEvent (EventExpose args)
        {

            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                Rectangle alloc = base.Allocation;
                Point center = new Point (alloc.Width / 2, alloc.Height / 2);
                double radius = (alloc.Width - 2) / 2;

                context.Arc (center.X, center.Y, radius, 0, HPToRadians ());

                if (!hit_points.IsFull) {
                    context.LineTo (center.X, center.Y);
                    context.LineTo (center.X + radius, center.Y);
                }

                context.Color = new Color (0, 0, 0);
                context.StrokePreserve ();

                context.Color = new Color (0.80, 0.80, 0.80);
                context.Fill ();
            }

            return true;
        }

        private void HitPointsChanged (object sender, HitPointsEventArgs args)
        {
            base.QueueDraw ();
        }
    }
}

