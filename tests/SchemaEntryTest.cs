//
// SchemaEntryTest.cs: Tests for SchemaEntry<T> class.
// Author: Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
// Copyright (C) 2007
//

using NUnit.Framework;
using System;

using Questar.Configuration;

namespace Questar.UnitTests
{
    [TestFixture]
    public class SchemaEntryTest
    {
        SchemaEntry<int> test_entry;

        [SetUp]
        public void Setup ()
        {
            test_entry = new SchemaEntry<int> ("", "test", 25,
                "Test Value", "Used for Questar unit tests.");
        }

        [Test]
        public void Properties ()
        {
            StringAssert.IsMatch ("", test_entry.Namespace);
            StringAssert.IsMatch ("test", test_entry.Key);
            Assert.AreEqual (25, test_entry.DefaultValue);
            StringAssert.IsMatch ("Test Value", test_entry.ShortDescription);
            StringAssert.IsMatch ("Used for Questar unit tests.",
                test_entry.LongDescription);
        }

        [Test]
        public void SetAndGetValue ()
        {
            test_entry.Value = 50;
            Assert.AreEqual (50, test_entry.Value);
        }

        // This test requires using a glib main loop, so GConf can
        // notify us the key's value changed.
        [Test]
        public void ChangedEvent ()
        {
            GLib.MainLoop loop = new GLib.MainLoop ();

            bool changed = false;
            test_entry.Changed += delegate {
                changed = true;
                loop.Quit ();
            };
            test_entry.Value = 100;
            loop.Run ();

            Assert.IsTrue (changed);
        }
    }
}

