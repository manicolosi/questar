using Gtk;
using System;

namespace Questar.Base
{
    public class Messages
    {
        private TextBuffer buffer;
        private string newline = "";

        private static Messages instance;

        private Messages ()
        {
            buffer = new TextBuffer (null);
        }

        public static Messages Instance
        {
            get {
                if (instance == null)
                    instance = new Messages ();

                return instance;
            }
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

