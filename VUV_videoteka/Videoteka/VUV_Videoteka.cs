using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    static class VUV_Videoteka
    {
        public static void Pocetak()
        {
            List<Film> listaFilmova = new();
            List<Glumac> listaGlumaca = new List<Glumac>();
            List<Redatelj> listaRedatelja = new List<Redatelj>();
            List<Zanr> listaZanrova = new List<Zanr>();
            List<Gledatelj> listaGledatelja = new List<Gledatelj>();
            List<Operater> listaOperatera = new();
            List<FilmNajam> listaNajma = new();
            try
            {
                listaFilmova = UcitajX.dohvatiFilmove();
                listaGlumaca = UcitajX.dohvatiGlumce();
                listaRedatelja = UcitajX.dohvatiRedatelje();
                listaZanrova = UcitajX.dohvatiZanr();
                listaGledatelja = UcitajX.dohvatiGledatelje();
                listaOperatera = UcitajX.dohvatiOperatere();
                listaNajma = UcitajX.dohvatiNajam();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Liste lista = new(listaFilmova, listaGlumaca, listaRedatelja, listaZanrova, listaGledatelja, listaOperatera, listaNajma);
            ucitajListe(lista);
            Operater op = new();
            prikaziIzbornik(lista, op);
        }
        public static void prikaziIzbornik(Liste lista, Operater op)
        {
            if (op.IdOp == null)
            {
                op = op.prijavaOperater(lista);
                prikaziIzbornik(lista, op);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Odaberite akciju!");
                Console.WriteLine("1 - Izdavanje filma\n2 - Povrat filma\n3 - Dodavanje/Brisanje/Azuriranje filma\n4 - Trazilica filmova prema kljucnoj\n5 - Trazilica gledatelja\n6 - Prikazi sve izdane filmove\n7 - Dodavanje/Azuriranje gledatelja\n8 - Odjava operatera");
                int odabir = Provjere.provjeraBroj();
                switch (odabir)
                {
                    case 1:
                        op.IzdavanjeFilma(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 2:
                        op.PovratFilma(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 3:
                        op.DodavanjeBrisanjeAzuriranje(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 4:
                        TrazilicaFilmovaPoKljucnojZanru(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 5:
                        Console.Clear();
                        TrazilicaGledatelja(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 6:
                        PrikaziSveIzdaneFilmove(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 7:
                        DodavanjeAzurirnjeGledatelja(lista, op);
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                    case 8:
                        OdjavaOperatera(lista, op);
                        //funkcija odjaviOperatera() koja ce primat objekt op i vracat novi objekt operatera koji ce bit prazan
                        break;
                    default:
                        Console.WriteLine("Niste idabrali valjanu opciju sa izbornika!");
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                }
            }

        }
        public static void TrazilicaFilmovaPoKljucnojZanru(Liste lista, Operater op)
        {
            Console.Clear();
            Film ff = new();
            Console.WriteLine("1. - Trazenje prema kljucnoj rijeci\n2. - Trazenje prema zanru");
            int sw = Provjere.provjeraBroj();
            string unos;
            switch (sw)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Unesite kljucnu rijec po kojoj zelie pretraziti bazu filmova");
                    Console.Write("Unos: ");
                    unos = Console.ReadLine().ToLower();
                    List<Film> filmovi = lista.Filmova.Where(x => x.Obrisan != true && x.Ime.ToLower().Contains(unos)).ToList();
                    if(filmovi.Count == 0)
                    {
                        Console.WriteLine("Nema pronadenih filmova po kljucnoj rijeci {0}", unos);
                    }
                    else
                    {
                        Console.WriteLine("Pronadeni filmovi po kljucnoj rijeci {0}\n", unos);
                        Tablice.IspisFilmovaSGledateljima(filmovi, lista.Najma);
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Unesite ime zanra po kojem zelie pretraziti bazu filmova");
                    Console.Write("Unos: ");
                    unos = Console.ReadLine().ToLower();
                    List<Zanr> zanrovi = lista.Zanrova.Where(x => x.Value.ToLower().Contains(unos)).ToList();
                    List<Film> sviFilmovi = new();
                    foreach (Film f in lista.Filmova)
                    {
                        foreach (Zanr z in zanrovi)
                        {
                            foreach (Zanr zz in f.Zanr)
                            {
                                if (zz.IdZanr == z.IdZanr && !sviFilmovi.Exists(t => t.Ime == f.Ime))
                                {
                                    sviFilmovi.Add(f);
                                }
                            }
                        }
                    }
                    if (sviFilmovi.Count == 0)
                    {
                        Console.WriteLine("Nema pronadenih filmova po unesenom nazivu zanra {0}!", unos);
                    }
                    else
                    {
                        Console.WriteLine("Pronadeni filmovi po zanru {0}", unos);
                        Tablice.IspisFilmovaSGledateljima(sviFilmovi, lista.Najma);
                    }
                    break;
                default:
                    Console.Clear();
                    ponovo:
                    Console.WriteLine("Niste odabrali postojecu opciju sa izbornika!");
                    Console.WriteLine("Zelite li odabrati ponovo?(da/ne)");
                    Console.Write("Odabir: ");
                    string unoss = Console.ReadLine();
                    if (unoss.ToLower() == "da")
                    {
                        TrazilicaFilmovaPoKljucnojZanru(lista, op);
                    }
                    else if (unoss.ToLower() == "ne")
                    {
                        PonovoPrikaziIzbornik(lista, op);
                    }
                    else
                    {
                        Console.Clear();
                        goto ponovo;
                    }
                    break;
            }
        }
        public static void PrikaziSveIzdaneFilmove(Liste lista, Operater op)
        {
            Console.Clear();
            var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Ime filma", "Godina izdanja", "Trajanje(min)", "Zanrovi", "Ime i prezime gledatelja" }, EnableCount = false });
            int cntr = 1;
            foreach (FilmNajam f in lista.Najma)
            {
                if (f.DatumVracanja == "")
                {
                    string zanrovi = "";
                    foreach (Zanr z in f.Film.Zanr)
                    {
                        zanrovi += z.Value + ", ";
                    }
                    tablica.AddRow(cntr, f.Film.Ime, f.Film.Godina, f.Film.Trajanje, zanrovi, f.Gledatelj.Ime + " " + f.Gledatelj.Prezime);
                    cntr++;
                }
            }
            tablica.Write();
        }
        private static void DodavanjeAzurirnjeGledatelja(Liste lista, Operater op)
        {
            Console.Clear();
            Gledatelj Gled = new();
            Console.WriteLine("1. - Dodavanje gledatelja\n2. - Azuriranje gledatelja\n3. - Brisanje gledatelja");
            int odabir = Provjere.provjeraBroj();
            if (odabir == 1)
            {
                Console.Clear();
                Console.WriteLine("Unesite podatke gledatelja:");
                string imeNovog = Provjere.ProvjeraIme();
                string prezimeNovog = Provjere.ProvjeraPrezime();
                string OIBnovog = Provjere.ProvjeraOIB();
                Console.WriteLine("Unesite adresu prebivalista gledatelja:");
                string adresa = Console.ReadLine();
                Gled = new(imeNovog, prezimeNovog, OIBnovog, adresa, RandomSifraGl(""), false);
                Console.WriteLine("Novi gledatelj {0} {1} uspjesno je dodan te mu je dodjeljena sifra {2}", Gled.Ime, Gled.Prezime, Gled.SifraGl);
                lista.Gledatelja.Add(Gled);
                UpisiX.upisiGledatelje(lista.Gledatelja);
            }
            else if (odabir == 2)
            {
                AzuriranjeAtributa(lista, Gled, op);
            }
            else if (odabir == 3)
            {
            ponovo:
                Console.Clear();
                List<Gledatelj> postojeci = new();
                foreach (Gledatelj gl in lista.Gledatelja.Where(x => x.Obrisan != true).ToList())
                {
                    int counter = 0;
                    foreach (FilmNajam ff in lista.Najma)
                    {
                        if (gl.SifraGl == ff.Gledatelj.SifraGl && ff.DatumVracanja != "")
                        {
                            counter++;
                        }
                    }
                    if (counter == lista.Najma.Where(x => x.Gledatelj.SifraGl == gl.SifraGl).Count())
                    {
                        postojeci.Add(gl);
                    }
                }
                if (postojeci.Count > 0)
                {
                    Console.WriteLine("Unesite redni broj gledatelja kojeg zelite obrisati");
                    Tablice.IspisiGledatelje(postojeci);
                    int sifraUnos = Provjere.provjeraBroj();
                    bool nadenGl = false;
                    for (int i = 0; i < postojeci.Count; i++)
                    {
                        if (sifraUnos == i + 1)
                        {
                            Gled = postojeci[i];
                            nadenGl = true;
                        }
                    }
                    if (!nadenGl)
                    {
                        Console.WriteLine("Niste odabrali redni broj iz tablice!");

                    nepravilanUnos:
                        Console.WriteLine("Zelite li ponovo birati gledatelja za brisanje?(da/ne)");
                        Console.Write("Unos: ");
                        string nastavlja = Console.ReadLine();
                        if (nastavlja.ToLower() == "da")
                        {
                            goto ponovo;
                        }
                        else if (nastavlja.ToLower() == "ne")
                        {
                            PonovoPrikaziIzbornik(lista, op);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Niste unijeli jednu od ponuđenih opcija!");
                            goto nepravilanUnos;
                        }
                    }
                    foreach (Gledatelj g in lista.Gledatelja)
                    {
                        if (g.SifraGl == Gled.SifraGl)
                        {
                            Gledatelj obrisan = new(Gled.Ime, Gled.Prezime, Gled.OIB, Gled.Adresa, Gled.SifraGl, true);
                            Console.WriteLine("Gledatelj {0} {1} uspjesno je obrisan iz baze podataka", Gled.Ime, Gled.Prezime);
                            lista.Gledatelja.Remove(g);
                            lista.Gledatelja.Add(obrisan);
                            break;
                        }
                    }
                    UpisiX.upisiGledatelje(lista.Gledatelja);
                }
            }
            else
            {
            opet:
                Console.WriteLine("Niste odabrali niti jednu od ponuđenih akcija!");
                Console.Write("Zelite li unijeti ponovo?(da/ne)");
                Console.WriteLine("Unos:");
                string unoss = Console.ReadLine();
                if (unoss.ToLower() == "da")
                {
                    DodavanjeAzurirnjeGledatelja(lista, op);
                }
                else if (unoss.ToLower() == "ne")
                {
                    PonovoPrikaziIzbornik(lista, op);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste unijeli jednu od ponuđenih opcija!");
                    Console.Write("Unos: ");
                    goto opet;
                }
            }
        }
        private static void AzuriranjeAtributa(Liste lista, Gledatelj Gled, Operater op)
        {
            Gledatelj izmjenjen;
            Console.Clear();
            var Postojeci = lista.Gledatelja.Where(x => x.Obrisan != true).ToList();
            Console.WriteLine("Unesite redni broj gledatelja kojeg zelite odabrati");
            Tablice.IspisiGledatelje(Postojeci);
            int sifraUnos = Provjere.provjeraBroj();
            bool nadenGl = false;
            for (int i = 0; i < Postojeci.Count; i++)
            {
                if (sifraUnos == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                {
                    Gled = Postojeci[i];
                    nadenGl = true;
                }
            }
            if (!nadenGl)
            {
                Console.Clear();
                Console.WriteLine("Niste odabrali redni broj gledatelja iz tablice!");
            }
            if (nadenGl)
            {
                Console.Clear();
                Console.WriteLine("Odaberite atribut gledatelja kojeg zelite azurirati");
                Console.WriteLine("1- Ime\n2- Prezime\n3- Adresa\n4 - OIB");
                int a = Provjere.provjeraBroj();
                switch (a)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Trenutno ime gledatelja: {0}", Gled.Ime);
                        Console.WriteLine("Unesite novo ime gledatelja");
                        string novoIme = Provjere.ProvjeraIme();
                        izmjenjen = new(novoIme, Gled.Prezime, Gled.OIB, Gled.Adresa, Gled.SifraGl, Gled.Obrisan);
                        Console.WriteLine("Novo ime gledatelja {0} {1} sada glasi: {2}", Gled.Ime, Gled.Prezime , izmjenjen.Ime);
                        lista.Gledatelja.Remove(Gled);
                        lista.Gledatelja.Add(izmjenjen);
                        UpisiX.upisiGledatelje(lista.Gledatelja);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Trenutno prezime gledatelja: {0}", Gled.Prezime);
                        Console.WriteLine("Unesite novo prezimeime gledatelja");
                        string novoPrezime = Provjere.ProvjeraPrezime();
                        izmjenjen = new(Gled.Ime, novoPrezime, Gled.OIB, Gled.Adresa, Gled.SifraGl, Gled.Obrisan);
                        Console.WriteLine("Novo prezime gledatelja {0} {1} sada glasi: {2}", Gled.Ime, Gled.Prezime, izmjenjen.Prezime);
                        lista.Gledatelja.Remove(Gled);
                        lista.Gledatelja.Add(izmjenjen);
                        UpisiX.upisiGledatelje(lista.Gledatelja);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Trenutno adresu prebivalista gledatelja: {0}", Gled.Adresa);
                        Console.WriteLine("Unesite novu adresu prebivalista gledatelja");
                        string novaAdresa = Console.ReadLine();
                        izmjenjen = new(Gled.Ime, Gled.Prezime, Gled.OIB, novaAdresa, Gled.SifraGl, Gled.Obrisan);
                        Console.WriteLine("Nova adresa prebivalista gledatelja {0} {1} sada je: {2}", Gled.Ime, Gled.Prezime, izmjenjen.Adresa);
                        lista.Gledatelja.Remove(Gled);
                        lista.Gledatelja.Add(izmjenjen);
                        UpisiX.upisiGledatelje(lista.Gledatelja);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Trenutan OIB gledatelja: {0}", Gled.OIB);
                        Console.WriteLine("Unesite novu vrijednost OIB-a gledatelja");
                        string noviOIB = Provjere.ProvjeraOIB();
                        izmjenjen = new(Gled.Ime, Gled.Prezime, noviOIB, Gled.Adresa, Gled.SifraGl, Gled.Obrisan);
                        Console.WriteLine("Novi uneseni OIB gledatelja {0} {1} sada je: {2}", Gled.Ime, Gled.Prezime, izmjenjen.OIB);
                        lista.Gledatelja.Remove(Gled);
                        lista.Gledatelja.Add(izmjenjen);
                        UpisiX.upisiGledatelje(lista.Gledatelja);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Niste odabrali neki od ponudenih atributa sa izbornika!");
                        PonovoPrikaziIzbornik(lista, op);
                        break;
                }
            }
            else
            {
            opet:
                Console.WriteLine("Zelite li se vratiti na ponovan odabir gledatelja za azuriranje?(da/ne)");
                Console.Write("Unos: ");
                string unoss = Console.ReadLine();
                if (unoss.ToLower() == "da")
                {
                    AzuriranjeAtributa(lista, Gled, op);
                }
                else if (unoss.ToLower() == "ne")
                {
                    PonovoPrikaziIzbornik(lista, op);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste unijeli jednu od ponuđenih opcija!");
                    goto opet;
                }
            }

        }
        private static void TrazilicaGledatelja(Liste lista, Operater op)
        {
            Film FF = new();
            Console.WriteLine("Unesite redni broj gledatelja kojeg zelite odabrati");
            var Postojeci = lista.Gledatelja.Where(x => x.Obrisan != true).ToList();
            Gledatelj gled = new();
            Tablice.IspisiGledatelje(Postojeci);
            int odabirGled = Provjere.provjeraBroj();
            bool nadenGl = false;
            for (int i = 0; i < Postojeci.Count; i++)
            {
                if (odabirGled == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                {
                    gled = Postojeci[i];
                    nadenGl = true;
                }
            }
            if (!nadenGl)
            {
                Console.Clear();
                Console.WriteLine("Niste odabrali redni broj iz tablice!");
                TrazilicaGledatelja(lista, op);

            }
            List<Film> filmoviKodGledatelja = new();
            foreach (FilmNajam najam in lista.Najma)
            {
                if (najam.Gledatelj.SifraGl == gled.SifraGl && najam.Film.Posuden == true)
                {
                    if (najam.DatumVracanja == "")
                    {
                        filmoviKodGledatelja.Add(najam.Film);
                    }
                }
            }
            Console.Clear();
            if (filmoviKodGledatelja.Count != 0)
            {
                Console.WriteLine("Filmovi koji se trenutno nalaze kod korisnika {0} {1}", gled.Ime, gled.Prezime);
                Tablice.IspisiFilmove(filmoviKodGledatelja);
            }
            else
            {
                Console.WriteLine("Uneseni gledatelj nema trenutno iznajmljenih filmova!");
            }
            skok:
            Console.WriteLine("Zeite li prikazati sve prethodno posudene filmove korisnika {0}?(da/ne)", gled.Ime);
            Console.Write("Odabir: ");
            string biraj = Console.ReadLine();
            if (biraj.ToLower() == "da")
            {
                int cntr = 1;
                Console.Clear();
                var tablica = new ConsoleTable(new ConsoleTableOptions { Columns = new[] { "R.Br.", "Ime", "Godina", "Trajanje", "Datum posudbe", "Datum vracanja", "Ukupna cijena najma", "Zanr" }, EnableCount = false });

                var grupiraniZanrovi = lista.Najma.SelectMany(c => c.Film.Zanr.GroupBy(p => new { zanrNaziv = p.Value }, (key, items) => new { Zanr = key.zanrNaziv, FilmNajam = c, })).OrderBy(x => x.Zanr).ThenBy(x => x.FilmNajam.Film.Ime).ToList();

                foreach (var grupiranZanr in grupiraniZanrovi)
                {
                    if (grupiranZanr.FilmNajam.Gledatelj.SifraGl == gled.SifraGl && grupiranZanr.FilmNajam.DatumVracanja != "")
                    {
                        DateTime datumIznajmljivanja = DateTime.ParseExact(grupiranZanr.FilmNajam.DatumIznajmljivanja, "d.M.yyyy. H:mm:ss", CultureInfo.InvariantCulture);
                        DateTime datumPovrata = DateTime.ParseExact(grupiranZanr.FilmNajam.DatumVracanja, "d.M.yyyy. H:mm:ss", CultureInfo.InvariantCulture);
                        TimeSpan ts = datumPovrata - datumIznajmljivanja;
                        int danaIznajmljen = (int)Math.Ceiling(ts.TotalMinutes / 1440);
                        tablica.AddRow(cntr, grupiranZanr.FilmNajam.Film.Ime, grupiranZanr.FilmNajam.Film.Godina, grupiranZanr.FilmNajam.Film.Trajanje, grupiranZanr.FilmNajam.DatumIznajmljivanja, grupiranZanr.FilmNajam.DatumVracanja, IzracunajNajam(grupiranZanr.FilmNajam.Film, danaIznajmljen) + " kn", grupiranZanr.Zanr);
                        cntr++;
                    }
                }
                tablica.Write();
            }
            else if (biraj.ToLower() == "ne")
            {
                PonovoPrikaziIzbornik(lista, op);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Niste unijeli valjani odabir!");
                goto skok;
            }
        }
        public static void PonovoPrikaziIzbornik(Liste lista, Operater op)
        {
            Console.WriteLine("\nPritisnite tipku < Enter > za ponovni prikaz glavnog izbornika, a tipku < ESC > za izlaz iz programa");
            bool krajUnosa = false;
            ConsoleKeyInfo sadrzajUnosa = Console.ReadKey(true);
            while (!krajUnosa)
            {
                if (sadrzajUnosa.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    OcistiListe(lista);
                    ucitajListe(lista);
                    krajUnosa = true;
                    prikaziIzbornik(lista, op);
                }
                else if (sadrzajUnosa.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Niste pritisnuli niti jednu od ponudenih tipki!");
                    Console.Clear();
                    PonovoPrikaziIzbornik(lista, op);
                }
            }
        }
        private static void OdjavaOperatera(Liste lista, Operater op)
        {
            Console.Clear();
            Console.WriteLine("Operater {0} uspjesno je odjavljen!\n", op.Ime);
            op = new Operater();
        odabir:
            Console.WriteLine("1. - Prijava operatera\n2. - Izlaz iz programa");
            int odabir = Provjere.provjeraBroj();
            if (odabir == 1)
            {
                op = op.prijavaOperater(lista);
                prikaziIzbornik(lista, op);
            }
            else if (odabir == 2)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Niste odabrali niti jednu od ponuđenih opcija!");
                goto odabir;
            }
        }

        private static void OcistiListe(Liste liste)
        {
            liste.Filmova.Clear();
            liste.Gledatelja.Clear();
            liste.Glumaca.Clear();
            liste.Najma.Clear();
            liste.Operatera.Clear();
            liste.Redatelja.Clear();
            liste.Zanrova.Clear();
        }
        private static Liste ucitajListe(Liste lista)
        {
            lista.Filmova = UcitajX.dohvatiFilmove();
            lista.Glumaca = UcitajX.dohvatiGlumce();
            lista.Redatelja = UcitajX.dohvatiRedatelje();
            lista.Zanrova = UcitajX.dohvatiZanr();
            lista.Gledatelja = UcitajX.dohvatiGledatelje();
            lista.Operatera = UcitajX.dohvatiOperatere();
            lista.Najma = UcitajX.dohvatiNajam();
            return lista;
        }
        public static string RandomSifraGl(string staraSifra)
        {
            var gledatelji = UcitajX.dohvatiGledatelje();
            staraSifra = gledatelji[0].SifraGl;
            for (int i = 0; i < gledatelji.Count; i++)
            {
                if (Convert.ToInt32(staraSifra) < Convert.ToInt32(gledatelji[i].SifraGl))
                {
                    staraSifra = gledatelji[i].SifraGl;
                }
            }
            if (int.TryParse(staraSifra, out int next))
            {
                next++;
            }
            string novaSira = next.ToString("D6");
            return novaSira;
        }
        public static double IzracunajNajam(Film filmKojiSeIznajmljuje, int danaIznajmljen)
        {
            double UkCijena = filmKojiSeIznajmljuje.CijenaNajma * danaIznajmljen;
            return UkCijena;
        }
    }
}




//bool proslo = false;
//Console.WriteLine("Unesite tocno ime atributa kojeg zelite azurirati");
//Console.Write("Unos: ");
//string odabirAtr = Console.ReadLine();
//foreach (PropertyInfo prop in Gled.GetType().GetProperties())
//{
//    if (prop.Name == odabirAtr)
//    {
//        proslo = true;
//        Console.WriteLine("\nTrenutno stanje atributa {0}: {1}", prop.Name, Gled.GetType().GetProperty(prop.Name).GetValue(Gled));
//        Console.Write("\nNova vrijednost: ");
//        var novoStanje = Convert.ChangeType(Console.ReadLine(), prop.PropertyType);
//        Gled.GetType().GetProperty(prop.Name).SetValue(Gled, novoStanje);
//        Console.WriteLine("\nNova vrijednost atributa {0} iznosi {1}", prop.Name, Gled.GetType().GetProperty(prop.Name).GetValue(Gled));   //zelim da program prepozna koji tip varijable ce korisnik unijeti??????
//        UpisiX.upisiGledatelje(lista.Gledatelja);
//    }
//}
//if (!proslo)
//{
//    Console.WriteLine("\nUneseno ime atributa nije prondeno!");
//}