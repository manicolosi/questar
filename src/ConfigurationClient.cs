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

        public T Get<T> (SchemaEntry<T> entry)
        {
            string key = base_key + entry.Namespace + "/" + entry.Key;

            try {
                return (T) client.Get (key);
            } catch (NoSuchKeyException) {
                Set<T> (entry, entry.DefaultValue);
                return entry.DefaultValue;
            }
        }

        public void Set<T> (SchemaEntry<T> entry, T value)
        {
            string key = base_key + entry.Namespace + "/" + entry.Key;
            client.Set (key, value);
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
