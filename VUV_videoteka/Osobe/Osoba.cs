using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VUV_videoteka
{
    abstract class Osoba
    {
        public string Ime { get; protected set; }
        public string Prezime { get; protected set; }
        public string OIB { get; protected set; }
    }
}