using System;
using System.Collections.Generic;
using System.IO;

namespace HelionCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var fs = File.OpenRead(@"C:\test.csv"))
            using (var reader = new StreamReader(fs))
            {
                var listA = new List<string>();
                var listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    Console.Out.WriteLine(values[0]);
                    listB.Add(values[1]);
                }
            }

            Console.In.ReadLine();
        }
    }
}