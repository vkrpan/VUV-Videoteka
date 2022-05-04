using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    static class Provjere
    {
        public static string ProvjeraIme()
        {
            bool kraj = false;
            do
            {
                Console.Write("Ime: ");
                string unos = Console.ReadLine();
                try
                {
                    if (unos != null && unos != "" && unos != " " && IsAllLetters(unos))
                    {
                        kraj = true;
                        return unos;
                    }
                    else
                    {
                        throw new ImeException("Ime ne smije ostati prazno te smije sadrzavati samo slova, tocku i crticu!");
                    }
                }
                catch(ImeException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            } while (!kraj);
            return string.Empty;
        }
        public static string ProvjeraPrezime()
        {
            bool kraj = false;
            do
            {
                Console.Write("Prezime: ");
                string unos = Console.ReadLine();
                try
                {
                    if (unos != null && unos != "" && unos != " " && IsAllLetters(unos))
                    {
                        kraj = true;
                        return unos;
                    }
                    else
                    {
                        throw new PrezimeException("Prezime ne smije ostati prazno te smije sadrzavati samo slova, tocku i crticu!");
                    }
                }
                catch (PrezimeException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            } while (!kraj);
            return string.Empty;
        }
        public static string ProvjeraOIB()
        {
            bool kraj = false;
            do
            {
                try
                {
                    Console.Write("OIB: ");
                    string unos = Console.ReadLine();
                    if (IsAllDigits(unos) && unos.Length == 11)
                    {
                        kraj = true;
                        return unos;
                    }
                    else
                    {
                        throw new OIBException("OIB mora sadrzavati tocno 11 znamenaka!");
                    }
                }
                catch(OIBException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
            } while (!kraj);
            return string.Empty;
        }
        public static double ProvjeraDouble()
        {
            bool kraj = false;
            do
            {
                try
                {
                    Console.Write("Cijena: ");
                    double unos = Convert.ToDouble(Console.ReadLine().Replace('.',','));
                    if (unos > 0)
                    {
                        kraj = true;
                        return unos;
                    }
                    else
                    {
                        throw new DoubleException("Cijena najma filma za jedan dan mora biti veca od 0!");
                    }
                }
                catch(DoubleException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }
                catch(Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                }

            } while (!kraj);
            return 0;
        }
        public static int provjeraBroj()
        {
            bool num = false;
            while (!num)
            {
                Console.Write("Unos: ");
                if (int.TryParse(Console.ReadLine(), out int broj))
                {
                    return broj;
                }
                else
                {
                    Console.WriteLine("Niste unijeli cijelobrojnu vrijednost, pokusajte ponoovo!");
                }
            }
            return 0;
        }
        public static string ProvjeraIdFilm(Liste lista)
        {
            bool krajUnosa = false;
            do
            {
                try
                {
                    bool postoji = false;
                    Console.Write("Odabir: ");
                    string id = Console.ReadLine();
                    foreach (Film f in lista.Filmova)
                    {
                        if (Convert.ToInt32(f.ID) == Convert.ToInt32(id))
                        {
                            postoji = true;
                        }
                    }
                    if (postoji)
                    {
                        Console.Clear();
                        Console.WriteLine("Unijeli ste id koji je vec dodjeljen nekom filmu!");
                    }
                    else
                    {
                        krajUnosa = true;
                        return id;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (!krajUnosa);
            return string.Empty;
        }
        public static string ProvjeraIdGl(Liste lista)
        {
            bool krajUnosa = false;
            do
            {
                try
                {
                    bool postoji = false;
                    Console.Write("Unos: ");
                    string id = Console.ReadLine();
                    foreach (Glumac gl in lista.Glumaca)
                    {
                        if (Convert.ToInt32(gl.IdGl) == Convert.ToInt32(id))
                        {
                            postoji = true;
                        }
                    }
                    if (postoji)
                    {
                        Console.Clear();
                        Console.WriteLine("Unijeli ste id koji je vec dodjeljen nekom glumcu!");
                    }
                    else
                    {
                        krajUnosa = true;
                        return id;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (!krajUnosa);
            return string.Empty;
        }
        public static string ProvjeraIdRed(Liste lista)
        {
            bool krajUnosa = false;
            do
            {
                try
                {
                    bool postoji = false;
                    Console.Write("Odabir: ");
                    string id = Console.ReadLine();
                    foreach (Redatelj red in lista.Redatelja)
                    {
                        if (Convert.ToInt32(red.IdRed) == Convert.ToInt32(id))
                        {
                            postoji = true;
                        }
                    }
                    if (postoji)
                    {
                        Console.Clear();
                        Console.WriteLine("Unijeli ste id koji je vec dodjeljen nekom redatelju!");
                    }
                    else
                    {
                        krajUnosa = true;
                        return id;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!krajUnosa);
            return string.Empty;
        }
        public static string ProvjeraIdZanr(Liste lista)
        {
            bool krajUnosa = false;
            do
            {
                try
                {
                    bool postoji = false;
                    Console.Write("Odabir: ");
                    string id = Console.ReadLine();
                    foreach (Zanr z in lista.Zanrova)
                    {
                        if (Convert.ToInt32(z.IdZanr) == Convert.ToInt32(id))
                        {
                            postoji = true;
                        }
                    }
                    if (postoji)
                    {
                        Console.Clear();
                        Console.WriteLine("Unijeli ste id koji je vec dodjeljen nekom zanru!");
                    }
                    else
                    {
                        krajUnosa = true;
                        return id;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (!krajUnosa);
            return string.Empty;
        }
        public static int provjeriGodinu()
        {
            bool dobar = false;
            while (!dobar)
            {
                Console.Write("Unos: ");
                if (int.TryParse(Console.ReadLine(), out int broj))
                {
                    if (broj <= DateTime.Now.Year)
                    {
                        return broj;
                    }
                    else
                    {
                        Console.WriteLine("Unijeli se godinu izdavanje filma koja je veca od trenutne godine!");
                    }
                }
                else
                {
                    Console.WriteLine("Niste unijeli cijelobrojnu vrijednost, pokusajte ponoovo!");
                }
            }
            return 0;
        }
        private static bool IsAllLetters(string s)
        {
            string pattern = @"^[-a-zA-Z. ]+$";
            Regex reg = new Regex(pattern);
            if (reg.IsMatch(s))
            {
                return true;
            }
            else
                return false;
        }
        private static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
