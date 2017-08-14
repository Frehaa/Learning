using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class DelegateClass
    {
        private delegate void MyDelegate();

        // You can field initialize a delegate if the method is static
        private MyDelegate myDelegateReference = MyStaticDelegateMethod;

        // You can't field initialize if the method is non-static
        //private MyDelegate myDelegateReference = MyNonStaticDelegateMethod;


        public DelegateClass()
        {
            // In the constructor you can initialize both static and non-static
            myDelegateReference = MyNonStaticDelegateMethod;
            myDelegateReference = MyStaticDelegateMethod;
        }


        public void MyNonStaticDelegateMethod()
        {

        }
        
        public static void MyStaticDelegateMethod()
        {

        }
    }
}
