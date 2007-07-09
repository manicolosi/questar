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
            world.Hero.Inventory.Add (ItemFactory.Create ("HealLightWounds"));
            world.Hero.Inventory.Add (ItemFactory.Create ("HealLightWounds"));
            world.Hero.Inventory.Add (ItemFactory.Create ("HealSeriousWounds"));
            world.Hero.Inventory.Add (ItemFactory.Create ("HealSeriousWounds"));
            MonsterFactory.Create ("imp");
            MonsterFactory.Create ("imp");
            MonsterFactory.Create ("troll");
            MonsterFactory.Create ("troll");
        }
    }
}

