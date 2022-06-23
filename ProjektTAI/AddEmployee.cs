using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektTAI
{
    public partial class AddEmployee : Form
    {
        string url = "";
        bool update = false;
        Emplo emp { set; get; } = new Emplo();
        public AddEmployee(Emplo emp)
        {
            InitializeComponent();
            Visible = true;
            url = "http://localhost:5297/api/Main/UpdateEmployee";
            update = true;
            this.emp = emp;
            textBox1.Text = emp.Imie;
            textBox2.Text = emp.Nazwisko;
            textBox3.Text = emp.NumerTelefonu;
        }

        public AddEmployee()
        {
            InitializeComponent();
            Visible = true;
            url = "http://localhost:5297/api/Main/AddEmployee";
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            // guard clauses
            if(textBox1.Text.Length == 0)
            {
                MessageBox.Show("Wpisz imię");
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Wpisz nazwisko");
                return;
            }
            if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("Wpisz numer");
                return;
            }

            // creating object from data in Form
            if(emp.Id == 0)
            {
                emp.SpecjalizacjePracownikas = new List<SpecjalizacjePracownika>();
                emp.Zlecenies = new List<Zleceny>();
            };
            emp.Imie = textBox1.Text;
            emp.Nazwisko = textBox2.Text;
            emp.NumerTelefonu = textBox3.Text;

            // sending to API
            Methods<Emplo>.AddOrModify(url, emp,update);
            Close();
        }
    }
}
