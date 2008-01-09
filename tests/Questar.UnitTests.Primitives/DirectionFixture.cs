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
        public void AllDoesNotIncludeNone ()
        {
            Assert.That (Direction.All, Has.No.Member (Direction.None));
        }

        [Test]
        public void NineDirectionsInAllIncludingNone ()
        {
            Assert.That (Direction.AllIncludingNone.Count (), Is.EqualTo (9));
        }

        [Test]
        public void AllIncludingNoneDoesIncludeNone ()
        {
            Assert.That (Direction.AllIncludingNone, Has.Member (Direction.None));
        }

        [Test]
        public void GetRandomIsReallyRandom ()
        {
            Direction [] random_dirs = {
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

        [Test]
        public void GetWithDeltasReturnsCorrectDirection ()
        {
            Assert.That (Direction.GetWithDeltas (0, 0),   Is.EqualTo (Direction.None));
            Assert.That (Direction.GetWithDeltas (0, -1),  Is.EqualTo (Direction.North));
            Assert.That (Direction.GetWithDeltas (1, -1),  Is.EqualTo (Direction.NorthEast));
            Assert.That (Direction.GetWithDeltas (1, 0),   Is.EqualTo (Direction.East));
            Assert.That (Direction.GetWithDeltas (1, 1),   Is.EqualTo (Direction.SouthEast));
            Assert.That (Direction.GetWithDeltas (0, 1),   Is.EqualTo (Direction.South));
            Assert.That (Direction.GetWithDeltas (-1, 1),  Is.EqualTo (Direction.SouthWest));
            Assert.That (Direction.GetWithDeltas (-1, 0),  Is.EqualTo (Direction.West));
            Assert.That (Direction.GetWithDeltas (-1, -1), Is.EqualTo (Direction.NorthWest));
        }
    }
}

