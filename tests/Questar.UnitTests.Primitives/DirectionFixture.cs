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
        public void CheckDirectionNames ()
        {
            Assert.That (Direction.None.Name,      Is.EqualTo ("none"));
            Assert.That (Direction.North.Name,     Is.EqualTo ("north"));
            Assert.That (Direction.NorthEast.Name, Is.EqualTo ("northeast"));
            Assert.That (Direction.East.Name,      Is.EqualTo ("east"));
            Assert.That (Direction.SouthEast.Name, Is.EqualTo ("southeast"));
            Assert.That (Direction.South.Name,     Is.EqualTo ("south"));
            Assert.That (Direction.SouthWest.Name, Is.EqualTo ("southwest"));
            Assert.That (Direction.West.Name,      Is.EqualTo ("west"));
            Assert.That (Direction.NorthWest.Name, Is.EqualTo ("northwest"));
        }

        [Test]
        public void CheckDirectionDeltas ()
        {
            Assert.That (Direction.None.DeltaX, Is.EqualTo (0));
            Assert.That (Direction.None.DeltaY, Is.EqualTo (0));

            Assert.That (Direction.North.DeltaX, Is.EqualTo (0));
            Assert.That (Direction.North.DeltaY, Is.EqualTo (-1));

            Assert.That (Direction.NorthEast.DeltaX, Is.EqualTo (1));
            Assert.That (Direction.NorthEast.DeltaY, Is.EqualTo (-1));

            Assert.That (Direction.East.DeltaX, Is.EqualTo (1));
            Assert.That (Direction.East.DeltaY, Is.EqualTo (0));

            Assert.That (Direction.SouthEast.DeltaX, Is.EqualTo (1));
            Assert.That (Direction.SouthEast.DeltaY, Is.EqualTo (1));

            Assert.That (Direction.South.DeltaX, Is.EqualTo (0));
            Assert.That (Direction.South.DeltaY, Is.EqualTo (1));

            Assert.That (Direction.SouthWest.DeltaX, Is.EqualTo (-1));
            Assert.That (Direction.SouthWest.DeltaY, Is.EqualTo (1));

            Assert.That (Direction.West.DeltaX, Is.EqualTo (-1));
            Assert.That (Direction.West.DeltaY, Is.EqualTo (0));

            Assert.That (Direction.NorthWest.DeltaX, Is.EqualTo (-1));
            Assert.That (Direction.NorthWest.DeltaY, Is.EqualTo (-1));
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

