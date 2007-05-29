using Gdk;
using Glade;
using Gtk;
using System;

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
            SetSizeAndPosition ();
        }

        private void SetupGlade ()
        {
            string path = "../data/questar.glade";
            Glade.XML gxml = new Glade.XML (path, "main_container", null);
            gxml.Autoconnect (this);
        }

        private void BuildWindow ()
        {
            Window.SetDefaultIconFromFile ("../data/questar.svg");

            menubar_container.Add (UIActions.Instance.MenuBar);
            message_view_container.Add (new MessageView ());
            map_view_container.Add (new MapView (World.Instance.Map));

            base.Add (main_container);

            base.ShowAll ();
        }

        private void SetSizeAndPosition ()
        {
            int x = UISchema.XPos.Get ();
            int y = UISchema.YPos.Get ();
            int width = UISchema.Width.Get ();
            int height = UISchema.Height.Get ();

            if (width != 0 && height != 0)
                base.Resize (width, height);

            if (x == 0 && y == 0)
                base.SetPosition (WindowPosition.Center);
            else
                base.Move (x, y);

            if (UISchema.Maximized.Get ())
                base.Maximize ();
            else
                base.Unmaximize ();
        }

        private void SetupHandlers ()
        {
            base.AddAccelGroup (UIActions.Instance.UI.AccelGroup);

            UIActions.Instance["Quit"].Activated += delegate {
                Close ();
            };

            UIActions.Instance["About"].Activated += delegate {
                Dialog about = new AboutDialog ();
                about.Run ();
                about.Destroy ();
            };

            ConfigurationClient.SyncToggleAction ("ShowMessages",
                UISchema.ShowMessages,
                delegate (ToggleAction action, SchemaEntry<bool> entry) {
                    message_view_container.Visible = action.Active;
                });
        }

        protected override bool OnDeleteEvent (Event args)
        {
            Close ();
            return base.OnDeleteEvent (args);
        }

        protected override bool OnWindowStateEvent (EventWindowState args)
        {
            WindowState state = args.NewWindowState;

            if ((state & WindowState.Withdrawn) == 0)
                UISchema.Maximized.Set ((state & WindowState.Maximized) != 0);

            return base.OnWindowStateEvent (args);
        }

        protected override bool OnConfigureEvent (EventConfigure args)
        {
            if ((base.GdkWindow.State & WindowState.Maximized) == 0) {
                int x, y, width, height;

                base.GetPosition (out x, out y);
                base.GetSize (out width, out height);

                UISchema.XPos.Set (x);
                UISchema.YPos.Set (y);
                UISchema.Width.Set (width);
                UISchema.Height.Set (height);
            }

            return base.OnConfigureEvent (args);
        }

        private void Close ()
        {
            EntryPoint.Quit ();
        }

        // FIXME: Gtk.Action[s] don't seem to work with accelerator's without
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
