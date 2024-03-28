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
    public partial class Form3 : Form
    {
        Service service = new Service();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            service.InsertDataTeam(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7);
        }
    }
}
