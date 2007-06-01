//
// TerrainManager.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Questar.Maps
{
    public class TerrainManager
    {
        private Dictionary<string,Terrain> terrains;

        public TerrainManager ()
        {
            terrains = new Dictionary<string,Terrain> ();

            Assembly assembly = Assembly.GetExecutingAssembly ();
            Stream stream = assembly.GetManifestResourceStream ("terrains.xml");
            XmlTextReader reader = new XmlTextReader (stream);

            while (reader.Read ()) {
                if ((reader.NodeType == XmlNodeType.Element) &&
                    (reader.Name == "terrain")) {
                    string key = reader["key"];

                    if (key == null)
                        throw new ApplicationException
                            ("Terrain does not contain a key attribute.");

                    ReadTerrain (reader, key);
                }
            }
        }

        private void ReadTerrain (XmlReader reader, string key)
        {
            Terrain terrain = new Terrain ();

            while (reader.Read ()) {
                if ((reader.NodeType == XmlNodeType.EndElement) &&
                    (reader.Name == "terrain"))
                    break;

                if (reader.NodeType != XmlNodeType.Element)
                    continue;

                string element = reader.Name;
                reader.Read ();

                if (element == "name")
                    terrain.Name = reader.Value;

                else if (element == "description")
                    terrain.Description = reader.Value;

                else if (element == "blocks")
                    terrain.IsBlocking = Boolean.Parse (reader.Value);

                else if (element == "tile")
                    terrain.Tiles.Add (reader.Value);
            }

            if (!terrain.IsValid)
                throw new ApplicationException (String.Format
                    ("Terrain '{0}' is missing a child element.",
                    key));

            terrains.Add (key, terrain);
        }

        public Terrain this[string key]
        {
            get {
                try {
                    return terrains[key];
                } catch {
                    throw new ApplicationException (String.Format
                        ("The terrain key '{0}' is invalid.", key));
                }
            }
        }
    }
}

