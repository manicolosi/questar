using System;
using System.Collections.Generic;
using System.Text;

namespace Questar.Items
{
	public class ItemDefinition
	{
		private string klass;
		private Dictionary<string,string> properties =
			new Dictionary<string,string> ();
		
		public ItemDefinition (string klass)
		{
			this.klass = klass;
		}
		
		public string Class
		{
			get { return klass; }
		}

		public IEnumerable<KeyValuePair<string,string>> Properties
		{
			get { return properties; }
		}
		
		public void AddProperty (string property, string value)
		{
			properties.Add (property, value);
		}
		
		// For debugging purposes.
		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			
			sb.Append (klass);
			sb.Append (": ");
			
			foreach (KeyValuePair<string,string> kv_pair in properties) {
				sb.Append ("[ ");
				sb.Append (kv_pair.Key);
				sb.Append (" -> ");
				sb.Append (kv_pair.Value);
				sb.Append (" ] ");
			}
			
			return sb.ToString ();
		}
	}
}
