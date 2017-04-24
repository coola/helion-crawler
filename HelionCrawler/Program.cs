using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HelionCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var fs = File.OpenRead(@"C:\test.csv"))
            using (var reader = new StreamReader(fs))
            {
                var idents = new List<string>();
                var names = new List<string>();

                var tasks = new List<Task>();
                while (!reader.EndOfStream)
                {
                   

                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var ident = values[0].Replace(@"""", string.Empty);

                    var brand = values[6];



                    if (ident == "Ident") continue;
                    if (brand != "Helion") continue;


                    idents.Add(ident);
                    names.Add(values[2]);

                    var currentUrl = $"http://helion.pl/ksiazki/{ident}";

                    var task = Task.Run(() =>
                    {
                        var doc = new HtmlDocument();
                        var client = new WebClient();
                        var html = client.DownloadString(currentUrl);
                        doc.LoadHtml(html);

                        var htmlNodeCollection =
                            doc.DocumentNode.SelectNodes(@"//*[@id=""left-big-col""]/div[1]/dl/dd[1]/span[4]/a/span");

                        var numberOfVotes = htmlNodeCollection != null ? htmlNodeCollection[0].InnerText : 0.ToString();

                        Console.Out.WriteLine($"ident: {ident}, liczba opinii: {numberOfVotes},  name: {values[2]}");
                    });
                    tasks.Add(task);
                }
            }

            Console.In.ReadLine();
        }
    }
}