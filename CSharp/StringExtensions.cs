using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public static class StringExtensions
    {
        /* Extends the string class with a mathod that writes to the console. 
         * It can be used like this:    "Test".Show();
         */
        public static void Show(this string t)
        {
            Console.WriteLine(t);
        }
    }
}
