using Learning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTests
{
    [TestClass]
    class InheritanceHidding
    {
        /* Even though the class is the same. What method is invoked is different*/
        [TestMethod]
        public void GetNonVirtualString_WhenCalledFromReferenceToBaseClass_ReturnsBaseClassString()
        {
            BaseClass myClass = new DerivedClass();
            DerivedClass derivedClass = (DerivedClass)myClass;

            Assert.AreEqual(expected: "String from base", actual: myClass.GetNonVirtualString());
        }

        [TestMethod]
        public void GetNonVirtualString_WhenCalledFromReferenceToDerivedClass_ReturnsDerivedClassString()
        {
            BaseClass myClass = new DerivedClass();
            DerivedClass derivedClass = (DerivedClass)myClass;
            
            Assert.AreEqual(expected: "String from derived", actual: derivedClass.GetNonVirtualString());
        }
    }
}
