using System;
using System.Collections.Generic;
using System.Text;

namespace VUV_videoteka
{
    class Gledatelj : Osoba
    {
        public string Adresa { get; private set; }
        public string SifraGl { get; private set; }
        public bool Obrisan { get; private set; }
        public Gledatelj()
        {

        }
        public Gledatelj(string ime, string prezime, string oib, string adresa, string sifra, bool obrisan)
        {
            Ime = ime;
            Prezime = prezime;
            OIB = oib;
            Adresa = adresa;
            SifraGl = sifra;
            Obrisan = obrisan;
        }
    }
}
