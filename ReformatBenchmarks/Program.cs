using CsvHelper;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReformatBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Benchmark> benchmarks;

            using (var reader = new StreamReader(args[0]))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                benchmarks = csv.GetRecords<Benchmark>().ToList();
            }

            Dictionary<string, ExpandoObject> methods = new Dictionary<string, ExpandoObject>();

            using (var reader = new StreamReader("results/methods.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var old = csv.GetRecords<dynamic>();
                foreach (dynamic method in old)
                {
                    methods.Add(method.Name, method);
                }
            }

            foreach(var method in benchmarks)
            {
                int offset = 0;
                string methodName= "";

                if (method.Method.StartsWith("PushBack"))
                {
                    methodName = "Pushback" + method.Items + "," + method.NewItems;
                    offset = 8;
                }
                else if (method.Method.StartsWith("PushFront"))
                {
                    methodName = "PushFront" + method.Items + "," + method.NewItems;
                    offset = 9;
                }
                else if (method.Method.StartsWith("PopBack"))
                {
                    methodName = "PopBack" + method.Items + "," + method.NewItems;
                    offset = 7;
                }
                else if (method.Method.StartsWith("PopFront"))
                {
                    methodName = "PopFront" + method.Items + "," + method.NewItems;
                    offset = 8;
                }

                methods.TryAdd(methodName, new ExpandoObject());
                methods[methodName].TryAdd("Name", methodName);
                methods[methodName].TryAdd(method.Method.Substring(offset) + method.ChunkSize, method.MeanTime);
            }

            using (var fileStream = new FileStream("results/methods.csv", FileMode.OpenOrCreate, FileAccess.Write))
            using (var writer = new StreamWriter(fileStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(methods.Values as IEnumerable<dynamic>);
            }
        }
    }
}
