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
    public partial class FormAdminPocetna : Form
    {
        public FormAdminPocetna(TextBox t)
        {
            InitializeComponent();
            label2.Text = t.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminDodavanjeAutomobila ada = new AdminDodavanjeAutomobila(label2);
            ada.Show();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            KupacAdmin KA = new KupacAdmin(label2);
            KA.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PonudaAdminForm paf = new PonudaAdminForm(label2);
            paf.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAdminRezervacije far = new FormAdminRezervacije();
            far.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminStatistika statistika = new AdminStatistika();
            statistika.Show();
        }

        
    }
}
