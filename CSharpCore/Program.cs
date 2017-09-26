using System;
using System.Linq;

namespace CSharpCore
{
    class Program
    {
        static void Main(string[] args)
        {
            (string Name, int Year)[] test =
            {
                (Name: "Tim", Year: 2000),
                (Name: "Bob", Year: 2001),
                (Name: "Peter", Year: 2000),
                (Name: "Martin", Year: 2002),
                (Name: "Jim", Year: 2001),
                (Name: "Carl", Year: 2000),
            };

            var i = from t in test
                    group t by t.Year into g
                    select new
                    {
                        g.Key,
                        Names = from u in test
                                where u.Year == g.Key
                                select u.Name
                    };

            foreach (var item in i)
            {
                Console.WriteLine(item.Key);
                foreach (var name in item.Names)
                {
                    Console.WriteLine($"\t{name}");
                }
                Console.WriteLine();
            }
        }
    }
}
