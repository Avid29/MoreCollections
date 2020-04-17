using BenchmarkDotNet.Running;
using Benchmarks.Deque;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<IdealDequeBenchmarks>();
            BenchmarkRunner.Run<DequeBenchmarks>();
            BenchmarkRunner.Run<ConstantDequeBenchmarks>();
            BenchmarkRunner.Run<DynamicDequeBenchmarks>();
            BenchmarkRunner.Run<ListDequeBenchmarks>();
            BenchmarkRunner.Run<ConstantOldDequeBenchmarks>();
        }
    }
}
