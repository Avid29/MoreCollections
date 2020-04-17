using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class ConstantDequeBenchmarks : DequeBenchmark
    {
        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int Items;

        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1_000, 2_000, 5_000, 10_000, 20_000, 50_000, 100_000, 1_000_000)]
        public int NewItems;

        //[Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        //[Params(8, 256, 512, 1024, 4096)]
        [Params(128)]
        public int ChunkSize;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        //[Benchmark]
        public void Initialize()
        {
            // Actually running this would take way too long. Throw away the results.
            if (ChunkSize < 32 && (Items > 2000 || NewItems > 2000))
            {
                NewItems = 0;
                deque = new GridDeque<int>(new int[] { 0, 1, 2, 3, 4 }, ChunkSize);
                return;
            }

            deque = new GridDeque<int>(ChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackConstant()
        {
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFrontConstant()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBackConstant()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFrontConstant()
        {
            PopFrontN(NewItems);
        }
    }
}
