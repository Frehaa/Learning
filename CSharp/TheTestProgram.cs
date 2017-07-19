using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class TheTestProgram
    {
        public static void Main()
        {
            Console.WriteLine("Console.WriteLine or cw for snippet");

            var c = new DerivedClass(); // Uses var keyword

            c.partialClassMethod();

            c = null;

            "Test".Show();
            
            Console.ReadKey();
        }
        
    }
}
