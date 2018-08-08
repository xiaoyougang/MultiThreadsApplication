using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;
    

namespace MultiThreadsApplication.ConsoleApp2_4
{
    /*
     * 
     * 介绍如何借助于 AutoResetEvent 类来从一个线程向另一个线程发送通知。
     * AutoResetEvent 类可以通知等待的线程有某事件发生。
     * 
     * 
     *   当主程序启动时，定义了两个AutoResetEvent实例。其中一个是从子线程向主线程发信号，另一个实例是从主线程向子线程发信号。我们
     * 向AutoResetEvent构造方法传入false，定义了这两个实例的初始状态为unsignaled。这意味着任何线程调用这两个对象中的任何一个的
     * WaitOne方法将会被阻塞，直到我们调用了Set方法。如果初始事件状态为true，那么AutoResetEvent实例的状态为signaled，如果线程
     * 调用WaitOne方法则会被立即处理。然后事件状态自动变为unsignaled，所以需要再对该实例调用一次Set方法，以便让其他的线程对该实例
     * 调用WaitOne方法从而继续执行。
     *   然后我们创建了第二个线程，其会执行第一个操作10秒钟，然后等待从第二个线程发出的信号。该信号意味着第一个操作已经完成。现在第二
     * 个线程在等待主线程的信号。我们对主线程做了一些附加工作，并通过调用_mainEvent.Set方法发送一个信号。然后等待从第二个线程发出的
     * 另一个信号。
     *   AutoResetEvent类采用的是内核时间模式，所以等待时间不能太长。
     * 
     */
    class Program
    {
        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var t = new Thread(() => Process(10));
            t.Start();
            WriteLine("1.Waiting for another thread to complete work");
            _workerEvent.WaitOne();

            WriteLine("3.1.First operation is complete!");
            WriteLine("3.2.Performing an operation on a main thread");
            Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();
            WriteLine("3.3.Now running the second operation on a second thread");
            _workerEvent.WaitOne();

            WriteLine("5.Second opertaion is completed.");
        }

        static void Process(int seconds)
        {
            WriteLine("2.1.Starting a long running work...");
            Sleep(TimeSpan.FromSeconds(seconds));
            WriteLine("2.2.Work is done!");
            _workerEvent.Set();
            WriteLine("2.3.Waiting for a main thread to complete its work");
            _mainEvent.WaitOne();

            WriteLine("4.1.Starting second operation...");
            Sleep(TimeSpan.FromSeconds(seconds));
            WriteLine("4.2.Work is done!");
            _workerEvent.Set();
        }
    }
}
