/*******************************************************************************
 *  CheckExtensionsFixture.cs: Unit Tests for CheckExtensions extension
 *  methods.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Extensions;

namespace Questar.UnitTests.Extensions
{
    [TestFixture]
    public class CheckExtensionsFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void CheckInRangePasses ()
        {
            4.CheckInRange ("4", 1, 10);
            1.CheckInRange ("1", 1, 10);
            10.CheckInRange ("10", 1, 10);

            4.0.CheckInRange ("4.0", 1.0, 10.0);
            1.0.CheckInRange ("1.0", 1.0, 10.0);
            10.0.CheckInRange ("10.0", 1.0, 10.0);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckInRangeFailsWhenBelowMin ()
        {
            4.CheckInRange ("4", 5, 10);
        }

        [Test]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckInRangeFailsWhenOverMax ()
        {
            11.CheckInRange ("11", 5, 10);
        }

        [Test]
        public void AssertNotNullPassesWhenNotNull ()
        {
            "a string".AssertNotNull ();
        }

        [Test]
        public void AssertNotNullPassesWhenNotNullWithParamName ()
        {
            "a string".AssertNotNull ("value");
        }

        [Test]
        [ExpectedException (typeof (ArgumentNullException))]
        public void AssertNotNullFailsWhenNull ()
        {
            object test = null;
            test.AssertNotNull ();
        }
    }
}
