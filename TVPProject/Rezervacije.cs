using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable()]
    class Rezervacije
    {
        private int idAutaRez;
        private int idKupca;
        private DateTime datumOd;
        private DateTime datumDo;
        private int cena;

        public Rezervacije(int idAutaRez, int idKupca, DateTime datumOd, DateTime datumDo, int cena)
        {
            this.idAutaRez = idAutaRez;
            this.idKupca = idKupca;
            this.datumOd = datumOd;
            this.datumDo = datumDo;
            this.cena = cena;
        }

        public int IdAutaRez { get => idAutaRez; set => idAutaRez = value; }
        public int IdKupca { get => idKupca; set => idKupca = value; }
        public DateTime DatumOd { get => datumOd; set => datumOd = value; }
        public DateTime DatumDo { get => datumDo; set => datumDo = value; }
        public int Cena { get => cena; set => cena = value; }
    }
}
