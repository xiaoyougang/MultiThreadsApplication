using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;


namespace MultiThreadsApplication.ConsoleApp8
{
    /*
     * 演示向线程传递参数：
     * 
     * 1.程序启动时，首先创建了ThreadSample类的一个对象，并提供一个迭代次数，然后使用该对象的CountNumbers方法
     *   启动线程。该方法运行在另一个线程中，但是使用数字10，该数字是通过ThreadSample对象的构造函数传入的。因此
     *   我们只是使用相同的间接方式将该迭代次数传递给另一个线程。
     *   
     * 2.第二种传递数据的方式是使用Thread.Start方法。该方法会接收一个对象，并将该对象传递给线程。为了使用该方法，
     *   在线程中启动的方法必须接受object类型的单个参数。在创建thread2线程时我们将8传作为一个对象传递给了Count
     *   方法，然后Count方法被转换为整型。
     *   
     * 3.第三种方式是使用lambda表达式。lambda表达式定义了一个不属于任何类的方法。我们创建了一个方法，该方法使用
     *   需要的参数调用了另一个方法，并在另一个线程中运行该方法。当启动thread3线程时，打印出了12个数字，这正是通
     *   过lambda表达式传递的数字。
     *   
     * 4.使用lambda表达式引用另一个C#对象的方式称为闭包。当lambda表达式中使用任何局部变量时，C#会生成一个类，并
     *   将该变量作为该类的一个属性。所以实际上该方式与thread1线程中使用的一样，但是我们无须定义该类，C#编译器会
     *   自动帮我们实现。这可能会导致几个问题。例如，如果在多个lambda表达式中使用相同的变量，它们会共享该变量值。
     *   示例中当启动thread4和thread5线程时，它们都会打印20，因为在这两个线程启动之前变量被修改为20.
     */
    class Program
    {
        static void Main(string[] args)
        {
            var sample = new ThreadSample(10);

            var thread1 = new Thread(sample.CountNumbers);
            thread1.Name = "ThreadOne";
            thread1.Start();
            thread1.Join();

            WriteLine("----------------------------------");

            var thread2 = new Thread(Count);
            thread2.Name = "ThreadTwo";
            thread2.Start(8);
            thread2.Join();

            WriteLine("----------------------------------");

            var thread3 = new Thread(() => CountNumbers(12));
            thread3.Name = "ThreadThree";
            thread3.Start();
            thread3.Join();

            WriteLine("----------------------------------");

            int i = 10;
            var thread4 = new Thread(() => PrintNumber(i));
            i = 20;
            var thread5 = new Thread(() => PrintNumber(i));
            thread4.Start();
            thread5.Start();
        }

        static void Count(object iterations)
        {
            CountNumbers((int)iterations);
        }

        static void CountNumbers(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Sleep(TimeSpan.FromSeconds(0.5));
                WriteLine($"{CurrentThread.Name} prints {i}");
            }
        }

        static void PrintNumber(int number)
        {
            WriteLine(number);
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
