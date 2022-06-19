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
    public partial class Warehouse : Form
    {
        public Warehouse()
        {
            InitializeComponent();
            Visible = true;
        }

        void LoadOnSetup()
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditDictionaries ED = new EditDictionaries("producer");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditDictionaries ED = new EditDictionaries("model");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            EditDictionaries ED = new EditDictionaries("type");
        }
    }
}
