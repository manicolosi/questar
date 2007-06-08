//
// MainWindow.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Gdk;
using Glade;
using Gtk;
using System;
using System.Reflection;

using Questar.Base;
using Questar.Configuration;

using Window = Gtk.Window;

namespace Questar.Gui
{
    public class MainWindow : GladeWindow
    {
        [Glade.Widget] private Container menubar_container;
        [Glade.Widget] private Container map_view_container;
        [Glade.Widget] private Container message_view_container;

        public MainWindow () : base ("main_window")
        {
            BuildWindow ();
            SetupHandlers ();
            LoadSettings ();
        }

        private void BuildWindow ()
        {
            menubar_container.Add (UIActions.Instance.MenuBar);
            message_view_container.Add (new MessageView ());
            map_view_container.Add (new WorldView (Game.Instance.World));

            base.Window.ShowAll ();
        }

        private void LoadSettings ()
        {
            int width = UISchema.Width.Value;
            int height = UISchema.Height.Value;

            if (width != 0 && height != 0)
                base.Window.Resize (width, height);

            int x = UISchema.XPos.Value;
            int y = UISchema.YPos.Value;

            if (x == 0 && y == 0)
                base.Window.SetPosition (WindowPosition.Center);
            else
                base.Window.Move (x, y);

            if (UISchema.Maximized.Value)
                base.Window.Maximize ();
            else
                base.Window.Unmaximize ();
        }

        private void OnWindowStateEvent (object sender,
            WindowStateEventArgs args)
        {
            WindowState state = args.Event.NewWindowState;

            if ((state & WindowState.Withdrawn) != 0) {
                bool maximized = (state & WindowState.Maximized) != 0;
                UISchema.Maximized.Value = maximized;
            }
        }

        [GLib.ConnectBefore]
        private void OnConfigureEvent (object sender, ConfigureEventArgs args)
        {
            Console.WriteLine ("Here");
            if (UISchema.Maximized.Value == true)
                return;

            int x, y, width, height;

            base.Window.GetPosition (out x, out y);
            base.Window.GetSize (out width, out height);


            UISchema.XPos.Value = x;
            UISchema.YPos.Value = y;
            UISchema.Width.Value = width;
            UISchema.Height.Value = height;
        }

        private void SetupHandlers ()
        {
            base.Window.AddAccelGroup (UIActions.Instance.UI.AccelGroup);

            base.Window.WindowStateEvent += OnWindowStateEvent;
            base.Window.ConfigureEvent += OnConfigureEvent;
            base.Window.DeleteEvent += delegate { Game.Instance.Quit (); };


            ConfigurationClient.SyncToggleAction ("ShowMessages",
                UISchema.ShowMessages,
                delegate (ToggleAction action, SchemaEntry<bool> entry) {
                    message_view_container.Visible = action.Active;
                });

            UISchema.ShowMessages.Changed += delegate {
                (UIActions.Instance["ShowMessages"] as ToggleAction).Active =
                    UISchema.ShowMessages.Value;
            };
            EventHandler<EventArgs> load_settings = delegate {
                LoadSettings ();
            };
            UISchema.Width.Changed     += load_settings;
            UISchema.Height.Changed    += load_settings;
            UISchema.XPos.Changed      += load_settings;
            UISchema.YPos.Changed      += load_settings;
            UISchema.Maximized.Changed += load_settings;
        }
    }
}
