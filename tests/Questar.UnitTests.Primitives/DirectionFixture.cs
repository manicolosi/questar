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
        public void CheckDirectionDeltas ()
        {
            Assert.That (Direction.None.Dx, Is.EqualTo (0));
            Assert.That (Direction.None.Dy, Is.EqualTo (0));

            Assert.That (Direction.North.Dx, Is.EqualTo (0));
            Assert.That (Direction.North.Dy, Is.EqualTo (-1));

            Assert.That (Direction.NorthEast.Dx, Is.EqualTo (1));
            Assert.That (Direction.NorthEast.Dy, Is.EqualTo (-1));

            Assert.That (Direction.East.Dx, Is.EqualTo (1));
            Assert.That (Direction.East.Dy, Is.EqualTo (0));

            Assert.That (Direction.SouthEast.Dx, Is.EqualTo (1));
            Assert.That (Direction.SouthEast.Dy, Is.EqualTo (1));

            Assert.That (Direction.South.Dx, Is.EqualTo (0));
            Assert.That (Direction.South.Dy, Is.EqualTo (1));

            Assert.That (Direction.SouthWest.Dx, Is.EqualTo (-1));
            Assert.That (Direction.SouthWest.Dy, Is.EqualTo (1));

            Assert.That (Direction.West.Dx, Is.EqualTo (-1));
            Assert.That (Direction.West.Dy, Is.EqualTo (0));

            Assert.That (Direction.NorthWest.Dx, Is.EqualTo (-1));
            Assert.That (Direction.NorthWest.Dy, Is.EqualTo (-1));
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

        [Test]
        public void GetRandomIsReallyRandom ()
        {
            List<Direction> random_dirs = new List<Direction> {
                Direction.GetRandom (),
                Direction.GetRandom (),
                Direction.GetRandom (),
                Direction.GetRandom ()
            };

            Assert.That (random_dirs, Has.Some.Not.EqualTo (random_dirs.First ()));
        }

        [Test]
        public void GetRandomWithRandomSeed ()
        {
            Direction dir = Direction.GetRandom (new Random (82488));
            Assert.That (Direction.West, Is.EqualTo (dir));
        }
    }
}

