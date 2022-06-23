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
            Methods<string>.GetDictionary(type,comboBox1);
            this.type = type;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type);
            dm.FormClosing += (closedSender, closedE) => Methods<string>.GetDictionary(type,comboBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type, (IDictionaries)comboBox1.SelectedItem);
            dm.FormClosing += (closedSender, closedE) => Methods<string>.GetDictionary(type, comboBox1);
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            string url = "";
            if((IDictionaries)comboBox1.SelectedItem is Producent)
                url = "http://localhost:5297/api/Main/DeleteProducent";
            else if ((IDictionaries)comboBox1.SelectedItem is Models)
                url = "http://localhost:5297/api/Main/DeleteModel";
            else if ((IDictionaries)comboBox1.SelectedItem is Type)
                url = "http://localhost:5297/api/Main/DeleteType";

            Methods<IDictionaries>.Deleter(url, (comboBox1.SelectedItem as IDictionaries).Id);
            
            Methods<string>.GetDictionary(type, comboBox1);
        }
    }
}
