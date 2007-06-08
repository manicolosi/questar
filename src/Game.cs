/*******************************************************************************
 *  Game.cs: FIXME
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using System;

using Questar.Gui;
using Questar.Maps;
using Questar.Actors;

namespace Questar.Base
{
    public class Game
    {
        public static Game Instance
        {
            get { return Singleton<Game>.Instance; }
        }

        private Window main_window;

        private World world;

        private Game ()
        {
            ProcessName = "questar";
            Window.DefaultIconName = "questar";
            Application.Init ();
        }

        public World World
        {
            get { return world; }
            set { world = value; }
        }

        public void Start ()
        {
            main_window = new MainWindow ();

            if (world != null)
                world.Start ();

            Application.Run ();
        }

        public void Quit ()
        {
            if (main_window != null)
                main_window.Destroy ();

            Application.Quit ();
        }


        private static string ProcessName
        {
            set { NativeMethods.prctl (15 /* PR_SET_NAME */, value, 0, 0, 0); }
        }
    }
}

