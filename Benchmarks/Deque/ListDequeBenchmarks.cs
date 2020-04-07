using BenchmarkDotNet.Attributes;
using Implementations.Deque;

namespace Benchmarks.Deque
{
    public class ListDequeBenchmarks : DequeBenchmark
    {
        [Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int Items;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        public void Initialize()
        {
            deque = new ListDeque<int>();
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackList()
        {
            PushBack();
        }

        [Benchmark]
        public void PushFrontList()
        {
            PushFront();
        }
    }
}
