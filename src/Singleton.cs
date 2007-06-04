/*******************************************************************************
 *  Singleton.cs: Generic class to support the Singleton pattern.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Reflection;

namespace Questar.Base
{
    public static class Singleton<T> where T: class
    {
        private static T instance;

        private static Singleton ()
        {
            CreateInstance ();
        }

        public static T Instance
        {
            get { return instance; }
        }

        public static void RecreateForTesting ()
        {
            CreateInstance ();
        }

        private static void CreateInstance ()
        {
            Type type = typeof (T);
            instance = type.InvokeMember (type.Name,
                BindingFlags.CreateInstance | BindingFlags.Instance |
                BindingFlags.NonPublic, null, null, null) as T;
        }
    }
}

