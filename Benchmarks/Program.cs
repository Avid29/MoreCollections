using BenchmarkDotNet.Running;
using Benchmarks.Deque;

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
