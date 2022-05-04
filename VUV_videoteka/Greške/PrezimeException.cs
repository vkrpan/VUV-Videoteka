using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    class PrezimeException : Exception
    {
        public PrezimeException(string message) : base(message)
        {

        }
    }
}
