/*******************************************************************************
 *  EventHelper.cs: Static utility methods for string objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

namespace Questar.Helpers
{
    public static class StringHelper
    {
        public static string SentenceCapitalize (object obj)
        {
            string str = obj.ToString ();
            string first_letter = str.Substring (0, 1).ToUpper ();
            return str.Remove (0, 1).Insert (0, first_letter);
        }
    }
}

