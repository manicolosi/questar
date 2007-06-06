//
// EntryPoint.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

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
            Game game = Game.Instance;

            new StartDialog ();

            game.World = CreateInitialWorld ();
            game.Start ();
        }

        private static World CreateInitialWorld ()
        {
            World world = World.Instance;

            world.Map = new Map ();
            world.AddActor (new Monster ("troll", world.Map));
            world.AddActor (new Monster ("imp", world.Map));
            world.Hero = new Hero (world.Map);

            return world;
        }
    }
}

