/*******************************************************************************
 *  IdleTurnLoopDriver.cs: An ITurnLoopDriver that uses a GLib.Idle
 *  object to schedule turns.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using GLib;
using System;

namespace Questar.Core
{
    public class IdleTurnLoopDriver : ITurnLoopDriver
    {
        private TurnLoop turn_loop;
        private bool is_running = false;

        public IdleTurnLoopDriver (TurnLoop turn_loop)
        {
            this.turn_loop = turn_loop;
        }

        public bool IsRunning
        {
            get { return is_running; }
        }

        public void Start ()
        {
            // TODO: Need to check if we're already running?
            Idle.Add (IdleHandler);
            is_running = true;
        }

        public void Stop ()
        {
            // TODO: Need to check if we're NOT already running?
            Idle.Remove (IdleHandler);
            is_running = false;
        }

        private bool IdleHandler ()
        {
            bool run_again = turn_loop.NextTurn ();

            if (!run_again)
                is_running = false ();

            return run_again;
        }
    }
}

