/*******************************************************************************
 *  ColorHelper.cs: Static utility methods for Cairo.Color objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Cairo;
using Gdk;
using System;

using Color = Cairo.Color;

namespace Questar.Helpers
{
    public class ColorHelper
    {
        public static Color BlendColors (double blend, Color a, Color b)
        {
            if (blend < 0.0 || blend > 1.0)
                throw new ArgumentException (
                    "Blend amount must be greater than 0.0 and less than 1.0.");

            double blend_ratio = 1.0 - blend;

            double blr = (a.R + b.R) * blend_ratio;
            double blg = (a.G + b.G) * blend_ratio;
            double blb = (a.B + b.B) * blend_ratio;

            return new Color (blr, blg, blb);
        }

        public static Color BlendColors (double blend, Gdk.Color a, Gdk.Color b)
        {
            Color converted_a = FromGdkColor (a);
            Color converted_b = FromGdkColor (b);

            return BlendColors (blend, converted_a, converted_b);
        }

        public static Color FromGdkColor (Gdk.Color gdk_color, double alpha)
        {
            return new Color (
                gdk_color.Red / 65355.0,
                gdk_color.Green / 65355.0,
                gdk_color.Blue / 65355.0,
                alpha);
        }

        public static Color FromGdkColor (Gdk.Color gdk_color)
        {
            return FromGdkColor (gdk_color, 1.0);
        }

        public static Color FromString (string color_str)
        {
            Gdk.Color gdk_color = new Gdk.Color (0, 0, 0);

            bool success = Gdk.Color.Parse (color_str, ref gdk_color);
            if (!success)
                throw new ApplicationException ("color_str");

            return FromGdkColor (gdk_color);
        }
    }
}

