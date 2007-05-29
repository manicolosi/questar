using Gtk;

using Questar.Base;

namespace Questar.Gui
{
    public class AboutDialog : Gtk.AboutDialog
    {
        public AboutDialog ()
        {
            base.Name = ProgramInformation.Name;
            base.Version = ProgramInformation.Version;
            base.Copyright = "Copyright \x00a9 2006-2007 Mark A. Nicolosi";
            base.Comments = "A roguelike game for the Gnome Desktop.";

            //base.License = ProgramInformation.License;
            //base.WrapLicense = true;

            base.Authors = ProgramInformation.Authors;
            base.Artists = ProgramInformation.Artists;
        }
    }
}

