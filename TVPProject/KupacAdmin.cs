using System;
using System.Collections;
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
    public partial class KupacAdmin : Form
    {
        public KupacAdmin(Label l)
        {
            InitializeComponent();
            label13.Text = l.Text.ToUpper();
            Load += new EventHandler(KupacAdmin_Load);
        }
        

        private void KupacAdmin_Load(object sender, EventArgs e)
        {
            //izlistavamo neregistrovane kupce i sortiramo ih
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
            for (int i = 0; i < kupci.Count - 1; i++)
            {
                for (int j = i + 1; j < kupci.Count; j++)
                {
                    if (kupci[i].Id > kupci[j].Id)
                    {
                        Kupac pom = kupci[i];
                        kupci[i] = kupci[j];
                        kupci[j] = pom;
                    }
                }
            }
            dataGridView1.DataSource = kupci;
            //formatiramo datum u datagridview
            dataGridView1.Columns["DatumRodjenja"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Refresh();


            //citamo registrovane kupce i sortiramo ih
            List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
            for (int i = 0; i < kupciReg.Count - 1; i++)
            {
                for (int j = i + 1; j < kupciReg.Count; j++)
                {
                    if (kupciReg[i].Id > kupciReg[j].Id)
                    {
                        Kupac pom = kupciReg[i];
                        kupciReg[i] = kupciReg[j];
                        kupciReg[j] = pom;
                    }
                }
            }
            
            dataGridView2.DataSource = kupciReg;
            //formatiramo datum u datagridview
            dataGridView2.Columns["DatumRodjenja"].DefaultCellStyle.Format = "dd.MM.yyyy";
            try
            {
                dataGridView2.Rows[0].Selected = false;
            }
            catch(Exception ex) {
              
            }
            dataGridView2.Refresh();

        }



        //DODAVANJE NALOGA
        private void button1_Click(object sender, EventArgs e)
        {
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
            List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                for (int i = 0; i < kupci.Count; i++)
                {
                    if (kupci[i].Id.ToString() == dataGridView1.Rows[rowIndex].Cells[0].Value.ToString())
                    {
                        kupciReg.Add(kupci[i]);
                        kupci.RemoveAt(i);
                    }

                }
                RadSaDatotekom.Upisi(kupciReg, "kupciReg.bin");
                RadSaDatotekom.Upisi(kupci, "kupci.bin");
                
            }
            catch(Exception exc){
                    
            }
            this.KupacAdmin_Load(this, e);


        }


        //ODBIJANJE NALOGA
        private void button2_Click(object sender, EventArgs e)
        {
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < kupci.Count; i++)
            {
                if (kupci[i].Id == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    kupci.RemoveAt(i);
                }
            }

            RadSaDatotekom.Upisi(kupci, "kupci.bin");
            this.KupacAdmin_Load(this, e);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

        }


        //IZMENA KUPCA
        private void button3_Click(object sender, EventArgs e)
        {
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
            List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView2.Rows[0].Selected = false;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                
                for (int i = 0; i < kupci.Count; i++)
                {
                    if (kupci[i].Id.ToString() == dataGridView1.Rows[rowIndex].Cells[0].Value.ToString())
                    {
                       
                        textBox2.Text = kupci[i].Ime;
                        textBox3.Text = kupci[i].Prezime;
                        textBox4.Text = kupci[i].Jmbg;
                        dateTimePicker1.Value = kupci[i].DatumRodjenja;
                        textBox5.Text = kupci[i].BrojTelefona;
                        textBox6.Text = kupci[i].KorisnickoIme;
                        textBox7.Text = kupci[i].Lozinka;
                    }
                }
                
            }
            else if (dataGridView2.SelectedRows.Count > 0) {
                dataGridView1.Rows[0].Selected = false;
                int rowIndex2 = dataGridView2.CurrentCell.RowIndex;
                
                for (int j = 0; j < kupciReg.Count; j++)
                {
                    if (kupciReg[j].Id.ToString() == dataGridView2.Rows[rowIndex2].Cells[0].Value.ToString())
                    {
                        
                        textBox2.Text = kupciReg[j].Ime;
                        textBox3.Text = kupciReg[j].Prezime;
                        textBox4.Text = kupciReg[j].Jmbg;
                        dateTimePicker1.Value = kupciReg[j].DatumRodjenja;
                        textBox5.Text = kupciReg[j].BrojTelefona;
                        textBox6.Text = kupciReg[j].KorisnickoIme;
                        textBox7.Text = kupciReg[j].Lozinka;
                    }
                }
                
            }
            
        }



        //CUVANJE IZMENE
        private void button5_Click(object sender, EventArgs e)
        {
            List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupci.bin");
            List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
            bool unetoSve = true;
            
            for (int i = 0; i < kupci.Count; i++) {
                
                if (kupci[i].KorisnickoIme == textBox6.Text) {
                    MessageBox.Show("Uneto korisnicko ime je zauzeto");
                    unetoSve = false;
                }
            }

            for (int i = 0; i < kupciReg.Count; i++)
            {
                if (kupciReg[i].KorisnickoIme == textBox6.Text)
                {
                    MessageBox.Show("Uneto korisnicko ime je zauzeto");
                    unetoSve = false;
                }
            }


            //PROVERE
            if (textBox2.Text.Count() < 1)
            {
                MessageBox.Show("Niste uneli ime!");
                textBox2.BackColor = Color.Red;
                unetoSve = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Morate uneti samo karaktere");
            }


            if (textBox3.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli prezime!");
                textBox3.BackColor = Color.Red;
                unetoSve = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Morate uneti samo karaktere");
            }



            if (textBox5.Text.Length > 8 && textBox5.Text.Length <= 11)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]+$"))
                {
                }
                else
                {
                    MessageBox.Show("Uneti broj nije validan -> Moraju biti uneseni samo brojevi!");
                    unetoSve = false;
                }
            }
            else
            {
                MessageBox.Show("Uneti broj je izvan opsega -> Broj cifara telefona mora biti izmedju 8 i 11!");
                unetoSve = false;
            }





            if ((textBox4.Text.Length == 13) && (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$")))
            {
            }
            else
            {
                MessageBox.Show("JMBG mora sadrzati samo brojeve od 13 cifara!");
                unetoSve = false;
            }




            //PROVERA DA LI SE UNETI DATUM POKLAPA SA JMBG
            string dateAndTime = dateTimePicker1.Value.ToShortDateString();
            string[] listDate = dateAndTime.Split('.');
            Int32 godinaProvera = Int32.Parse(listDate[2]);
            if ((DateTime.Now.Year - godinaProvera) < 18)
            {
                MessageBox.Show("Morate imati vise od 18 godina da bi ste se registrovali");
                unetoSve = false;
            }
            Int32 mesecInt = Int32.Parse(listDate[1]);
            Int32 danInt = Int32.Parse(listDate[0]);
            if (mesecInt < 10)
            {
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




            //ako je uneto sve onda se pravi novi kupac,a stari se brise
            if (unetoSve) {
                //IZMENA ZA NEREGISTROVANE KUPCE
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    MessageBox.Show(rowIndex.ToString());
                    for (int i = 0; i < kupci.Count; i++)
                    {
                        if (kupci[i].Id == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                        {
                            
                            Kupac kupacPom = new Kupac(kupci[i].Id, textBox2.Text, textBox3.Text, textBox4.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text, textBox7.Text);
                            kupci.RemoveAt(i);
                            kupci.Insert(i, kupacPom);
                        }
                    }
                    RadSaDatotekom.Upisi(kupci, "kupci.bin");
                    MessageBox.Show("Neregistrovani kupac je uspesno izmenjen!");
                }

                //IZMENA ZA REGISTROVANE KUPCE
                else if (dataGridView2.SelectedRows.Count > 0)
                {
                    button5.Visible = false;
                    int rowIndex2 = dataGridView2.CurrentCell.RowIndex;
                    for (int j = 0; j < kupciReg.Count; j++)
                    {
                        if (kupciReg[j].Id == int.Parse(dataGridView2.Rows[rowIndex2].Cells[0].Value.ToString()))
                        {
                            Kupac kupacPom = new Kupac(kupciReg[j].Id, textBox2.Text, textBox3.Text, textBox4.Text, dateTimePicker1.Value, textBox5.Text, textBox6.Text, textBox7.Text);
                            kupciReg.RemoveAt(j);

                            kupciReg.Insert(j, kupacPom);
                        }

                    }
                    RadSaDatotekom.Upisi(kupciReg, "kupciReg.bin");
                    MessageBox.Show("Registrovani kupac je uspesno izmenjen!");
                }
            }
            this.KupacAdmin_Load(this, e);
        }
        

        //BRISANJE REGISTROVANOG KORISNIKA
        private void button6_Click(object sender, EventArgs e)
        {
            List<Kupac> kupciReg = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");

            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            for (int i = 0; i < kupciReg.Count; i++)
            {
                if (kupciReg[i].Id == int.Parse(dataGridView2.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    kupciReg.RemoveAt(i);
                }
            }
            RadSaDatotekom.Upisi(kupciReg, "kupciReg.bin");
            MessageBox.Show("Registrovani korisnik je uspesno uklonjen.");
            this.KupacAdmin_Load(this, e);
            
        }
    }
}
