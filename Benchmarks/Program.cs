using BenchmarkDotNet.Reports;
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
            List<Summary> summaries = new List<Summary>();
            //summaries.Add(BenchmarkRunner.Run<ConstantDequeBenchmarks>());
            //summaries.Add(BenchmarkRunner.Run<DynamicDequeBenchmarks>());
            summaries.Add(BenchmarkRunner.Run<ListDequeBenchmarks>());

            // TODO: Log
            Printer.Printer.Print(summaries);
        }
    }
}
