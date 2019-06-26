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
    public partial class RegistracijaFrm : Form
    {
        public RegistracijaFrm()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
        }
        

        //REGISTRACIJA
        private void button1_Click(object sender, EventArgs e)
        {
            //AKO JE unetoSve==true tek se onda korisnik moze registrovati
            bool unetoSve = true;
            //PROVERE
            if (textBox1.Text.Count() < 1) {
                MessageBox.Show("Niste uneli ime!");
                textBox1.BackColor = Color.Red;
                unetoSve = false;
            } else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Morate uneti samo karaktere");
            }

            string dateAndTime = dateTimePicker1.Value.ToShortDateString();
            string[] listDate = dateAndTime.Split('.');
            Int32 godinaProvera = Int32.Parse(listDate[2]);
            if ((DateTime.Now.Year - godinaProvera) < 18) {
                MessageBox.Show("Morate imati vise od 18 godina da bi ste se registrovali");
                unetoSve = false;
            }
            Int32 mesecInt = Int32.Parse(listDate[1]);
            Int32 danInt = Int32.Parse(listDate[0]);
            if (mesecInt < 10) {
                listDate[1] = "0" + listDate[1];
            }
            if (danInt < 10)
            {
                listDate[0] = "0" + listDate[0];
            }
            string godinaSplitZbog3 = listDate[2].Substring(1, 3);
            string dan = textBox4.Text.Substring(0, 2);
            string mesec = textBox4.Text.Substring(2, 2);
            string godina = textBox4.Text.Substring(4, 3);
            if (listDate[0] != dan || listDate[1] != mesec || godinaSplitZbog3 != godina)
            {
                MessageBox.Show("Datum rodjenja i podaci iz JMBG se ne poklapaju!");
                unetoSve = false;
            }


            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli prezime!");
                textBox2.BackColor = Color.Red;
                unetoSve = false;
            }else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Morate uneti samo karaktere");
            }
            

            if (textBox4.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti JMBG!");
                textBox4.BackColor = Color.Red;
                unetoSve = false;
            }

            if ((textBox4.Text.Length == 13) && (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$")))
            {
            }
            else {
                MessageBox.Show("JMBG mora sadrzati samo brojeve od 13 cifara!");
                unetoSve = false;
            }

            if (textBox5.Text.Count() < 1)
            {
                MessageBox.Show("Morate uneti broj telefona!");
                textBox5.BackColor = Color.Red;
                unetoSve = false;
            }
            if (textBox5.Text.Length > 8 && textBox5.Text.Length <= 11)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]+$")){
                }
                else
                {
                    MessageBox.Show("Uneti broj nije validan -> Moraju biti uneseni samo brojevi!");
                    unetoSve = false;
                }

            }
            else {
                MessageBox.Show("Uneti broj je izvan opsega -> Broj cifara telefona mora biti izmedju 8 i 11!");
                unetoSve = false;
            }
            if (textBox6.Text.Count() < 1)
            {
                MessageBox.Show("Morate uneti korisnicko ime!");
                textBox6.BackColor = Color.Red;
                unetoSve = false;
            }
            if (textBox3.Text.Count() < 1)
            {
                MessageBox.Show("Morate uneti lozinku!");
                textBox3.BackColor = Color.Red;
                unetoSve = false;
            }
            if (textBox7.Text.Count() < 1)
            {
                MessageBox.Show("Morate ponoviti lozinku!");
                textBox7.BackColor = Color.Red;
                unetoSve = false;
            }
            if (textBox3.Text != textBox7.Text) {
                MessageBox.Show("Ponovljena lozinka nije jednaka prethodno unesenoj lozinci!");
                unetoSve = false;
            }

            if (textBox3.Text.Length < 5) {
                MessageBox.Show("Lozinka mora imati minimum 5 karaktera");
                unetoSve = false;
            }

            //AKO JE SVE POPUNJENO
            if (unetoSve == true) {
                List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
                List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
                List<Kupac> zajednickaLista = new List<Kupac>();
                //DODAJEMO REGISTROVANE I NEREGISTROVANE KUPCE U ZAJEDNICKU LISTU DA BI ODREDILI JEDINSTVENI ID ZA KUPCA KOJI SE REGISTRUJE
                for (int i = 0; i < kupci.Count; i++)
                {
                    zajednickaLista.Add(kupci[i]);
                }
                for (int j = 0; j < kupciReg.Count; j++)
                {
                    zajednickaLista.Add(kupciReg[j]);
                }

                int maxID=0;
                for (int i = 0; i < zajednickaLista.Count; i++) {
                    if (zajednickaLista[i].Id > maxID) {
                        maxID = zajednickaLista[i].Id;
                    }
                }

                //PRAVIMO KUPCA I DODELJUJEMO MU JEDINSTVENI ID
                Kupac k = new Kupac(maxID+1, textBox1.Text, textBox2.Text, textBox4.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text, textBox7.Text);
                
                
                bool sveOK = true;
                //PROVERAVAMO DA LI JE KORISNICKO IME KOD NEREGISTROVANIH KUPACA ZAUZETO
                for (int i = 0; i < kupci.Count; i++) {
                    if (kupci[i].KorisnickoIme == k.KorisnickoIme) {
                        MessageBox.Show("Korisnicko ime je vec zauzeto");
                        textBox6.BackColor = Color.Red;
                        sveOK = false;
                    }
                }

                //PROVERAVAMO DA LI JE KORISNICKO IME KOD REGISTROVANIH KUPACA ZAUZETO
                for (int j = 0; j < kupciReg.Count; j++) {
                    if (kupciReg[j].KorisnickoIme == k.KorisnickoIme) {
                        MessageBox.Show("Korisnicko ime je vec zauzeto");
                        textBox6.BackColor = Color.Red;
                        sveOK = false;
                    }
                }

                //PROVERAVAMO DA LI JE KORISNICKO IME KOD ADMINA ZAUZETO
                List<Administrator> administratori = RadSaDatotekom.Procitaj<Administrator>("administratori.bin");
                for (int i = 0; i < administratori.Count; i++) {
                    if (administratori[i].KorisnickoIme == k.KorisnickoIme) {
                        MessageBox.Show("Korisnicko ime je vec zauzeto");
                        textBox6.BackColor = Color.Red;
                        sveOK = false;
                    }
                }

                //AKO JE SVE OK, DODAJEMO KUPCA
                if (sveOK) {
                    MessageBox.Show("Uspesno ste se registrovali, samo morate sacekati da admin odobri nalog");
                    kupci.Add(k);
                    RadSaDatotekom.Upisi(kupci, "kupci.bin");
                }
            }
        }

        
        //PROVERE NA PROMENU TEKSTA
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if((textBox4.Text.Length == 13) && (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$")))
                {
                textBox4.BackColor = Color.Green;
            }
            else {
                textBox4.BackColor = Color.Red;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length > 8 && textBox5.Text.Length <= 11)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]+$"))
                {
                    textBox5.BackColor = Color.Green;
                }
                else
                {
                    textBox5.BackColor = Color.Red;
                }
            }
            else
            {
                textBox5.BackColor = Color.Red;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]+$"))
            {
                textBox1.BackColor = Color.Green;
            }
            else {
                textBox1.BackColor = Color.Red;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
            {
                textBox2.BackColor = Color.Green;
            }
            else
            {
                textBox2.BackColor = Color.Red;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox7.Text)
            {
                textBox7.BackColor = Color.Green;
                textBox3.BackColor = Color.Green;
            }
            else {
                textBox7.BackColor = Color.Red;
                textBox3.BackColor = Color.Red;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length < 5)
            {
                textBox3.BackColor = Color.Red;
            }
            else {
                textBox3.BackColor = Color.Green;
            }
        }
    }
}
