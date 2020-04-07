using BenchmarkDotNet.Attributes;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    public class ConstantDequeBenchmarks : DequeBenchmark
    {
        [Params(5000, 10000)]
        //[Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000)]
        public int Items;

        [Params(32, 64, 128, 256, 512, 1024, 2048, 4096)]
        //[Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        public int ChunkSize;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        public void Initialize()
        {
            deque = new ConstantDeque<int>(ChunkSize);
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
