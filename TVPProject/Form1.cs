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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // ako je kliknuto dugme za registraciju otvara se registraciona forma
        private void button1_Click(object sender, EventArgs e)
        {
            RegistracijaFrm formReg = new RegistracijaFrm();
            formReg.Show();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            bool flagUsername = true; 

            //proveravamo da li uneseni podaci pripadaju administratoru
            List<Administrator> administratori = RadSaDatotekom.Procitaj<Administrator>("administratori.bin");
            foreach (Administrator admin in administratori)
            {
                if (admin.KorisnickoIme == textBox1.Text && admin.Lozinka == textBox2.Text)
                {
                    FormAdminPocetna fap = new FormAdminPocetna(textBox1);
                    fap.Show();
                    flagUsername = false;
                }
            }

            //proveravamo da li uneseni podaci pripadaju registrovanom kupcu
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
            foreach (Kupac k in kupci) {
                if (k.KorisnickoIme == textBox1.Text && k.Lozinka==textBox2.Text) {
                    FormKupac fk = new FormKupac(k);
                    fk.Show();
                    flagUsername = false;
                }
            }

            //ako nesto nije OK onda se izbacuje poruka
            if (flagUsername) {
                MessageBox.Show("Korisnicko ime ili lozinka su netacni ili admin jos uvek nije odobrio nalog!");
            }

        }
    }
}
