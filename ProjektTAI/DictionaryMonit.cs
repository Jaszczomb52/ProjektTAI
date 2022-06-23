using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektTAI
{
    public partial class DictionaryMonit : Form
    {
        string url = "";
        IDictionaries? obj = null;
        bool update = false;

        public DictionaryMonit(string type, IDictionaries dict)
        {
            InitializeComponent();
            Visible = true;
            if (type == "model")
            {
                obj = (Models)dict;
                label1.Text = "Wpisz model";
                textBox1.Text = (dict as Models).Model;
                url = "http://localhost:5297/api/Main/UpdateModel";
            }
            else if (type == "producer")
            {
                obj = (Producent)dict;
                label1.Text = "Wpisz nazwę producenta";
                textBox1.Text = (dict as Producent).Nazwa;
                url = "http://localhost:5297/api/Main/UpdateProducent";
            }
            else if (type == "type")
            {
                obj = (Type)dict;
                label1.Text = "Wpisz typ";
                textBox1.Text = (dict as Type).Typ;
                url = "http://localhost:5297/api/Main/UpdateType";
            }
            update = true;
        }
        public DictionaryMonit(string type)
        {
            InitializeComponent();
            Visible = true;
            if (type == "model")
            {
                obj = new Models();
                label1.Text = "Wpisz model";
                url = "http://localhost:5297/api/Main/AddModel";
            }
            else if (type == "producer")
            {
                obj = new Producent();
                label1.Text = "Wpisz nazwę producenta";
                url = "http://localhost:5297/api/Main/AddProducent";
            }
            else if (type == "type")
            {
                obj = new Type();
                label1.Text = "Wpisz typ";
                url = "http://localhost:5297/api/Main/AddType";
            }
            if (obj == null)
                return;
            obj.CzescNaMagazynies = new List<CzescNaMagazyny>();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Pole nie może być puste");
                return;
            }

            if (obj is Models)
                Methods<Models>.AddOrModify(url, update ?
                    new Models() {Model = textBox1.Text,Id = obj.Id } :
                    new Models() { Model = textBox1.Text }, update);
            else if (obj is Producent)
                Methods<Producent>.AddOrModify(url, update ?
                    new Producent() { Nazwa = textBox1.Text, Id = obj.Id } :
                    new Producent() { Nazwa = textBox1.Text }, update);
            else if (obj is Type)
                Methods<Type>.AddOrModify(url, update ?
                    new Type() { Typ = textBox1.Text, Id = obj.Id } :
                    new Type() { Typ = textBox1.Text }, update);

        }
    }
}
