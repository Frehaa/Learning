using NUnit.Framework;
using TestProject;

namespace UnitTestProject
{

    [TestFixture]
    public class PropertiesTest
    {
        [Test]
        public void PropertyDefaultIsTrue()
        {
            Properties prop = new Properties();

            Assert.AreEqual(true, prop.Test);
        }

        [Test]
        public void PropertyValueChangeWhenSetFalse()
        {
            Properties prop = new Properties();
            prop.Test = false;

            Assert.AreEqual(false, prop.Test);
        }
    }
}
