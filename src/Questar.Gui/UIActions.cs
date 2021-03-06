/*******************************************************************************
 *  UIActions.cs: Encapsulates Gtk.Actions.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gdk;
using Gtk;
using System;

using Key = Gdk.Key;
using Action = Gtk.Action;

using Questar.Base;
using Questar.Core;

namespace Questar.Gui
{
    public class UIActions
    {
        public static UIActions Instance
        {
            get { return Singleton<UIActions>.Instance; }
        }

        private ActionGroup normal_actions = new ActionGroup ("Normal");
        private ActionGroup toggle_actions = new ActionGroup ("Toggle");
        private ActionGroup map_actions    = new ActionGroup ("Map");
        private ActionGroup hero_actions   = new ActionGroup ("Hero");

        private UIManager ui = new UIManager ();

        private UIActions ()
        {
            PopulateActionGroups ();
            ui.AddUiFromResource ("questar-ui.xml");
        }

        private void PopulateActionGroups ()
        {
            normal_actions.Add (new ActionEntry [] {
                new ActionEntry ("GameMenu", null, "_Game", null, null, null),
                new ActionEntry ("New", Stock.New, "_New",
                    "<control>N", null, null),
                new ActionEntry ("Open", Stock.Open, "_Open",
                    "<Control>O", null, null),
                new ActionEntry ("Quit", Stock.Quit, "_Quit",
                    "<control>Q", null, delegate { Game.Instance.Quit (); }),

                new ActionEntry ("EditMenu", null, "_Edit", null, null, null),
                new ActionEntry ("Preferences", Stock.Preferences,
                    "_Preferences", null, null, delegate {
                        new PreferenceDialog ();
                    }),

                new ActionEntry ("ViewMenu", null, "_View", null, null, null),
                new ActionEntry ("ZoomIn", Stock.ZoomIn, "Zoom _In",
                    Accelerator.Name ((uint) Key.plus, ModifierType.ControlMask),
                    null, null),
                new ActionEntry ("ZoomOut", Stock.ZoomOut, "Zoom _Out",
                    Accelerator.Name ((uint) Key.minus, ModifierType.ControlMask),
                    null, null),
                new ActionEntry ("NormalSize", Stock.Zoom100, "_Normal Size",
                    "<control>0", null, null),
                new ActionEntry ("CharacterInformation", null, "_Character Information",
                    "<control>c", null, delegate {
                        new CharacterDialog ();
                    }),

                new ActionEntry ("HelpMenu", null, "_Help", null, null, null),
                new ActionEntry ("About", Stock.About, "_About",
                    null, null, delegate {
                        Dialog dialog = new AboutDialog ();
                        dialog.Run ();
                        dialog.Destroy ();
                    }),
            });

            toggle_actions.Add (new ToggleActionEntry []
            {
                new ToggleActionEntry ("ShowGridLines", null, "Show _Gridlines",
                    null, null, null, false),
                new ToggleActionEntry ("ShowMessages", null, "Show _Messages",
                    "<control>m", null, null, true),
                new ToggleActionEntry ("FullScreen", null, "Full Screen",
                    "F11", null, null, false)
            });

            map_actions.Add (new ActionEntry []
            {
                new ActionEntry ("MoveHere", null, "_Move Here",
                    null, null, null),
                new ActionEntry ("Target", null, "_Target",
                    null, null, null),
                new ActionEntry ("Examine", null, "_Examine",
                    null, null, null)
            });

            hero_actions.Add (new ActionEntry []
            {
                new ActionEntry ("MoveNorth", null, "Move _North",
                    null, null, null),
                new ActionEntry ("MoveSouth", null, "Move _South",
                    null, null, null),
                new ActionEntry ("MoveWest", null, "Move _West",
                    null, null, null),
                new ActionEntry ("MoveEast", null, "Move _East",
                    null, null, null),
                new ActionEntry ("MoveNorthEast", null, "Move North East",
                    null, null, null),
                new ActionEntry ("MoveSouthEast", null, "Move South East",
                    null, null, null),
                new ActionEntry ("MoveNorthWest", null, "Move North West",
                    null, null, null),
                new ActionEntry ("MoveSouthWest", null, "Move South West",
                    null, null, null),

                new ActionEntry ("DoNothing", null, "Do Nothing",
                    null, null, null),

                new ActionEntry ("PickUp", null, "Pick Up Item",
                    null, null, null)
            });

            ui.InsertActionGroup (normal_actions, 0);
            ui.InsertActionGroup (toggle_actions, 0);
            ui.InsertActionGroup (map_actions, 0);
            ui.InsertActionGroup (hero_actions, 0);
        }

        public UIManager UI
        {
            get { return ui; }
        }

        public Action this[string action_name]
        {
            get { return GetAction (action_name); }
        }

#if false
        public ActionGroup GetActionGroup (string group_name)
        {
            foreach (ActionGroup group in ui.ActionGroups)
                if (group.Name == group_name)
                    return group;

            return null;
        }
#endif

        public Widget GetWidget (string widget_path)
        {
            return ui.GetWidget (widget_path);
        }

        public Action GetAction (string action_name)
        {
            foreach (ActionGroup group in ui.ActionGroups)
                foreach (Action action in group.ListActions ())
                    if (action.Name == action_name)
                        return action;

            return null;
        }

        public MenuBar MenuBar
        {
            get { return GetWidget ("/MenuBar") as MenuBar; }
        }

        public Menu WorldViewContextMenu
        {
            get { return GetWidget ("/WorldViewContextMenu") as Menu; }
        }
    }
}
