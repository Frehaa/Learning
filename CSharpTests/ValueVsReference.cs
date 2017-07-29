using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpTests
{
    [TestClass]
    public class ValueVsReference
    {
        [TestMethod]
        public void ModifyingClassThroughMethodChangesValue()
        {
            MyClass myClass = new MyClass(10);
            ModifyMyClassTo(myClass, 1000);
            Assert.AreEqual(1000, myClass.Value);
        }

        [TestMethod]
        public void ModifyMethodReferenceOfMyClassNothingHappens()
        {
            MyClass myClass = new MyClass(4);
            SetReferenceToMyClassToNull_NothingHappensToMyClass(myClass);

            Assert.IsNotNull(myClass);
        }

        [TestMethod]
        public void ModifyingStructThroughMethodDoesntChangeValue()
        {
            MyStruct myStruct = new MyStruct(20);
            TryToModifyMyStructTo(myStruct, 0);
            Assert.AreEqual(20, myStruct.Value);
        }


        private static void ModifyMyClassTo(MyClass myClass, int value)
        {
            myClass.Value = value;
        }

        private static void SetReferenceToMyClassToNull_NothingHappensToMyClass(MyClass myClass)
        {
            myClass = null;
        }

        private static void TryToModifyMyStructTo(MyStruct myStruct, int value)
        {
            myStruct.Value = value;
        }

        private class MyClass
        {
            public int Value { get; set; }

            public MyClass(int i)
            {
                Value = i;
            }
        }

        private struct MyStruct
        {
            public int Value { get; set; }

            public MyStruct(int i)
            {
                Value = i;
            }
        }
    }
}
