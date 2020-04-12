﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MoreCollections.Generic;

namespace Benchmarks.Deque
{
    public class ConstantDequeBenchmarks : DequeBenchmark
    {
        [Params(1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int Items;

        [Params(8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096)]
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
            if (ChunkSize < 32 && Items > 2000)
            {
                deque = new ConstantDeque<int>(new int[] { 0, 1, 2, 3, 4 }, ChunkSize);
                return;
            }

            deque = new ConstantDeque<int>(ChunkSize);
            for (int i = 0; i < Items; i++)
            {
                deque.PushBack(i);
            }
        }

        [Benchmark]
        public void PushBackConstant()
        {
            PushBack();
        }

        [Benchmark]
        public void PushFrontConstant()
        {
            PushFront();
        }

        [Benchmark]
        public void PopBackConstant()
        {
            PopBack();
        }

        [Benchmark]
        public void PopFrontConstant()
        {
            PopFront();
        }
    }
}
