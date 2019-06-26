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
    public partial class FormRezervacije : Form
    {
        Automobil a = new Automobil();
        Kupac k;
        
        public FormRezervacije(Label l)
        {
            this.k = new Kupac();
            InitializeComponent();
            label15.Text = l.Text;
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            List<string> marke = new List<string>();

        }


        private void FormRezervacije_Load(object sender, EventArgs e)
        {

            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Font = new Font(c.Font.FontFamily, 10);
                }
                if (c is Button)
                {
                    c.Font = new Font(c.Font.FontFamily, 10, FontStyle.Bold);
                }
                if (c is ComboBox)
                {
                    if (c.Name != comboBox1.Name)
                    {
                        c.Enabled = false;
                    }
                }
            }

            //citamo automobile i njhove marke smestamo u listu stringova
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            List<string> marke = new List<string>();

            for (int i = 0; i < automobili.Count; i++)
            {
                if (marke.Contains(automobili[i].Marka) == false)
                {
                    marke.Add(automobili[i].Marka);
                }
            }

            for (int i = 0; i < marke.Count; i++)
            {
                comboBox1.Items.Add(marke[i]);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            string izabranaMarka = comboBox1.Text;
            // kada se selektuje combobox za marku aktivira se combobox za modele 
            comboBox2.Enabled = true;

            //pravljenje stringa za modele
            List<string> modeliIzabraneMarke = new List<string>(); 

            foreach (Automobil auto in automobili)
            {
                if (auto.Marka == izabranaMarka)
                {
                    if (modeliIzabraneMarke.Contains(auto.Model) == false)
                    {
                        modeliIzabraneMarke.Add(auto.Model);
                    }
                }
            }
            foreach (Control c in this.Controls)
            {
                if (c is ComboBox)
                {
                    c.Text = "";
                }
            }
            comboBox2.Items.Clear();
            foreach (string model in modeliIzabraneMarke)
            {
                comboBox2.Items.Add(model);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            int imaAuta = 0;
            //pravimo automobil za poredjenje
            Automobil autoPom = new Automobil();
            //proveravamo da li podaci iz combobokseva za auto i model poklapaju sa automobilom iz liste
            foreach (Automobil auto in automobili)
            {
                if (auto.Marka == comboBox1.Text && auto.Model == comboBox2.Text)
                {
                    imaAuta++;
                }
            }
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";

            if (imaAuta == 1)
            {
                foreach (Automobil a in automobili)
                {
                    //prolazimo kroz automobile i ako se porede marka i model iz sa podacima iz comboboxa
                    //autuPom se dodeljuju vrednosti auta koje je pronadjeno
                    if (a.Marka == comboBox1.Text && a.Model == comboBox2.Text)
                    {
                        autoPom = a;
                    }
                }

                //i posle prema tom automobilu popunjavamo ostale comboboksove
                comboBox3.Text = autoPom.Godiste.ToString();
                comboBox4.Text = autoPom.Kubikaza.ToString();
                comboBox5.Text = autoPom.Karoserija.ToString();
                comboBox6.Text = autoPom.BrojVrata.ToString();
                comboBox7.Text = autoPom.Gorivo;
                comboBox8.Text = autoPom.Pogon;
                comboBox9.Text = autoPom.VrstaMenjaca;
            }
            else
            { 
                //ako ima vise automobila iste marke i modela
                //ukljucuju se ostali comboboxovi
                foreach (Control c in this.Controls)
                {
                    c.Enabled = true;
                }
                List<Automobil> pomAuta = new List<Automobil>();
                foreach (Automobil auto in automobili)
                {
                    if (auto.Marka == comboBox1.Text && auto.Model == comboBox2.Text)
                    {
                        pomAuta.Add(auto);
                    }
                }

                //ako ima automobila sa istim godistem ispisi ga samo jednom
                if (pomAuta[0].Godiste == pomAuta[1].Godiste)
                {
                    comboBox3.Text = pomAuta[0].Godiste.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox3.Items.Add(pom1.Godiste);
                    }
                }

                //ako ima automobila sa istom kubikazom ispisi je samo jednom
                if (pomAuta[0].Kubikaza == pomAuta[1].Kubikaza)
                {
                    comboBox4.Text = pomAuta[0].Kubikaza.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox4.Items.Add(pom1.Kubikaza);
                    }
                }

                //ako ima automobila sa istom karoserijom ispisi je samo jednom
                if (pomAuta[0].Karoserija == pomAuta[1].Karoserija)
                {
                    comboBox5.Text = pomAuta[0].Karoserija.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox5.Items.Add(pom1.Karoserija);
                    }
                }

                //ako ima automobila sa istim brojem vrata ispisi ih samo jednom
                if (pomAuta[0].BrojVrata == pomAuta[1].BrojVrata)
                {
                    comboBox6.Text = pomAuta[0].BrojVrata.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox6.Items.Add(pom1.BrojVrata);
                    }
                }

                //ako ima automobila sa istim gorivom ispisi ga samo jednom
                if (pomAuta[0].Gorivo == pomAuta[1].Gorivo)
                {
                    comboBox7.Text = pomAuta[0].Gorivo.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox7.Items.Add(pom1.Gorivo);
                    }
                }

                //ako ima automobila sa istim pogom ispisi njegov pogon samo jednom
                if (pomAuta[0].Pogon == pomAuta[1].Pogon)
                {
                    comboBox8.Text = pomAuta[0].Pogon.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox8.Items.Add(pom1.Pogon);
                    }
                }

                //ako ima automobila sa istom vrstom menjaca ispisi vrstu samo jednom
                if (pomAuta[0].VrstaMenjaca == pomAuta[1].VrstaMenjaca)
                {
                    comboBox9.Text = pomAuta[0].VrstaMenjaca.ToString();
                }
                else
                {
                    foreach (Automobil pom1 in pomAuta)
                    {
                        comboBox9.Items.Add(pom1.VrstaMenjaca);
                    }
                }
                
            }
          
        }


        private void inicijalizacijaAutomobila(Automobil auto)
        {
            //citanje datoteke automobili.bin
            List<Automobil> auta = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            List<Automobil> autaIspis = new List<Automobil>();
            for (int i = 0; i < auta.Count; i++)
            { //prolazak kroz auta, ako se auto poklapa sa nekim iz liste, dodajemo ga u listu
                if (auta[i].Id==auto.Id)
                {
                    autaIspis.Add(auta[i]);
                }
            }
            if (autaIspis.Count == 1)
            {
                this.a = autaIspis[0]; //ako postoji samo jedno takvo auto, a inicijalizujemo njime.
            }
            else
            { //ako ih postoji vise, inicijalizujemo ih nekim od ponudjenih, jer su podaci potpuno isti
                Random rnd = new Random();
                int rndBr = rnd.Next(0, auta.Count);
                this.a = autaIspis[rndBr];
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {

            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
            //ako se podaci iz comboboxova poklapaju i automobila iz liste, dodajemo id vrednost automobilu
            for (int i = 0; i < automobili.Count; i++)
            {
                if (automobili[i].Marka == comboBox1.Text && automobili[i].Model == comboBox2.Text && automobili[i].Godiste == Convert.ToInt32(comboBox3.Text) && automobili[i].Kubikaza == Convert.ToInt32(comboBox4.Text) && automobili[i].Karoserija == comboBox5.Text && automobili[i].BrojVrata == Convert.ToInt32(comboBox6.Text) && automobili[i].Gorivo == comboBox7.Text && automobili[i].Pogon == comboBox8.Text && automobili[i].VrstaMenjaca == comboBox9.Text)
                {
                    a.Id = automobili[i].Id;
                }
            }
            textBox1.Text = "";
            bool inicijalizuj = true;

            //proveramo da li su sva polja popunjena
            foreach (Control c in this.Controls) 
            {
               
                if (c.Text == "" && c is ComboBox)
                {
                    MessageBox.Show("Niste popunili sva polja.");
                    inicijalizuj = false;
                    break;
                }
            }

            //ako su popunjena sva polja
            if (inicijalizuj) 
            {
                //pravi se novi auto                   
                Automobil auto = new Automobil(a.Id,comboBox1.Text, comboBox2.Text, Convert.ToInt32(comboBox3.Text), Convert.ToInt32(comboBox4.Text), comboBox9.Text,comboBox7.Text, comboBox8.Text, comboBox5.Text,Convert.ToInt32(comboBox6.Text));
                try //hvata se izuzetak, koji nastaje ukoliko neko promeni tekst u comboboxu
                {
                    //funkcija inicijalizuj auto dobija kao argument napravljeni auto
                    this.inicijalizacijaAutomobila(auto); 
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Uneti podaci nisu tacni.");
                }

                //funkcija koja ispisuje slobodne termine
                this.ispisiDatume();
              
            }

        }


        
        private void ispisiDatume()
        {
            List<Rezervacije> rezervacije = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
            //pomocna lista za smestaj rezervacija
            List<Rezervacije> rezZaIspis = new List<Rezervacije>();

            //Citamo sve rezervacije i ako se id poklapaju rezervacija se upisuje u pomocnu listu rezervacija
            foreach (Rezervacije rezerevacija in rezervacije)
            {
                if (rezerevacija.IdAutaRez== this.a.Id)
                {
                    
                    if (rezZaIspis.Contains(rezerevacija) == false)
                    {
                        rezZaIspis.Add(rezerevacija);
                    }
                }
            }

            //ako ima vise rezervacija one se sortiraju po datumu
            if (rezZaIspis.Count > 1)
            {
                for (int i = 0; i < rezZaIspis.Count - 1; i++)
                {
                    for (int j = i + 1; j < rezZaIspis.Count; j++)
                    {
                        if (rezZaIspis[i].DatumOd > rezZaIspis[j].DatumOd)
                        {
                            Rezervacije r = rezZaIspis[i];
                            rezZaIspis[i] = rezZaIspis[j];
                            rezZaIspis[j] = r;
                        }
                    }
                }
            }


            List<Ponuda> ponude = RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
            List<Ponuda> ponudeZaTrazeniAuto = new List<Ponuda>();

            //proveravamo da li je trazeni automobil u ponudi
            foreach (Ponuda ponuda in ponude)
            {     
                if (ponuda.IdAuta == this.a.Id)
                {
                    ponudeZaTrazeniAuto.Add(ponuda);
                    //prikaz ponude u textboxu
                    textBox1.Text = ponuda.IdAuta + " -> " + ponuda.DatumOd.ToShortDateString() + " - " + ponuda.DatumDo.ToShortDateString() + Environment.NewLine;
                }
            }

            // pitamo koliko ima ponuda za trazeni auto, i iz njih citamo minimalni i maximalni datum
            if (ponudeZaTrazeniAuto.Count >= 1)
            {
                DateTime datumMin = ponudeZaTrazeniAuto[0].DatumOd;
                DateTime datumMax = ponudeZaTrazeniAuto[0].DatumDo;

                for (int i = 0; i < ponudeZaTrazeniAuto.Count; i++)
                {
                    if (datumMin > ponudeZaTrazeniAuto[i].DatumOd)
                    {
                        datumMin = ponudeZaTrazeniAuto[i].DatumOd;
                    }
                }

                for (int i = 0; i < ponudeZaTrazeniAuto.Count; i++)
                {
                    if (datumMax < ponudeZaTrazeniAuto[i].DatumDo)
                    {
                        datumMax = ponudeZaTrazeniAuto[i].DatumDo;
                    }
                }

                textBox1.Text = "";
                //ispis slobodnih termina
                foreach (Ponuda ponuda in ponudeZaTrazeniAuto)
                {
                    textBox1.Text += ponuda.ToString();
                }
                textBox1.Text += Environment.NewLine + "Slobodni termini: " + Environment.NewLine;
                if (rezZaIspis.Count > 0)
                {
                    for (int i = 0; i < rezZaIspis.Count; i++)
                    {
                        if (rezZaIspis[i].DatumOd >= datumMin && rezZaIspis[i].DatumDo <= datumMax)
                        {
                            textBox1.Text += datumMin.ToShortDateString() + " - " + rezZaIspis[i].DatumOd.ToShortDateString() + Environment.NewLine;
                            datumMin = rezZaIspis[i].DatumDo;
                            if (i == rezZaIspis.Count - 1 && rezZaIspis[i].DatumDo <= datumMax)
                            {
                                textBox1.Text += rezZaIspis[i].DatumDo.ToShortDateString() + " - " + datumMax.ToShortDateString() + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    textBox1.Text += datumMin.ToShortDateString() + " - " + datumMax.ToShortDateString() + Environment.NewLine;
                }
            }
            else
            {
                MessageBox.Show("Ne postoje ponude za trazeni auto.");
            }
        }




        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        
            List<Rezervacije> rezervacije = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
            //proveravamo validnost datuma
            if (dateTimePicker1.Value < DateTime.Now)
            {
                MessageBox.Show("Ne mozete birati datum iz proslosti.");
                dateTimePicker1.Value = DateTime.Now;
            }
            //
            //proveravamo da li je termin zauzet
            else
            {
                foreach (Rezervacije rezervacija in rezervacije)
                {
                    if (rezervacija.IdAutaRez == this.a.Id)
                    {
                        if (dateTimePicker1.Value > rezervacija.DatumDo && dateTimePicker1.Value < rezervacija.DatumDo)
                        {
                            MessageBox.Show("Termin je vec zauzet");
                            dateTimePicker1.Value = DateTime.Now;
                            break;
                        }
                    }
                }
            }
        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            bool nastavi = true;
            List<Rezervacije> rezervacije = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
            List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");

            //ovde trazimo id automobila koji prosledjujemo objektu
            for (int i = 0; i < automobili.Count; i++)
            {
                if (automobili[i].Marka == comboBox1.Text && automobili[i].Model == comboBox2.Text && automobili[i].Godiste == Convert.ToInt32(comboBox3.Text) && automobili[i].Kubikaza == Convert.ToInt32(comboBox4.Text) && automobili[i].Karoserija == comboBox5.Text && automobili[i].BrojVrata == Convert.ToInt32(comboBox6.Text) && automobili[i].Gorivo == comboBox7.Text && automobili[i].Pogon == comboBox8.Text && automobili[i].VrstaMenjaca == comboBox9.Text)
                {
                    a.Id = automobili[i].Id;
                }
            }
            
            //opet proveravamo validnost
            if (dateTimePicker2.Value < DateTime.Now)
            {
                dateTimePicker1.Value = DateTime.Now;
                nastavi = false;
            }
           
            else
            {
                //proveravamo da li je termin zauzet
                foreach (Rezervacije rezervacija in rezervacije)
                {
                    if (rezervacija.IdAutaRez == this.a.Id)
                    {
                        if (dateTimePicker2.Value > rezervacija.DatumOd && dateTimePicker2.Value < rezervacija.DatumDo)
                        {
                            MessageBox.Show("Termin je vec zauzet");
                            dateTimePicker1.Value = DateTime.Now;
                            nastavi = false;
                        }
                    }
                }
            }

            //ako je sve OK
            if (nastavi)
            {
                //trazimo broj dana izmedju datuma
                double brojDana = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).TotalDays;
                double cena = 0;
                List<Ponuda> ponude =RadSaDatotekom.Procitaj<Ponuda>("ponuda.bin");
                //prvo pitamo da li je uneti datum u jednom mesecu
                //trazimo ponudu za taj auto u jednom i pravimo cenu
                if (dateTimePicker1.Value.Month == dateTimePicker2.Value.Month)
                {
                    foreach (Ponuda ponuda in ponude)
                    {
                        if (ponuda.IdAuta == this.a.Id)
                        {
                            if (dateTimePicker1.Value.Date > ponuda.DatumOd && dateTimePicker2.Value.Date <= ponuda.DatumDo)
                            {
                                cena = brojDana * ponuda.CenaPoDanu;
                                if (cena > 0)
                                    textBox10.Text = cena.ToString();
                            }
                        }
                    }
                }
                else
                //ako nije
                //dinamiciki se racuna raspon izmedju dana u mesecima
                {
                    double brojDana1 = 0;
                    double brojDana2 = 0;
                    double cena1 = 0;
                    double cena2 = 0;
                    foreach (Ponuda ponuda in ponude)
                    {
                        if (ponuda.IdAuta == this.a.Id)
                        {
                            if (dateTimePicker1.Value.Month == ponuda.DatumOd.Month)
                            {
                                brojDana1 += (ponuda.DatumDo - dateTimePicker1.Value.Date).TotalDays;
                                cena1 = brojDana1 * ponuda.CenaPoDanu;
                               
                            }
                            if (dateTimePicker2.Value.Month == ponuda.DatumDo.Month)
                            {
                                brojDana2 += (dateTimePicker2.Value.Date - ponuda.DatumOd.Date).TotalDays;
                                cena2 = brojDana2 * ponuda.CenaPoDanu;
                              
                            }

                        }
                    }
                    if ((cena1 + cena2) > 0)
                    {
                        double someValue = (cena1 + cena2);
                        String.Format("{0:0.00}", someValue);
                        textBox10.Text = someValue.ToString();
                    }
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            bool nastavi = true;
            //ako je datumDo veci od datumOd prijavice gresku 
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                nastavi = false;
                MessageBox.Show("Netacni datumi rezervacije");
            }

            //ako je sve ok i textbox za cenu ima vrednost
            if (textBox10.Text.Trim().Length != 0 && nastavi)
            {
                List<Rezervacije> rezervacijeUpis = RadSaDatotekom.Procitaj<Rezervacije>("rezervacije.bin");
                List<Kupac> kupci = RadSaDatotekom.Procitaj<Kupac>("kupciReg.bin");
                List<Automobil> automobili = RadSaDatotekom.Procitaj<Automobil>("automobili.bin");
                //izvlacimo id kupca da bi smo ga iskorisntili u kreiranju rezervacije
                for (int i = 0; i < kupci.Count; i++) {
                    if (kupci[i].KorisnickoIme == label15.Text) {
                        k.Id = kupci[i].Id;
                    }
                }

                //a onda izvlacimo i id automobila
                for (int i = 0; i < automobili.Count; i++) {
                    if (automobili[i].Marka == comboBox1.Text && automobili[i].Model == comboBox2.Text && automobili[i].Godiste == Convert.ToInt32(comboBox3.Text) && automobili[i].Kubikaza == Convert.ToInt32(comboBox4.Text) && automobili[i].Karoserija == comboBox5.Text && automobili[i].BrojVrata == Convert.ToInt32(comboBox6.Text) && automobili[i].Gorivo == comboBox7.Text && automobili[i].Pogon == comboBox8.Text && automobili[i].VrstaMenjaca == comboBox9.Text) {
                        a.Id = automobili[i].Id;
                    }
                }
                
                //pravimo rezervaciju
                Rezervacije r = new Rezervacije(a.Id, k.Id, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, Convert.ToInt32(double.Parse(textBox10.Text)));
                rezervacijeUpis.Add(r);
                RadSaDatotekom.Upisi(rezervacijeUpis, "rezervacije.bin");
                MessageBox.Show("Rezervacija je uspesno izvrsena.");
                
                //nakon uspesne rezervacije zatvara je forma za rezervacije i otvara se pocetna forma za kupca
                for (int i = 0; i < kupci.Count; i++) {
                    if (kupci[i].KorisnickoIme == label15.Text) {
                        FormKupac formKupac = new FormKupac(kupci[i]);
                        formKupac.Show();
                        this.Close();
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Nije moguce napraviti rezervaciju");
            }
        }

    }
}
