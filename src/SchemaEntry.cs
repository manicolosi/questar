//
// SchemaEntry.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

// Mostly stolen from Banshee.

namespace Questar.Configuration
{
    public struct SchemaEntry<T>
    {
        public readonly string Namespace;
        public readonly string Key;
        public readonly T DefaultValue;
        public readonly string ShortDescription;
        public readonly string LongDescription;

        public SchemaEntry (string namespce, string key, T default_value,
            string short_description, string long_description)
        {
            Namespace = namespce;
            Key = key;
            DefaultValue = default_value;
            ShortDescription = short_description;
            LongDescription = long_description;
        }

        public T Get ()
        {
            return ConfigurationClient.Instance.Get<T> (this);
        }

        public void Set (T value)
        {
            ConfigurationClient.Instance.Set<T> (this, value);
        }
    }
}

