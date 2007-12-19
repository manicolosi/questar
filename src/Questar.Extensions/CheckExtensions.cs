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
        public static void CheckInRange<T> (this T self, string param, T min, T max)
            where T: IComparable
        {
            if (self.CompareTo (min) < 0 || self.CompareTo (max) > 0)
                throw new ArgumentException (param);
        }
    }
}

