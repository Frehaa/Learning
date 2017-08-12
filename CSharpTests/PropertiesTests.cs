using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class PropertiesTest
    {
        [TestMethod]
        public void PropertyDefaultIsTrue()
        {
            Properties prop = new Properties();

            Assert.AreEqual(true, prop.Test);
        }

        [TestMethod]
        public void PropertyValueChangeWhenSetFalse()
        {
            Properties prop = new Properties();
            prop.Test = false;

            Assert.AreEqual(false, prop.Test);
        }

        public class Properties
        {
            public bool Test { get; set; }
            public int Test2 { get; set; }
            public int Test3 { get; private set; }
            public int Test4 { private get; set; }
            //private int TestCant1 { protected get; set; }
            //public int TestCant2 { protected get; protected set; }
            //public bool CompilesButCrashes { get => TestCant3; set => TestCant3 = value; }
        }
    }
}