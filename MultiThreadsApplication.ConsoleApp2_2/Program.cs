using System;
using System.Threading;
using static System.Console;

namespace MultiThreadsApplication.ConsoleApp2_2
{
    /*
     * 演示如何使用Mutex类来同步两个单独的程序。
     * 
     *   Mutex是一种原始的同步方式，它只对一个线程授予对共享资源的独占访问。
     * 
     *   当主程序启动时，定义了一个指定名称的互斥量，设置initialOwner标志为false。这意味着如果互斥量已被创建，则允许程序获取该互斥量。
     * 如果没有获取到互斥量，程序则显示Running,等待直到按下了任意键，然后释放该互斥量并退出。如果再运行同样的一个程序，则会在5秒钟内尝试
     * 获取互斥量，如果此时在第一个程序按下了任意键，第二个程序则会开始执行显示“Running!”。而如果保持等待5秒钟，则第二个程序将无法获取到
     * 该互斥量，会显示“Second instance is running!”。
     *   该方式可用在不同的程序中同步线程，可被推广到大量的使用场景中。
     *   
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            const string MutexName = "CSharpThreading";
            using (var mutex = new Mutex(false, MutexName))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    WriteLine("Second instance is running!");
                }
                else
                {
                    WriteLine("Running!");
                    ReadLine();
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
