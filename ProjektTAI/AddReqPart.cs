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
    public partial class AddReqPart : Form
    {
        int ZlId;
        public AddReqPart(int id)
        {
            ZlId = id;
            InitializeComponent();
            Visible = true;
            string url = "http://localhost:5297/api/Main/GetWarehouse";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var temp = JsonConvert.DeserializeObject<List<CzescNaMagazyny>>(text)!;
                    comboBox2.DataSource = temp;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            int id = (comboBox2.SelectedItem as CzescNaMagazyny)!.id;
            CzescUzytaDoZlecenium temp = new CzescUzytaDoZlecenium()
            {
                id = 0,
                idczesci = id,
                idzlecenia = ZlId,
                dataWpisu = DateTime.Now
            };
            await Methods<CzescUzytaDoZlecenium>.AddOrModify("http://localhost:5297/api/Main/AddZlecCz", temp, false);
            Close();
        }
    }
}
