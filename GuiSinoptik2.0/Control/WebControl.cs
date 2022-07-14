using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiSinoptik2._0.Control
{
    public class WebControl
    {
        private WebClient client = new WebClient();
        private string html;
        private HtmlAgilityPack.HtmlDocument htmlDoc;

        public WebControl()
        {
            html = Encoding.UTF8.GetString(client.DownloadData("https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B4%D0%BD%D0%B5%D0%BF%D1%80-303007131"));
            htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
        }
        public string getTemperature(temperatue temp)
        {
            var node = htmlDoc.DocumentNode.SelectNodes("//p[@class='infoHistoryval']//span");
            return node[temp == temperatue.MAX ? 0 : 1].InnerText.Replace("deg;", "");
        }

        public string getYear(temperatue temp)
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='infoHistoryval']");
            string returnableText;
            switch (temp)
            {
                case temperatue.MAX:
                    return returnableText = node.InnerText.Substring(node.InnerText.IndexOf("(") + 1, node.InnerText.IndexOf(")") - 1 - node.InnerText.IndexOf("("));
                case temperatue.MIN:
                    return returnableText = node.InnerText.Substring(node.InnerText.LastIndexOf("(") + 1, node.InnerText.LastIndexOf(")") - 1 - node.InnerText.LastIndexOf("("));
            }
            return String.Empty;
        }

        public PictureBox getPicture(Point coord)
        {
            PictureBox picBox = new PictureBox();
            picBox.Location = coord;

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='img']//img");
            string src = node.GetAttributeValue<string>("src", "");


            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile($"https:{src}", "image.png");
            }
            picBox.Image = new Bitmap("image.png");
            picBox.Size = new Size(node.GetAttributeValue<int>("width", 100), node.GetAttributeValue<int>("height", 100));

            return picBox;
        }

        public string getDescriprion()
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='wDescription clearfix']//div[@class='rSide']//div");
            return node.InnerText;
        }
        public string getNarodDescriprion()
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='oDescription clearfix']//div[@class='rSide']//div");
            return node.InnerText;
        }

        public string getRising()
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='infoDaylight']");
            return node.InnerText.Replace("<span>", "").Replace("</span>", "");
        }

        public string getOldYears()
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='infoHistory']");
            return node.InnerText.Replace("<br>", "").Replace("</p>", "");
        }

        public Bitmap getPictureNode(string regex,int id)
        {

            var node = htmlDoc.DocumentNode.SelectNodes(regex);
            string src = node[id].GetAttributeValue<string>("src", "");


            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile($"https:{src}", $"image{id}.png");
            }
            return new Bitmap($"image{id}.png");
        }
        public string getNodesForGrid(string regex)
        {
            string returnableString = String.Empty;
            foreach (var item in htmlDoc.DocumentNode.SelectNodes(regex))
            {
                returnableString += $"{item.InnerText}   ";
            }

            return returnableString;
        }

        public string getNodesForGrid(string regex, int startindex, int endindex)
        {
            string returnableString = String.Empty;
            var node = htmlDoc.DocumentNode.SelectNodes(regex);
            for (int i = 0; i < node.Count; i++)
            {
                if (i >= startindex && i < endindex)
                {
                    returnableString += $"{node[i].InnerText}   ";
                }
            }

            return returnableString;
        }

        public string getTodayTemperature()
        {
            return htmlDoc.DocumentNode.SelectSingleNode("//p[@class='today-temp']").InnerText;
        }
    }
    public enum temperatue
    {
        MAX,
        MIN
    }
}
