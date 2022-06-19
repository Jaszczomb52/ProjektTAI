using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using System.Net;
using Newtonsoft.Json;

namespace ProjektTAI
{
    public partial class EditDictionaries : Form
    {
        string type = "";
        public EditDictionaries(string type)
        {
            InitializeComponent();
            Visible = true;
            GetDictionary(type);
            this.type = type;
        }

        async private void GetDictionary(string type)
        {
            string url = "";
            if (type == "model")
                url = "http://localhost:5297/api/Main/GetModels";
            if (type == "producer")
                url = "http://localhost:5297/api/Main/GetProducents";
            if (type == "type")
                url = "http://localhost:5297/api/Main/GetTypes";

            using (WebClient client = new WebClient())
            {
                try
                {
                    IDictionaries[]? emp = null;
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    if (type == "model")
                        emp = JsonConvert.DeserializeObject<Models[]>(text);
                    else if (type == "producer")
                        emp = JsonConvert.DeserializeObject<Producent[]>(text);
                    else if (type == "type")
                        emp = JsonConvert.DeserializeObject<Type[]>(text);
                    if (emp == null)
                        throw new NoNullAllowedException();
                    foreach(IDictionaries x in emp)
                    {
                        comboBox1.Items.Add(x);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type, (IDictionaries)comboBox1.SelectedItem);
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res;
                try
                {
                    string url = "";
                    if((IDictionaries)comboBox1.SelectedItem is Producent)
                        url = "http://localhost:5297/api/Main/DeleteProducent";
                    else if ((IDictionaries)comboBox1.SelectedItem is Models)
                        url = "http://localhost:5297/api/Main/DeleteModel";
                    else if ((IDictionaries)comboBox1.SelectedItem is Type)
                        url = "http://localhost:5297/api/Main/DeleteType";

                    res = await client.DeleteAsync(url + "/" + (comboBox1.SelectedItem as IDictionaries).Id.ToString());
                    if (res.IsSuccessStatusCode)
                        MessageBox.Show(await res.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
