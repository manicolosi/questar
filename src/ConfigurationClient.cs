//
// ConfigurationClient.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

// Mostly stolen from Banshee.
using GConf;
using Gtk;
using System;

using Questar.Gui;

namespace Questar.Configuration
{
    public class ConfigurationClient
    {
        private static ConfigurationClient instance;

        public static ConfigurationClient Instance
        {
            get {
                if (instance == null)
                    instance = new ConfigurationClient ();

                return instance;
            }
        }

        private const string base_key = "/apps/questar/";
        private Client client;

        private ConfigurationClient ()
        {
            client = new Client ();
        }

        public void AddNotify<T> (SchemaEntry<T> entry, NotifyEventHandler handler)
        {
            client.AddNotify (base_key + entry.Namespace + "/" + entry.Key,
                handler);
        }

        public T Get<T> (SchemaEntry<T> entry)
        {
            string key = base_key + entry.Namespace + "/" + entry.Key;

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
            string key = base_key + entry.Namespace + "/" + entry.Key;

            if (typeof (T).IsEnum)
                real_value = value.ToString ();

            client.Set (key, real_value);
        }

        public delegate void SyncToggleActionHandler (ToggleAction action,
            SchemaEntry<bool> entry);

        public static void SyncToggleAction (string action_name,
            SchemaEntry<bool> entry, SyncToggleActionHandler handler)
        {
            ToggleAction action = UIActions.Instance[action_name]
                as ToggleAction;
            action.Active = entry.Get ();

            handler (action, entry);

            action.Activated += delegate {
                entry.Set (action.Active);
                handler (action, entry);
            };
        }
    }
}
