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
        List<CzescNaMagazyny>? cz;
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
                    cz = JsonConvert.DeserializeObject<List<CzescNaMagazyny>>(text)!;
                    dataGridView1.DataSource = cz.Select(x => new CustomCzescNaMagazyny
                    {
                        idtypuNavigation = x.idtypuNavigation,
                        idmodeluNavigation = x.idmodeluNavigation,
                        idproducentaNavigation = x.idproducentaNavigation,
                        archiwum = x.archiwum,
                        kodSegmentu = x.kodSegmentu
                    }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btnEvent("producer");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnEvent("model");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            btnEvent("type");
        }

        private void btnEvent(string type)
        {
            EditDictionaries ED = new EditDictionaries(type);
            ED.FormClosing += (closedSender, closedE) => LoadOnSetup();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenAP(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenAP(true);
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            var temp = GetGrid();
            if (temp is null)
                return;
            string url = "http://localhost:5297/api/Main/DeleteCzesc";
            await Methods<CzescNaMagazyny>.Deleter(url, temp.id);
            this.Controls.Clear();
            this.InitializeComponent();
            LoadOnSetup();
        }

        void OpenAP(bool modify)
        {
            if(modify)
            {
                CzescNaMagazyny? E = GetGrid();
                if (E is null)
                    return;
                AddPart AP = new AddPart(E, true, new int[3] {E.idmodelu, E.idproducenta, E.idtypu });
                AP.FormClosing += (s, e) => LoadOnSetup();
            }
            else
            {
                AddPart AP = new AddPart();
                AP.FormClosing += (s, e) => LoadOnSetup();
            }
        }

        CzescNaMagazyny? GetGrid()
        {
            CzescNaMagazyny? E = null;
            if (cz == null)
                return null;
            try
            {
                E = cz[dataGridView1.SelectedRows[0].Index];
                return E;
            }
            catch
            {
                try
                {
                    E = cz[dataGridView1.SelectedCells[0].RowIndex];
                    return E;
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }
            if (E == null)
                return null;
            return null;
        }
    }
}
