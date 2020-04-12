using BenchmarkDotNet.Validators;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Benchmarks.Printer
{
    public class Table
    {
        string FunctionName { get; set; }

        Dictionary<int, Dictionary<int, double>> Entries { get; set; }

        public void AddValue(int ChunkSize, int Items, double value)
        {
            Entries.TryAdd(ChunkSize, new Dictionary<int, double>());
            Entries[ChunkSize].Add(Items, value);
        }

        public void Print()
        {
            PrintHeader();
            PrintValues();
        }

        private void PrintHeader()
        {
            Console.Write("     "); // 5 spaces
            foreach (var items in Entries[0])
            {
                Console.Write("{0,8}", items.Key);
            }
            Console.WriteLine();
        }

        private void PrintValues()
        {
            foreach (var chunkSize in Entries)
            {
                Console.Write("{0,5}", chunkSize.Key);
                foreach (var item in chunkSize.Value)
                {
                    Console.Write("{0,8}", item.Value);
                }
                Console.WriteLine();
            }
        }
    }
}
