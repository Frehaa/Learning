using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class Dictionaries
    {
        [TestMethod]
        public void HashtableReturnsNullWhenKeyNotInTable()
        {
            IDictionary table = new Hashtable
            {
                { "One", "Two" }
            };

            Assert.AreEqual(null, table["Not in table"]);
        }

        [TestMethod] [ExpectedException(typeof (KeyNotFoundException))]
        public void DictionaryThrowsKeyNotFoundWhenKeyNotInTable()
        {
            IDictionary<int, int> table = new Dictionary<int, int>
            {
                { 1, 2 }
            };
            
            int i = table[5];
        }


    }
}
