using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{
    class C
    {
        protected string M1()
        {
            return "C::M1";
        }
    }

    interface iC
    {
        //public string fieldA; // Error : Interfaces cannot contain fields
        string M2(); 
        string Osobina //osobina u interfejsu
        {
            get;
            set;
        }
    }

    class D : C, iC
    {
        private string x;

        public D()
        {
            x = "Osobina";
        }

        public string M2()
        {
            M1();
            return "D::M2";
        }

        public string Osobina
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
    }

    public class Primer3 : AbstractPrimer
    {
        public Primer3(frmPrimer2 f): base(f) {}

        public override void izvrsi()
        {
            D d = new D();
            ispisi("Primer 3 - nasledjivanje 2.\r\n");
            ispisi("#############\r\n");

            ispisi("Pozvano d.M2() -> " + d.M2());
            ispisi("\r\n");

            ispisi("Pozvano d.Osobina -> " + d.Osobina);
            ispisi("\r\n");
        }

    }
}