using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp11
{
    /*
     * 演示在线程中如何正确地处理异常
     * 
     * *在线程中始终使用try/catch代码块是非常重要的，因为不可能在线程之外来捕获异常*
     * 
     *   当主程序启动时，定义了两个将会抛出异常的线程。其中一个对异常进行了处理，另一个则没有。可以看到第二个异常没有被包裹
     * 启动线程的try/catch代码块捕获到。所以如果直接使用线程，一般来说不要在线程中抛出异常，而是在线程代码块中使用try/catch
     * 代码块。
     * 
     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
            // 正确的处理方式
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            // 错误的处理方式
            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
            }
            catch (Exception ex)
            {
                WriteLine("We won't get here!");
            }
        }

        static void BadFaultyThread()
        {
            WriteLine("Starting a faulty thread....");
            Sleep(TimeSpan.FromSeconds(2));
            throw new Exception("Boom!!!");
        }

        static void FaultyThread()
        {
            try
            {
                WriteLine("Starting a faulty thread....");
                Sleep(TimeSpan.FromSeconds(2));
                throw new Exception("Boom!!!");
            }
            catch (Exception ex)
            {
                WriteLine($"Exception handled: {ex.Message}");
            }
        }
    }
}
