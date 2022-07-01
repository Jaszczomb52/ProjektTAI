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
using static System.Windows.Forms.ComboBox;

namespace ProjektTAI
{
    public partial class AddPart : Form
    {
        bool modify = false;
        CzescNaMagazyny cz;
        DictList? md = null;
        DictList? pr = null;
        DictList? tp = null;
        string url;

        public AddPart(CzescNaMagazyny cz, bool modify, int[] indexes)
        {
            InitializeComponent();
            Visible = true;
            this.modify = modify;
            this.cz = cz;
            LoadCombos();
            textBox1.Text = cz.kodSegmentu;
            checkBox1.Checked = cz.archiwum;
            comboBox3.SelectedIndex = md.dc.FindIndex(_ => _.Id == indexes[0]);
            comboBox2.SelectedIndex = pr.dc.FindIndex(_ => _.Id == indexes[1]);
            comboBox1.SelectedIndex = tp.dc.FindIndex(_ => _.Id == indexes[2]);
            url = "http://localhost:5297/api/Main/UpdateCzesc";
        }

        public AddPart()
        {
            InitializeComponent();
            Visible = true;
            cz = new CzescNaMagazyny();
            LoadCombos();
            url = "http://localhost:5297/api/Main/AddCzesc";
        }

        private void LoadCombos()
        {
            md = Methods<Models>.GetDictionary(comboBox3);
            pr = Methods<Producent>.GetDictionary(comboBox2);
            tp = Methods<Type>.GetDictionary(comboBox1);
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            cz.idmodelu = (comboBox3.SelectedItem as Models)!.Id;
            cz.idtypu = (comboBox1.SelectedItem as Type)!.Id;
            cz.idproducenta = (comboBox2.SelectedItem as Producent)!.Id;
            cz.kodSegmentu = textBox1.Text;
            cz.archiwum = checkBox1.Checked;
            cz.idmodeluNavigation = null;
            cz.idtypuNavigation = null;
            cz.idproducentaNavigation = null;
            await Methods<CzescNaMagazyny>.AddOrModify(url,cz, modify);
            Close();
        }
    }
}
