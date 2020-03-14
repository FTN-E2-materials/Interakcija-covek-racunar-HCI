using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vezbe2Primer.util
{
    public abstract class AbstractPrimer
    {
        public frmPrimer2 Forma{
            get;
            set;
        }
        public AbstractPrimer(frmPrimer2 forma)
        {
            Forma = forma;
        }

        internal void ispisi(string s)
        {
            Forma.ispisi(s);
        }

        public abstract void izvrsi();
    }
}
