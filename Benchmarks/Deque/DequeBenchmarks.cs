using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class DequeBenchmarks : DequeBenchmark
    {
        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int Items;

        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int NewItems;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        //[Benchmark]
        public void Initialize()
        {
            deque = new Deque<int>();
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBack()
        {
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFront()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBack()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFront()
        {
            PopFrontN(NewItems);
        }
    }
}
