/*******************************************************************************
 *  TestSingleton.cs: Unit Test for Singleton<T> class.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using NUnit.Framework;

using Questar.Base;

namespace Questar.UnitTests
{
    [TestFixture]
    public class TestSingleton
    {
        private class MockSingleton
        {
            public static MockSingleton Instance
            {
                get { return Singleton<MockSingleton>.Instance; }
            }

            private MockSingleton ()
            {
            }
        }

        [Test]
        public void OneInstance ()
        {
            MockSingleton a = MockSingleton.Instance;
            MockSingleton b = MockSingleton.Instance;

            Assert.AreSame (a, b);
        }
    }
}

