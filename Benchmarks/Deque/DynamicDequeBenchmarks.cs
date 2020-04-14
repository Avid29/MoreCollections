using BenchmarkDotNet.Attributes;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class DynamicDequeBenchmarks : DequeBenchmark
    {
        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int Items;

        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int NewItems;

        //[Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        [Params(128)]
        public int InitialChunkSize;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        //[Benchmark]
        public void Initialize()
        {
            deque = new DynamicDeque<int>(InitialChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackDynamic()
        {
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFrontDynamic()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBackDynamic()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFrontDynamic()
        {
            PopFrontN(NewItems);
        }
    }
}
