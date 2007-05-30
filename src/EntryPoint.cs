using Gtk;
using System;

using Questar.Actors;
using Questar.Base;
using Questar.Gui;
using Questar.Maps;

namespace Questar
{
    public class EntryPoint
    {
        public static void Main ()
        {
            ProcessName = "questar";

            Application.Init ();

            SetupWorld ();
            new MainWindow ();

            Application.Run ();
        }

        public static void Quit ()
        {
            Application.Quit ();
        }

        private static void SetupWorld ()
        {
            World world = World.Instance;
            world.Map = new Map ();
            world.AddActor (new Hero (world.Map));
            world.AddActor (new Monster ("troll", world.Map));
            world.AddActor (new Monster ("imp", world.Map));
            world.Start ();
        }

        private static string ProcessName
        {
            set {
                NativeMethods.prctl (15 /* PR_SET_NAME */, value, 0, 0, 0);
            }
        }
    }
}

