/*******************************************************************************
 *  ExtensionMethods.cs: Various extension methods. Some of these are
 *  covered by Linq, but it's not working on Mono, so they'll be removed
 *  later.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;

namespace Questar.Extensions
{
    public static class ExtensionMethods
    {
        public static int Count<T> (this IEnumerable<T> enumerable)
        {
            int count = 0;

#pragma warning disable 0219

            foreach (T t in enumerable)
                count++;

#pragma warning restore 0219

            return count;
        }

        public static List<T> AsList<T> (this IEnumerable<T> enumerable)
        {
            return new List<T> (enumerable);
        }

        public static T First<T> (this IEnumerable<T> enumerable)
        {
            foreach (T t in enumerable)
                return t;

            return default (T);
        }

        private static Random random;

        public static T Random<T> (this IEnumerable<T> enumerable)
        {
            if (random == null)
                random = new Random ();

            return Random (enumerable, random);
        }

        public static T Random<T> (this IEnumerable<T> enumerable, Random random)
        {
            int i = random.Next (Count (enumerable));
            return AsList (enumerable)[i];
        }
    }
}

