using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVPProject
{
    public partial class FormAdminRezervacije : Form
    {
        List<Rezervacije> rezervacije = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
        List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
        List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
        public FormAdminRezervacije()
        {
            InitializeComponent();
            foreach (Kupac k in kupci)
            {
                comboBox1.Items.Add(k.Id);
            }

            foreach (Automobil a in automobili)
            {
                comboBox2.Items.Add(a.Id);
            }
        }


        //proverava se chekirani radiobutton i na osnovu njega se bira da li se filtriraju rezervacije po kupcu ili automobilu
        private void button3_Click(object sender, EventArgs e)
        {
            List<Rezervacije> rezervacije2 = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");

            if (radioButton1.Checked) {
                    List<Rezervacije> rezPom = new List<Rezervacije>();
                   
                    for (int i = 0; i < rezervacije2.Count; i++)
                    {
                        if (comboBox1.Text == rezervacije2[i].IdKupca.ToString())
                        {
                            rezPom.Add(rezervacije2[i]);
                        }
                    }
                    if (rezPom.Count < 1) {
                        MessageBox.Show("Ne postoje rezervacije za zeljenu osobu");
                    }
                    dataGridView2.DataSource = rezPom;
                    dataGridView2.Refresh();
            }

            if (radioButton2.Checked) {
                List<Rezervacije> rezPom2 = new List<Rezervacije>();
                for (int i = 0; i < rezervacije.Count; i++) {
                    if (comboBox2.Text == rezervacije[i].IdAutaRez.ToString()) {
                        rezPom2.Add(rezervacije[i]);
                    }
                }
                if (rezPom2.Count < 1)
                {
                    MessageBox.Show("Ne postoje rezervacije za zeljeni automobil");
                }
                dataGridView2.DataSource = rezPom2;
                dataGridView2.Refresh();
            }

        }

       
        //na promenu automobila u comboboxu ispisuje se kupac u textboxu sa strane
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < kupci.Count; i++)
                {
                    if (comboBox1.Text == kupci[i].Id.ToString())
                    {
                        textBox2.Text = kupci[i].ToString();
                    }

                }

        }

        //na promenu automobila u comboboxu ispisuje se automobil u textboxu sa strane
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < automobili.Count; i++)
            {
                if (comboBox2.Text == automobili[i].Id.ToString())
                {
                    textBox3.Text = automobili[i].ToString();
                }

            }

           
        }


        //BRISANJE REZERVACIJE
        private void button2_Click(object sender, EventArgs e)
        {

            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            for (int i = 0; i < rezervacije.Count; i++)
            {
                if (rezervacije[i].IdAutaRez == int.Parse(dataGridView2.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    rezervacije.RemoveAt(i);
                }
            }

            RadSaDatotekom.Upisi(rezervacije, "rezervacije.bin");
            MessageBox.Show("Rezervacija je uspesno uklonjena");
        }
    }

    
}
