using System;
using System.Collections.Generic;
using System.Text;

namespace VUV_videoteka
{
    class Redatelj : Osoba
    {
        public string IdRed { get;}
        public Redatelj()
        {

        }
        public Redatelj(string idred, string ime, string prezime, string oib)
        {
            IdRed = idred;
            Ime = ime;
            Prezime = prezime;
            OIB = oib;
        }
    }
}
