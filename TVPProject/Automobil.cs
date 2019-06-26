using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable]
    class Automobil
    {
        private int id;
        private string marka;
        private string model;
        private int godiste;
        private int kubikaza;
        private string vrstaMenjaca;
        private string gorivo;
        private string pogon;
        private string karoserija;
        private int brojVrata;

        public int Id { get => id; set => id = value; }
        public string Marka { get => marka; set => marka = value; }
        public string Model { get => model; set => model = value; }
        public int Godiste { get => godiste; set => godiste = value; }
        public int Kubikaza { get => kubikaza; set => kubikaza = value; }
        public string VrstaMenjaca { get => vrstaMenjaca; set => vrstaMenjaca = value; }
        public string Gorivo { get => gorivo; set => gorivo = value; }
        public string Pogon { get => pogon; set => pogon = value; }
        public string Karoserija { get => karoserija; set => karoserija = value; }
        public int BrojVrata { get => brojVrata; set => brojVrata = value; }

        public Automobil() { }

        public Automobil(int id, string marka, string model, int godiste, int kubikaza, string pogon, string vrstaMenjaca, string karoserija,string gorivo,int brojVrata)
        {
            this.id = id;
            this.marka = marka;
            this.model = model;
            this.godiste = godiste;
            this.kubikaza = kubikaza;
            this.vrstaMenjaca = vrstaMenjaca;
            this.gorivo = gorivo;
            this.pogon = pogon;
            this.karoserija = karoserija;
            this.brojVrata = brojVrata;
        }

        public Automobil(string marka, string model, int godiste, int kubikaza, string vrstaMenjaca, string gorivo, string pogon, string karoserija,int brojVrata)
        {
            this.marka = marka;
            this.model = model;
            this.godiste = godiste;
            this.kubikaza = kubikaza;
            this.vrstaMenjaca = vrstaMenjaca;
            this.gorivo = gorivo;
            this.pogon = pogon;
            this.karoserija = karoserija;
            this.brojVrata = brojVrata;
        }

        public override string ToString()
        {
            return "Automobil ID: " + id +Environment.NewLine+ "marka: " + marka + Environment.NewLine + "model: " + model + Environment.NewLine + "godiste: " + godiste + Environment.NewLine + "kubikaza: " + kubikaza + Environment.NewLine + "vrsta menjaca: " + vrstaMenjaca + Environment.NewLine + "gorivo: " + gorivo + Environment.NewLine + "karoserija: " + karoserija + Environment.NewLine + "broj vrata: " + brojVrata;
        }
    }
}
