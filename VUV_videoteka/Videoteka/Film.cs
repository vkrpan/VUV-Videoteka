using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VUV_videoteka
{
    class Film
    {
        public string ID { get; }
        public string Ime { get; }
        public int Godina { get; }
        public int Trajanje { get; }
        public bool Posuden { get; }
        public double CijenaNajma { get; }
        public bool Obrisan { get; }
        public List<Glumac> Glumci { get; }
        public List<Redatelj> Redatelj { get; }
        public List<Zanr> Zanr { get; }
        public Film()
        {

        }
        public Film(string id,string ime, int godina, int trajanje, bool posuden, double cijenaNajma, bool obrisan, List<Glumac> glumci, List<Redatelj> redatelj, List<Zanr> zanr)
        {
            ID = id;
            Ime = ime;
            Godina = godina;
            Trajanje = trajanje;
            Posuden = posuden;
            CijenaNajma = cijenaNajma;
            Obrisan = obrisan;
            Glumci = glumci;
            Redatelj = redatelj;
            Zanr = zanr;
        }

    }
}
