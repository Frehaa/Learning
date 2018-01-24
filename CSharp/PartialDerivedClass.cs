using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    partial class DerivedClass
    {
        public void PartialClassMethod()
        {
            Console.WriteLine("Partial class Method");
        }

        partial void PartialMethod(int test);
    }

    
}
