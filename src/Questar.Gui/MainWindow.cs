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

            if (UISchema.FullScreen.Value)
                base.Window.Fullscreen ();
            else
                base.Window.Unfullscreen ();
        }

        [GLib.ConnectBefore]
        private void OnWindowStateEvent (object sender,
            WindowStateEventArgs args)
        {
            WindowState state = args.Event.NewWindowState;

            bool maximized = (state & WindowState.Maximized) != 0;
            UISchema.Maximized.Value = maximized;
        }

        [GLib.ConnectBefore]
        private void OnConfigureEvent (object sender, ConfigureEventArgs args)
        {
            if (UISchema.Maximized.Value == true)
                return;

            UISchema.XPos.Value   = args.Event.X;
            UISchema.YPos.Value   = args.Event.Y;
            UISchema.Width.Value  = args.Event.Width;
            UISchema.Height.Value = args.Event.Height;
        }

        private void SetupHandlers ()
        {
            base.Window.AddAccelGroup (UIActions.Instance.UI.AccelGroup);

            base.Window.WindowStateEvent += OnWindowStateEvent;
            base.Window.ConfigureEvent += OnConfigureEvent;
            base.Window.DeleteEvent += delegate { Game.Instance.Quit (); };

            ConfigurationClient.SyncToggleAction ("FullScreen",
                UISchema.FullScreen,
                delegate (ToggleAction action, SchemaEntry<bool> entry) {
                    if (action.Active)
                        base.Window.Fullscreen ();
                    else
                        base.Window.Unfullscreen ();
                });

            ConfigurationClient.SyncToggleAction ("ShowMessages",
                UISchema.ShowMessages,
                delegate (ToggleAction action, SchemaEntry<bool> entry) {
                    message_view_container.Visible = action.Active;
                });

            UISchema.ShowMessages.Changed += delegate {
                ((ToggleAction) UIActions.Instance["ShowMessages"]).Active =
                    UISchema.ShowMessages.Value;
            };
        }
    }
}

