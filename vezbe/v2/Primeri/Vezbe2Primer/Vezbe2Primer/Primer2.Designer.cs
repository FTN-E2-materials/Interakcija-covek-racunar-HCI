namespace Vezbe2Primer
{
    partial class frmPrimer2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbPrimer = new System.Windows.Forms.ComboBox();
            this.btnIzvrsi = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbPrimer
            // 
            this.cmbPrimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrimer.FormattingEnabled = true;
            this.cmbPrimer.Items.AddRange(new object[] {
            "P1-Nizovi",
            "P2-Preopterćenje metoda",
            "P3-Sintaksa nasleđivanja",
            "P4-Preopterećeni operatori",
            "P5-Osnove struktura podataka",
            "P6-Serijalizacija",
            "ZADATAK"});
            this.cmbPrimer.Location = new System.Drawing.Point(12, 12);
            this.cmbPrimer.Name = "cmbPrimer";
            this.cmbPrimer.Size = new System.Drawing.Size(636, 21);
            this.cmbPrimer.TabIndex = 0;
            // 
            // btnIzvrsi
            // 
            this.btnIzvrsi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzvrsi.Location = new System.Drawing.Point(654, 8);
            this.btnIzvrsi.Name = "btnIzvrsi";
            this.btnIzvrsi.Size = new System.Drawing.Size(87, 25);
            this.btnIzvrsi.TabIndex = 1;
            this.btnIzvrsi.Text = "Pokreni";
            this.btnIzvrsi.UseVisualStyleBackColor = true;
            this.btnIzvrsi.Click += new System.EventHandler(this.btnIzvrsi_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtConsole.Location = new System.Drawing.Point(12, 50);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(808, 212);
            this.txtConsole.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(747, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 25);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Obriši tekst";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmPrimer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 274);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnIzvrsi);
            this.Controls.Add(this.cmbPrimer);
            this.Name = "frmPrimer2";
            this.Text = "Drugi čas vežbi - primeri";
            this.Load += new System.EventHandler(this.frmPrimer2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPrimer;
        private System.Windows.Forms.Button btnIzvrsi;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnClear;
    }
}

