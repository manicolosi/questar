//
// UISchema.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Questar.Gui;

namespace Questar.Configuration
{
    public static class UISchema
    {
        public static readonly SchemaEntry<int> Width =
            new SchemaEntry<int> ("user_interface", "width", 800,
                "Window Width",
                "Width of the main interface window."
            );

        public static readonly SchemaEntry<int> Height =
            new SchemaEntry<int> ("user_interface", "height", 600,
                "Window Height",
                "Height of the main interface window."
            );

        public static readonly SchemaEntry<int> XPos =
            new SchemaEntry<int> ("user_interface", "x_pos", 0,
                "Window Position X",
                "Pixel position of main interface window on the X axis."
            );

        public static readonly SchemaEntry<int> YPos =
            new SchemaEntry<int> ("user_interface", "y_pos", 0,
                "Window Position Y",
                "Pixel position of the main interface window on the Y axis."
            );

        public static readonly SchemaEntry<bool> Maximized =
            new SchemaEntry<bool> ("user_interface", "maximized", false,
                "Window Maximized",
                "True if the main interface window is to be maximized, " +
                "otherwise false."
            );

        public static readonly SchemaEntry<bool> Fullscreen =
            new SchemaEntry<bool> ("user_interface", "fullscreen", false,
                "Window Fullscreen",
                "True if the main interface window is to be fullscreen, " +
                "otherwise false."
            );

        public static readonly SchemaEntry<ZoomSetting> Zoom =
            new SchemaEntry<ZoomSetting> ("user_interface", "zoom",
                ZoomSetting.Normal, "Tile Set Zoom",
                "Zoom of the tile set. Must be one of Smallest, Small, " +
                "Normal, Large, or Largest."
            );

        public static readonly SchemaEntry<string> TileSet =
            new SchemaEntry<string> ("user_interface", "tile_set", "Default",
                "Tile Set Name",
                "Name of the tile set to use."
            );

        public static readonly SchemaEntry<bool> DrawGridLines =
            new SchemaEntry<bool> ("user_interface", "draw_grid_lines", false,
                "Draw Grid Lines",
                "True if grid lines should be drawn over the map, " +
                "otherwise false."
            );

        public static readonly SchemaEntry<bool> ShowMessages =
            new SchemaEntry<bool> ("user_interface", "show_messages", true,
                "Show Messages",
                "True if the message area should be shown, otherwise false."
            );
    }
}
