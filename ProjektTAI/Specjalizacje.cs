using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektTAI
{
    public partial class Specjalizacje : Form
    {
        Emplo[]? emps = null;
        public Specjalizacje(Emplo[] emps)
        {
            InitializeComponent();
            Visible = true;
            this.emps = emps;
            comboBox1.DataSource = emps;
            LoadEmplo((Emplo)comboBox1.SelectedItem);
            comboBox1.SelectedIndexChanged += (e, s) => { LoadEmplo((Emplo)comboBox1.SelectedItem); };
        }

        void LoadEmplo(Emplo emp)
        {
            ReloadEmplo();
            dataGridView1.DataSource = emp.SpecjalizacjePracownikas.Select(x =>
            new CustomSpecjalizacjePracownika 
            {
                NaprawaSoftu = x.NaprawaSoftu,
                NaprawaCzesci = x.NaprawaCzesci,
                Budowanie = x.Budowanie,
                Diagnostyka = x.Diagnostyka,
            }).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SpecChecker())
                MessageBox.Show("Wpis już istnieje. Zmodyfikuj go lub usuń.");
            else
                CreateAS(false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (SpecChecker())
                CreateAS(true);
            else
                MessageBox.Show("Brak wpisu. Dodaj wpis aby go modyfikować.");
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (SpecChecker())
            {
                Methods<Emplo>.Deleter("http://localhost:5297/api/Main/DeleteSpec", comboBox1.SelectedItem as Emplo is not null ? (comboBox1.SelectedItem as Emplo)!.SpecjalizacjePracownikas[0].Id : -1);
                LoadEmplo((Emplo)comboBox1.SelectedItem);
            }
            else
                MessageBox.Show("Nie ma wpisu do usunięcia.");
        }

        bool SpecChecker()
        {
            return (comboBox1.SelectedItem as Emplo).SpecjalizacjePracownikas.Count == 1 ? true : false;
        }

        void CreateAS(bool modify)
        {
            AddSpec AS = new AddSpec((Emplo)comboBox1.SelectedItem, modify);
            AS.FormClosing += (s, e) => LoadEmplo((Emplo)comboBox1.SelectedItem);
        }

        private void ReloadEmplo()
        {
            var temp = Employees.GetEmplos();
            emps = temp is null ? temp : new Emplo[1];
            temp = null;
        }

    }
}
