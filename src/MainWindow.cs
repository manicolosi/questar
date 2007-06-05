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
    [Binding (Gdk.Key.Up,           "ActivateAction", "MoveNorth")]
    [Binding (Gdk.Key.KP_Up,        "ActivateAction", "MoveNorth")]
    [Binding (Gdk.Key.Down,         "ActivateAction", "MoveSouth")]
    [Binding (Gdk.Key.KP_Down,      "ActivateAction", "MoveSouth")]
    [Binding (Gdk.Key.Left,         "ActivateAction", "MoveWest")]
    [Binding (Gdk.Key.KP_Left,      "ActivateAction", "MoveWest")]
    [Binding (Gdk.Key.Right,        "ActivateAction", "MoveEast")]
    [Binding (Gdk.Key.KP_Right,     "ActivateAction", "MoveEast")]
    [Binding (Gdk.Key.KP_Page_Up,   "ActivateAction", "MoveNorthEast")]
    [Binding (Gdk.Key.KP_Page_Down, "ActivateAction", "MoveSouthEast")]
    [Binding (Gdk.Key.KP_Home,      "ActivateAction", "MoveNorthWest")]
    [Binding (Gdk.Key.KP_End,       "ActivateAction", "MoveSouthWest")]
    [Binding (Gdk.Key.KP_Begin,     "ActivateAction", "DoNothing")]
    public class MainWindow : Window
    {
        [Glade.Widget] private Container main_container;
        [Glade.Widget] private Container menubar_container;
        [Glade.Widget] private Container map_view_container;
        [Glade.Widget] private Container message_view_container;

        public MainWindow () : base ("Questar")
        {
            SetupGlade ();
            BuildWindow ();
            SetupHandlers ();
            LoadSettings ();

            new StartDialog ();
        }

        private void SetupGlade ()
        {
            Glade.XML gxml = new Glade.XML ("questar.glade", "main_container");
            gxml.Autoconnect (this);
        }

        private void BuildWindow ()
        {
            Assembly assembly = Assembly.GetExecutingAssembly ();
            Window.DefaultIcon = new Pixbuf (assembly, "questar.svg");

            menubar_container.Add (UIActions.Instance.MenuBar);
            message_view_container.Add (new MessageView ());
            map_view_container.Add (new MapView (World.Instance.Map));

            base.Add (main_container);
            base.ShowAll ();
        }

        private void LoadSettings ()
        {
            int width = UISchema.Width.Value;
            int height = UISchema.Height.Value;

            if (width != 0 && height != 0)
                base.Resize (width, height);

            int x = UISchema.XPos.Value;
            int y = UISchema.YPos.Value;

            if (x == 0 && y == 0)
                base.SetPosition (WindowPosition.Center);
            else
                base.Move (x, y);

            if (UISchema.Maximized.Value)
                base.Maximize ();
            else
                base.Unmaximize ();
        }

        private void SetupHandlers ()
        {
            base.AddAccelGroup (UIActions.Instance.UI.AccelGroup);

            UIActions.Instance["Quit"].Activated += delegate {
                EntryPoint.Quit ();
            };

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

        protected override bool OnDeleteEvent (Event args)
        {
            EntryPoint.Quit ();
            return false;
        }

        protected override bool OnWindowStateEvent (EventWindowState args)
        {
            WindowState state = args.NewWindowState;

            if ((state & WindowState.Withdrawn) == 0)
                UISchema.Maximized.Value = (state & WindowState.Maximized) != 0;

            return base.OnWindowStateEvent (args);
        }

        protected override bool OnConfigureEvent (EventConfigure args)
        {
            if (UISchema.Maximized.Value)
                return false;

            int x, y, width, height;

            base.GetPosition (out x, out y);
            base.GetSize (out width, out height);

            UISchema.XPos.Value = x;
            UISchema.YPos.Value = y;
            UISchema.Width.Value = width;
            UISchema.Height.Value = height;

            return base.OnConfigureEvent (args);
        }

        // HACK: Gtk.Action[s] don't seem to work with accelerator's without
        // modifier keys, for instance Gdk.Key.Up. The work around is to bind
        // to them using Gtk.BindingAttribute and manually Activate () them.
#pragma warning disable 0169
        private bool ActivateAction (string action)
#pragma warning restore 0169
        {
            UIActions.Instance[action].Activate ();
            return true;
        }
    }
}
