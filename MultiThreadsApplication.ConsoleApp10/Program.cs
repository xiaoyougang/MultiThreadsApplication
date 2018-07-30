using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace MultiThreadsApplication.ConsoleApp10
{
    /*
     * 演示另一个常见的多线程错误——死锁（deadlock）.
     * 
     *   在LockTooMuch方法中我们锁定了第一个对象，等待一秒后锁定第二个对象。然后在另一个线程中启动该方法。最后尝试在主线程中先
     * 先锁定第二个对象再锁定第一个对象。如果像示例中的第一部分一样使用lock将会造成死锁。第一个线程保持对obj1对象的锁定，等待直到
     * obj2对象被释放。主线程保持对obj2对象的锁定并等待直到obj1对象被释放，但obj1对象永远不会被释放。
     *   使用Ｍonitor类的TryEnter方法可以避免死锁，该方法接受一个超时参数。如果我们能够获取被lock保护的资源之前，超时参数过期，
     * 则该方法返回false。
     *   
     */
    class Program
    {
        static void Main(string[] args)
        {
            object obj1 = new object();
            object obj2 = new object();

            //new Thread(() => LockTooMuch(obj1, obj2)).Start();            
            //lock(obj2)
            //{
            //    Sleep(1000);
            //    WriteLine("This will be a deadlock!");
            //    lock(obj1)
            //    {
            //        WriteLine("Acquired a protected resource succesfully");
            //    }
            //}

            new Thread(() => LockTooMuch(obj1, obj2)).Start();
            WriteLine("-------------------------------------------------");
            lock (obj2)
            {
                Sleep(1000);
                WriteLine("Monitor.TryEnter allows not to get stuck, returning false after a specified timeout is elapsed");
                if (Monitor.TryEnter(obj1, TimeSpan.FromSeconds(5)))
                {
                    WriteLine("Acquired a protected resource succesfully");
                }
                else
                {
                    WriteLine("Timeout acquired a resource!");
                }
            }
        }

        static void LockTooMuch(object obj1, object obj2)
        {
            lock(obj1)
            {
                Sleep(1000);
                lock (obj2);
            }
        }
    }
}
