using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class ConstantOldDequeBenchmarks : DequeBenchmark
    {
        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int Items;

        [Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int NewItems;

        //[Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
        [Params(8, 256, 512, 1024, 4096)]
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
                deque = new ConstantDequeOld<int>(new int[] { 0, 1, 2, 3, 4 }, ChunkSize);
                return;
            }

            deque = new ConstantDequeOld<int>(ChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackConstantOld()
        {
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFrontConstantOld()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBackConstantOld()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFrontConstantOld()
        {
            PopFrontN(NewItems);
        }
    }
}
