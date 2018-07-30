using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp2_3
{
    /*
     * 演示SemaphoreSlim类如何限制同时访问同一资源的线程数量。
     * 
     *   当程序启动时，创建了SemaphoreSlim类的一个实例，并在其构造函数中指定允许的并发数量。然后启动了6个不同名称和不同初始运行时间的
     * 线程。每个线程都尝试获取数据库的访问，但是我们借助于信号系统限制了访问数据库的并发数为4个线程。当有4个线程获取了数据库的访问后，
     * 其他两个线程需要等待，直到之前线程中的一个完成工作并调用Release方法来发出信号。
     *   SemaphoreSlim并不使用Windows内核信号量，而且也不支持进程间同步。所以在跨程序同步的场景下可以使用Semaphore。
     * 
     */
    class Program
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);
                
        static void AccessDatabase(string name, int seconds)
        {
            WriteLine($"{name} waits to access a database");
            _semaphore.Wait();
            WriteLine($"{name} was granted an access to a database");
            Sleep(TimeSpan.FromSeconds(seconds));
            WriteLine($"{name} is completed.");
            _semaphore.Release();
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 6; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(()=> AccessDatabase(threadName, secondsToWait));
                t.Start();
            }
        }
    }
}
