using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTests
{
    [TestClass]
    public class SetTests
    {
        // What happens:
        // item 1 is inserted with hashcode 1 
        // item 1 can be found in set because item 1 has hashcode 1, and item 1 is equal to item 1
        // item 1 is changed
        // item 1 cannot be found in set because item 1 has hashcode 2 now
        // Item 2 is created so it has hashcode 1
        // Item 1 cannot be found in set because even though it was inserted with hashcode 1, it is not equal to the changed item 1

        // Conclusion: 
        // Item 1 can no longer be found in the set, 
        //      UNLESS in the unlikely case that it was changed to have a value which coincidenly also had hashcode 1
        [TestMethod]
        public void ChangingTheObjectAlreadyInsertedInSet()
        {
            ValueClass item1 = new ValueClass()
            {
                Value = 5
            };

            ISet<ValueClass> set = new HashSet<ValueClass>
            {
                item1
            };

            Assert.IsTrue(set.Contains(item1));

            item1.Value = 10;

            Assert.IsFalse(set.Contains(item1));

            ValueClass item2 = new ValueClass()
            {
                Value = 5
            };

            Assert.IsFalse(set.Contains(item2));
        }

        [TestMethod]
        public void TwoDifferentButIdenticalItemsAreTreatedAsSame()
        {

            ValueClass item1 = new ValueClass()
            {
                Value = 15
            };

            ValueClass item2 = new ValueClass()
            {
                Value = 15
            };

            ISet<ValueClass> set = new HashSet<ValueClass>()
            {
                item1
            };

            Assert.IsTrue(set.Contains(item2));
        }








        private class ValueClass
        {
            public int Value { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                ValueClass other = (ValueClass)obj;

                return Value == other.Value;
            }

            public override int GetHashCode()
            {
                return Value.GetHashCode();
            }
        }
    }
}
