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
    public partial class Form4 : Form
    {
        Service service = new Service();
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            service.InsertDataPlayer(textBox1, textBox2, textBox3, textBox4, textBox5);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
