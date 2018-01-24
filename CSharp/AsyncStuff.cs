using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class AsyncStuff
    {
        public static Task<string> GetVAsync()
        {
            return Task.Run(() =>
            {
                Task.Delay(3000).Wait();
                return "V";
            });
        } 
    }
}
