using System;
using System.Globalization;
using System.Reflection;

namespace Questar.Items
{
	public class ItemBuilder
	{
		private Item item;
		
		public ItemBuilder (string klass) : this ("Questar.Items", klass)
		{ }
		
		public ItemBuilder (string namespce, string klass) :
			this (Assembly.GetCallingAssembly (), namespce, klass)
		{ }
			
		public ItemBuilder (Assembly asm, string namespce, string klass)
		{
			string full_name = String.Format ("{0}.{1}", namespce, klass);
			item = asm.CreateInstance (full_name) as Item;

			if (item == null)
				throw new ApplicationException (String.Format
		            ("Unable to create instance of {0}", full_name));
		}

		public void SetProperty (string property, IConvertible prop_val)
		{
			PropertyInfo prop_info = item.GetType ().GetProperty (property);

			if (prop_info == null)
				throw new ApplicationException (String.Format (
		            "Unable to set property {0}.{1} to \"{2}\"",
				    item.GetType (), property, prop_val));
			
			Type prop_type = prop_info.PropertyType;
			object param;

			try {
				param = prop_val.ToType (prop_type, NumberFormatInfo.InvariantInfo);
			} catch (FormatException e) {
				throw new ApplicationException (String.Format (
				    "prop_val cannot be converted to a {0}", prop_type), e);
			}
			
			prop_info.SetValue (item, param, null);
		}
		
		public Item GetItem ()
		{
			return item;
		}
	}
}
