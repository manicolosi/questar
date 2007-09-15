/*******************************************************************************
 *  Game.cs: Singleton object responsible for keeping track of the
 *  game's internal state and behavior.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using System;

using Questar.Base;
using Questar.Gui;
using Questar.Maps;
using Questar.Actors;

namespace Questar.Core
{
    public class Game
    {
        public static Game Instance
        {
            get { return Singleton<Game>.Instance; }
        }

        private MainWindow main_window;
        private TurnLoop turn_loop;
        private ITurnLoopDriver driver;
        private Map current_map;
        private Actor hero;

        private Game ()
        {
            ProcessName = ProgramInformation.Name.ToLower ();
            Window.DefaultIconName = ThemeIcons.Application;
            Application.Init ();

            turn_loop = new TurnLoop ();
            driver = new IdleTurnLoopDriver (turn_loop);
        }

        public ITurnLoopDriver TurnLoopDriver
        {
            get { return driver; }
        }

        public TurnLoop TurnLoop
        {
            get { return turn_loop; }
        }

        public Map CurrentMap
        {
            get { return current_map; }
            set { current_map = value; }
        }

        public Actor Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        public void Start ()
        {
            driver.Start ();

            if (main_window == null) {
                main_window = new MainWindow ();
                Application.Run ();
            }
        }

        public void Quit ()
        {
            driver.Stop ();

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

