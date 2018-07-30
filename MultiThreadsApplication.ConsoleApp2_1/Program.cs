using System;
using System.Threading;
using static System.Console;

namespace MultiThreadsApplication.ConsoleApp2_1
{
    /*
     * 演示基本的原子操作。
     * 
     *   当程序运行时，会创建三个线程来运行TestCounter方法中的代码。该方法对一个对象按序执行了递增或递减操作。起初的Counter对象不是
     * 线程安全的，会遇到竞争条件。所以第一个计数器的结果是不确定的，会得到一些不正确的非零结果。
     *   在Chapter1中我们通过锁对象解决了这个问题。在一个线程获取旧的计数器值并计算后赋新值之前，其他线程都被阻塞了。
     *   然而，如果我们采用这种方式执行操作，中途不能停止。而借助Interlocked类我们无需锁定任何对象就可以获得正确的结果。
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("不正确的 counter:");
            var c = new Counter();
            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            WriteLine($"计算出的 Total count: {c.Count}");

            WriteLine("--------------------------------------");
            WriteLine("正确的 counter:");
            var c1 = new CounterNoLock();
            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            WriteLine($"计算出的 Total count: {c1.Count}");
        }

        static void TestCounter(CounterBase counter)
        {
            for (int i = 0; i < 100000; i++)
            {
                counter.Increment();
                counter.Decrement();
            }
        }
    }
}
