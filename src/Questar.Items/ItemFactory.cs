/*******************************************************************************
 *  ItemFactory.cs: A factory that creates Item objects.
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

namespace Questar.Items
{
    public static class ItemFactory
    {
        private const string resource = "items.xml";
        private static Dictionary<string, Item> items = null;

        public static Item Create (string id)
        {
            if (items == null)
                Load ();

            if (!items.ContainsKey (id))
                throw new ArgumentException ("The given id is invalid.");

            return items[id].Clone ();
        }

        public static void Load ()
        {
            items = new Dictionary<string, Item> ();

            Assembly assembly = Assembly.GetExecutingAssembly ();
            Stream stream = assembly.GetManifestResourceStream (resource);

            XmlDocument document = new XmlDocument ();
            document.Load (stream);

            XmlNodeList nodes = document.GetElementsByTagName ("Item");

            foreach (XmlNode node in nodes) {
                string id_attribute = null;
                string type_attribute = null;

                foreach (XmlAttribute attribute in node.Attributes) {
                    if (attribute.Name == "Id")
                        id_attribute = attribute.Value;
                    else if (attribute.Name == "Type")
                        type_attribute = attribute.Value;
                }

                if (id_attribute == null || type_attribute == null)
                    throw new ApplicationException (
                        "Item node must have an 'Id' and 'Type' attribute.");

                LoadItem (node, id_attribute, type_attribute);
            }
        }

        private static void LoadItem (XmlNode node, string id, string type)
        {
            Assembly assembly = Assembly.GetExecutingAssembly ();
            Item item = (Item) assembly.CreateInstance ("Questar.Items." + type);

            items.Add (id, item);
        }
    }
}

