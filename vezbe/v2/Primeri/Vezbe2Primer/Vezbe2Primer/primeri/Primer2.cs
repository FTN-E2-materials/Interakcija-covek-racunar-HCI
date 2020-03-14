using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{
    class A
    {
        public A(int x) {}

        public string M1() // u C# metode _nisu_ podrazumevano virtuelne!
        {
            return "A::M1";
        }
        public virtual string M2()
        {
            return "A::M2";
        }
    }

    class B : A
    {
        public B() : base(5) {}

        public string M1()
        {
            return "B::M1";
        }

        public override string M2()
        {
            return "B::M2";
        }
    }

    public class Primer2 : AbstractPrimer
    {
        public Primer2(frmPrimer2 f) : base(f) {}

        public override void izvrsi()
        {
            A a = new A(1);
            A x = null;
            B b = new B();
            x = b; // u referenci tipa A cuvamo tip B

            ispisi("Primer 2 - nasledjivanje.\r\n");
            ispisi("#############\r\n");

            ispisi("Pozvano a.M1() -> " + a.M1());
            ispisi("\r\n");
            ispisi("Pozvano a.M2() -> " + a.M2());
            ispisi("\r\n");

            ispisi("Pozvano b.M1() -> " + b.M1());
            ispisi("\r\n");
            ispisi("Pozvano b.M2() -> " + b.M2());
            ispisi("\r\n");

            ispisi("Pozvano x.M1() -> " + x.M1());
            ispisi("\r\n");
            ispisi("Pozvano x.M2() -> " + x.M2());
            ispisi("\r\n");
        }

    }
}