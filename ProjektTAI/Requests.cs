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
    public partial class Requests : Form
    {
        Zleceny[]? req = null;
        public Requests()
        {
            InitializeComponent();
            Visible = true;
            LoadOnSetup();
        }

        void LoadOnSetup()
        {
            req = GetReq();
            req = req ?? new Zleceny[1];
            dataGridView1.DataSource = req;
        }

        public static Zleceny[] GetReq()
        {
            string url = "http://localhost:5297/api/Main/GetReq";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var emp = JsonConvert.DeserializeObject<Zleceny[]>(text);
                    return emp!;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null!;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddReq AE = new AddReq();
            Enabled = false;
            AE.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zleceny? E = null;

            if (req == null)
                return;
            try
            {
                E = req[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = req[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }


            if (E == null)
                return;
            AddReq AE = new AddReq(E);
            Enabled = false;
            AE.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            Zleceny? E = null;

            if (req == null)
                return;
            try
            {
                E = req[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = req[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }


            if (E == null)
                return;

            string url = "http://localhost:5297/api/Main/DeleteReq";
            await Methods<Zleceny>.Deleter(url, E.Id);
            LoadOnSetup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Zleceny? E = null;

            if (req == null)
                return;
            try
            {
                E = req[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = req[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }

            if (E == null)
                return;

            Parts P = new Parts(E);
            Enabled = false;
            P.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
        }
    }
}
