using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;
using static System.Diagnostics.Process;

namespace MultiThreadsApplication.ConsoleApp6
{
    /*
     * 演示线程优先级的几种不同的可能选项。线程优先级决定了该线程可占用多少CPU时间。
     * 
     *   当主程序启动时定义了两个不同的线程。第一个线程优先级为ThreadPriority.Highest,即具有最高优先级。第二个线程
     * 优先级为ThreadPriority.Lowest,即具有最低优先级。我们最先打印出主线程的优先级值，然后在所有可用的CPU核心上启
     * 动这两个线程。如果拥有一个以上的计算核心，将在两秒钟内得到初步结果。最高优先级的线程通常会计算出更多的迭代，但是
     * 两个值应该很接近。然而，如果有其他程序占用了所有的CPU核心运行负载，结果会截然不同。
     *   为了模拟该情形，我们设置了ProcessorAffinity选项，让操作系统将所有的线程运行在单个CPU核心（第一个核心）上，
     * 运行结果完全不同，并且计算耗时将超过2秒钟。这是因为CPU核心大部分时间在运行高优先级的线程，只留给其它线程很少的时
     * 间来运行。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine($"Current thread priority: {CurrentThread.Priority}");
            WriteLine("Running on all cores available");
            RunThreads();

            Sleep(TimeSpan.FromSeconds(2));
            WriteLine("Running on a single core");
            GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();
        }
        
        static void RunThreads()
        {
            var sample = new ThreadSample();
            var thread1 = new Thread(sample.CountNumbers);
            thread1.Name = "ThreadOne";
            thread1.Priority = ThreadPriority.Highest;
            var thread2 = new Thread(sample.CountNumbers);
            thread2.Name = "ThreadTwo";
            thread2.Priority = ThreadPriority.Lowest;

            thread1.Start();
            thread2.Start();
            Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();

        }
    }


    class ThreadSample
    {
        private bool _isStopped = false;
        public void Stop()
        {
            _isStopped = true;
        }
        public void CountNumbers()
        {
            long counter = 0;
            while(!_isStopped)
            {
                counter++;
            }
            WriteLine($"{ CurrentThread.Name} with " +
                    $"{CurrentThread.Priority, 11} priority " +
                    $"has a count = {counter, 13:N0}");
        }
    }
}
