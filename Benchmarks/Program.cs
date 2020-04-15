using BenchmarkDotNet.Running;
using Benchmarks.Deque;

namespace Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<IdealDequeBenchmarks>();
            BenchmarkRunner.Run<FlatDequeBenchmarks>();
            //BenchmarkRunner.Run<ConstantDequeBenchmarks>();
            //BenchmarkRunner.Run<ConstantOldDequeBenchmarks>();
            //BenchmarkRunner.Run<DynamicDequeBenchmarks>();
            //BenchmarkRunner.Run<ListDequeBenchmarks>();
        }
    }
}
