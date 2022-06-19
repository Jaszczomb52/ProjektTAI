using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
            LoadOnSetup();
        }

        void LoadOnSetup()
        {
            string url = "http://localhost:5297/api/Main/GetWarehouse";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var emp = JsonConvert.DeserializeObject<CzescNaMagazyny[]>(text);
                    dataGridView1.DataSource = emp;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
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
