using System;
using System.Threading;
using static System.Console;

namespace MultiThreadsApplication.ConsoleApp9
{
    /*
     * 演示如何使用lock关键字
     * 
     * Counter类不是线程安全的，会发生竞争条件（race condition）.
     * 为了保证不会发生这种情形，必须保证当有线程操作counter对象时，所有其他线程必须等待直到当前线程完成操作。我们可以使用
     * lock关键字来实现这种行为。如果锁定一个对象，需要访问该对象的所有其他线程则会处于阻塞状态，并等待直到该对象解除锁定。
     * 这可能会导致严重的性能问题。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Incorrect counter");
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
            WriteLine($"Total count: {c.Count}");
            WriteLine("--------------------------------");

            WriteLine("Correct counter");
            var c1 = new CounterWithLock();
            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            WriteLine($"Total count: {c1.Count}");
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



    abstract class CounterBase
    {
        public abstract void Increment();

        public abstract void Decrement();
    }

    class Counter : CounterBase
    {
        public int Count { get; private set; }
        public override void Increment()
        {
            Count++;
        }
        public override void Decrement()
        {
            Count--;
        }
    }

    class CounterWithLock : CounterBase
    {
        private readonly object _syncRoot = new object();
        public int Count { get; private set; }
        public override void Increment()
        {
            lock(_syncRoot)
            {
                Count++;
            }
        }
        public override void Decrement()
        {
            lock (_syncRoot)
            {
                Count--; 
            }
        }
    }
}
