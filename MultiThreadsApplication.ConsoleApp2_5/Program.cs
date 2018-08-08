using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp2_5
{
    /*
     * 
     * 介绍如何使用ManualResetEventSlim类来在线程间以更灵活的方式传递信号。
     * 
     * 
     *   当主程序启动时，首先创建了ManualResetEventSlim类的一个实例。然后启动了三个线程，等待事件信号通知它们继续执行。
     *   ManualResetEventSlim 的整个工作方式有点像人群通过大门。前节讨论过的AutoResetEvent事件像一个旋转门，一次只允许一人通过。
     * ManualResetEventSlim 是 ManualResetEvent 的混合版本，一直保持大门敞开直到手动调用Reset方法。当调用_mainEvent.Set时，
     * 相当于打开了大门从而允许准备好的线程接收信号并继续工作。然而线程3还处于睡眠状态，没有赶上时间。当调用_mainEvent.Reset相当于
     * 关闭了大门。最后一个线程已经准备好执行，但是不得不等待下一个信号，即要等待好几秒钟。
     * 
     * 
     */
    class Program
    {
        static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);

        static void Main(string[] args)
        {
            var t1 = new Thread(() => TravelThroughGates("Thread 1", 5));
            var t2 = new Thread(() => TravelThroughGates("Thread 2", 6));
            var t3 = new Thread(() => TravelThroughGates("Thread 3", 12));
            t1.Start();
            t2.Start();
            t3.Start();

            Sleep(TimeSpan.FromSeconds(6));
            WriteLine("The gates are now open!");
            _mainEvent.Set();
            Sleep(TimeSpan.FromSeconds(2));
            _mainEvent.Reset();
            WriteLine("The gates have been closed!");

            Sleep(TimeSpan.FromSeconds(10));
            WriteLine("The gates are now open for the second time!");
            _mainEvent.Set();
            Sleep(TimeSpan.FromSeconds(2));
            WriteLine("The gates have been closed!");
            _mainEvent.Reset();
        }

        static void TravelThroughGates(string threadName, int seconds)
        {
            WriteLine($"{threadName} falls to sleep");
            Sleep(TimeSpan.FromSeconds(seconds));
            WriteLine($"{threadName} waits for the gates to open!");
            _mainEvent.Wait();
            WriteLine($"{threadName} enters the gates!");
        }

    }
}
