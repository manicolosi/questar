/*******************************************************************************
 *  TestGame.cs: Unit Tests for Game class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;
using GLib;
using NUnit.Framework;

using Questar.Base;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestGame
    {
        [Test]
        public void SingleInstance ()
        {
            Game a = Game.Instance;
            Game b = Game.Instance;

            Assert.AreSame (a, b);
        }

        [Test]
        public void QuitStopsMainLoop ()
        {
            Idle.Add (delegate {
                Game.Instance.Quit ();
                return false;
            });
            Application.Run ();
        }
    }
}

