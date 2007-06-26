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

            XmlDocument document = new XmlDocument ();
            document.Load (stream);

            XmlNodeList nodes = document.GetElementsByTagName ("Monster");

            foreach (XmlNode node in nodes) {
                foreach (XmlAttribute attribute in node.Attributes)
                    if (attribute.Name == "Id")
                        LoadMonster (attribute.Value, node);
            }
        }

        private void LoadMonster (string id, XmlNode node)
        {
            MonsterDefinition definition = new MonsterDefinition (id);

            foreach (XmlNode child in node.ChildNodes) {
                if (child.Name == "Name") {
                    definition.Name = child.FirstChild.Value;
                    foreach (XmlAttribute attribute in child.Attributes)
                        if (attribute.Name == "Prefix")
                            definition.Prefix = attribute.Value;
                }

                else if (child.Name == "Description")
                    definition.Description = child.FirstChild.Value;

                else if (child.Name == "Tile")
                    definition.TileId = child.FirstChild.Value;

                else if (child.Name == "MaxHP")
                    definition.MaxHP = Int32.Parse (child.FirstChild.Value);
            }

            definitions.Add (id, definition);
        }
    }
}

