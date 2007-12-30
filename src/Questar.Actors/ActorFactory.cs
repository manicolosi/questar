/*******************************************************************************
 *  ActorFactory.cs: A factory that creates Actors (both Monsters and
 *  the Hero).
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
using Questar.Primitives;

namespace Questar.Actors
{
    public class ActorFactory : AbstractEntityFactory<Actor>
    {
        private const string resource = "monsters.xml";

        public static ActorFactory Instance
        {
            get { return Singleton<ActorFactory>.Instance; }
        }

        private Dictionary<string, MonsterDefinition> definitions;

        private ActorFactory ()
        {
            definitions = new Dictionary<string, MonsterDefinition> ();

            Assembly assembly = Assembly.GetExecutingAssembly ();
            Stream stream = assembly.GetManifestResourceStream (resource);

            XmlDocument document = new XmlDocument ();
            document.Load (stream);

            XmlNodeList nodes = document.GetElementsByTagName ("Monster");

            foreach (XmlNode node in nodes) {
                foreach (XmlAttribute attribute in node.Attributes) {
                    if (attribute.Name == "Id") {
                        LoadMonster (attribute.Value, node);
                    }
                }
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

        public override Actor Create (string monster_id)
        {
            Actor actor = new Monster (definitions[monster_id]);
            OnCreation (actor);

            return actor;
        }

        // TODO: Later this could take BirthOptions parameter.
        public Actor Create ()
        {
            Actor actor = new Hero ();
            OnCreation (actor);

            return Actor;
        }
    }
}
