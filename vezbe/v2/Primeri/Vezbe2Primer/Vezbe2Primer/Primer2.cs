using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vezbe2Primer.util;
using Vezbe2Primer.primeri;

namespace Vezbe2Primer
{
    public partial class frmPrimer2 : Form
    {
        private AbstractPrimer[] primeri = null;

        public frmPrimer2()
        {
            InitializeComponent();
        }

        private void frmPrimer2_Load(object sender, EventArgs e)
        {
            cmbPrimer.SelectedIndex = 0;
            primeri = new AbstractPrimer[12];
            primeri[0] = new Primer1(this);
            primeri[1] = new Primer2(this);
            primeri[2] = new Primer3(this);
            primeri[3] = new Primer4(this);
            primeri[4] = new Primer5(this); 
            primeri[5] = new Primer6(this);
            primeri[6] = new Primer5(this);
            primeri[7] = new Primer6(this);
            primeri[11] = new Zadatak(this);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }
        /// <summary>
        /// Ovo je metoda koja služi da se lako ispiše tekst u okviru polja za tekst. 
        /// </summary>
        /// <param name="s">String koji se ispisuje.</param>
        public void ispisi(string s)
        {
            txtConsole.Text += s;
        }

        private void btnIzvrsi_Click(object sender, EventArgs e)
        {
            primeri[cmbPrimer.SelectedIndex].izvrsi();
        }

    }
}
