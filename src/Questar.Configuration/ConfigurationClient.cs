//
// ConfigurationClient.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

// Mostly stolen from Banshee.
using GConf;
using Gtk;
using System;

using Questar.Base;
using Questar.Gui;

namespace Questar.Configuration
{
    public class ConfigurationClient
    {
        public static ConfigurationClient Instance
        {
            get { return Singleton<ConfigurationClient>.Instance; }
        }

        public delegate void SyncToggleActionHandler (ToggleAction action,
            SchemaEntry<bool> entry);

        public static void SyncToggleAction (string action_name,
            SchemaEntry<bool> entry, SyncToggleActionHandler handler)
        {
            ToggleAction action = UIActions.Instance[action_name]
                as ToggleAction;
            action.Active = entry.Value;

            handler (action, entry);

            action.Activated += delegate {
                entry.Value = action.Active;
                handler (action, entry);
            };
        }

        private const string base_key = "/apps/questar/";

        private Client client;

        private ConfigurationClient ()
        {
            client = new Client ();
        }

        public void AddNotify<T> (SchemaEntry<T> entry,
            NotifyEventHandler handler)
        {
            client.AddNotify (GetFullKeyName<T> (entry), handler);
        }

        public T Get<T> (SchemaEntry<T> entry)
        {
            string key = GetFullKeyName<T> (entry);

            try {
                object value = client.Get (key);

                if (typeof (T).IsEnum)
                    value = Enum.Parse (typeof (T), value as string, true);

                return (T) value;
            }
            catch (NoSuchKeyException) {
                Set<T> (entry, entry.DefaultValue);
                return entry.DefaultValue;
            }
            catch (ArgumentException) {
                Set<T> (entry, entry.DefaultValue);
                return entry.DefaultValue;
            }
        }

        public void Set<T> (SchemaEntry<T> entry, T value)
        {
            object real_value = value;
            string key = GetFullKeyName<T> (entry);

            if (typeof (T).IsEnum)
                real_value = value.ToString ();

            client.Set (key, real_value);
        }

        private string GetFullKeyName<T> (SchemaEntry<T> entry)
        {
            string namespce = entry.Namespace + "/";
            if (String.IsNullOrEmpty (entry.Namespace))
                namespce = "";

            return base_key + namespce + entry.Key;
        }
    }
}

