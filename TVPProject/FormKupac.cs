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
    partial class FormKupac : Form
    {

        //inicijalizujemo kupca za koga pretrazujemo rezervacije
        Kupac k;
        List<Rezervacije> rezervacije = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
        public FormKupac(Kupac kupac)
        {
            InitializeComponent();
            this.k = kupac;
            label4.Text = kupac.KorisnickoIme;
        }


        //dugme koje vodi na rezervacije
        private void button2_Click(object sender, EventArgs e)
        {
            FormRezervacije frmRz = new FormRezervacije(label4);
            frmRz.Show();
            this.Close();
        }


        //BRISANJE REZERVACIJE
        //prolazimo kroz listu rezervacija i poredimo IdAutaRez i celiju iz datagridvew1 u kojoj se nalazi id automobila  
        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            MessageBox.Show(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
            for (int i = 0; i < rezervacije.Count; i++)
            {
                if (rezervacije[i].IdAutaRez == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    //ako se poklapaju rezervacija se brise i iznova se lista upisuje u datoteku
                    rezervacije.RemoveAt(i);
                    MessageBox.Show("Rezervacija je uspesno uklonjena...");
                }
            }

            RadSaDatotekom.Upisi(rezervacije, "rezervacije.bin");
            this.FormKupac_Load(this, e);
        }

        //PRIKAZ REZERVACIJA
        //kada se prikaze forma onda se prikazuje kupac i sve njegove rezervacije
        private void FormKupac_Load(object sender, EventArgs e)
        {
            //pravimo listu u koju smestamo kupca ciji je id jednak idKupca u rezervacijama
            List<Rezervacije> rezPom = new List<Rezervacije>();

            for (int i = 0; i < rezervacije.Count; i++)
            {
                if (k.Id == rezervacije[i].IdKupca)
                {
                    rezPom.Add(rezervacije[i]);
                }
            }
            //i prikazujmo ga u datagridvew
            dataGridView1.DataSource = rezPom;
            dataGridView1.Refresh();

        }
            
            
            
            
           
        
    }
}
