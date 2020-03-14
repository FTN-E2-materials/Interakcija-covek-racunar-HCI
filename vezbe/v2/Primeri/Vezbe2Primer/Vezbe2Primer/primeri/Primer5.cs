using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{
    public class Primer5 : AbstractPrimer
    {
        public Primer5(frmPrimer2 f) : base(f) {}

        public override void izvrsi()
        {
            List<string> a = new List<string>(10);//u Javi ovo bi bio ArrayList
            Dictionary<string, string> b = new Dictionary<string, string>();//u Javi ovo bi bila HashMap-a
            HashSet<string> c = new HashSet<string>(); //u Javi ovo bi bio isto HashSet
            //So, both { 1, 2, 3 } and { 3, 2, 1 } are equal.

            a.Add("1");
            a.Add("2");

            b.Add("Prvo", "aaa");
            b.Add("Drugo", "bbb");

            c.Add("x");

            ispisi("Primer 7 - kolekcije.\r\n");
            ispisi("#############\r\n");

            ispisi(a[0]); //razlika u odnosu na Javu jeste što se operator [] koristi za pristup
            ispisi("\r\n");

            ispisi(a[1]); 
            ispisi("\r\n");

            ispisi(b["Prvo"]);//radi i za asocijativne nizove
            ispisi("\r\n");

            c.Add("first element");
            ispisi(c.Contains("first element").ToString());
        }
    }
}