using Gtk;
using System;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

using Questar.Actors;
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
            //world.AddActor (new Monster ("troll", world.Map));
            world.Start ();
        }

        [DllImport ("libc")]
        private static extern int prctl (int option, string name,
            ulong arg3, ulong arg4, ulong arg5);

        private static string ProcessName
        {
            set {
                prctl (15 /* PR_SET_NAME */, value, 0, 0, 0);
            }
        }
    }
}

