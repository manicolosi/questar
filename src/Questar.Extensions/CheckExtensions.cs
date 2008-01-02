/*******************************************************************************
 *  CheckExtensions.cs: Extension methods that can be used for pre and
 *  post conditions. The methods throw exceptions when they fail.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;

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
    }
}

