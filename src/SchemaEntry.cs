//
// SchemaEntry.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

// Mostly stolen from Banshee.
using GConf;
using System;

using Questar.Helpers;

namespace Questar.Configuration
{
    public class SchemaEntry<T>
    {
        public readonly string Namespace;
        public readonly string Key;
        public readonly T DefaultValue;
        public readonly string ShortDescription;
        public readonly string LongDescription;

        public event EventHandler<EventArgs> Changed;

        public SchemaEntry (string namespce, string key, T default_value,
            string short_description, string long_description)
        {
            Namespace = namespce;
            Key = key;
            DefaultValue = default_value;
            ShortDescription = short_description;
            LongDescription = long_description;

            ConfigurationClient.Instance.AddNotify (this, delegate {
                EventHelper.Raise (this, Changed);
            });
        }

        public T Value
        {
            get {
                return ConfigurationClient.Instance.Get<T> (this);
            }
            set {
                ConfigurationClient.Instance.Set<T> (this, value);
            }
        }
    }
}

