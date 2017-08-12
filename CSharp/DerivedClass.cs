using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    public partial class DerivedClass : BaseClass, IDisposable
    {
        /* Field initializers go off first from derived and up the inheritence chain. 
         * The order is derived field initializers from top to bottom, the parents field initializers if it has any and thats ones parents and so on.
         * Next is the base class constructor, followed by the child constructor and so on.
         * 
         * This allows the base class to call virtual methods to give while still allowing the derived method to have some data.
        */ 
        private int derivedInt = DerivedInit();
        private int derivedBase;

        private static int DerivedInit()
        {
            Console.WriteLine("DerivedInit");
            
            return 2;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DerivedClass()
        {
            derivedBase = base.baseInt;
            Console.WriteLine("DerivedClass");
        }

        ~DerivedClass()
        {
            Console.WriteLine("DerivedDestructor");
        }
        

    }
}
