using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable()]
    class Kupac : Korisnik
    {
        private int id;

        public int Id { get => id; set => id = value; }

        public Kupac(){ }

        public Kupac(int id, string ime, string prezime, string jmbg, DateTime datumRodjenja, string brojTelefona, string korisnickoIme, string lozinka)
            : base(ime, prezime, jmbg, datumRodjenja, brojTelefona, korisnickoIme, lozinka)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine + "Ime: " + Ime + Environment.NewLine + "Prezime:" + Prezime +Environment.NewLine+ "JMBG: " + Jmbg + Environment.NewLine + "d. rodjenja: " + DatumRodjenja.ToShortDateString() + Environment.NewLine + "Br. telefona: " + BrojTelefona + Environment.NewLine + "Kor. ime: " + KorisnickoIme;
        }
    }
}
