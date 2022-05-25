using System;
using System.Net.Http;
using System.Linq;
using HtmlAgilityPack;

namespace Program
{

    class Program
    {
        public static void Main()
        {

            var scraper = new Scraper();
            scraper.Run();

            Console.Read();
        }
    }

    class Scraper
    {

        string url = "https://kopalniawiedzy.pl/wiadomosci";
        public async void Run()
        {
            string html = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                html = await client.GetStringAsync(new Uri(url));
            }
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var nodes = document.DocumentNode
            .Descendants("a")
            .Where(x => x.GetAttributeValue("class", "").Contains("title"));

            if (nodes != null)
            {
                foreach (var item in nodes)
                {
                    Console.WriteLine(string.Join('\n', item.InnerText));
                }

            }

        }
    }
}