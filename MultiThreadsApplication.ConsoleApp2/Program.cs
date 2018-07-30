using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp2
{
    /*
     * 演示如何让一个线程休眠一段时间而不用消耗操作系统资源。
     * 
     *   当程序运行时，会创建一个线程，该线程会执行PrintNumbersWithDelay方法中的代码。然后会立即执行PrintNumbers方法。
     * 关键之处在于在PrintNumbersWithDelay方法中加入了Thread.Sleep方法调用。这将是导致线程执行该代码时，在打印任何数字
     * 之前会等待指定的时间。当线程处于休眠状态时，它会占用尽可能少的CPU时间。结果我们会发现通常后运行的PrintNumbers方法中
     * 的代码会比独立线程中的PrintNumbersWithDelay方法中的代码先执行。
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            PrintNumbers();
        }

        static void PrintNumbers()
        {
            WriteLine("Strating ...");
            for (int i = 0; i < 10; i++)
            {
                WriteLine(i);
            }
        }

        static void PrintNumbersWithDelay()
        {
            WriteLine("Strating ...");
            for (int i = 0; i < 10; i++)
            {
                Sleep(TimeSpan.FromSeconds(2));
                WriteLine(i);
            }

        }
    }
}
