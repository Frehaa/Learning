using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Learning;

namespace UnitTestProject
{
    [TestClass]
    public class ClassOperators
    {
        [TestMethod]
        public void AdditionCorrect()
        {
            BaseClass one = new BaseClass();
            BaseClass two = new BaseClass();

            Assert.AreEqual(2, (one + two).baseInt);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AdditionNull()
        {
            BaseClass one = new BaseClass();
            BaseClass two = null;

            BaseClass result = one + two;
        }
    }
}
