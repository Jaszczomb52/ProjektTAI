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
    public partial class Parts : Form
    {
        Zleceny zl;
        List<CzescUzytaDoZlecenium>? cz = null;
        public Parts(Zleceny zl)
        {
            this.zl = zl;
            InitializeComponent();
            Visible = true;
            LoadOnSetup();
            dataGridView1.DataSource = GetCustom();
        }

        void LoadOnSetup() 
        {
            string url = "http://localhost:5297/api/Main/GetZlecCz";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var emp = JsonConvert.DeserializeObject<CzescUzytaDoZlecenium[]>(text);
                    cz = emp.Where(_ => _.idzlecenia == zl.Id).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddReqPart ARP = new AddReqPart(zl.Id);
            Enabled = false;
            ARP.FormClosing += (s, e) => { LoadOnSetup(); dataGridView1.DataSource = GetCustom(); Enabled = true; };
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:5297/api/Main/DeleteZlecCz";

            CzescUzytaDoZlecenium? E = null;
            if (cz == null)
                return;
            try
            {
                E = cz[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = cz[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }
            if (E == null)
                return;
            await Methods<CzescUzytaDoZlecenium>.Deleter(url, E.id);
            LoadOnSetup();
            dataGridView1.DataSource = GetCustom();
        }

        List<CustomCzescUzytaDoZlecenium> GetCustom()
        {
            List<CustomCzescUzytaDoZlecenium> temp = new List<CustomCzescUzytaDoZlecenium>();
            if (cz is null)
                return new List<CustomCzescUzytaDoZlecenium>();
            foreach (CzescUzytaDoZlecenium c in cz)
            {
                c.idczesciNavigation.idmodeluNavigation = Methods<Models>.GetDictionary().dc.Where(_ => _.Id == c.idczesciNavigation.idmodelu).First() as Models;
                c.idczesciNavigation.idtypuNavigation = Methods<Type>.GetDictionary().dc.Where(_ => _.Id == c.idczesciNavigation.idtypu).First() as Type;
                var t = Methods<Producent>.GetDictionary().dc;
                c.idczesciNavigation.idproducentaNavigation = t.Where(_ => _.Id == c.idczesciNavigation.idproducenta).First() as Producent;
            }
            foreach (CzescUzytaDoZlecenium c in cz)
            {
                temp.Add(new CustomCzescUzytaDoZlecenium()
                {
                    id = c.id,
                    idczesciNavigation = c.idczesciNavigation is null ? null : c.idczesciNavigation.ToString(),
                    idzleceniaNavigation = c.idzleceniaNavigation is null ? null : c.idzleceniaNavigation.ToString(),
                    dataWpisu = c.dataWpisu
                });
            }
            return temp;
        }
    }
}
