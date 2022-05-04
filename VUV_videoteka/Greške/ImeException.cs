using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    class ImeException : Exception
    {
        public ImeException(string message) : base(message)
        {

        }
    }
}
