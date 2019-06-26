using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable()]
    class Administrator:Korisnik
    {
        private int id;

        public int Id { get => id; set => id = value; }

        public Administrator() { }

        public Administrator(int id, string ime, string prezime, string jmbg, DateTime datumRodjenja, string brojTelefona, string korisnickoIme, string lozinka)
           : base(ime, prezime, jmbg, datumRodjenja, brojTelefona, korisnickoIme, lozinka)
        {
            this.id = id;
        }
    }
}
