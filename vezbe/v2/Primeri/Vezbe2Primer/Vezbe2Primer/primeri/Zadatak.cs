using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{
    public class Zadatak : AbstractPrimer
    {
        public Zadatak(frmPrimer2 f)
            : base(f)
        {

        }

        public override void izvrsi()
        {
            ispisi("ZADATAK\r\n");
            ispisi("#############\r\n");
            ispisi("\r\n");
        }

    }
}
