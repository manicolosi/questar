using Cairo;
using Gdk;
using System;

using Color = Cairo.Color;

namespace Questar.Gui
{
    public class CairoUtilities
    {
        public static Color BlendColors (Color a, Color b, double blend)
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

        public static Color BlendColors (Gdk.Color a, Gdk.Color b, double blend)
        {
            Color converted_a = ConvertToCairoColor (a);
            Color converted_b = ConvertToCairoColor (b);

            return BlendColors (converted_a, converted_b, blend);
        }

        public static Color ConvertToCairoColor (Gdk.Color gdk_color)
        {
            return new Color (
                gdk_color.Red / 65355.0,
                gdk_color.Green / 65355.0,
                gdk_color.Blue / 65355.0);
        }
    }
}

