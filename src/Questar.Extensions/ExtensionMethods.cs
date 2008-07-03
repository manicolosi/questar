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
        public static void Each<T> (this IEnumerable<T> self, Action<T> action)
        {
            foreach (T item in self)
                action (item);
        }

        public static int Count<T> (this IEnumerable<T> source)
        {
            int count = 0;

#pragma warning disable 0219

            foreach (T item in source)
                count++;

#pragma warning restore 0219

            return count;
        }

        public static List<T> AsList<T> (this IEnumerable<T> source)
        {
            return new List<T> (source);
        }

        public static T First<T> (this IEnumerable<T> source)
        {
            foreach (T item in source)
                return item;

            return default (T);
        }

        public static bool Any<T> (this IEnumerable<T> source,
	    Func<T, bool> predicate)
        {
            source.AssertNotNull ("source");
            predicate.AssertNotNull ("predicate");

	    foreach (T item in source)
                if (predicate (item))
                    return true;

            return false;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            source.AssertNotNull ("source");
            selector.AssertNotNull ("selector");

            foreach (TSource item in source)
                yield return selector (item);
        }

        public static IEnumerable<T> Where<T> (this IEnumerable<T> source,
            Func<T, bool> predicate)
        {
            source.AssertNotNull ("source");
            predicate.AssertNotNull ("predicate");

            foreach (T item in source)
                if (predicate (item))
                    yield return item;
        }

        public static IEnumerable<T> DefaultIfEmpty<T> (
            this IEnumerable<T> source, T default_value)
        {
            source.AssertNotNull ("source");

            if (source.Count () == 0) {
                yield return default_value;
                yield break;
            }

            foreach (T item in source)
                yield return item;
        }

        private static Random random;

        public static T Random<T> (this IEnumerable<T> source)
        {
            if (random == null)
                random = new Random ();

            return Random (source, random);
        }

        public static T Random<T> (this IEnumerable<T> source, Random random)
        {
            int i = random.Next (Count (source));
            return AsList (source)[i];
        }

        public static T Clamp<T> (this T self, T low, T high)
            where T : IComparable<T>
        {
            if (self.CompareTo (high) > 0)
                return high;

            if (self.CompareTo (low) < 0)
                return low;

            return self;
        }
    }
}

