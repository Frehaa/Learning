using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class BaseClass
    {
        public int baseInt = DerivedInit();
    
        private static int DerivedInit()
        {
            Console.WriteLine("BaseInit");
            return 1;
        }

        public BaseClass()
        {
            Console.WriteLine("BaseClass");
        }

        public static BaseClass operator+(BaseClass lh, BaseClass rh)
        {
            BaseClass retuener = new BaseClass();
            retuener.baseInt = lh.baseInt + rh.baseInt;
            return retuener;
        }
    }
}
