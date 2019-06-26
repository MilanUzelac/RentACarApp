using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable()]
    class Korisnik
    {
        private string ime;
        private string prezime;
        private string jmbg;
        private DateTime datumRodjenja;
        private string brojTelefona;
        private string korisnickoIme;
        private string lozinka;

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public string BrojTelefona { get => brojTelefona; set => brojTelefona = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }

        public Korisnik() { }

        public Korisnik(string ime, string prezime, string jmbg, DateTime datumRodjenja, string brojTelefona, string korisnickoIme, string lozinka)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.brojTelefona = brojTelefona;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
        }
    }
}
