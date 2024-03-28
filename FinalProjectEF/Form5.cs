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
    public partial class Form5 : Form
    {
        Service service = new Service();
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete?","Delete",MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                service.DeleteMatch(textBox1, textBox2, dateTimePicker1, "Y");
            }
            else 
            {
                service.DeleteMatch(textBox1, textBox2, dateTimePicker1, "N");
            }
        }
    }
}
