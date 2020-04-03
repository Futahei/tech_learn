using System;
using System.Collections.Generic;
using st = System.Threading;

namespace _02_level3
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer(new ConsoleView());
            var ticker = new Ticker();
            ticker.Subscribe(timer);

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
            private readonly List<IObserver> observers = new List<IObserver>();
            bool isStop = false;

            public Ticker()
            {
                st::TimerCallback callback = state =>
                {
                    if (!isStop)
                    {
                        Notify();
                    }
                };

                var timer = new st::Timer(callback, null, 500, 1000);
            }

            public void Subscribe(IObserver observer)
            {
                observers.Add(observer);
            }

            void Notify()
            {
                observers.ForEach(o => o.update(GetTime()));
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
            IView view;

            public Timer(IView view)
            {
                this.view = view;
            }

            public void update(string time)
            {
                view.display(time);
            }
        }

        interface IView
        {
            void display(string message);
        }

        class ConsoleView : IView
        {
            public void display(string message)
            {
                Console.Clear();
                Console.WriteLine(message);
            }
        }
    }
}
