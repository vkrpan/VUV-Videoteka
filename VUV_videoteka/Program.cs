using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace VUV_videoteka
{
    class Program
    {
        static void Main(string[] args)
        {
            VUV_Videoteka.Pocetak();
        }
    }
}


//var filmoviTest = listaFilmova.Select(x => new
//{
//    Glumci = string.Join(", ", x.Glumci.Select(gl => gl.Ime + " " + gl.Prezime)),
//    Naziv = x.Ime,
//    Redatelj = string.Join(", ", x.Redatelj.Select(red => $"{red.Ime} {red.Prezime}"))
//});
//foreach (var item in filmoviTest)
//{
//    Console.WriteLine($"Glumci: {item.Glumci} \nNaziv: {item.Naziv} \nRedatelj: {item.Redatelj}\n\n");
//}


//Liste l = new Liste(listaFilmova, listaGlumaca, listaRedatelja, listaZanrova, listaGledatelja, listaOperatera);
//prikaziIzbornik(l);
//List<Film> filmovi = Ucitaj.dohvatiFilmove();
//foreach (Film f in listaFilmova)
//{
//     Console.WriteLine("{0} {1}", f.Posuden ? " Posuden" : "Nije posuden", f.Ime);
//}


//public static void SetValue(PropertyInfo info, object instance, object value)
//{
//    info.SetValue(instance, Convert.ChangeType(value, info.PropertyType));
//}


//var input = Convert.ChangeType(Console.ReadLine(), prop.PropertyType)