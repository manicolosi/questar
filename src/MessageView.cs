using Gtk;
using System;

using Questar.Base;

namespace Questar.Gui
{
    public class MessageView : ScrolledWindow
    {
        public TextView view;

        public MessageView ()
        {
            view = new TextView ();
            view.Editable = false;
            view.LeftMargin = 4;
            view.RightMargin = 4;
            view.CanFocus = false;

            view.Buffer.InsertText += delegate {
                ScrollToBottom ();
            };

            base.Add (view);

            view.Buffer = Messages.Instance.Buffer;
        }

        private void ScrollToBottom ()
        {
            base.Vadjustment.Value = base.Vadjustment.Upper;
            base.Vadjustment.ChangeValue ();
        }
    }
}

