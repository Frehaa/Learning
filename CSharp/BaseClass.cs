﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
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
            int value = GetValueFromDerivedField();
            Console.WriteLine("Value from GetValueFromDerivedField: " + value);
        }

        public static BaseClass operator+(BaseClass lh, BaseClass rh)
        {
            BaseClass result = new BaseClass
            {
                baseInt = lh.baseInt + rh.baseInt
            };
            return result;
        }

        public virtual int GetValueFromDerivedField()
        {
            Console.WriteLine("Value not from derived field");
            return 0;
        }

        public string GetNonVirtualString()
        {
            return "String from base";
        }
    }
}
