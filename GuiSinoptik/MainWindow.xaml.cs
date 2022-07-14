using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuiSinoptik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string Str { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WebClient client = new WebClient();
            string html = Encoding.UTF8.GetString(client.DownloadData("https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B4%D0%BD%D0%B5%D0%BF%D1%80-303007131"));
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='infoHistoryval']");
            //str = node.InnerHtml.Replace("&deg;", "").Replace("<br>", "").Replace("<i>", "").Replace("<span>", "").Replace("</i>", "").Replace("</span>", "");
           // Console.WriteLine(node.InnerHtml.Replace("&deg;", "").Replace("<br>", "").Replace("<i>", "").Replace("<span>", "").Replace("</i>", "").Replace("</span>", ""));
        }
    }
}
