using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpTests
{
    [TestClass]
    public class TypesStuff
    {
        [TestMethod]
        public void IntComparedToUint()
        {
            int left = 1;
            uint right = 1;
            Assert.IsTrue(left == right);
            Assert.IsFalse(left.Equals(right));
        }
    }
}
