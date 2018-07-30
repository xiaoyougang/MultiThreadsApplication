using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp4
{
    /*
     * 演示如何终止线程的执行。
     * 
     *   当主程序和单独的数字打印线程运行时，我们等待6秒后对线程调用了t.Abort方法。这给线程注入了ThreadAbortException
     * 方法，导致线程被终结。这非常危险，因为该异常可以在任何时刻发生并可能彻底摧毁应用程序。另外，使用该技术不一定总能终止
     * 线程。目标线程可以通过处理该异常并调用Thread.ResetAbort方法来拒绝被终止。
     *   因此并不推荐使用Abort方法来终止线程。可优先使用一些其他方法，比如提供一个CancellationToken方法来取消线程的执行。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            WriteLine("A thread has been aborted.");
            t = new Thread(PrintNumbers);
            t.Start();
            PrintNumbers();
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

        static void PrintNumbers()
        {
            WriteLine("Strating ...");
            for (int i = 0; i < 10; i++)
            {
                WriteLine(i);
            }
        }
    }
}
