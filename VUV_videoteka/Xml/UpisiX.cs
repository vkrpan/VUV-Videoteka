using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VUV_videoteka
{
    static class UpisiX
    {
        public static void upisiFilmove(List<Film> filmovi)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Filmovi.xml");
            doc.Root.RemoveNodes();
            foreach (Film zapis in filmovi)
            {
                var newElement = new XElement("film",
                    new XAttribute("idFilm", zapis.ID),
                    new XAttribute("ime", zapis.Ime),
                    new XAttribute("godina", zapis.Godina),
                    new XAttribute("trajanje", zapis.Trajanje),
                    new XAttribute("posuden", zapis.Posuden),
                    new XAttribute("cijenaNajma", zapis.CijenaNajma.ToString()),
                    new XAttribute("deleted", zapis.Obrisan));
                foreach(Redatelj red in zapis.Redatelj)
                {
                    newElement.Add(new XElement("redatelj",
                                                   new XAttribute("idRed", red.IdRed)));
                }
                foreach (Glumac gl in zapis.Glumci)
                {
                    newElement.Add(new XElement("glumac",
                                                   new XAttribute("idGl", gl.IdGl)));
                }
                foreach (Zanr z in zapis.Zanr)
                {
                    newElement.Add(new XElement("zanr",
                                                   new XAttribute("idZanr", z.IdZanr)));
                }
                doc.Element("Filmovi").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Filmovi.xml");
        }
        public static void upisiGlumce(List<Glumac> glumci)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Glumci.xml");
            doc.Root.RemoveNodes();
            foreach (Glumac zapis in glumci)
            {
                var newElement = new XElement("glumac",
                    new XAttribute("idGl", zapis.IdGl),
                    new XAttribute("ime", zapis.Ime),
                    new XAttribute("prezime", zapis.Prezime),
                    new XAttribute("OIB", zapis.OIB));
                doc.Element("Glumci").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Glumci.xml");
        }
        public static void upisiRedatelje(List<Redatelj> redatelji)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Redatelji.xml");
            doc.Root.RemoveNodes();
            foreach (Redatelj zapis in redatelji)
            {
                var newElement = new XElement("redatelj",
                    new XAttribute("idRed", zapis.IdRed),
                    new XAttribute("ime", zapis.Ime),
                    new XAttribute("prezime", zapis.Prezime),
                    new XAttribute("OIB", zapis.OIB));
                doc.Element("Redatelji").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Redatelji.xml");
        }
        public static void upisiZanrove(List<Zanr> zanrovi)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Zanrovi.xml");
            doc.Root.RemoveNodes();
            foreach (Zanr zapis in zanrovi)
            {
                var newElement = new XElement("zanr",
                    new XAttribute("idZanr", zapis.IdZanr),
                    new XAttribute("value", zapis.Value));
                doc.Element("Zanr").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Zanrovi.xml");
        }
        public static void upisiOperatere(List<Operater> operateri)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Operateri.xml");
            doc.Root.RemoveNodes();
            foreach (Operater zapis in operateri)
            {
                var newElement = new XElement("operater",
                    new XAttribute("idOp", zapis.IdOp),
                    new XAttribute("ime", zapis.Ime),
                    new XAttribute("prezime", zapis.Prezime),
                    new XAttribute("OIB", zapis.OIB));
                doc.Element("Operateri").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Operateri.xml");
        }
        public static void upisiGledatelje(List<Gledatelj> gledatelji)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Gledatelji.xml");
            doc.Root.RemoveNodes();
            foreach (Gledatelj zapis in gledatelji)
            {
                var newElement = new XElement("gledatelj",
                    new XAttribute("ime", zapis.Ime),
                    new XAttribute("prezime", zapis.Prezime),
                    new XAttribute("OIB", zapis.OIB),
                    new XAttribute("adresa", zapis.Adresa),
                    new XAttribute("sifraGled", zapis.SifraGl),
                    new XAttribute("deleted", zapis.Obrisan));
                doc.Element("Gledatelji").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\Gledatelji.xml");
        }
        public static void upisiNajam(List<FilmNajam> najam)
        {
            var doc = XDocument.Load(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\FilmNajam.xml");
            doc.Root.RemoveNodes();
            foreach (FilmNajam zapis in najam)
            {
                var newElement = new XElement("film",
                    new XAttribute("sifraNajam", zapis.SifraNajma),
                    new XAttribute("datIznajm", zapis.DatumIznajmljivanja),
                    new XAttribute("datVracanja", zapis.DatumVracanja),
                    new XAttribute("idOp", zapis.Operater.IdOp),
                    new XAttribute("idFilm", zapis.Film.ID),
                    new XAttribute("sifraGl", zapis.Gledatelj.SifraGl));
                doc.Element("filmNajam").Add(newElement);
            }
            doc.Save(@"C:\Users\VK\Desktop\faks\2. semestar\OOP\KV\VUV_videoteka\VUV_videoteka\Xml\FilmNajam.xml");
        }
    }
}
