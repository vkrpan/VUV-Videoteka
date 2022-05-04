using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    class Liste
    {
        public List<Film> Filmova { get; set; }
        public List<Glumac> Glumaca { get; set; }
        public List<Redatelj> Redatelja { get; set; }
        public List<Zanr> Zanrova { get; set; }
        public List<Gledatelj> Gledatelja { get; set; }
        public List<Operater> Operatera { get; set; }
        public List<FilmNajam> Najma { get; set; }
        public Liste()
        {

        }
        public Liste(List<Film> filmovi, List<Glumac> glumci, List<Redatelj> redatelji, List<Zanr> zanrovi, List<Gledatelj> gledatelji, List<Operater> operateri, List<FilmNajam> najam)
        {
            Filmova = filmovi;
            Glumaca = glumci;
            Redatelja = redatelji;
            Zanrova = zanrovi;
            Gledatelja = gledatelji;
            Operatera = operateri;
            Najma = najam;
        }
        //public List<Film> Filmova
        //{
        //    get { return UcitajX.dohvatiFilmove(); }
        //    private set { Filmovi = value; }
        //}
        //public List<Glumac> Glumaca
        //{
        //    get { return UcitajX.dohvatiGlumce(); }
        //    private set { Glumci = value; }
        //}
        //public List<Redatelj> Redatelja
        //{
        //    get { return UcitajX.dohvatiRedatelje(); }
        //    private set { Redatelji = value; }
        //}
        //public List<Zanr> Zanrova
        //{
        //    get { return UcitajX.dohvatiZanr(); }
        //    private set { Zanrovi = value; }
        //}
        //public List<Gledatelj> Gledatelja
        //{
        //    get { return UcitajX.dohvatiGledatelje(); }
        //    private set { Gledatelji = value; }
        //}
        //public List<Operater> Operatera
        //{
        //    get { return UcitajX.dohvatiOperatere(); }
        //    private set { Operateri = value; }
        //}
        //public List<FilmNajam> Najma
        //{
        //    get { return UcitajX.dohvatiNajam(); }
        //    private set { IznajmljeniFilmovi = value; }
        //}
    }
}
