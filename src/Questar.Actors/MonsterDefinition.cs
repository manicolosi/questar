/*******************************************************************************
 *  MonsterDefinition.cs: A structure to contain information used to
 *  create Monsters.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

namespace Questar.Actors
{
    public struct MonsterDefinition
    {
        public string Id;
        public string Name;
        public string Prefix;
        public string Description;
        public string TileId;
        public int MaxHP;

        public MonsterDefinition (string id)
        {
            Id = id;
            Name = "(No Name)";
            Prefix = "a";
            Description = "(No Description)";
            TileId = "default";
            MaxHP = 0;
        }
    }
}

