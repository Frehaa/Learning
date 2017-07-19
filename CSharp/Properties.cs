using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class Properties
    {
        public bool Test { get => Test; set => Test = value; }
        public int Test2 { get; set; }
        public int Test3 { get; private set; }
        public int Test4 { private get; set; }
        //private int TestCant1 { protected get; set; }
        //public int TestCant2 { protected get; protected set; }
        

    }
}
