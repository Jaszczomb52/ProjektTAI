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
            dict(type);
            this.type = type;
        }

        void dict(string type)
        {
            if (type == "model")
                Methods<Models>.GetDictionary(comboBox1);
            if (type == "producer")
                Methods<Producent>.GetDictionary(comboBox1);
            if (type == "type")
                Methods<Type>.GetDictionary(comboBox1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type);
            dm.FormClosing += (closedSender, closedE) => dict(type);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DictionaryMonit dm = new DictionaryMonit(type, (IDictionaries)comboBox1.SelectedItem);
            dm.FormClosing += (closedSender, closedE) => dict(type);
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

            await Methods<IDictionaries>.Deleter(url, (comboBox1.SelectedItem as IDictionaries)!.Id);
            dict(type);
        }
    }
}
