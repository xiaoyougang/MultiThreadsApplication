using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp5
{
    /*
     * 演示一个线程可能会有哪些状态。
     * 
     * 获取线程是否启动或是处于阻塞状态等信息是非常有用的。由于线程是独立运行的，所以其状态可以在任何时候被改变。
     * 线程状态： 未开始、运行中、阻塞中、已终止、已停止　
          Unstarted  Running  WaitSleepJoin   Aborted  Stopped
     * 
     *   当程序启动时定义了两个不同的线程。一个将被终止，另一个则会成功运行。线程状态位于Thread对象的ThreadState属性中。
     * ThreadState属性是一个C#枚举对象。刚开始线程状态是ThreadState.Unstarted。然后我们启动线程，并估计在一个周期为
     * 30次迭代的区间中，线程状态会从ThreadState.Running变为ThreadState.WaitSleepJoin。最后可以看到第二个线程t2
     * 成功完成并且状态为ThreadState.Stopped。另外还有一些其他的线程状态，这些要么已经被弃用，要么没有我们实验过的几种
     * 状态有用。
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithStatus);
            Thread t2 = new Thread(DoNothing);
            WriteLine(t.ThreadState.ToString());
            t2.Start();
            t.Start();
            for (int i = 0; i < 30; i++)
            {
                WriteLine(t.ThreadState.ToString() + (i+1));
            }
            Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            WriteLine("A thread has been aborted.");
            WriteLine(t.ThreadState.ToString());
            WriteLine(t2.ThreadState.ToString());
        }

        static void DoNothing()
        {
            Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            WriteLine("Strating ...");
            WriteLine(CurrentThread.ThreadState.ToString());
            for (int i = 0; i < 10; i++)
            {
                Sleep(TimeSpan.FromSeconds(2));
                WriteLine(i);
            }
        }
    }
}
