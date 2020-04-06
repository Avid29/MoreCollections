using BenchmarkDotNet.Attributes;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    public class ConstantDequeBenchmarks : DequeBenchmark
    {
        //[Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        [Params(1, 2, 5, 10, 20, 50, 100)]
        public int Items;

        [Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        public int ChunkSize;

        [GlobalSetup]
        public void Setup()
        {
            deque = new ConstantDeque<int>(ChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void Initialize()
        {
            
        }

        public void PushBackDeque()
        {
            PushBack();
        }

        public void PushFrontDeque()
        {
            PushFront();
        }
    }
}
