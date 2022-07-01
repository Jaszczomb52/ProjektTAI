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
            Enabled = false;
            AE.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
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
            Enabled = false;
            AE.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
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
            await Methods<Emplo>.Deleter(url, E.Id);
            LoadOnSetup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Specjalizacje S = new Specjalizacje(employees!);
            Enabled = false;
            S.FormClosing += (s, e) => { LoadOnSetup(); Enabled = true; };
        }

        public static Emplo[] GetEmplos()
        {
            string url = "http://localhost:5297/api/Main/Employees";
            using (WebClient client = new WebClient())
            {
                try
                {
                    Thread.Sleep(20);
                    string text = Encoding.UTF8.GetString(client.DownloadData(url));
                    var emp = JsonConvert.DeserializeObject<Emplo[]>(text);
                    return emp!;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null!;
                }
            }
        }
    }
}
