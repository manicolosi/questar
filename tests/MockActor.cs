/*******************************************************************************
 *  MockActor.cs: Mock object that implements the Actor abstract class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using GLib;
using System;
using System.Collections.Generic;

using Questar.Actors;
using Questar.Actors.Actions;
using Questar.Base;
using Questar.Maps;
using Questar.Primitives;

namespace Questar.UnitTests
{
    public class MockActor : Actor
    {
        bool is_turn_ready = false;
        Dictionary<int, bool> turns = new Dictionary<int, bool> ();

        World world;
        MainLoop loop;

        public MockActor (string name, MainLoop loop, World world)
        {
            base.Name = name;
            base.HitPoints = new HitPoints (0, 0);
            base.Tile = "Blah";
            base.Location = new Point (0, 0);
            base.Map = null;

            this.world = world;
            this.loop = loop;
        }

        public MockActor (string name) : this (name, null, null)
        {
        }

        public MockActor () : this (null, null, null)
        {
        }

        public void Create ()
        {
            base.OnCreation ();
        }

        public override bool IsTurnReady
        {
            get { return is_turn_ready; }
        }

        public override IAction Action
        {
            get {
                if (!is_turn_ready)
                    throw new InvalidOperationException (
                        "An action is unavailable, check IsTurnReady first");

                is_turn_ready = false;
                turns.Add (world.Round, true);

                loop.Quit ();

                return new DoNothingAction (this);
            }
        }

        public void MakeTurnReady ()
        {
            is_turn_ready = true;
            world.IsPaused = false;
        }

        public bool TookTurnInRound (int round)
        {
            return turns.ContainsKey (round);
        }
    }
}

