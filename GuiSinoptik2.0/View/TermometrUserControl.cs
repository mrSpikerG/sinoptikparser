using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiSinoptik2._0.View
{
    public partial class TermometrUserControl : UserControl
    {
        public int Temperature { get; set; }
        public TermometrUserControl(int temperature)
        {
            InitializeComponent();
            this.Temperature = temperature;
            this.pictureBox1.Size = new Size(8, 40+Temperature);
            this.pictureBox1.Location = new Point(4, this.Size.Height - 17 - Temperature);
        }
    }
}
