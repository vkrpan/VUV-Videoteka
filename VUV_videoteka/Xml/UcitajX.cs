using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace VUV_videoteka
{
    static class UcitajX
    {
        public static List<Film> dohvatiFilmove()
        {
            List<Film> listaFilmova = new List<Film>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Filmovi.xml");
            var grana = docX.Root.Elements("film");
            foreach (XElement el in grana)
            {
                listaFilmova.Add(new Film(el.Attribute("idFilm").Value,
                                          el.Attribute("ime").Value,
                                          Convert.ToInt32(el.Attribute("godina").Value),
                                          Convert.ToInt32(el.Attribute("trajanje").Value),
                                          Convert.ToBoolean(el.Attribute("posuden").Value),
                                          Convert.ToDouble(el.Attribute("cijenaNajma").Value),
                                          Convert.ToBoolean(el.Attribute("deleted").Value),
                                          glumciFilm(el),
                                          redateljFilm(el),
                                          zanrFilm(el)));
            }

            return listaFilmova;
        }
        private static List<Glumac> glumciFilm(XElement element)
        {
            var listaGl = new List<Glumac>();
            var glumci = element.Elements("glumac");
            foreach (Glumac gl in dohvatiGlumce())
            {
                foreach (XElement el in glumci)
                {
                    if (gl.IdGl == el.Attribute("idGl").Value)
                    {
                        listaGl.Add(gl);
                    }
                }
            }
            return listaGl;
        }
        private static List<Redatelj> redateljFilm(XElement element)
        {
            var listaRed = new List<Redatelj>();
            var redatelj = element.Elements("redatelj");
            foreach (Redatelj red in dohvatiRedatelje())
            {
                foreach (XElement el in redatelj)
                {
                    if (red.IdRed == el.Attribute("idRed").Value)
                    {
                        listaRed.Add(red);
                    }
                }
            }
            return listaRed;
        }
        private static List<Zanr> zanrFilm(XElement element)
        {
            var listaZanr = new List<Zanr>();
            var zanr = element.Elements("zanr");
            foreach (Zanr z in dohvatiZanr())
            {
                foreach (XElement el in zanr)
                {
                    if (z.IdZanr == el.Attribute("idZanr").Value)
                    {
                        listaZanr.Add(z);
                    }
                }
            }
            return listaZanr;
        }
        public static List<Glumac> dohvatiGlumce()
        {
            List<Glumac> glumci = new List<Glumac>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Glumci.xml");
            var grana = docX.Root.DescendantNodes();
            foreach (XElement el in grana)
            {
                glumci.Add(new Glumac(el.Attribute("idGl").Value,
                                      el.Attribute("ime").Value,
                                      el.Attribute("prezime").Value,
                                      el.Attribute("OIB").Value));
            }
            return glumci;
        }
        public static List<Redatelj> dohvatiRedatelje()
        {
            List<Redatelj> redatelji = new List<Redatelj>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Redatelji.xml");
            var grana = docX.Root.DescendantNodes();
            foreach (XElement el in grana)
            {
                redatelji.Add(new Redatelj(el.Attribute("idRed").Value,
                                      el.Attribute("ime").Value,
                                      el.Attribute("prezime").Value,
                                      el.Attribute("OIB").Value));
            }
            return redatelji;
        }
        public static List<Zanr> dohvatiZanr()
        {
            List<Zanr> zanr = new List<Zanr>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Zanrovi.xml");
            var grana = docX.Root.DescendantNodes();
            foreach (XElement el in grana)
            {
                zanr.Add(new Zanr(el.Attribute("idZanr").Value,
                                  el.Attribute("value").Value));
            }
            return zanr;
        }
        public static List<Operater> dohvatiOperatere()
        {
            List<Operater> operateri = new List<Operater>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Operateri.xml");
            var grana = docX.Root.DescendantNodes();
            foreach (XElement el in grana)
            {
                operateri.Add(new Operater(el.Attribute("idOp").Value,
                                           el.Attribute("ime").Value,
                                           el.Attribute("prezime").Value,
                                           el.Attribute("OIB").Value));
            }
            return operateri;
        }
        public static List<Gledatelj> dohvatiGledatelje()
        {
            List<Gledatelj> gledatelji = new List<Gledatelj>();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Gledatelji.xml");
            var grana = docX.Root.DescendantNodes();
            foreach (XElement el in grana)
            {
                gledatelji.Add(new Gledatelj(el.Attribute("ime").Value,
                                           el.Attribute("prezime").Value,
                                           el.Attribute("OIB").Value,
                                           el.Attribute("adresa").Value,
                                           el.Attribute("sifraGled").Value,
                                           Convert.ToBoolean(el.Attribute("deleted").Value)));
            }
            return gledatelji;
        }
        public static List<FilmNajam> dohvatiNajam()
        {
            List<FilmNajam> najam = new();
            XDocument docX = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\FilmNajam.xml");
            var grana = docX.Root.Elements("film");
            foreach (XElement el in grana)
            {
                najam.Add(new FilmNajam(el.Attribute("sifraNajam").Value,
                                       (el.Attribute("datIznajm").Value),
                                       (el.Attribute("datVracanja").Value),
                                        nadenGledatelj(el),
                                        nadenOperater(el),
                                        nadenFilm(el)));
            }
            return najam;
        }
        private static Gledatelj nadenGledatelj(XElement element)
        {
            List<Gledatelj> gledatelji = UcitajX.dohvatiGledatelje();
            foreach (Gledatelj gl in gledatelji)
            {
                if (gl.SifraGl == element.Attribute("sifraGl").Value)
                {
                    return gl;
                }
            }
            return null;
        }
        private static Operater nadenOperater(XElement element)
        {
            List<Operater> operateri = UcitajX.dohvatiOperatere();
            foreach (Operater op in operateri)
            {
                if (op.IdOp == element.Attribute("idOp").Value)
                {
                    return op;
                }
            }
            return null;
        }
        private static Film nadenFilm(XElement element)
        {
            List<Film> filmovi = UcitajX.dohvatiFilmove();
            foreach (Film f in filmovi)
            {
                if (f.ID == element.Attribute("idFilm").Value)
                {
                    return f;
                }
            }
            return null;
        }
    }
}