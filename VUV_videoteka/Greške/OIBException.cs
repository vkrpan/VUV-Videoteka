﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VUV_videoteka
{
    class OIBException : Exception
    {
        public OIBException(string message) : base(message)
        {

        }
    }
}
