//
// Messages.cs: Description Goes Here
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
//

using Gtk;
using System;

namespace Questar.Base
{
    public class Messages
    {
        public static Messages Instance
        {
            get { return Singleton<Messages>.Instance; }
        }

        private TextBuffer buffer;
        private string newline = "";

        private Messages ()
        {
            buffer = new TextBuffer (null);
        }

        public void Add (string message)
        {
            TextIter end = Buffer.EndIter;

            Buffer.Insert (ref end, newline + message);
            newline = Environment.NewLine;
        }

        public TextBuffer Buffer
        {
            get { return buffer; }
        }
    }
}

