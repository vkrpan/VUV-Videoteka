using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VUV_videoteka
{
    class FilmNajam
    {
        public string SifraNajma { get; }
        public string DatumIznajmljivanja { get; }
        public string DatumVracanja { get; }
        public Operater Operater { get; }
        public Film Film { get;}
        public Gledatelj Gledatelj { get; }
        public FilmNajam(string sifraNajma, string datIznajmljivanja, string datvracanja, Gledatelj gledatelj, Operater operater, Film film)
        {
            SifraNajma = sifraNajma;
            DatumIznajmljivanja = datIznajmljivanja;
            DatumVracanja = datvracanja;
            Operater = operater;
            Film = film;
            Gledatelj = gledatelj;
        }
    }
}
