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
    public partial class AddPart : Form
    {
        bool modify = false;
        CzescNaMagazyny cz;
        string url;

        public AddPart(CzescNaMagazyny cz, bool modify)
        {
            InitializeComponent();
            Visible = true;
            this.modify = modify;
            this.cz = cz;
            LoadCombos();
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
            Methods<Models>.GetDictionary("model",comboBox3);
            Methods<Producent>.GetDictionary("producer",comboBox2);
            Methods<Type>.GetDictionary("type",comboBox1);
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            cz.idmodeluNavigation = null;
            cz.idtypuNavigation = null;
            cz.idproducentaNavigation = null;
            Methods<CzescNaMagazyny>.AddOrModify(url,cz, modify);
            Close();
        }
    }
}
