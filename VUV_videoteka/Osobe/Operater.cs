using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VUV_videoteka
{
    class Operater : Osoba, IOperater
    {
        public string IdOp { get; }
        public Operater()
        {

        }
        public Operater(string idOp, string ime, string prezime, string oib)
        {
            Ime = ime;
            Prezime = prezime;
            IdOp = idOp;
            OIB = oib;
        }
        public void IzdavanjeFilma(Liste lista, Operater op)
        {
            bool nadenF = false;
            Film odabrani = new();
            Gledatelj gled = new();
            Console.Clear();
            do
            {
                var Postojeci = lista.Filmova.Where(x => x.Obrisan != true && x.Posuden != true).ToList();
                Console.WriteLine("Unesite redni broj filma kojeg zelite iznajmiti:");
                Tablice.IspisiFilmove(Postojeci);
                int odabirF = Provjere.provjeraBroj();
                for (int i = 0; i < Postojeci.Count; i++)
                {
                    if (odabirF == i + 1)                                            
                    {
                        nadenF = true;
                        odabrani = Postojeci[i];
                    }
                }
                if (!nadenF)
                {
                    Console.Clear();
                    Console.WriteLine("Niste odabrali redni broj filma iz tablice!");
                }
            } while (!nadenF);

            bool indic = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Unesite redni broj gledatelja kojeg zelite odabrati");
                var Postojeci = lista.Gledatelja.Where(x => x.Obrisan != true).ToList();
                Tablice.IspisiGledatelje(Postojeci);
                int odabirGled = Provjere.provjeraBroj();
                bool nadenGl = false;
                for (int i = 0; i < Postojeci.Count; i++)
                {
                    if (odabirGled == i + 1)                                            
                    {
                        gled = Postojeci[i];
                        nadenGl = true;
                    }
                }
                if (nadenGl)
                {
                    foreach (Film f in lista.Filmova)
                    {
                        if (f.Ime == odabrani.Ime)
                        {
                            FilmNajam izdaniFilm = new(RandomSifraNajam(""), DateTime.Now.ToString(), "", gled, op, f);
                            lista.Najma.Add(izdaniFilm);
                            Film promjena = new(f.ID, f.Ime, f.Godina, f.Trajanje, true, f.CijenaNajma, f.Obrisan, f.Glumci, f.Redatelj, f.Zanr);
                            List<Film> noviFilmovi = lista.Filmova.Where(x => x.ID != f.ID).ToList();
                            noviFilmovi.Add(promjena);
                            Console.WriteLine("\nFilm {0} uspjesno iznajmljen korisniku {1} {2} u {3}!", promjena.Ime, gled.Ime, gled.Prezime, izdaniFilm.DatumIznajmljivanja);
                            UpisiX.upisiFilmove(noviFilmovi);
                            UpisiX.upisiNajam(lista.Najma);
                            indic = true;
                        }
                    }
                }
                if (!nadenGl)
                {
                    Console.Clear();
                    Console.WriteLine("Odabrani gledatelj ne postoji!\nZelite li stvoriti novog gledatelja?(da/ne)");
                    Console.Write("Odabir: ");
                    string daNE = Console.ReadLine();
                    if (daNE.ToLower() == "da")
                    {
                        Console.Write("\nUnesite podatke o novom gledatelju!\n");
                        string imeNovog = Provjere.ProvjeraIme();
                        string prezime = Provjere.ProvjeraPrezime();
                        string OIBnovog = Provjere.ProvjeraOIB();
                        Console.Write("Adresa: ");
                        string adresa = Console.ReadLine();
                        Gledatelj noviGl = new(imeNovog, prezime, OIBnovog, adresa, VUV_Videoteka.RandomSifraGl(""), false);
                        lista.Gledatelja.Add(noviGl);
                        Console.WriteLine("Novi korisnik {0} {1} uspjesno je dodan!", imeNovog, prezime);
                        UpisiX.upisiGledatelje(lista.Gledatelja);
                    }
                    else if (daNE.ToLower() == "ne")
                    {
                        VUV_Videoteka.PonovoPrikaziIzbornik(lista, op);
                    }
                }
            } while (!indic);
        }
        public void PovratFilma(Liste lista, Operater o)
        {
            bool nadenGl = false;
            Gledatelj gled = new();
            Film FF = new();
            Console.Clear();
            do
            {
                List<Gledatelj> imajuIznajmljene = new();
                foreach (Gledatelj el in lista.Gledatelja.Where(x => x.Obrisan != true).ToList())
                {
                    foreach (FilmNajam el2 in lista.Najma)
                    {
                        if (el.SifraGl == el2.Gledatelj.SifraGl && el2.DatumVracanja == "")
                        {
                            if (imajuIznajmljene.Count == 0)
                            {
                                imajuIznajmljene.Add(el);
                            }

                            if (imajuIznajmljene.Any(ii => ii.SifraGl != el.SifraGl))
                            {
                                imajuIznajmljene.Add(el);
                            }
                        }
                    }
                }
                if (imajuIznajmljene.Count != 0)
                {
                    Console.WriteLine("Unesite redni broj gledatelja kojeg zelite odabrati");
                    Tablice.IspisiGledatelje(imajuIznajmljene);
                    int odabirGled = Provjere.provjeraBroj();
                    for (int i = 0; i < imajuIznajmljene.Count; i++)
                    {
                        if (odabirGled == i + 1)                                            
                        {
                            gled = imajuIznajmljene[i];
                            nadenGl = true;
                        }
                    }
                    if (!nadenGl)
                    {
                        Console.Clear();
                        Console.WriteLine("Niste odabrali redni broj gledatelja iz tablice!");
                    }
                }
                else
                {
                    Console.WriteLine("Ne postoji niti jedan gledatelj koji trenutno ima iznajmljen film!");
                    VUV_Videoteka.PonovoPrikaziIzbornik(lista, o);
                }
            } while (!nadenGl);
            bool nadenF = false;
            bool posuden = true;
            bool IznajmFilmNaden = false;
            List<Film> pomocna = new();
            Console.Clear();
            do
            {
                foreach (FilmNajam najam in lista.Najma)
                {
                    if (najam.Gledatelj.SifraGl == gled.SifraGl && najam.Film.Posuden == true && najam.Film.Obrisan == false && najam.DatumVracanja == "")
                    {

                        pomocna.Add(najam.Film);
                        IznajmFilmNaden = true;

                    }
                }
                if (IznajmFilmNaden)
                {
                filmOdabir:
                    Tablice.IspisiFilmove(pomocna);
                    Console.WriteLine("Unesite redni broj filma kojeg zelite vratiti");
                    int odabirZaVratit = Provjere.provjeraBroj();
                    for (int i = 0; i < pomocna.Count; i++)
                    {
                        if (odabirZaVratit == i + 1)                                            
                        {
                            if (pomocna[i].Posuden == true)
                            {
                                nadenF = true;
                                FF = pomocna[i];
                            }
                        }
                    }
                    string vracen;
                    bool datumIspravan = false;
                    bool datumRealan = false;
                    int danaIznajmljen = 0;
                    double placanje = 0;
                    Console.Clear();
                    if (nadenF)
                    {
                        do
                        {
                            Console.WriteLine("Unesite datum vracanja filma u formatu \nPrimjer: <10.6.2021. 08:06:04>");
                            Console.Write("\nUnos: ");
                            vracen = Console.ReadLine();
                            DateTime datumPovrata = new();
                            if (DateTime.TryParseExact(vracen, "d.M.yyyy. H:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out datumPovrata))
                            {
                                datumIspravan = true;
                                foreach (FilmNajam iznajmFilm in lista.Najma)
                                {
                                    if (FF.ID == iznajmFilm.Film.ID && iznajmFilm.Gledatelj.SifraGl == gled.SifraGl)
                                    {
                                        DateTime datumIznajmljivanja = DateTime.ParseExact(iznajmFilm.DatumIznajmljivanja, "d.M.yyyy. H:mm:ss", CultureInfo.InvariantCulture);             
                                        datumPovrata = DateTime.ParseExact(vracen, "d.M.yyyy. H:mm:ss", CultureInfo.InvariantCulture);
                                        if (datumPovrata > datumIznajmljivanja)
                                        {
                                            datumRealan = true;
                                            TimeSpan ts = datumPovrata - datumIznajmljivanja;
                                            danaIznajmljen = (int)Math.Ceiling(ts.TotalMinutes / 1440);        //osigurava da se naplati +1 dan kad prodje 24h+1min
                                        }
                                    }
                                }
                            }
                            else if (!datumIspravan)
                            {
                                Console.Clear();
                                Console.WriteLine("Niste unijei datum u ispravnom formatu, pokusajte ponovo!\n");
                            }

                            if (!datumRealan && datumIspravan == true)
                            {
                                Console.Clear();
                                Console.WriteLine("Unijeli ste datum povratka koji je prije datuma iznajmljivanja filma!");
                            }
                        } while (!datumRealan);
                        List<FilmNajam> filmNajams = new();
                        foreach (FilmNajam najam in lista.Najma)
                        {
                            if (najam.Film.ID == FF.ID && najam.Gledatelj.SifraGl == gled.SifraGl)
                            {
                                FilmNajam vracenFilm = new FilmNajam(najam.SifraNajma, najam.DatumIznajmljivanja, vracen, gled, najam.Operater, najam.Film);
                                filmNajams = lista.Najma.Where(x => x.SifraNajma != najam.SifraNajma).ToList();
                                filmNajams.Add(vracenFilm);
                                placanje = VUV_Videoteka.IzracunajNajam(FF, danaIznajmljen);
                                posuden = false;
                            }
                        }
                        Film vracenF = new(FF.ID, FF.Ime, FF.Godina, FF.Trajanje, false, FF.CijenaNajma, FF.Obrisan, FF.Glumci, FF.Redatelj, FF.Zanr);
                        List<Film> noviFilmovi = lista.Filmova.Where(x => x.Ime != FF.Ime).ToList();
                        noviFilmovi.Add(vracenF);
                        UpisiX.upisiFilmove(noviFilmovi);
                        UpisiX.upisiNajam(filmNajams);
                        Console.WriteLine("\nFilm {0} je uspjesno vracen {1}!\nUkupno za platiti: {2} kn", FF.Ime, vracen, placanje);
                    }
                    else
                    {
                        Console.WriteLine("Niste odabrali redni broj filma sa izborika!");
                        goto filmOdabir;
                    }
                }
                else
                {
                    Console.WriteLine("Odabrani korisnik nema iznajmljenih filmova!");
                    break;

                }
            } while (posuden);
        }
        public void DodavanjeBrisanjeAzuriranje(Liste lista, Operater op)
        {
            Console.Clear();
            Film noviF = new();
            Console.WriteLine("1. Dodavanje filma\n2. Brisanje filma\n3. Azuriranje filma");
            int odabirAkcije = Provjere.provjeraBroj();
            switch (odabirAkcije)
            {

                case 1:                                                //dodavanje filma
                    Console.Clear();
                    Console.WriteLine("Unesite id filma");
                    string id = Provjere.ProvjeraIdFilm(lista);
                    Console.WriteLine("Unesite naziv filma");
                    Console.Write("Odabir: ");
                    string ime = Console.ReadLine();
                    Console.WriteLine("Unesite godinu stvaranja filma");
                    int godina = Provjere.provjeriGodinu();
                    Console.WriteLine("Unesite trajanje filma u minutama");
                    int trajanje = Provjere.provjeraBroj();
                    Console.WriteLine("Unesite cijenu najma filma za jedan dan u HRK");
                    double cijenaNajma = Provjere.ProvjeraDouble();
                    List<Glumac> g = ListaGlumacaUnos(lista);
                    List<Redatelj> r = ListaRedateljaUnos(lista);
                    List<Zanr> z = ListaZanrovaUnos(lista);
                    noviF = new(id, ime, godina, trajanje, false, cijenaNajma, false, g, r, z);
                    lista.Filmova.Add(noviF);
                    UpisiX.upisiFilmove(lista.Filmova);
                    Console.WriteLine("Novi film {0} uspjesno je dodan!", ime);
                    break;
                case 2:
                    Console.Clear();
                    bool fNaden = false;
                    var Postojeci = lista.Filmova.Where(x => x.Obrisan != true && x.Posuden != true).ToList();
                    Console.WriteLine("Odaberite film koji zelite obrisati\n");
                    Tablice.IspisiFilmove(Postojeci);
                    int odabir = Provjere.provjeraBroj();
                    for (int i = 0; i < Postojeci.Count; i++)
                    {
                        if (odabir == i + 1)
                        {
                            noviF = Postojeci[i];
                            fNaden = true;
                        }
                    }
                    foreach (Film f in lista.Filmova)
                    {
                        if (noviF.ID == f.ID)
                        {
                            Console.WriteLine("Uspjesno ste obrisali {0}", f.Ime);
                            lista.Filmova.Remove(f);
                            lista.Filmova.Add(new Film(noviF.ID, noviF.Ime, noviF.Trajanje, noviF.Godina, noviF.Posuden, noviF.CijenaNajma, true, noviF.Glumci, noviF.Redatelj, noviF.Zanr));
                            break;
                        }
                    }
                    if (!fNaden)
                    {
                        Console.WriteLine("Niste odabrali postojeci redni broj filma iz tablice!");
                    }
                    UpisiX.upisiFilmove(lista.Filmova);
                    break;
                case 3:
                    AzuriranjeFilma(noviF, lista, op);
                    break;
                default:
                odabir:
                    Console.WriteLine("Niste odabrali neku od ponuđenih opcija sa izbornika! Zeite li ponovo odabrati?(da/ne)");
                    Console.Write("Odabir: ");
                    string biraj = Console.ReadLine();
                    if (biraj.ToLower() == "da")
                    {
                        DodavanjeBrisanjeAzuriranje(lista, op);
                    }
                    else if (biraj.ToLower() == "ne")
                    {
                        VUV_Videoteka.PonovoPrikaziIzbornik(lista, op);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Niste unijeli valjanu opciju!");
                        goto odabir;
                    }
                    break;
            }
        }
        private void AzuriranjeFilma(Film noviF, Liste lista, Operater op)
        {
            Console.Clear();
            Console.WriteLine("\nUnesite redni broj filma kojeg zelite azurirati");
            var Postojeci = lista.Filmova.Where(x => x.Obrisan != true && x.Posuden != true).ToList();
            Tablice.IspisiFilmove(Postojeci);
            int odabirFilma = Provjere.provjeraBroj();
            bool nadenF = false;
            for (int i = 0; i < Postojeci.Count; i++)
            {
                if (odabirFilma == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                {
                    noviF = Postojeci[i];
                    nadenF = true;
                }
            }
            if (!nadenF)
            {
                Console.WriteLine("Niste odabrali redni broj iz tablice!");
                VUV_Videoteka.PonovoPrikaziIzbornik(lista, op);
            }
            Console.Clear();
        ponovo:
            Console.WriteLine("1. - Azuriranje imena filma\n2. - Azuriranje cijene filma\n3. - Azuriranje zanrova");
            var odabir = Provjere.provjeraBroj();
            bool proslo = false;
            if (odabir == 1)
            {
                Console.Clear();
                proslo = true;
                Console.WriteLine("Trenutno ime filma: {0}", noviF.Ime);
                Console.Write("\nNovo ime: ");
                string novoStanje = Console.ReadLine();
                bool postoji = false;
                foreach (Film f in lista.Filmova)
                {
                    if (f.Ime.ToLower() == novoStanje.ToLower())
                    {
                        postoji = true;
                    }
                }
                if (!postoji)
                {
                    Console.WriteLine("Filmu {0} uspjesno je promjenjeno ime na: {1}", noviF.Ime, novoStanje);
                    lista.Filmova.Add(new Film(noviF.ID, novoStanje, noviF.Godina, noviF.Trajanje, noviF.Posuden, noviF.CijenaNajma, noviF.Obrisan, noviF.Glumci, noviF.Redatelj, noviF.Zanr));
                    lista.Filmova.Remove(noviF);
                }
                else
                {
                    Console.WriteLine("Unijeli ste ime filma koji vec postoji u bazi!");
                }
            }
            else if (odabir == 2)
            {
                Console.Clear();
                proslo = true;
                Console.WriteLine("Trenutna pojedinacna cijena filma {0}: {1}", noviF.Ime, noviF.CijenaNajma);
                Console.Write("\nUnesite novu jedinicnu cijena filma");
                double novoStanje = Provjere.ProvjeraDouble();
                Console.WriteLine("Filmu {0} uspjesno je promjenjena cijena najma sa:{1}  na:{2}", noviF.Ime, noviF.CijenaNajma, novoStanje);
                lista.Filmova.Add(new Film(noviF.ID, noviF.Ime, noviF.Godina, noviF.Trajanje, noviF.Posuden, novoStanje, noviF.Obrisan, noviF.Glumci, noviF.Redatelj, noviF.Zanr));
                lista.Filmova.Remove(noviF);
            }
            else if (odabir == 3)
            {
                proslo = true;
                AzuriranjeZanrova(lista, noviF);
            }
            if (!proslo)
            {
            odabir:
                Console.WriteLine("Niste odabrali valjanu opciju!\nZelite li odabrati akciju ponovo?(da/ne)");
                Console.Write("Unos: ");
                string unos = Console.ReadLine();
                if (unos.ToLower() == "da")
                {
                    goto ponovo;
                }
                else if (unos.ToLower() == "ne")
                {
                    VUV_Videoteka.PonovoPrikaziIzbornik(lista, op);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste unijeli valjanu opciju!");
                    goto odabir;
                }
            }
            UpisiX.upisiFilmove(lista.Filmova);
        }
        private void AzuriranjeZanrova(Liste lista, Film noviF)
        {
            bool kraj = false;
            Zanr zNaden = new();
            do
            {
                Console.Clear();
                string zanrovi = Tablice.ZanroviString(noviF);
                Console.WriteLine("Film {0} trenutno spada u zanr: {1}", noviF.Ime, zanrovi);
                Console.WriteLine("\n1. - Dodavanje zanra\n2. - Brisanje zanra");
                int zOdabir = Provjere.provjeraBroj();
                if (zOdabir == 1)
                {
                    Console.Clear();
                unos:
                    Console.WriteLine("Odaberite zanr kojeg zelite dodati na film");
                    Tablice.IspisZanrova(lista.Zanrova);
                    int odabir = Provjere.provjeraBroj();
                    for (int i = 0; i < lista.Zanrova.Count; i++)
                    {
                        if (odabir == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                        {
                            zNaden = lista.Zanrova[i];
                        }
                    }
                    foreach (Zanr z in lista.Zanrova)
                    {
                        if (z.IdZanr == zNaden.IdZanr)
                        {
                            if (noviF.Zanr.Any(x => x.IdZanr == z.IdZanr))
                            {
                                Console.Clear();
                                Console.WriteLine("Zanr koji ste odabrali vec je dodjeljen filmu!");
                                goto unos;
                            }
                        }
                    }
                    bool pronaden = false;
                    foreach (Zanr Z in lista.Zanrova)
                    {
                        if (zNaden.IdZanr == Z.IdZanr)
                        {
                            noviF.Zanr.Add(Z);
                            Console.WriteLine("\nZanr {0} uspjesno je dodan na film {1}", Z.Value, noviF.Ime);
                            pronaden = true;
                        }
                    }
                    if (!pronaden)
                    {
                        Console.WriteLine("ID zanra koji ste unijeli nije pronaden u bazi!");
                    }
                    UpisiX.upisiFilmove(lista.Filmova);
                }
                else if (zOdabir == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Odaberite zanr kojeg zelite obrisati sa filma");
                    Tablice.IspisZanrova(noviF.Zanr);
                    int ZanOdabir = Provjere.provjeraBroj();
                    for (int i = 0; i < noviF.Zanr.Count; i++)
                    {
                        if (ZanOdabir == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                        {
                            zNaden = noviF.Zanr[i];
                        }
                    }
                    bool pronaden = false;
                    foreach (Zanr Z in noviF.Zanr)
                    {
                        if (zNaden.IdZanr == Z.IdZanr)
                        {
                            Console.WriteLine("Zanr {0} je uspjesno obrisan iz filma {1}", Z.Value, noviF.Ime);
                            noviF.Zanr.Remove(Z);
                            pronaden = true;
                            break;
                        }
                    }
                    if (!pronaden)
                    {
                        Console.WriteLine("Niste odabrali redni broj zanra iz tablice!");
                    }
                    UpisiX.upisiFilmove(lista.Filmova);
                }
                else
                {
                    Console.WriteLine("Niste odabrali ponudenu opciju sa izbornika!");
                }
                odaberi:
                Console.WriteLine("\nZelite li nastaviti azuriranje zanrova?(da/ne)");
                Console.Write("Odabir: ");
                string nastavak = Console.ReadLine();
                if (nastavak.ToLower() == "da")
                {
                    continue;
                }
                else if (nastavak.ToLower() == "ne")
                {
                    kraj = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste odabrali valjanu opciju!");
                    goto odaberi;
                }
            } while (!kraj);
        }
        private List<Glumac> ListaGlumacaUnos(Liste lista)
        {
            List<Glumac> glumci = new();
            Glumac glumcc = new();
            bool stopGl = false;
            do                                                                //petlja se vrti dok korisnik ne odluci prestati unositi glumce za novi film
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("1. - Unosenje novog glumca\n2. - Odabir postojeceg glumca iz baze");
                    int odabir = Provjere.provjeraBroj();
                    if (odabir == 1)                                           //dodavanje novog glumca 
                    {
                        Console.Clear();
                        Console.WriteLine("Unesite podatke o glumcu");
                        Console.WriteLine("ID glumca");
                        string idGl = Provjere.ProvjeraIdGl(lista);
                        string imeNovog = Provjere.ProvjeraIme();
                        string prezimeNovog = Provjere.ProvjeraPrezime();
                        string OIBnovog = Provjere.ProvjeraOIB();
                        glumci.Add(new Glumac(idGl, imeNovog, prezimeNovog, OIBnovog));
                        lista.Glumaca.Add(new Glumac(idGl, imeNovog, prezimeNovog, OIBnovog));
                        UpisiX.upisiGlumce(lista.Glumaca);
                    }
                    else if (odabir == 2)                                      //odabir postojeceg glumca iz baze
                    {
                        bool nadenGlum = false;
                        List<Glumac> unike = new();
                        foreach (Glumac GL in lista.Glumaca)
                        {
                            foreach (Glumac gl in glumci)
                            {
                                if (GL.IdGl == gl.IdGl)
                                {
                                    continue;
                                }
                                else
                                {
                                    unike.Add(GL);
                                }
                            }
                        }
                        Tablice.IspisiGlumce(unike);
                        Console.WriteLine("Unesite redni broj glumca");
                        int Odabir = Provjere.provjeraBroj();
                        for (int i = 0; i < unike.Count; i++)
                        {
                            if (Odabir == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                            {
                                glumcc = unike[i];
                            }
                        }
                        foreach (Glumac GlUmAc in lista.Glumaca)
                        {
                            if (glumcc.IdGl == GlUmAc.IdGl)
                            {
                                nadenGlum = true;
                                Console.Clear();
                                Console.WriteLine("Glumac {0} {1} je uspjesno odabran!", GlUmAc.Ime, GlUmAc.Prezime);
                                glumci.Add(GlUmAc);
                                break;
                            }
                        }
                        if (!nadenGlum)
                        {
                            Console.WriteLine("Niste unijeli redni broj glumca sa izbornika!");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Niste odabrali ponudenu opciju sa izbornika!");

                    }
                }
                while (glumci.Count == 0);
            odaberi:
                Console.WriteLine("Zelite li nastaviti sa unosom glumaca?(da/ne)");
                Console.Write("Unos:");
                string nastavak = Console.ReadLine();
                if (nastavak.ToLower() == "ne")
                {
                    stopGl = true;
                }
                else if (nastavak.ToLower() == "da")
                    continue;
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste odabrali valjanu opciju!");
                    goto odaberi;
                }
            } while (!stopGl);
            return glumci;
        }
        private List<Redatelj> ListaRedateljaUnos(Liste lista)
        {
            List<Redatelj> redatelji = new();
            Redatelj red = new();
            bool stopRed = false;
            do                                                                //petlja se vrti dok korisnik ne odluci prestati unositi redatelje za novi film
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("1. - Unosenje novog redatelja\n2. - Odabir postojeceg redatelja iz baze");
                    int odabir = Provjere.provjeraBroj();
                    if (odabir == 1)                                           //dodavanje novog redatelja 
                    {
                        Console.WriteLine("Unesite podatke o redatelju");
                        Console.WriteLine("ID");
                        string idRed = Provjere.ProvjeraIdRed(lista);
                        string imeNovog = Provjere.ProvjeraIme();
                        string prezimeNovog = Provjere.ProvjeraPrezime();
                        string OIBnovog = Provjere.ProvjeraOIB();
                        redatelji.Add(new Redatelj(idRed, imeNovog, prezimeNovog, OIBnovog));
                        lista.Redatelja.Add(new Redatelj(idRed, imeNovog, prezimeNovog, OIBnovog));
                        UpisiX.upisiRedatelje(lista.Redatelja);
                    }
                    else if (odabir == 2)                                      //odabir postojeceg redatelja iz baze
                    {
                        Console.Clear();
                        Console.WriteLine("Odaberite redatelja");
                        List<Redatelj> unike = new();
                        foreach (Redatelj Redate in lista.Redatelja)
                        {
                            foreach (Redatelj RED in redatelji)
                            {
                                if (RED.IdRed == Redate.IdRed)
                                {
                                    continue;
                                }
                                else
                                {
                                    unike.Add(Redate);
                                }
                            }
                        }
                        Tablice.IspisRedatelja(unike);
                        int odabirR = Provjere.provjeraBroj();
                        for (int i = 0; i < unike.Count; i++)
                        {
                            if (odabirR == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                            {
                                red = unike[i];
                            }
                        }
                        bool pronaden = false;
                        foreach (Redatelj R in lista.Redatelja)
                        {
                            if (red.IdRed == R.IdRed)
                            {
                                Console.Clear();
                                Console.WriteLine("Redatelj {0} {1} je uspjesno odabran", R.Ime, R.Prezime);
                                redatelji.Add(R);
                                pronaden = true;
                                break;
                            }
                        }
                        if (!pronaden)
                        {
                            Console.WriteLine("Niste unijeli redni broj redatelja sa izbornika!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Niste odabrali ponudenu opciju sa izbornika!");

                    }

                }
                while (redatelji.Count == 0);
            odaberi:
                Console.WriteLine("Zelite li nastaviti sa unosom redatelja?(da/ne)");
                Console.Write("Odabir: ");
                string nastavak = Console.ReadLine();
                if (nastavak.ToLower() == "ne")
                {
                    stopRed = true;
                }
                else if (nastavak.ToLower() == "da")
                    continue;
                else
                {
                    Console.Clear();
                    Console.WriteLine("Niste odabrali valjanu opciju!");
                    goto odaberi;
                }
            } while (!stopRed);
            return redatelji;
        }
        private List<Zanr> ListaZanrovaUnos(Liste lista)
        {
            List<Zanr> zanrovi = new();
            Zanr zanr = new();
            bool stopZanr = false;
            do                                                                //petlja se vrti dok korisnik ne odluci prestati unositi zanrove za novi film
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("1. - Unosenje novog zanra\n2. - Odabir postojeceg zanra");
                    int odabir = Provjere.provjeraBroj();
                    if (odabir == 1)                                           //dodavanje novog zanra 
                    {
                        Console.Clear();
                        Console.WriteLine("Unesite id zanra");
                        string idZanr = Provjere.ProvjeraIdZanr(lista);
                        Console.Write("Unesite ime zanra: ");
                        string imeZanr = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Uspjesno ste dodali zanr {0}", imeZanr);
                        zanrovi.Add(new Zanr(idZanr, imeZanr));
                        lista.Zanrova.Add(new Zanr(idZanr, imeZanr));
                    }
                    else if (odabir == 2)                                      //odabir postojeceg zanra iz baze
                    {
                        Console.Clear();
                        Console.WriteLine("Odaberite zanr");
                        List<Zanr> unike = new();
                        foreach (Zanr zz in lista.Zanrova)
                        {
                            foreach (Zanr z in zanrovi)
                            {
                                if (z.IdZanr == zz.IdZanr)
                                {
                                    continue;
                                }
                                else
                                {
                                    unike.Add(zz);
                                }
                            }
                        }
                        Tablice.IspisZanrova(unike);
                        int ZOdabir = Provjere.provjeraBroj();
                        for (int i = 0; i < unike.Count; i++)
                        {
                            if (ZOdabir == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                            {
                                zanr = unike[i];
                            }
                        }
                        bool pronaden = false;
                        foreach (Zanr Z in lista.Zanrova)
                        {
                            if (zanr.IdZanr == Z.IdZanr)
                            {
                                Console.Clear();
                                Console.WriteLine("Zanr {0} je uspjesno odabran", Z.Value);
                                zanrovi.Add(Z);
                                pronaden = true;
                                break;
                            }
                        }
                        if (!pronaden)
                        {
                            Console.WriteLine("Niste odabrali redni broj zanra sa izbornika!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Niste odabrali valjanu opciju sa izbornika!");
                    }
                }
                while (zanrovi.Count == 0);
            odaberi:
                Console.WriteLine("Zelite li nastaviti sa unosom zanrova?(da/ne)");
                Console.Write("Odabir: ");
                string nastavak = Console.ReadLine();
                if (nastavak.ToLower() == "ne")
                {
                    stopZanr = true;
                }
                else if (nastavak.ToLower() == "da")
                    continue;
                else
                {
                    Console.WriteLine("Niste odabrali valjanu opciju!");
                    goto odaberi;
                }
            } while (!stopZanr);
            UpisiX.upisiZanrove(lista.Zanrova);
            return zanrovi;
        }
        public Operater prijavaOperater(Liste lista)
        {
            Operater op = new();
            Console.Clear();
            bool opNaden = false;
            do
            {
                bool nePostoji = true;
                Console.WriteLine("Odaberite redni broj operatera i unesite njegov id kao lozinku za ulaz");
                Tablice.IspisOperatera(lista.Operatera);
                int odabi = Provjere.provjeraBroj();
                for (int i = 0; i < lista.Operatera.Count; i++)
                {
                    if (odabi == i + 1)                                            // i+1 zato sto indeksi filmova u for-u krecu od 0, a u tablici koja ispisuje filmove krece od 1
                    {
                        op = lista.Operatera[i];
                        nePostoji = false;
                    }
                }
                if (!nePostoji)
                {
                    Console.Write("Lozinka: ");
                    string id = Console.ReadLine();
                    foreach (Operater opp in lista.Operatera)
                    {
                        if (id == op.IdOp && opp.IdOp == op.IdOp)
                        {
                            opNaden = true;
                            return op;
                        }
                    }
                }
                if (!opNaden)
                {
                    Console.Clear();
                    Console.WriteLine("Unesena je kriva lozinka za operatera {0}", op.Ime);
                }
                if (nePostoji)
                {
                    Console.Clear();
                    Console.WriteLine("Niste odabrali redni broj operatera sa izbornika");
                }
            } while (!opNaden);
            return null;
        }

        private string RandomSifraNajam(string staraSifra)
        {
            List<FilmNajam> najam = UcitajX.dohvatiNajam();
            staraSifra = najam[0].SifraNajma;
            for (int i = 0; i < najam.Count; i++)
            {
                if (Convert.ToInt32(staraSifra) < Convert.ToInt32(najam[i].SifraNajma))
                {
                    staraSifra = najam[i].SifraNajma;
                }
            }
            if (int.TryParse(staraSifra, out int next))
            {
                next++;
            }
            string novaSifra = next.ToString("D6");
            return novaSifra;
        }
    }
}














///////////////////////////////////////////// IZSLISTAVANJE IMENA SVOJSTVA I VRIJEDNOSTI TOG SVOJSTVA TRENUTNOG OBJEKTA

//foreach (Film f in lista.Filmova)
//{
//    if (f.Ime == odabir)
//    {
//        Console.WriteLine("Odaberite atribut filma kojeg zelite promjeniti");
//        foreach (PropertyInfo prop in f.GetType().GetProperties())
//        {
//            Console.WriteLine("{0}: {1}", prop.Name, f.GetType().GetProperty(prop.Name).GetValue(f));
//        }
//    }
//}





/////////// AZURIRANJE BILO KOJEG ATRIBUTA NEOVISNO O TIPU PODATKA

//Console.WriteLine("\nUnesite ime atributa kojeg zelite azurirati");
//Console.WriteLine("ID    Ime    Godina    Trajanje    Posuden    CijenaNajma");
//var odabirAtr = Console.ReadLine();
//foreach (PropertyInfo prop in noviF.GetType().GetProperties())
//{
//    if (prop.Name == odabirAtr)
//    {
//        Console.WriteLine("\nTrenutno stanje {0}: {1}", prop.Name, noviF.GetType().GetProperty(prop.Name).GetValue(noviF));
//        Console.Write("\nNovo stanje: ");
//        var novoSanje = Console.ReadLine();
//        noviF.GetType().GetProperty(prop.Name).SetValue(noviF,novoSanje);
//        proslo = true;
//        Console.WriteLine("\nNovo stanje atributa {0} iznosi {1}", prop.Name, noviF.GetType().GetProperty(prop.Name).GetValue(noviF));
//    }
//}


///////////////////////////////////////////////////////////////////////////////////////////////////////////
//bool proslo = false;
//foreach (PropertyInfo prop in noviF.GetType().GetProperties())
//{
//    if (prop.Name == odabirAtr)
//    {
//        Console.WriteLine("\nTrenutno stanje {0}: {1}", prop.Name, noviF.GetType().GetProperty(prop.Name).GetValue(noviF));
//        noviF.GetType().GetProperty(prop.Name).SetValue(noviF, novoSanje);
//        proslo = true;
//        Console.WriteLine("\nNovo stanje atributa {0} iznosi {1}", prop.Name, noviF.GetType().GetProperty(prop.Name).GetValue(noviF));   //zelim da program prepozna koji tip varijable ce korisnik unijeti??????
//    }
//}