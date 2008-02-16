/*******************************************************************************
 *  CheckExtensions.cs: Extension methods that can be used for pre and
 *  post conditions. The methods throw exceptions when they fail.
 *
 *  Copyright (C) 2007, 2008
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Questar.Extensions
{
    public static class CheckExtensions
    {
        public static void CheckInRange<T> (this T self, string param_name,
            T min, T max) where T: IComparable
        {
            if (self.CompareTo (min) < 0 || self.CompareTo (max) > 0)
                throw new ArgumentException (param_name);
        }

        public static void AssertIsTrue (this bool self)
        {
            if (self != true)
                throw new ArgumentException ();
        }

        public static void AssertIsFalse (this bool self)
        {
            if (self != false)
                throw new ArgumentException ();
        }

        // FIXME: This should be contrained to reference types.
        public static void AssertNotNull<T> (this T self, string param_name)
        {
            if (self == null)
                throw new ArgumentNullException (param_name);
        }

        public static void AssertNotNull<T> (this T self)
        {
            AssertNotNull (self, null);
        }

        public static void AssertContains<T> (this IEnumerable<T> self, T item)
        {
            IList<T> list = self as IList<T>;
            if (list == null)
                throw new NotImplementedException (
                    "AssertContains() does not work on non-IList<T> types");

            AssertIsTrue (list.Contains (item));
        }

        public static void AssertDoesNotContain<T> (this IEnumerable<T> self,
            T item)
        {
            IList<T> list = self as IList<T>;
            if (list == null)
                throw new NotImplementedException (
                    "AssertDoesNotContain() does not work on non-IList<T> types");

            AssertIsFalse (list.Contains (item));
        }

        public static void AssertEqualTo<T> (this T self, T other)
        {
            AssertIsTrue (self.Equals (other));
        }
    }
}

