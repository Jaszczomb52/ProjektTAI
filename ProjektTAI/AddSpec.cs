﻿using System;
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
        public AddSpec(Emplo em, bool modify)
        {
            InitializeComponent();
            this.modify = modify;
            this.em = em;
            Text = $"Edycja {em}";
            Visible = true;
            if(modify)
            {
                textBox1.Text = (em.SpecjalizacjePracownikas[0]).NaprawaSoftu.ToString();
                textBox2.Text = (em.SpecjalizacjePracownikas[0]).NaprawaCzesci.ToString();
                textBox3.Text = (em.SpecjalizacjePracownikas[0]).Diagnostyka.ToString();
                textBox4.Text = (em.SpecjalizacjePracownikas[0]).Budowanie.ToString();
            }
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            if(!modify)
                em.SpecjalizacjePracownikas.Add(new SpecjalizacjePracownika()
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
                em.SpecjalizacjePracownikas[0].NaprawaSoftu = int.Parse(textBox1.Text);
                em.SpecjalizacjePracownikas[0].NaprawaCzesci = int.Parse(textBox2.Text);
                em.SpecjalizacjePracownikas[0].Diagnostyka = int.Parse(textBox3.Text);
                em.SpecjalizacjePracownikas[0].Budowanie = int.Parse(textBox4.Text);
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res;
                try
                {
                    res = modify ? 
                        await client.PutAsJsonAsync("http://localhost:5297/api/Main/UpdateSpec", (SpecjalizacjePracownika)em.SpecjalizacjePracownikas[0]): 
                        await client.PostAsJsonAsync("http://localhost:5297/api/Main/AddSpec", (SpecjalizacjePracownika)em.SpecjalizacjePracownikas[0]);

                    if (res.IsSuccessStatusCode)
                        MessageBox.Show(await res.Content.ReadAsStringAsync());
                    else
                        MessageBox.Show("Nieprawidłowe wywołanie");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Close();
            }
        }
    }
}
