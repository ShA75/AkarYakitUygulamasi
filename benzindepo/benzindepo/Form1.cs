using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace benzindepo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Yakıt b95 = new Yakıt();
        Yakıt b97 = new Yakıt();
        Yakıt d = new Yakıt();
        Yakıt ed = new Yakıt();
        Yakıt lpg = new Yakıt();
        int i = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Label[] miktars = { label6, label7, label8, label9, label10 };
            ProgressBar[] pbyakıt = { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
            TextBox[] texts = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            String[] ekleneceklerm = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text };
            Yakıt[] yakıts = { b95, b97, d, ed, lpg };
            String[] sonm = new string[5];
            NumericUpDown[] nud = { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5 };

            for (; i < ekleneceklerm.Length; i++)
            {
                try
                {
                    if (Convert.ToDouble(ekleneceklerm[i]) > 1000 ||
                        Convert.ToDouble(miktars[i].Text) + (Convert.ToDouble(ekleneceklerm[i])) > 1000)
                    {
                        texts[i].Text = "Hata!";
                        //yakıts[i].miktar = yakıts[i].miktar;
                        //  sonm[i] = Convert.ToString(yakıts[i].miktar);
                    }
                    else
                    {
                        yakıts[i].miktar = yakıts[i].miktar + Convert.ToDouble(ekleneceklerm[i]);
                        // sonm[i] = Convert.ToString(yakıts[i].miktar);
                        texts[i].Text = ekleneceklerm[i];

                    }
                }
                catch
                {
                    texts[i].Text = "Hata!";
                    //yakıts[i].miktar = yakıts[i].miktar;

                }
                nud[i].Maximum = Convert.ToDecimal(yakıts[i].miktar);
                sonm[i] = Convert.ToString(yakıts[i].miktar);
            }
            i = 0;
            dosyayaYaz(1, sonm);
            dosyaOku(1, yakıts, miktars, pbyakıt);


        }

        //dosyadan okuyup label ve progressbarları doldurur
        public void dosyaOku(int a, Yakıt[] gelen, Label[] lbl, ProgressBar[] pg)
        {
            try
            {
                String[] bilgiler;

                if (a == 1)
                {
                    bilgiler = System.IO.File.ReadAllLines(Application.StartupPath + "\\depo.txt");
                    for (; i < bilgiler.Length; i++)
                    {
                        gelen[i].miktar = Convert.ToDouble(bilgiler[i]);
                        if (lbl != null)
                            lbl[i].Text = bilgiler[i];
                        if (pg != null)
                            pg[i].Value = Convert.ToInt32(bilgiler[i]);
                    }
                    i = 0;
                }
                else
                {
                    bilgiler = System.IO.File.ReadAllLines(Application.StartupPath + "\\fiyat.txt");
                    for (; i < bilgiler.Length; i++)
                    {
                        gelen[i].fiyat = Convert.ToDouble(bilgiler[i]);
                        if (lbl != null)
                            lbl[i].Text = Convert.ToString(gelen[i].fiyat);
                    }
                    i = 0;
                }
            }
            catch { }
        }
        //yeni yakıt bilgilerini dosyaya yazar
        public void dosyayaYaz(int a, string[] sgelen)
        {
            try
            {
                if (a == 1) System.IO.File.WriteAllLines(Application.StartupPath + "\\depo.txt", sgelen);
                else System.IO.File.WriteAllLines(Application.StartupPath + "\\fiyat.txt", sgelen);
            }
            catch { }

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Yakıt[] yakıts = { b95, b97, d, ed, lpg };
            Label[] miktars = { label6, label7, label8, label9, label10 };
            Label[] fiyats = { label16, label17, label18, label19, label20 };
            ProgressBar[] pbyakıt = { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
            NumericUpDown[] nud = { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5 };


            dosyaOku(1, yakıts, miktars, pbyakıt);
            dosyaOku(2, yakıts, fiyats, null);

            String[] ytürü = { "Benzin(95)", "Benzin(97)", "Dizel", "Euro Dizel", "LPG" };
            comboBox1.Items.AddRange(ytürü);
            for (; i < ytürü.Length; i++)
            {
                nud[i].Maximum = Convert.ToDecimal(yakıts[i].miktar);
                nud[i].Enabled = false;




            }
            i = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label[] fiyats = { label16, label17, label18, label19, label20 };
            TextBox[] texts2 = { textBox6, textBox7, textBox8, textBox9, textBox10 };
            String[] ekleneceklerf = { textBox6.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text };
            String[] sonf = new string[5];
            Yakıt[] yakıts = { b95, b97, d, ed, lpg };

            for (; i < ekleneceklerf.Length; i++)
            {
                try
                {
                    yakıts[i].fiyat = yakıts[i].fiyat +
                        (Convert.ToDouble(ekleneceklerf[i]) / 100 * yakıts[i].fiyat);
                    fiyats[i].Text = Convert.ToString(yakıts[i].fiyat);
                    texts2[i].Text = ekleneceklerf[i];
                    sonf[i] = Convert.ToString(yakıts[i].fiyat);


                }

                catch
                {
                    texts2[i].Text = "Hata!";
                    yakıts[i].fiyat = yakıts[i].fiyat;
                    sonf[i] = Convert.ToString(yakıts[i].fiyat);
                }



            }
            dosyayaYaz(2, sonf);
            dosyaOku(2, yakıts, fiyats, null);
            i = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Yakıt[] yakıts = { b95, b97, d, ed, lpg };
            String[] sonm = new String[5];
            Label[] miktars = { label6, label7, label8, label9, label10 };
            ProgressBar[] pbyakıt = { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
            NumericUpDown[] nud = { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5 };
            double sonsat;

            try
            {
                for (; i < nud.Length; i++)
                {

                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen bir yakıt seçiniz");
                    }
                    else
                    {

                        for (; i < nud.Length; i++)
                        {
                            if (nud[i].Enabled == true)
                            {

                                double silinecek = Convert.ToDouble(nud[i].Value);

                                // MessageBox.Show(Convert.ToString(yakıts[i].miktar));

                                sonsat = yakıts[i].miktar - silinecek;
                                yakıts[i].miktar = sonsat;
                                label28.Text = Convert.ToString(silinecek * yakıts[i].fiyat);
                            }

                            sonm[i] = Convert.ToString(yakıts[i].miktar);
                        }


                    }
                    
                }
                i = 0;
                dosyayaYaz(1, sonm);
                dosyaOku(1, yakıts, miktars, pbyakıt);
            }
            catch { }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] ytürü = { "Benzin(95)", "Benzin(97)", "Dizel", "Euro Dizel", "LPG" };
            NumericUpDown[] nud = { numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5 };
            Yakıt[] yakıts = { b95, b97, d, ed, lpg };



            for (; i < nud.Length; i++)
            {
                nud[i].Value = 0;
                nud[i].Enabled = false;
                yakıts[i].tür = ytürü[i];
                if (comboBox1.Text == ytürü[i])
                {
                    nud[i].Enabled = true;

                }
            }
            i = 0;
        }



    }
}
