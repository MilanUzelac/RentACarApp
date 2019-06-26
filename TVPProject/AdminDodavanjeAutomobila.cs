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
    public partial class AdminDodavanjeAutomobila : Form
    {
        
        public AdminDodavanjeAutomobila(Label l)
        {
            InitializeComponent();
            label13.Text = l.Text;
            comboBox1.Items.Add("Prednji");
            comboBox1.Items.Add("Zadnji");
            comboBox1.Items.Add("4x4");

            comboBox2.Items.Add("Automatik");
            comboBox2.Items.Add("Manuelni");

            comboBox3.Items.Add("Limuzina");
            comboBox3.Items.Add("Karavan");
            comboBox3.Items.Add("Hečbek");

            comboBox4.Items.Add("Dizel");
            comboBox4.Items.Add("Benzin");
            comboBox4.Items.Add("Plin");

            comboBox5.Items.Add(3);
            comboBox5.Items.Add(5);

        }

        private void AdminDodavanjeAutomobila_Load(object sender, EventArgs e)
        {
            //na load popunjavamo datagridview automobilima
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            dataGridView1.DataSource = automobili;
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //provere
            bool unetoSve = true;
            
            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli marku!");
                textBox2.BackColor = Color.Red;
                unetoSve = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Morate uneti samo karaktere kod marke automobila!");
                unetoSve = false;
            }


            if (textBox3.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli model!");
                textBox3.BackColor = Color.Red;
                unetoSve = false;
            }
            


            if (textBox4.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli godiste automobila!");
                textBox4.BackColor = Color.Red;
                unetoSve = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Godiste mora biti celi broj!");
                unetoSve = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$")) {
                if (Int32.Parse(textBox4.Text) <= 1950)
                {
                    MessageBox.Show("Ne koristimo automobile starije od 1950 godista!");
                    unetoSve = false;
                }
                else if (Int32.Parse(textBox4.Text) > 2019)
                {
                    MessageBox.Show("Ne mozete dodati automobil koji jos nije proizveden!");
                    unetoSve = false;
                }
            }


            
            if (textBox5.Text.Length < 1)
            {
                MessageBox.Show("Niste uneli kubikazu!");
                textBox5.BackColor = Color.Red;
                unetoSve = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]*$"))
            {
                MessageBox.Show("Kubikaza mora biti celi broj!");
                unetoSve = false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]*$"))
            {
                if (Int32.Parse(textBox5.Text) < 1200)
                {
                    MessageBox.Show("Ne primamo automobile ispod 1200 kubika!");
                    unetoSve = false;
                }
                else if (Int32.Parse(textBox5.Text) > 5000)
                {
                    MessageBox.Show("Ne primamo automobile sa vise od 5000 kubika!");
                    unetoSve = false;
                }
            }




            //ako je sve uneto kako treba
            if (unetoSve) {
                List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
                int maxID = 0;
                //dodeljuje se id automobilu da se ne bi poklapao sa drugim id drugih automobila
                for (int i = 0; i < automobili.Count; i++) {
                    if (automobili[i].Id > maxID)
                    {
                        maxID = automobili[i].Id;
                    }
                }

                //pa se pravi automobil kome se prosledjuje dodeljen Id
                try
                {
                    Automobil a = new Automobil(maxID + 1, textBox2.Text, textBox3.Text, Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text), comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, Int32.Parse(comboBox5.Text));
                    //proveravamo da li automobil postoji
                    foreach (Automobil auto in automobili)
                    {
                        if (auto.Equals(a))
                        {
                            MessageBox.Show("Automobil vec postoji!");
                            return;
                        }
                    }
                    automobili.Add(a);
                }
                catch (Exception) {
                    MessageBox.Show("Uneti podaci nisu validni");
                    return;
                }
                
                MessageBox.Show("Uspesno ste dodali automobil!");
                //upis i ponovno ispisivanje na ekranu
               
                RadSaDatotekom.Upisi(automobili, "automobili.bin");
                this.AdminDodavanjeAutomobila_Load(this, e);
            }
            
        }
        

        

        //MARKA PROMENA BOJE
        private void textBox2_TextChanged(object sender, EventArgs e)
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


        //GODISTE PROMENA BOJE
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "^[0-9]*$")&&(textBox4.Text.Length==4))
            {
                if (Int32.Parse(textBox4.Text) > 1950 && Int32.Parse(textBox4.Text) <= 2019)
                {
                    textBox4.BackColor = Color.Green;
                }
                else {
                    textBox4.BackColor = Color.Red;
                }
                
            }
            else
            {
                textBox4.BackColor = Color.Red;
            }
        }

        //KUBIKAZA PROMENA BOJE
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^[0-9]*$") && (textBox5.Text.Length == 4))
            {
                if (Int32.Parse(textBox5.Text) >= 1200 && Int32.Parse(textBox5.Text) <= 5000)
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


        //BRISANJE AUTOMOBILA
        private void button2_Click(object sender, EventArgs e)
        {
            List<Automobil> auta = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < auta.Count; i++)
            {
                if (auta[i].Id == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    auta.RemoveAt(i);
                }
            }
            RadSaDatotekom.Upisi(auta, "automobili.bin");
            this.AdminDodavanjeAutomobila_Load(this, e);
        }


        //POPUNJAVANJE TEXTBOKSOVA ZA IZMENU
        private void button3_Click(object sender, EventArgs e)
        {
            List<Automobil> auta = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < auta.Count; i++)
            {
                if (auta[i].Id == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    textBox2.Text = auta[i].Marka;
                    textBox3.Text = auta[i].Model;
                    textBox4.Text = auta[i].Godiste.ToString();
                    textBox5.Text = auta[i].Kubikaza.ToString();
                    comboBox1.Text = auta[i].Pogon;
                    comboBox2.Text = auta[i].VrstaMenjaca;
                    comboBox3.Text = auta[i].Karoserija;
                    comboBox4.Text = auta[i].Gorivo;
                    comboBox5.Text = auta[i].BrojVrata.ToString();
                    button4.Visible = true;
                }
            }
        }
        
        //BRISE SE STARI AUTO, A DODAJE SE NOVI
        private void button4_Click(object sender, EventArgs e)
        {
            List<Automobil> auta = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            
            for (int i = 0; i < auta.Count; i++)
            {
                if (auta[i].Id == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()))
                {
                    Automobil autoPom = new Automobil(auta[i].Id, textBox2.Text, textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text), comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, int.Parse(comboBox5.Text));
                    auta.RemoveAt(i);
                    auta.Insert(i, autoPom);
                }

            }
            RadSaDatotekom.Upisi(auta, "automobili.bin");
            MessageBox.Show("Automobil je uspesno izmenjen!");
            button4.Visible = false;
            this.AdminDodavanjeAutomobila_Load(this, e);
        }


        //BRZO PRAZNJENJE TEXTBOKSOVA
        private void button5_Click(object sender, EventArgs e)
        {
         
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";

            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
        }
    }
}
