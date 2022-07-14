using HtmlAgilityPack;
using System;
using System.Net;
using System.Text;

namespace sinoptikparser
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            string html = Encoding.UTF8.GetString(client.DownloadData("https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B4%D0%BD%D0%B5%D0%BF%D1%80-303007131"));
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='infoHistoryval']");
        
            Console.WriteLine(node.InnerHtml.Replace("&deg;","").Replace("<br>","").Replace("<i>", "").Replace("<span>", "").Replace("</i>", "").Replace("</span>", ""));
        }
    }
}
