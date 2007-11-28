/*******************************************************************************
 *  DirectionFixture.cs: Unit Tests for Direction objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;

using Questar.Extensions;
using Questar.Primitives;

namespace Questar.UnitTests.Primitives
{
    [TestFixture]
    public class DirectionFixture
    {
        [SetUp]
        public void SetUp ()
        {
        }

        [Test]
        public void EightDirectionsInAll ()
        {
            Assert.That (Direction.All.Count (), Is.EqualTo (8));
        }

        [Test]
        public void AllDirectionsDoesNotIncludeNone ()
        {
            Assert.That (Direction.All.AsList (), Has.No.Member (Direction.None));
        }
    }
}

