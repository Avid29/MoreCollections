using BenchmarkDotNet.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Benchmarks.Printer
{
    public class Printer
    {
        Dictionary<string, Table> Tables { get; set; }

        public Printer(List<Summary> summaries)
        {
            Tables = new Dictionary<string, Table>();
        }

        public void BuildTables(List<Summary> summaries)
        {
            foreach (var summary in summaries)
            {
                foreach (var report in summary.Reports)
                {
                    Tables.TryAdd(report.BenchmarkCase.Job.ResolvedId, new Table());


                    int? ChunkSize = report.BenchmarkCase.Parameters.Items.FirstOrDefault(x => x.Name == "ChunkSize" || x.Name == "InitialChunkSize").Value as int?;
                    
                    if (ChunkSize == null)
                    {
                        ChunkSize = 0;
                    }

                    int? Items = report.BenchmarkCase.Parameters.Items.FirstOrDefault(x => x.Name == "Items").Value as int?;

                    Tables[report.BenchmarkCase.Job.ResolvedId].AddValue(ChunkSize.Value, Items.Value, report.ResultStatistics.Mean);
                }
            }
        }

        public void Print()
        {
            foreach (var table in Tables.Values)
            {
                table.Print();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void Print(List<Summary> summaries)
        {
            Printer printer = new Printer(summaries);
            printer.Print();
        }
    }
}
