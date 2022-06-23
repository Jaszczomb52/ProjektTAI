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
using Newtonsoft.Json;

namespace ProjektTAI
{
    public partial class Employees : Form
    {
        Emplo[]? employees = null; 
        public Employees()
        {
            InitializeComponent();
            LoadOnSetup();
        }

        void LoadOnSetup()
        {
            employees = GetEmplos();
            employees = employees is null ? employees = new Emplo[1] : employees;
            dataGridView1.DataSource = employees;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee AE = new AddEmployee();
            AE.FormClosing += AE_FormClosing;
            Visible = false;
        }

        private void AE_FormClosing(object? sender, FormClosingEventArgs e)
        {
            LoadOnSetup();
            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Emplo? E = null;
            if (employees == null)
                return;
            try
            {
                E = employees[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = employees[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }
            if (E == null)
                return;
            AddEmployee AE = new AddEmployee(E);
            AE.FormClosing += AE_FormClosing;
            Visible = false;
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            Emplo? E = null;

            if (employees == null)
                return;
            try
            {
                E = employees[dataGridView1.SelectedRows[0].Index];
            }
            catch
            {
                try
                {
                    E = employees[dataGridView1.SelectedCells[0].RowIndex];
                }
                catch
                {
                    MessageBox.Show("Zaznacz wiersz");
                }
            }

            if (E == null)
                return;

            string url = "http://localhost:5297/api/Main/DeleteEmployee";
            Methods<Emplo>.Deleter(url, E.Id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Specjalizacje S = new Specjalizacje(employees);
            S.FormClosing += (s, e) => LoadOnSetup();
        }

        public static Emplo[] GetEmplos()
        {
            string url = "http://localhost:5297/api/Main/Employees";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var emp = JsonConvert.DeserializeObject<Emplo[]>(text);
                    return emp;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            }
        }
    }
}
