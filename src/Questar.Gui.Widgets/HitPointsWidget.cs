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

using Rectangle = Gdk.Rectangle;
using Color = Cairo.Color;

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

        private double HitPointsToRadians ()
        {
            return (hit_points.AsDouble * 360) * (Math.PI / 180);
        }

        protected override bool OnExposeEvent (EventExpose args)
        {

            using (Context context = CairoHelper.Create (base.GdkWindow)) {
                Rectangle allocation = base.Allocation;
                double xc = allocation.Width / 2;
                double yc = allocation.Height / 2;
                double radius = (allocation.Width / 2) - 4;

                context.Arc (xc, yc, radius, 0, HitPointsToRadians ());
                if (!hit_points.IsFull) {
                    context.LineTo (xc, yc);
                    context.LineTo (xc + radius, yc);
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

