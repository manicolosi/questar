/*******************************************************************************
 *  EntryPoint.cs: Questar's entry point, AKA Main().
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

using Questar.Actors;
using Questar.Base;
using Questar.Gui;
using Questar.Items;
using Questar.Maps;
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
            World world = new World ();
            Game.Instance.World = world;

            world.Map = new Map ();
            new Hero ();

            ItemFactory factory = ItemFactory.Instance;
            world.Hero.Inventory.Add (factory.Create ("HealLight"));
            world.Hero.Inventory.Add (factory.Create ("HealLight"));
            world.Hero.Inventory.Add (factory.Create ("HealSerious"));
            world.Hero.Inventory.Add (factory.Create ("HealSerious"));

            MonsterFactory.Create ("imp");
            MonsterFactory.Create ("imp");
            MonsterFactory.Create ("troll");
            MonsterFactory.Create ("troll");

            ItemFactory.Instance.Create ("HealLight", new MapLocation (world.Map, 5, 5));
        }
    }
}

