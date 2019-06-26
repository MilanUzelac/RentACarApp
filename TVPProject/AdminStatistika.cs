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
    public partial class AdminStatistika : Form
    {
        List<Label> labele; //labele za ispis id i procenata
        List<Rezervacije> rezervacije; //lista sa rezervacijama
        public AdminStatistika()
        {
            InitializeComponent();
            labele = new List<Label>();
        }

        private void AdminStatistika_Load(object sender, EventArgs e)
        {
            //punjenje comboboxa na osnovu ponuda
            List<string> rbrMesesa = new List<string>();
            List<Ponuda> ponude = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            foreach (Ponuda ponuda in ponude)
            {
                if (!rbrMesesa.Contains(ponuda.DatumOd.Month.ToString()))
                {
                    rbrMesesa.Add(ponuda.DatumOd.Month.ToString());
                }
            }

            foreach (string mesec in rbrMesesa)
            {
                comboBox1.Items.Add(mesec);
            }
        }
        int rbr; //izabrani redni broj meseca
        void crtanje(object sender, PaintEventArgs g)
        {
            List<Rezervacije> rez = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
            rezervacije = new List<Rezervacije>();
            foreach (Rezervacije r in rez) //prolazak kroz sve rezervacije
            {
                if (r.DatumDo.Month == rbr) //ako mi je rezervaciju u okviru izabranog meseca, dodaje se u rezrvacije za ispis
                {
                    rezervacije.Add(r);
                }
            }

            double ukupnoDana = 0;
            int visina = 30;
            int sirina = 30;
            int x = this.Width - 200;
            int y = 190;
            int ugao = -90;
            int pomeraj = 0;
            Color boja = new Color();
            Random rnd = new Random();
            SolidBrush brush = new SolidBrush(boja);

            foreach (Rezervacije r in rezervacije)
            {
                //racunanje ukupnog broja dana(bice imenilnac kasnije)
                double dani = (r.DatumDo - r.DatumOd).TotalDays;
                ukupnoDana += dani;
            }
            List<int> idAuta = new List<int>();

            foreach (Rezervacije r in rezervacije)
            {
                //pravljenje liste koja ce cuvati idjeve auta
                if (idAuta.Contains(r.IdAutaRez) == false && r.DatumDo.Month == rbr)
                {
                    idAuta.Add(r.IdAutaRez);
                }
            }
            //pravljeje hes tabele(kljuc,vrednost) -> cuvace mi id auta i br dana u kojima je on izdat
            Hashtable brojDana = new Hashtable(); 

            for (int i = 0; i < idAuta.Count; i++)
            {
                double brojDanaPoAutu = 0;
                for (int j = 0; j < rezervacije.Count; j++)
                {
                    if (idAuta[i] == rezervacije[j].IdAutaRez)
                    {
                        brojDanaPoAutu += (rezervacije[j].DatumDo - rezervacije[j].DatumOd).TotalDays;
                    }
                }
                brojDana.Add(idAuta[i], brojDanaPoAutu);
            }


            //prolazak kroz hes tabelu, id -> br dana u kojima je izdat
            foreach (DictionaryEntry dan in brojDana)
            {
                //pravljenje slucajne rgb boje
                boja = Color.FromArgb(rnd.Next(1, 255), rnd.Next(1, 255), rnd.Next(1, 255)); 
                //boja je boja kojom ce cetkica da boji
                brush.Color = boja;
                //pravljenje kvadrata za "legendu"
                g.Graphics.FillRectangle(brush, new Rectangle(x, y, visina, sirina)); 
                //dodavanje labele koja ce ispisivati procente i id auta
                Label l = new Label(); 
                l.Location = new Point(x + 50, y);
                //nadovezivanje dogadjaja
                l.Click += prikaziAuto; 
                this.labele.Add(l);
                y += 35;
                //ugao za popunjavanje
                pomeraj = (int)(((double)(dan.Value) / ukupnoDana) * 360); 
                //ispis procenata
                l.Text = "Id auta: " + dan.Key.ToString() + " -> " + ((((double)(dan.Value) / ukupnoDana)) * 100).ToString("n2"); 
                //crtanje pite, svaki sledeci pocinje od kraja prethodnog
                g.Graphics.FillPie(brush, new Rectangle(100, 100, 200, 200), ugao, pomeraj); 
                //racunanje pocetne tacke za popunu
                ugao = ugao + pomeraj;
            }

            foreach (Label labela in labele)
            {
                this.Controls.Add(labela);
            }
        }

        //PRIKAZUJE AUTO KADA SE KLIKNE NA AUTOMOBIL U LEGENDI
        private void prikaziAuto(object sender, EventArgs e)
        {
            Label l = sender as Label;
            string[] reci = l.Text.Split(' ');
            List<Automobil> auta = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            foreach (Automobil a in auta)
            {
                if (a.Id == int.Parse(reci[2]))
                {
                    MessageBox.Show(a.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Label labela in labele)
            {
                labela.Dispose();
            }
            bool uspesno2 = int.TryParse(comboBox1.Text, out rbr);
            if (uspesno2)
            {
                //nadovezivanje na paint dogadjaj
                this.Paint += crtanje;
                //osvezavanje ekrana
                this.Invalidate(); 
            }
        }

    }
}
