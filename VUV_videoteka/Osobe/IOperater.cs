using System;
using System.Collections.Generic;
using System.Text;

namespace VUV_videoteka
{
    interface IOperater
    {
        void IzdavanjeFilma(Liste lista, Operater op);
        void PovratFilma(Liste lista, Operater op);
        Operater prijavaOperater(Liste lista);
    }
}
