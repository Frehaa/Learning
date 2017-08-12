using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Learning;

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

    }
}