using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    static class Tablice
    {


        public static void IspisiGlumce(List<Glumac> glumci)
        {
            ConsoleTable tablica = new ConsoleTable(new[] { "R.Br.", "Ime", "Prezime", "OIB" });

            tablica.Options.EnableCount = false;

            int cntr = 1;
            foreach (Glumac gl in glumci)
            {
                tablica.AddRow(cntr, gl.Ime, gl.Prezime, gl.OIB);
                cntr++;
            }
            tablica.Write();
        }
        public static void IspisZanrova(List<Zanr> zanrovi)
        {
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Zanr" }, EnableCount = false });
            int cntr = 1;
            foreach (Zanr z in zanrovi)
            {
                tablica.AddRow(cntr, z.Value);
                cntr++;
            }
            tablica.Write();

        }
        public static string ZanroviString(Film f)
        {
            string zanrovi = "";
            for (int i = 0; i < f.Zanr.Count; i++)
            {
                if (i == f.Zanr.Count - 1)
                {
                    zanrovi += f.Zanr[i].Value;
                }
                else
                {
                    zanrovi += f.Zanr[i].Value + ", ";
                }
            }
            return zanrovi;
        }
        public static void IspisiFilmove(List<Film> filmovi)
        {
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Naziv filma", "Godina izdanja", "Trajanje(min)", "Zanrovi" }, EnableCount = false });
            int count = 1;
            foreach (Film f in filmovi)
            {
                if (!f.Obrisan)
                {
                    tablica.AddRow(count, f.Ime, f.Godina + ".", f.Trajanje, ZanroviString(f));
                    count++;
                }
            }
            tablica.Write();
        }
        public static void IspisFilmovaSGledateljima(List<Film> filmovi, List<FilmNajam> filmoviNajam)
        {
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Naziv filma", "Godina izdanja", "Trajanje(min)", "Zanrovi", "Iznajmljen", "Ime i prezime gledatelja", "OIB gledatelja", "Adresa prebivalista gledatelja" }, EnableCount = false });
            int count = 1;
            foreach (Film f in filmovi)
            {
                foreach (FilmNajam fn in filmoviNajam)
                {
                    if (f.Posuden == true && f.ID == fn.Film.ID)
                    {
                        tablica.AddRow(count + ".", f.Ime, f.Godina + ".", f.Trajanje, ZanroviString(f), f.Posuden ? "Da" : "Ne", fn.Gledatelj.Ime + " " + fn.Gledatelj.Prezime, fn.Gledatelj.OIB, fn.Gledatelj.Adresa);
                        break;
                    }
                }
                if (f.Posuden == false)
                {
                    tablica.AddRow(count + ".", f.Ime, f.Godina, f.Trajanje, ZanroviString(f), f.Posuden ? "Da" : "Ne", "", "", "");
                }
                count++;
            }
            tablica.Write();

        }
        public static void IspisOperatera(List<Operater> operateri)
        {
            var tablica = new ConsoleTable(
                new ConsoleTableOptions
                {
                    Columns = new[] { "R.Br.", "Ime", "Prezime" },
                    EnableCount = false
                });
            int cntr = 1;
            foreach (Operater op in operateri)
            {
                tablica.AddRow(cntr, op.Ime, op.Prezime);
                cntr++;
            }
            tablica.Write();
        }
        public static void IspisiGledatelje(List<Gledatelj> gledatelji)
        {
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "Redni Br.", "Sifra", "Ime", "Prezime", "Adresa", "OIB" }, EnableCount = false });
            int cntr = 1;
            foreach (Gledatelj gl in gledatelji)
            {
                if (gl.Obrisan == false)
                {
                    tablica.AddRow(cntr, gl.SifraGl, gl.Ime, gl.Prezime, gl.Adresa, gl.OIB);
                    cntr++;
                }
            }
            tablica.Write();
        }
        public static void IspisRedatelja(List<Redatelj> redatelji)
        {
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Ime", "Prezime" }, EnableCount = false });
            int cntr = 1;
            foreach (Redatelj red in redatelji)
            {
                tablica.AddRow(cntr, red.Ime, red.Prezime);
                cntr++;
            }
            tablica.Write();
        }
    }
}
