using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class EnumeratorStuff
    {
        [TestMethod]
        public void MoveNextEnumeratorSumAfterTenSteps()
        {
            int sum = 0;
            IEnumerator<int> enumerator = Increment().GetEnumerator();
            for (int i = 0; i <= 10; ++i)
            {
                enumerator.MoveNext();
                sum += enumerator.Current;
            }

            // Returns sum of 1 through 10
            Assert.AreEqual(55, sum);
        }

        [TestMethod]
        public void CurrentBeforeAnyMoveNext()
        {
            IEnumerator<int> enumerator = Increment().GetEnumerator();

            // Returns default value
            Assert.AreEqual(0, enumerator.Current);
        }

        [TestMethod]
        public void CurrentAfterSingleMoveNext()
        {
            IEnumerator<int> enumerator = Increment().GetEnumerator();
            enumerator.MoveNext();

            // Returns default value
            Assert.AreEqual(0, enumerator.Current);
        }

        [TestMethod]
        public void UsingInfiniteIEnumerable()
        {
            IEnumerator<int> e =  Infinite().GetEnumerator();

            for (int i = 0; i < 10; ++i)
            {
                e.MoveNext();
            }

            Assert.AreEqual(2917, e.Current);
        }

        [TestMethod]
        public void UseAfterDisposeMakesNoDifference()
        {
            IEnumerator<int> e = Infinite().GetEnumerator();

            e.Dispose();

            e.MoveNext();

            Assert.AreEqual(1, e.Current);
        }

        public static IEnumerable<int> Increment()
        {
            int i = 0;
            while (i >= 0)
            {
                yield return i;
                ++i;
            }
        }

        public static IEnumerable<int> Infinite()
        {
            int i = 1;
            while (true)
            {
                yield return i;

                i = 2 * i + i / 2;
            }
        }
    }
}
