using BenchmarkDotNet.Attributes;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    public class DynamicDequeBenchmarks : DequeBenchmark
    {
        [Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int Items;

        [Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        public int InitialChunkSize;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        public void Initialize()
        {
            deque = new DynamicDeque<int>(InitialChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackDeque()
        {
            PushBack();
        }

        [Benchmark]
        public void PushFrontDeque()
        {
            PushFront();
        }
    }
}
