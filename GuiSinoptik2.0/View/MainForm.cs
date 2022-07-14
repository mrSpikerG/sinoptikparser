using GuiSinoptik2._0.Control;
using GuiSinoptik2._0.Model;
using GuiSinoptik2._0.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiSinoptik2._0
{
    public partial class MainForm : Form
    {
        //
        //  Model
        //
        public TemperatureInfo MaxTemperature { get; set; } = new TemperatureInfo(temperatue.MAX);
        public TemperatureInfo MinTemperature { get; set; } = new TemperatureInfo(temperatue.MIN);


        //
        //  Controls
        //
        private WebControl control = new WebControl();


        //
        //  View
        //
        private PictureBox PicBox;
        private Label[] AdditionalLabels;
        private PictureBox[] PictureLine;
        private Label[] Grid;
        private TermometrUserControl termometr;


        public MainForm()
        {
            InitializeComponent();

            //
            //  Settings
            //
            this.BackColor = Color.White;
            this.ClientSize = new Size(1100, 500);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);






            //
            //  Текстовые полоски в таблице
            //
            Grid = new Label[6];
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = new Label();
            }

            //
            //  Полоска картинок в таблице
            //
            PictureLine = new PictureBox[8];
            for (int i = 0; i < PictureLine.Length; i++)
            {
                PictureLine[i] = new PictureBox();
            }

            //
            //  Текущая температура 
            //
            string temp = control.getTodayTemperature().Replace("&deg;", "").Replace("+", "").Replace("C", "");

            //
            //  Заполнение таблицы
            //

            //  Температура
            Grid[0].Text = control.getNodesForGrid("//tr[@class='temperature']//td").Replace("deg;", "").Replace(" ", "  ");

            //  Температура чувствуется
            Grid[1].Text = control.getNodesForGrid("//tr[@class='temperatureSens']//td").Replace("deg;", "").Replace(" ", "  ");

            //  Влажность 
            Grid[3].Text = control.getNodesForGrid("//tbody//tr//td", 40, 48).Replace(" ", "   ");

            //  Вероятность осадков
            Grid[5].Text = control.getNodesForGrid("//tbody//tr//td", 56, 64).Replace(" ", "    ");

            //  Давление  
            Grid[2].Text = control.getNodesForGrid("//tr[@class='gray']//td", 0, 8).Replace(" ", "  ");

            //  Скорость ветра
            Grid[4].Text = control.getNodesForGrid("//tr[@class='gray']//td", 8, 16).Replace(" ", "  ").Trim();




            //
            //  Допольнительные полоски к таблице 
            //
            AdditionalLabels = new Label[6];
            for (int i = 0; i < AdditionalLabels.Length; i++)
            {
                AdditionalLabels[i] = new Label();
            }

            //  Наименование столбцов
            AdditionalLabels[0].Text = control.getNodesForGrid("//tr[@class='gray time']//td");
            AdditionalLabels[0].Location = new Point(600, 50);
            AdditionalLabels[0].AutoSize = true;

            //  Главное, что работает :D
            AdditionalLabels[1].Text = "       Ночь              Утро                  День                  Вечер";
            AdditionalLabels[1].Location = new Point(600, 30);
            AdditionalLabels[1].AutoSize = true;

            //  Наименование рядов
            AdditionalLabels[2].Text = "Температура, °C\n\n\nчувствуется как\n\n\nДавление, мм\n\n\nВлажность, %\n\n\nВетер, м / сек\n\n\nВероятность осадков, % ";
            AdditionalLabels[2].Size = new Size(100, 500);
            AdditionalLabels[2].Location = new Point(500, 100);

            //  Регистрация допольнительных полосок
            this.Controls.Add(AdditionalLabels[0]);
            this.Controls.Add(AdditionalLabels[1]);
            this.Controls.Add(AdditionalLabels[2]);

            //
            //  Регистрация таблицы + Заполнение картинок
            //
            //  Картинки
            for (int i = 0; i < PictureLine.Length; i++)
            {
                PictureLine[i].Size = new Size(30, 25);
                PictureLine[i].Location = new Point(600 + i * 35, 70);
                PictureLine[i].Image = control.getPictureNode("//table[@class='weatherDetails']//img[@class='weatherImg']", i);
                this.Controls.Add(PictureLine[i]);
            }
            // Таблица
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i].AutoSize = true;
                Grid[i].Location = new Point(600, 40 * i + 100);
                this.Controls.Add(Grid[i]);
            }


            //
            //  Термометр       (показывает температуру)
            //
            termometr = new TermometrUserControl(Convert.ToInt32(temp));
            termometr.Location = new Point(20, 40);
            termometr.Size = new Size(16, 95);
            this.Controls.Add(termometr);


            //
            //  Labels
            //

            //  Максимальная температура
            this.label1.Text = MaxTemperature.ToString();
            this.label1.Location = new Point(this.label1.Location.X-10, 360);

            //  Минимальная температура
            this.label2.Text = MinTemperature.ToString();
            this.label2.Location = new Point(this.label1.Location.X, 380);

            //  Активное время
            this.label3.Text = control.getNodesForGrid("//p[@class='today-time']");

            //  Восход и закат
            this.label4.Text = control.getRising().Trim();
            this.label4.Location = new Point(this.label1.Location.X, this.label4.Location.Y + 140);

            //  Активная температура
            this.TodayTemperature.Text = control.getTodayTemperature().Replace("deg;", "°");
            this.TodayTemperature.BackColor = Color.Transparent;
            this.TodayTemperature.AutoSize = true;
            this.TodayTemperature.Location = new Point(this.TodayTemperature.Location.X, this.TodayTemperature.Location.Y + 60);
            this.TodayTemperature.Font = new Font(this.TodayTemperature.Font.FontFamily, 30);

            //  Описание
            this.Descript.Text = control.getDescriprion();
            this.Descript.AutoSize = false;
            this.Descript.Location = new Point(this.Descript.Location.X, this.Descript.Location.Y + 50);
            this.Descript.Size = new Size(250, 100);

            //  Народные слухи
            this.Narod.Text = control.getNarodDescriprion();
            this.Narod.AutoSize = false;
            this.Narod.Location = new Point(this.Narod.Location.X, this.Narod.Location.Y + 60);
            this.Narod.Size = new Size(250, 100);

            //  Погода за последние 130 лет 0.0
            this.label5.Text = control.getOldYears();
            this.label5.Location = new Point(this.label1.Location.X, 340);

            //
            //  Картинка активной погоды
            //
            PicBox = control.getPicture(new Point(0, 10));
            this.Controls.Add(PicBox);
        }


    }
}
