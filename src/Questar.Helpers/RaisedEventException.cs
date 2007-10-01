/*******************************************************************************
 *  RaisedEventException.cs: This Exception is thrown when an event
 *  handler throws an exception.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Text;

namespace Questar.Helpers
{
    public class RaisedEventException : ApplicationException
    {
        string message;

        public RaisedEventException (Delegate handler, object sender,
            EventArgs args, Exception inner_ex) : base (null, inner_ex)
        {
            StringBuilder sb = new StringBuilder ();

            sb.AppendLine ("An Exception was thrown by an event handler method.");
            sb.AppendFormat ("Handler: {0}\n", handler);
            sb.AppendFormat ("Sender: {0}\n", sender);

            if (args != null)
                sb.AppendFormat ("EventArgs: {0}\n", args);

            message = sb.ToString ();
        }

        public RaisedEventException (Delegate handler, object sender,
            Exception inner_ex) : this (handler, sender, null, inner_ex)
        {
        }

        public override string Message
        {
            get { return message; }
        }
    }
}

