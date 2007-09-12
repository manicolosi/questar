using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

using Questar.Base;
using Questar.Primitives;

namespace Questar.Items
{
    public class ItemFactory : AbstractEntityFactory<Item>
    {
        private const string resource = "items.xml";

        public static ItemFactory Instance
        {
            get { return Singleton<ItemFactory>.Instance; }
        }

        private Dictionary<string, ItemDefinition> definitions;

        private ItemFactory ()
        {
            definitions = new Dictionary<string, ItemDefinition> (); 
            Assembly assembly = Assembly.GetExecutingAssembly ();
			Stream stream = assembly.GetManifestResourceStream (resource);

			XmlDocument document = new XmlDocument ();
			document.Load (stream);
			
			XmlNodeList nodes = document.GetElementsByTagName ("Item");

			string id = null;
			string klass = null;
			
			foreach (XmlNode node in nodes) {
				foreach (XmlAttribute attribute in node.Attributes) {
					switch (attribute.Name) {
					case "id":
						id = attribute.Value;
						break;
					case "class":
						klass = attribute.Value;
						break;
					}
				}

				LoadItem (node, id, klass);
			}
		}
		
		public override Item Create (string item_id)
		{
			if (!definitions.ContainsKey (item_id))
				throw new ApplicationException (String.Format (
				    "ItemFactory doesn't contain an Item with id \"{0}\"",
				    item_id));
			
			ItemDefinition definition = definitions[item_id];
			
			ItemBuilder builder = new ItemBuilder (
			    Assembly.GetExecutingAssembly (), "Questar.Items.Concrete",
			    definition.Class);
			
			foreach (KeyValuePair<string,string> kv_pair in definition.Properties)
				builder.SetProperty (kv_pair.Key, kv_pair.Value);
			
            Item item = builder.GetItem ();
            OnCreation (item);

			return item;
		}

		private void LoadItem (XmlNode node, string id, string klass)
		{
			if (id == null)
				throw new ApplicationException (
			        "Item doesn't contain an 'id' attribute");
			
			if (klass == null)
				throw new ApplicationException (String.Format (
				    "Item with id \"{0}\" doesn't contain a 'class' attribute",
				    id));
				
			ItemDefinition def = new ItemDefinition (klass);
			
			foreach (XmlNode child in node.ChildNodes)
				def.AddProperty (child.Name, child.FirstChild.Value);

			definitions.Add (id, def);
		}
	}
}
