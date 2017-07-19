using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    partial class DerivedClass
    {
        public void partialClassMethod()
        {
            Console.WriteLine("Partial class Method");
        }

        partial void partialMethod(int test);
    }

    
}
