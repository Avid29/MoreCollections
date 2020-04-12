using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReformatBenchmarks
{
    public class Benchmark
    {
        [Name("Method")]
        public string Method { get; set; }

        [Name("NewItems")]
        public int NewItems { get; set; }

        [Name("Items")]
        public int Items { get; set; }

        [Name("ChunkSize")]
        public string ChunkSize { get; set; }

        [Name("Mean")]
        public string MeanTime { get; set; }

        [Name("Median")]
        public string MedianTime { get; set; }
    }
}
