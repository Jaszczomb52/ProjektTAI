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
    public partial class AddReq : Form
    {
        Zleceny? zl = null;
        bool modify = false;
        string url = "";
        public AddReq(Zleceny z)
        {
            InitializeComponent();
            Visible = true;
            zl = z;
            zl.Id = z.Id;
            textBox1.Text = z.Email;
            textBox2.Text = z.Imie;
            textBox3.Text = z.Nazwisko;
            textBox5.Text = z.NumerTelefonu ?? "";
            textBox4.Text = z.Koszt.ToString();
            textBox6.Text = z.Status;
            richTextBox1.Text = z.OpisZlecenia;
            dateTimePicker1.Value = z.DataPrzyjecia;
            dateTimePicker2.Value = z.DataWydania ?? DateTime.Today;
            checkBox1.Checked = z.KontaktTelefoniczny;
            checkBox2.Checked = z.SzybkieZlecenie;
            var emp = Employees.GetEmplos().ToList();
            comboBox1.DataSource = emp;
            comboBox1.SelectedIndex = emp.IndexOf(emp.FirstOrDefault(_ => _.Id == z.Idpracownika) ?? new Emplo());
            modify = true;
            url = "http://localhost:5297/api/Main/UpdateReq";
        }

        public AddReq()
        {
            InitializeComponent();
            Visible = true;
            var emp = Employees.GetEmplos().ToList();
            comboBox1.DataSource = emp;
            comboBox1.SelectedIndex = 0;
            url = "http://localhost:5297/api/Main/AddReq";
            zl = new Zleceny();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            if (!Checker())
                return;
            try
            {
                zl.Email = textBox1.Text;
                zl.Imie = textBox2.Text;
                zl.Nazwisko = textBox3.Text;
                zl.NumerTelefonu = textBox5.Text.Trim() == "" ? null : textBox5.Text;
                zl.Koszt = decimal.Parse(textBox4.Text);
                zl.Status = textBox6.Text;
                zl.OpisZlecenia = richTextBox1.Text;
                zl.DataPrzyjecia = dateTimePicker1.Value;
                zl.DataWydania = checkBox3.Checked ? dateTimePicker2.Value : null;
                zl.KontaktTelefoniczny = checkBox1.Checked;
                zl.SzybkieZlecenie = checkBox2.Checked;
                zl.Idpracownika = (comboBox1.SelectedItem as Emplo)!.Id;

                if (!modify)
                    zl.Id = 0;

                int c;

                if (!int.TryParse(textBox5.Text, out c) && textBox5.Text.Trim() != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Wpisany numer telefonu nie jest liczbą, zawiera znaki " +
                        "(przykładowo + na początku) lub zawiera więcej niż 9 cyfr. Czy chcesz kontynuować?", "Błąd konwersji", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                        return;
                }

                await Methods<Zleceny>.AddOrModify(url, zl, modify);
                Close();
            }
            catch
            {
                MessageBox.Show("Upewnij się, że poprawnie wypełnione zostały wszystkie wymagane pola.\n" +
                    "Koszt podaj z przecinkiem, nie wpisuj liter w pola liczbowe.");
            }
            
        }

        bool Checker()
        {
            if  ( 
                textBox1.Text.Trim() == "" ||
                textBox2.Text.Trim() == "" ||
                textBox3.Text.Trim() == "" ||
                textBox4.Text.Trim() == "" ||
                textBox6.Text.Trim() == "" ||
                richTextBox1.Text.Trim() == ""
                )
            {
                MessageBox.Show("Uzupełnij obowiązkowe pola");
                return false;
            }
            return true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                dateTimePicker2.Enabled = true;
            else
                dateTimePicker2.Enabled = false;
        }
    }
}
