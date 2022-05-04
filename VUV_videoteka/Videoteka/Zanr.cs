using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace VUV_videoteka
{
    class Zanr
    {
        public string IdZanr { get; }
        public string Value { get; }
        public Zanr()
        {

        }
        public Zanr(string idZanr, string value)
        {
            IdZanr = idZanr;
            Value = value;
        }
    }
}
