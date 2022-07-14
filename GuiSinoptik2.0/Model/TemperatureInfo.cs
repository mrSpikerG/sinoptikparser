using HtmlAgilityPack;
using System;
using System.Net;
using System.Text;
using GuiSinoptik2._0.Control;

namespace GuiSinoptik2._0.Model
{
    public class TemperatureInfo
    {
        public string Descript { get; set; }
        public string Year { get; set; }
        public string Temperatue { get; set; }

        private WebControl control = new WebControl();
        public TemperatureInfo(temperatue temp)
        {
            this.Descript = temp == temperatue.MAX ? "Макс:" : "Мин:";
            this.Temperatue = control.getTemperature(temp);
            this.Year = control.getYear(temp);
        }

        public override string ToString()
        {
            return $"{this.Descript} {this.Temperatue} ({this.Year})";
        }
    }
}
