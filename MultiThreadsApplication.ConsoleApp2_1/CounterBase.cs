using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadsApplication.ConsoleApp2_1
{
    abstract class CounterBase
    {
        public abstract void Increment();

        public abstract void Decrement();
    }

    class Counter : CounterBase
    {
        private int _count;
        public int Count => _count;
        public override void Increment()
        {
            _count++;
        }
        public override void Decrement()
        {
            _count--;
        }
    }

    class CounterNoLock : CounterBase
    {
        private int _count;
        public int Count => _count;
        public override void Increment()
        {
            Interlocked.Increment(ref _count);
        }
        public override void Decrement()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
