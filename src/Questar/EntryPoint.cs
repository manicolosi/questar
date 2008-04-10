/*******************************************************************************
 *  EntryPoint.cs: Questar's entry point, AKA Main().
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Core;
using Questar.Gui;
using Questar.Items;
using Questar.Maps.Generation;
using Questar.Primitives;

namespace Questar
{
    public class EntryPoint
    {
        public static void Main ()
        {
            Game game = Game.Instance;

            //new StartDialog ();

            CreateInitialWorld ();
            game.Start ();
        }

        private static void CreateInitialWorld ()
        {
            Game game = Game.Instance;

            IMapGenerator map_gen = new StupidGenerator ();
            game.CurrentMap = map_gen.Generate ();

            game.Hero = new Hero ();

            MonsterFactory.Instance.Create ("imp");
            MonsterFactory.Instance.Create ("imp");
            MonsterFactory.Instance.Create ("troll");
            MonsterFactory.Instance.Create ("troll");

            ItemFactory factory = ItemFactory.Instance;
            factory.Create ("HealLight", new Location (game.CurrentMap, 5, 5));
            //game.CurrentMap.GetRandomGrid (g => !g.IsBlocking)
                //.Add (factory.Create ("HealLight"));

            // Use an ActorLocation with the Location override of
            // ItemFactory.Create() and make the Inventory adding
            // automagic.
            game.Hero.Inventory.Add (factory.Create ("HealLight"));
            game.Hero.Inventory.Add (factory.Create ("HealLight"));
            game.Hero.Inventory.Add (factory.Create ("HealSerious"));
            game.Hero.Inventory.Add (factory.Create ("HealSerious"));

        }
    }
}

