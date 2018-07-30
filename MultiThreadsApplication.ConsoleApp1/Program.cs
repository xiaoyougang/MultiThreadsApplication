using System;
using System.Threading;
using static System.Console;

namespace MultiThreadsApplication.ConsoleApp1
{
    class Program
    {
        /*
         * 演示如何创建一个线程。
         * 
         * 示例中创建了一个线程来运行PrintNumbers方法。当我们构造一个线程时，ThreadStart或ParameterizedThreadStart
         * 的实例委托会传给构造函数。我们只需指定在不同线程运行的方法名，而C#编译器则在后台创建这些对象。然后我们在主线程中
         * 以通常的方式启动了一个线程来运行PrintNumbers方法。
         * 
         */
        static void Main(string[] args)
        {
            Thread t = new Thread(PrintNumbers);
            t.Start();
            PrintNumbers();
        }

        static void PrintNumbers()
        {
            WriteLine("Starting...");
            for (int i = 0; i < 10; i++)
            {
                WriteLine("Current Thread:" +Thread.CurrentThread.ManagedThreadId +"   "+i);
                Thread.Sleep(1);
            }
        }
    }
}
