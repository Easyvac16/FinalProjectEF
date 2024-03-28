using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProjectEF
{
    public partial class Form2 : Form
    {
        Service service = new Service();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service.PopulateMatchesTable(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, dateTimePicker1);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
