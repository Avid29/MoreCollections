using BenchmarkDotNet.Attributes;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class IdealDequeBenchmarks : DequeBenchmark
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
            deque = new Deque<int>(Items + NewItems);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackIdeal()
        {
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFrontIdeal()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBackIdeal()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFrontIdeal()
        {
            PopFrontN(NewItems);
        }
    }
}
