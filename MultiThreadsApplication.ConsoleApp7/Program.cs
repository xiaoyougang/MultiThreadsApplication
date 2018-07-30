using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp7
{
    /*
     * 演示前台线程和后台线程，及如何设置该选项来影响程序的行为。
     * 
     *   当主程序启动时定义了两个不同的线程。默认情况下，显式创建的线程是前台线程。通过手动的设置thread2对象
     * 的IsBackground属性为true来创建一个后台线程。通过配置前台线程迭代次数10、后台线程迭代次数20，来实现
     * 前台线程会比后台线程先完成。然后运行程序。
     *   前台线程完成后，程序结束并且后台线程被终结。这是前台线程与后台线程有主要区别：进程会等待所有的前台线程
     * 完成后再结束工作，但是如果只剩下后台线程，则会直接结束工作。如果程序定义了一个不会完成的前台线程，主程序
     * 并不会正常结束。
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            var sampleForeground = new ThreadSample(10);
            var sampleBackground = new ThreadSample(20);

            var thread1 = new Thread(sampleForeground.CountNumbers);
            thread1.Name = "ForegroundThread";
            var thread2 = new Thread(sampleBackground.CountNumbers);
            thread2.Name = "BackgroundThread";
            thread2.IsBackground = true;

            thread1.Start();
            thread2.Start();
        }
    }


    class ThreadSample
    {
        private readonly int _iterations;
        public ThreadSample(int iterations)
        {
            _iterations = iterations;
        }
        public void CountNumbers()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Sleep(TimeSpan.FromSeconds(0.5));
                WriteLine($"{CurrentThread.Name} prints {i}");
            }
        }
    }
}
