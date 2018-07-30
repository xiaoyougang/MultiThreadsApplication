using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp3
{
    /*
     * 演示如何让程序等待一个线程中的计算完成，然后在代码中使用该线程的计算结果。
     * 
     *   当程序运行时，启动了一个耗时很长的线程来打印数字，打印每个数字前要等待两秒。但我们在主程序中调用了t.Join方法，
     * 该方法允许我们等待线程t完成。当线程t完成时，主程序会继续运行。借助该技术可以实现在两个线程间同步执行步骤。第一个线
     * 程等待另一个线程完成后再继续执行。第一个线程等待时是处于阻塞状态（如同调用Thread.Sleep方法一样）。
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Starting...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            t.Join();
            WriteLine("Thread completed");
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
