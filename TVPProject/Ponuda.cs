using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPProject
{
    [Serializable()]
    class Ponuda
    {
        private int idAuta;
        private DateTime datumOd;
        private DateTime datumDo;
        private int cenaPoDanu;

        public int IdAuta { get => idAuta; set => idAuta = value; }
        public DateTime DatumOd { get => datumOd; set => datumOd = value; }
        public DateTime DatumDo { get => datumDo; set => datumDo = value; }
        public int CenaPoDanu { get => cenaPoDanu; set => cenaPoDanu = value; }

        public Ponuda() { }

        public Ponuda(int idAuta, DateTime datumOd, DateTime datumDo, int cenaPoDanu)
        {
            this.idAuta = idAuta;
            this.datumOd = datumOd;
            this.datumDo = datumDo;
            this.cenaPoDanu = cenaPoDanu;
        }

        public override string ToString()
        {
            return "Ponuda: " + idAuta + " - " + datumOd.ToShortDateString() + " - " + DatumDo.ToShortDateString() + " - > Cena po danu: " + cenaPoDanu + Environment.NewLine;
        }

    }
}
