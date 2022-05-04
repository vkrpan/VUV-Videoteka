using System;
using System.Collections.Generic;
using System.Text;

namespace VUV_videoteka
{
    class Glumac : Osoba
    {
        public string IdGl { get; }
        public Glumac()
        {

        }
        public Glumac(string idGL,string ime, string prezime, string oib)
        {
            Ime = ime;
            Prezime = prezime;
            IdGl = idGL;
            OIB = oib;
        }
    }
}
