using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVPProject
{
    public partial class PonudaAdminForm : Form
    {
        public PonudaAdminForm(Label l)
        {
            InitializeComponent();
            label6.Text = l.Text;
           
        }

        //PRI POKRETANJU DODAJEMO ID AUTOMOBILA U COMBO
        private void PonudaAdminForm_Load(object sender, EventArgs e)
        {
            List<Ponuda> ponudePom = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            comboBox1.Items.Clear();
            for (int i = 0; i < automobili.Count; i++)
            {
                comboBox1.Items.Add(automobili[i].Id);
            }
            dataGridView1.DataSource = ponudePom;
            dataGridView1.Columns["datumOd"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns["datumDo"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Refresh();
        }


        //DODAVANJE PONUDE
        private void button1_Click(object sender, EventArgs e)
        {
            List<Ponuda> ponude = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            Regex regexCena = new Regex(@"^\d+$");
            bool sveOKe = false;
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                sveOKe = false;
                MessageBox.Show("Datumi nisu validni!");
            }
            else {
                sveOKe = true;
            }
            if (!regexCena.IsMatch(textBox1.Text))
            {
                MessageBox.Show("Morate uneti broj za cenu!");
                sveOKe = false;
                return;
            }
           

            if (sveOKe) {

                    try
                    {
                        Ponuda p = new Ponuda(int.Parse(comboBox1.Text), dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToInt32(textBox1.Text));
                        ponude.Add(p);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Uneti podaci nisu validni");
                        return;
                    }

                    RadSaDatotekom.Upisi(ponude, "ponuda.bin");
                    MessageBox.Show("Ponuda je uspesno dodata.");
                    this.PonudaAdminForm_Load(this, e);
                }
              
            

        }

        //PROMENOM SELEKTOVANOG ID ISPISUJEMO AUTOMOBIL U TEXTBOX PORED
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            for (int i = 0; i < automobili.Count; i++) {           
                if (automobili[i].Id.ToString() == comboBox1.Text) {
                    textBox2.Text = "";
                    textBox2.Text = automobili[i].ToString();
                    break;
                }
            } 
        }

        //BRISANJE PONUDE
        private void button2_Click(object sender, EventArgs e)
        {
            List<Ponuda> ponuda = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < ponuda.Count; i++) {
                if (ponuda[i].IdAuta == int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString())) {
                    ponuda.RemoveAt(i);
                }
            }
            RadSaDatotekom.Upisi(ponuda, "ponuda.bin");
            this.PonudaAdminForm_Load(this, e);

        }


       // DODAVANJE NA IZMENU
        private void button3_Click(object sender, EventArgs e)
        {
            List<Ponuda> ponude = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < ponude.Count; i++)
            {
                if (ponude[i].IdAuta.ToString() == dataGridView1.Rows[rowIndex].Cells[0].Value.ToString())
                {
                    comboBox1.Text = ponude[i].IdAuta.ToString();
                    textBox1.Text = ponude[i].CenaPoDanu.ToString();
                    dateTimePicker1.Value = ponude[i].DatumOd;
                    dateTimePicker2.Value = ponude[i].DatumDo;
                    button4.Visible = true;
                }
            }
        }

        //CUVANJE IZMENE
        private void button4_Click(object sender, EventArgs e)
        {
            List<Ponuda> ponude = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            for (int i = 0; i < ponude.Count; i++)
            {
                MessageBox.Show(ponude[i].IdAuta.ToString());
                MessageBox.Show(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
                if (ponude[i].IdAuta.ToString() == dataGridView1.Rows[rowIndex].Cells[0].Value.ToString())
                {
                    MessageBox.Show("Radi if za izmenu!");
                    ponude.RemoveAt(i);
                    Ponuda p = new Ponuda(int.Parse(comboBox1.Text), dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToInt32(textBox1.Text));
                    ponude.Insert(i, p);
                }

            }
            RadSaDatotekom.Upisi(ponude, "ponuda.bin");
            MessageBox.Show("Ponuda je uspesno izmenjena!");
            button4.Visible = false;
            button3.Visible = true;
            this.PonudaAdminForm_Load(this, e);
        }
        
        //BRZO PRAZNJENJE COMBOBOKSA
        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            button4.Visible = false;
            button3.Visible = true;
        }

        
    }
}
