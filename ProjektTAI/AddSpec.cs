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
    public partial class AddSpec : Form
    {
        bool modify;
        Emplo em;
        string url = "http://localhost:5297/api/Main/AddSpec";
        public AddSpec(Emplo em, bool modify)
        {
            InitializeComponent();
            this.modify = modify;
            this.em = em;
            Text = $"Edycja {em}";
            Visible = true;
            if(modify)
            {
                textBox1.Text = (em.SpecjalizacjePracownikas![0]).NaprawaSoftu.ToString();
                textBox2.Text = (em.SpecjalizacjePracownikas![0]).NaprawaCzesci.ToString();
                textBox3.Text = (em.SpecjalizacjePracownikas![0]).Diagnostyka.ToString();
                textBox4.Text = (em.SpecjalizacjePracownikas![0]).Budowanie.ToString();
                url = "http://localhost:5297/api/Main/UpdateSpec";
            }
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "")
            {
                MessageBox.Show("Wpisz wszystkie wartości");
                return;
            }
            else if (int.Parse(textBox1.Text) > 5 ||
                int.Parse(textBox2.Text) > 5 ||
                int.Parse(textBox3.Text) > 5 ||
                int.Parse(textBox4.Text) > 5)
            {
                MessageBox.Show("Wartości nie mogą być większe od 5");
                return;
            }
            else if (int.Parse(textBox1.Text) <= 0 ||
                int.Parse(textBox2.Text) <= 0 ||
                int.Parse(textBox3.Text) <= 0 ||
                int.Parse(textBox4.Text) <= 0)
            {
                MessageBox.Show("Wartości nie mogą być mniejsze od 1");
                return;
            }
            if (!modify)
                em.SpecjalizacjePracownikas!.Add(new SpecjalizacjePracownika()
                {
                    Id = 0,
                    NaprawaSoftu = int.Parse(textBox1.Text),
                    NaprawaCzesci = int.Parse(textBox2.Text),
                    Diagnostyka = int.Parse(textBox3.Text),
                    Budowanie = int.Parse(textBox4.Text),
                    Idpracownika = em.Id
                });
            else
            {
                em.SpecjalizacjePracownikas![0].NaprawaSoftu = int.Parse(textBox1.Text);
                em.SpecjalizacjePracownikas![0].NaprawaCzesci = int.Parse(textBox2.Text);
                em.SpecjalizacjePracownikas![0].Diagnostyka = int.Parse(textBox3.Text);
                em.SpecjalizacjePracownikas![0].Budowanie = int.Parse(textBox4.Text);
            }
            em.SpecjalizacjePracownikas[0].IdpracownikaNavigation = null;
            await Methods<SpecjalizacjePracownika>.AddOrModify(url,em.SpecjalizacjePracownikas[0], modify);
            Close();
        }
    }
}
