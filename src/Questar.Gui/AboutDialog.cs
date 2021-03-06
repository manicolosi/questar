/*******************************************************************************
 *  AboutDialog.cs: Provides a dialog that allows the user to view
 *  information about Questar and the people that helped.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Gtk;

using Questar.Base;

namespace Questar.Gui
{
    public class AboutDialog : Gtk.AboutDialog
    {
        public AboutDialog ()
        {
            base.LogoIconName = ThemeIcons.Application;
            base.ProgramName = ProgramInformation.Name;
            base.Version = ProgramInformation.Version;
            base.Copyright = "Copyright \x00a9 2006-2007 Mark A. Nicolosi";
            base.Comments = "A roguelike game for the Gnome Desktop.";

            //base.License = ProgramInformation.License;
            //base.WrapLicense = true;

            base.Authors = ProgramInformation.Authors;
            base.Artists = ProgramInformation.Artists;

            base.SkipTaskbarHint = true;
        }
    }
}

