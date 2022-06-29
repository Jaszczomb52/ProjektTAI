using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektTAI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employees empl = new Employees();
            empl.Visible = true;
            Visible = false;
            empl.FormClosing += (s,e) => Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse W = new Warehouse();
            Visible = false;
            W.FormClosing += (s, e) => Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
