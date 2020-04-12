﻿using BenchmarkDotNet.Attributes;
using Implementations.Deque;

namespace Benchmarks.Deque
{
    [JsonExporter]
    [JsonExporterAttribute.Full]
    [JsonExporterAttribute.Brief]
    public class ListDequeBenchmarks : DequeBenchmark
    {
        [Params(0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int Items;

        [Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int NewItems;

        [IterationSetup]
        public void Setup()
        {
            Initialize();
        }

        //[Benchmark]
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
            PushBackN(NewItems);
        }

        [Benchmark]
        public void PushFrontList()
        {
            PushFrontN(NewItems);
        }

        [Benchmark]
        public void PopBackList()
        {
            PopBackN(NewItems);
        }

        [Benchmark]
        public void PopFrontList()
        {
            PopFrontN(NewItems);
        }
    }
}
