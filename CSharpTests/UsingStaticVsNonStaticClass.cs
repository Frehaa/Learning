using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CSharpTests.StaticClass;
using static CSharpTests.NonStaticClass;

namespace CSharpTests
{
    [TestClass]
    public class UsingStaticVsNonStaticClass
    {
        [TestMethod]
        public void UsingStaticClassMethod()
        {
            Assert.AreEqual(true, StaticClassStaticMethod());
        }

        [TestMethod]
        public void UsingNonStaticClassMethod()
        {
            Assert.AreEqual(true, NonStaticClassStaticMethod());
        }
    }

    static class StaticClass
    {
        public static bool StaticClassStaticMethod()
        {
            return true;
        }
    }

    class NonStaticClass
    {
        public static bool NonStaticClassStaticMethod()
        {
            return true;
        }
    }
}
