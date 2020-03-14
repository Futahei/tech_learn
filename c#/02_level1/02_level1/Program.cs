using System;
using System.Threading;

namespace _02_level1
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer;
            ConsoleKeyInfo keyInfo;
            bool isStop = false;

            TimerCallback callback = state =>
            {
                if (!isStop)
                {
                    Console.Clear();
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"));
                }
            };

            timer = new Timer(callback, null, 500, 1000);

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.S)
                {
                    isStop = !isStop;
                }

            }
            while (keyInfo.Key != ConsoleKey.Enter);
        }
    }
}
