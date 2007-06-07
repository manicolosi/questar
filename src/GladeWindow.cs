/*******************************************************************************
 *  GladeWindow.cs: Abstract base class for Questar's windows that use
 *  Glade.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Glade;
using Gtk;
using System;

namespace Questar.Gui
{
    public abstract class GladeWindow
    {
        private const string glade_resource = "questar.glade";

        Glade.XML glade;
        Window window;

        public GladeWindow (string name)
        {
            glade = new Glade.XML (glade_resource, name);
            glade.Autoconnect (this);

            window = glade[name] as Window;
        }

        public Window Window
        {
            get { return window; }
        }
    }
}

