using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

namespace Vezbe2Primer.primeri
{
    public class Primer1 : AbstractPrimer
    {
        public Primer1(frmPrimer2 f) : base(f)
        {

        }

        public override void izvrsi()
        {
            int[] niz = new int[10]; //običan jednodimenzionalni niz
            int[,] vNiz = new int[5, 4]; //višedimenzionalni niz koji se sastoji od 5 puta 4 vrednosti. Isto kao u C/C++ 
            int[][] nNiz = new int[5][]; //isto tako višedimenzionalni niz, ali je ovo niz nizova, odn. "nazubljeni" niz.
                                        //nazubljeni niz može imati redove sa različitim brojem elemenata
            for (int i = 0; i < nNiz.Length; i++)
            {
                nNiz[i] = new int[i+1];
            }

            for (int i = 0; i < niz.Length; i++)
            {
                niz[i] = i + 1;
            }

            for (int i = 0; i < vNiz.GetLength(0); i++)
            {
                for (int j = 0; j < vNiz.GetLength(1); j++)
                {
                    vNiz[i, j] = i * j;
                }
            }

            for (int i = 0; i < nNiz.Length; i++)
            {
                for (int j = 0; j < nNiz[i].Length; j++)
                {
                    nNiz[i][j] = i * j;
                }
            }

            ispisi("Primer 1 - Nizovi:\r\n");
            ispisi("############\r\n");
            ispisi("niz = \r\n");
            for (int i = 0; i < niz.Length; i++)
            {
                ispisi(niz[i].ToString());
                ispisi("      ");
            }
            ispisi("\r\n");

            ispisi("############\r\n");
            ispisi("vNiz = \r\n");
            for (int i = 0; i < vNiz.GetLength(0); i++)
            {
                for (int j = 0; j < vNiz.GetLength(1); j++)
                {
                    ispisi(vNiz[i, j].ToString());
                    ispisi("     ");
                }
                ispisi("\r\n");
            }

            ispisi("############\r\n");
            ispisi("nNiz = \r\n");

            for (int i = 0; i < nNiz.Length; i++)
            {
                for (int j = 0; j < nNiz[i].Length; j++)
                {
                    ispisi(nNiz[i][j].ToString());
                    ispisi("     ");
                }
                ispisi("\r\n");
            }
            ispisi("############\r\n");
        }
    }
}
