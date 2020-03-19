using System;
using st = System.Threading;

namespace _02_level2
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer();
            var ticker = new Ticker(timer);

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.S)
                {
                    ticker.StartOrStop();
                }

            }
            while (keyInfo.Key != ConsoleKey.Enter);
        }

        class Ticker
        {
            IObserver observer;

            st::Timer timer;
            bool isStop = false;

            public Ticker(IObserver observer)
            {
                this.observer = observer;

                st::TimerCallback callback = state =>
                {
                    if (!isStop)
                    {
                        observer.update(GetTime());
                    }
                };

                timer = new st::Timer(callback, null, 500, 1000);
            }

            public void StartOrStop()
            {
                isStop = !isStop;
            }
            
            string GetTime()
            {
                return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
            }
        }

        interface IObserver
        {
            void update(string message);
        }

        class Timer : IObserver
        {

            public void update(string time)
            {
                Console.Clear();
                Console.WriteLine(time);
            }
        }
    }
}
