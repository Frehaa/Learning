#define Test

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class Diagnostics
    {
        [TestMethod]
        public void CompilationSymbolsPresent()
        {
#if !Test
            Assert.Fail("Compilation Symbol not working")
#endif
        }

        [TestMethod]
        public void CompilationSymbolsNotPresent()
        {
#if NotTest
            Assert.Fail("Compilation Symbol not working");
#else
#endif
        }

        [TestMethod]
        public void ConditionalMethodWithSymbol()
        {
            bool test = false;

            ConditionalTrueWhenTest(ref test);

            Assert.IsTrue(test);
        }

        [System.Diagnostics.Conditional("Test")]
        private static void ConditionalTrueWhenTest(ref bool input)
        {
            input = true;
        }

        [TestMethod]
        public void ConditionalMethodWithoutSymbol()
        {
            bool test = false;

            ConditionalTrueWhenNotTest(ref test);

            Assert.IsFalse(test);
        }

        [System.Diagnostics.Conditional("NotTest")]
        private void ConditionalTrueWhenNotTest(ref bool input)
        {
            input = true;
        }
    }


}