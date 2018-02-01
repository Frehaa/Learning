using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CSharpCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stuff();
            //ProducerConsumer();
            //UsingAnActionBlock();
            //FileIO();
            StructPassing();

            Console.ReadKey();
        }

        private static void StructPassing()
        {
            var m = new MyStruct
            {
                Value = true,
                OtherValue = true
            };

            TakesStruct(m);

            m.Value = false;
            m.OtherValue = false;

            Console.WriteLine($"From outside: {m.Value} - {m.OtherValue}");
            Console.WriteLine(m.GetHashCode());

        }

        private async static void TakesStruct(MyStruct m)
        {
            await Task.Delay(300);
            await Task.Run(() =>
            {
                Console.WriteLine($"From inside: {m.Value} - {m.OtherValue}");
                Console.WriteLine(m.GetHashCode());
            });
        }

        struct MyStruct
        {
            public bool Value { get; set; }
            public bool OtherValue { get; set; }
        }

        private static void FileIO()
        {
            FileStream stream1 = new FileStream("learning.txt", FileMode.Open, FileAccess.Write, FileShare.None);
            //FileStream stream2 = new FileStream("learning.txt", FileMode.Open, FileAccess.Read);
        }

        private static void UsingAnActionBlock()
        {
            var proccessInput = new ActionBlock<string>(s =>
            {
                Console.WriteLine($"user inut: {s}");
            });

            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    proccessInput.Post(input);
                }
            }
        }

        private static void Stuff()
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

        private static BufferBlock<string> s_buffer = new BufferBlock<string>();

        static void ProducerConsumer()
        {
            Task t1 = Task.Run(() => Producer());
            Task t2 = Task.Run(() => ConsumerAsync());
            Task t3 = Task.Run(() => Producer2());
            Task.WaitAll(t1, t2);
            
        }

        private static void Producer2()
        {
            Random random = new Random();
            int counter = 0;
            while (true)
            {
                s_buffer.Post((counter++).ToString());
                Task.Delay(500).Wait();

                if (counter == 10)
                    counter = 0;
            }
        }

        private static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    s_buffer.Post(input);
                }
            }
        }

        private static void ConsumerAsync()
        {
            while (true)
            {
                string data = s_buffer.Receive();
                Console.WriteLine($"user input: {data}");
            }
        }

    }
}
