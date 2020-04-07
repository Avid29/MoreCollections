using BenchmarkDotNet.Running;
using Benchmarks.Deque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ConstantDequeBenchmarks>();
            BenchmarkRunner.Run<DynamicDequeBenchmarks>();
            BenchmarkRunner.Run<ListDequeBenchmarks>();
        }
    }
}
