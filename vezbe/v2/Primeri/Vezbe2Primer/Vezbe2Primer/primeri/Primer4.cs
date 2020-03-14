using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{

    struct Complex
    {
        double real;
        double imaginary;

        public Complex(double r, double i)
        {
            real = r;
            imaginary = i;
        }

        public override string ToString()
        {
            return (String.Format("{0} + {1}i", real, imaginary));
        }

        public static implicit operator string(Complex c) //overridujemo implicitnu konverziju u string
        {
            return c.ToString();
        }

        public static Complex operator +(Complex c1, Complex c2) //preopterecenje binarnog operatora
        {
            return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
        }
    }

    public class Primer4 : AbstractPrimer
    {
        public Primer4(frmPrimer2 f): base(f) {}

        public override void izvrsi()
        {
            ispisi("Primer 4 - operatori.\r\n");
            ispisi("#############\r\n");
            Complex a = new Complex(1.0, 1.0);
            Complex b = new Complex(2.0, 2.0);
            ispisi("A = ");
            ispisi(a.ToString());
            ispisi("\r\n");

            ispisi("B = ");
            ispisi(b);
            ispisi("\r\n");

            ispisi("A + B = ");
            ispisi(a+b);
            ispisi("\r\n");

            ispisi("A + 2B = ");
            a += b;
            a += b;
            ispisi(a);
            ispisi("\r\n");
        }
    }
}