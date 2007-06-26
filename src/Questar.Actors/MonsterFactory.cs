/*******************************************************************************
 *  MonsterFactory.cs: A factory that creates Monsters from
 *  MonsterDefinitions.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

using Questar.Base;

namespace Questar.Actors
{
    public class MonsterFactory
    {
        private const string resource = "monsters.xml";

        private Dictionary<string, MonsterDefinition> definitions =
            new Dictionary<string, MonsterDefinition> ();

        public static MonsterFactory Instance
        {
            get { return Singleton<MonsterFactory>.Instance; }
        }

        private MonsterFactory ()
        {
            Load ();
        }

        public Monster Create (string id)
        {
            return new Monster (definitions[id]);
        }

        private void Load ()
        {
            Assembly assembly = Assembly.GetExecutingAssembly ();
            Stream stream = assembly.GetManifestResourceStream (resource);
            XmlTextReader reader = new XmlTextReader (stream);

            string id;

            while (reader.Read ()) {
                if ((reader.NodeType == XmlNodeType.Element) &&
                    (reader.Name == "Monster")) {
                    id = reader["Id"];

                    if (id == null)
                        throw new ApplicationException
                            ("Monster does not contain an 'Id' attribute.");

                    LoadMonster (reader, id);
                }
            }
        }

        private void LoadMonster (XmlReader reader, string id)
        {
            MonsterDefinition definition = new MonsterDefinition (id);

            while (reader.Read ()) {
                if ((reader.NodeType == XmlNodeType.EndElement) &&
                    (reader.Name == "Monster"))
                    break;

                if (reader.NodeType != XmlNodeType.Element)
                    continue;

                string element = reader.Name;
                reader.Read ();

                if (element == "Name")
                    definition.Name = reader.Value;

                else if (element == "Description")
                    definition.Description = reader.Value;

                else if (element == "Tile")
                    definition.TileId = reader.Value;

                else if (element == "MaxHP")
                    definition.MaxHP = Int32.Parse (reader.Value);
            }

            definitions.Add (id, definition);
        }
    }
}

