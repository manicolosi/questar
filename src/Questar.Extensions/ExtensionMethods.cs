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
#pragma warning disable 0436

    // I get warning that this conflicts with an imported type (part
    // of LINQ), but if a leave this definition out it can't find it.
    // Probably will be fixed soon.
    public delegate T Func<A0, T> (A0 arg0);

#pragma warning restore 0436

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

        public static IEnumerable<T> Where<T> (this IEnumerable<T> enumerable,
            Func<T, bool> filter)
        {
            foreach (T t in enumerable)
                if (filter (t))
                    yield return t;
        }

        public static IEnumerable<T> DefaultIfEmpty<T> (
            this IEnumerable<T> enumerable, T default_value)
        {
            if (enumerable.Count () == 0) {
                yield return default_value;
                yield break;
            }

            foreach (T t in enumerable)
                yield return t;
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

        public static T Clamp<T> (this T self, T low, T high)
            where T : IComparable<T>
        {
            if (self.CompareTo (high) > 0)
                return high;
            else if (self.CompareTo (low) < 0)
                return low;

            return self;
        }
    }
}

