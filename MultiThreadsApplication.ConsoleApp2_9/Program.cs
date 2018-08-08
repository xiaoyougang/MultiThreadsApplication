using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp2_9
{
    /*
     * 介绍如何不使用内核模型的方式来使用线程等待。
     * 另外，我们介绍了SpinWait，它是一个混合同步构造，被设计为使用用户模式等待一段时间，然后切换到内核模式以节省CPU时间。
     * 
     * 
     *   当主程序启动时，定义了一个线程，将执行一个无止境的循环，直到20毫秒后主线程设置_isCompleted变量为true。我们可以试验运行
     * 该周期为20~30秒，通过Windows任务管理器测量CPU的负载情况。取决于CPU内核数量，任务管理器将显示一个显著的处理时间。
     *   我们使用 volatile 关键字来声明_isCompleted静态字段。volatile关键字指出一个字段可能会被同时执行的多个线程修改。声明为
     * volatile的字段不会被编译器和处理器优化为只能被单个线程访问。这确保了该字段总是最新的值。
     *   然后我们使用了 SpinWait 版本，用于在每个迭代打印一个特殊标志位来显示线程是否切换为阻塞状态。运行该线程5毫秒来查看结果。刚
     * 开始，SpinWait尝试使用用户模式，在9个迭代后，开始切换线程为阻塞状态。如果尝试测量该版本的CPU负载，在Windows任务管理器将
     * 不会看到任何CPU的使用。
     * 
     * 
     */
    class Program
    {
        static volatile bool _isCompleted = false;

        static void Main(string[] args)
        {
            var t1 = new Thread(UserModeWait);
            var t2 = new Thread(HybridSpinWait);

            WriteLine("Running user mode waiting");
            t1.Start();
            Sleep(20);
            _isCompleted = true;

            Sleep(TimeSpan.FromSeconds(1));
            _isCompleted = false;
            WriteLine("Running hybrid spinwait construct waiting");
            t2.Start();
            Sleep(5);
            _isCompleted = true;
        }

        static void UserModeWait()
        {
            while(!_isCompleted)
            {
                Write(".");
            }
            WriteLine();
            WriteLine("Waiting is complete");
        }

        static void HybridSpinWait()
        {
            var w = new SpinWait();
            while(!_isCompleted)
            {
                w.SpinOnce();
                WriteLine(w.NextSpinWillYield);
            }
            WriteLine("Waiting is complete");
        }
    }
}
