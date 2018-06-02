using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace YapayBagisiklik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //------------------------- sadece sayi girisi saglanir-----------------------------//
        private void txtBasPopulasyon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIterasyonSay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        //----------------------------------------------------------------------------------//

        //---------------------------istenlmeyen sayi girislerini engeller------------------//

        private void txtn_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txtn.Text) >= Convert.ToInt16(txtBasPopulasyon.Text))
            {
                lblHata1.Visible = true;
                btnBaslangicUret.Enabled = false;
            }
            btnBaslangicUret.Enabled = true;
        }

        private void txtMutasyon_TextChanged(object sender, EventArgs e)
        {

        }
        //----------------------------------------------------------------------------------//      
        double Fx(double x1, double x2) //f(x) fonksiyonu olusturulur.
        {
            double pi = 3.14;
            return 21.5 + x1 * Math.Sin(4 * pi * x1) + x2 * Math.Sin(2 * 0 * pi * x2);
        }

        Double[] x1dizi = new double[30000];
        Double[] x2dizi = new double[30000];
        Double[] Fxdizi = new double[30000];

        private void btnBaslangicUret_Click(object sender, EventArgs e)
        {

            Random r = new Random();

            int S = Convert.ToInt16(txtBasPopulasyon.Text);
            int n = Convert.ToInt16(txtn.Text);
            int Beta = Convert.ToInt16(txtBeta.Text);


            for (int i = 0; i < S; i++)//baslangic populasyonu olusturulur.tabloya yazilir.
            {
                i = dgBasPopulasyon.Rows.Add();
                Double x1 = -3 + r.NextDouble() * 15.1;
                Double x2 = 4.1 + r.NextDouble() * 1.7;

                dgBasPopulasyon.Rows[i].Cells[0].Value = i;
                dgBasPopulasyon.Rows[i].Cells[1].Value = x1;
                dgBasPopulasyon.Rows[i].Cells[2].Value = x2;
                dgBasPopulasyon.Rows[i].Cells[3].Value = Fx(x1, x2);
                x1dizi[i] = x1;
                x2dizi[i] = x2;
                Fxdizi[i] = Fx(x1, x2);


            }

            btnCalistir.Enabled = true;
        }

        
        private void btnCalistir_Click(object sender, EventArgs e)
        {
            btnBaslangicUret.Enabled = false;
            Random r = new Random();

            int S = Convert.ToInt16(txtBasPopulasyon.Text);
            int n = Convert.ToInt16(txtn.Text);
            int Beta = Convert.ToInt16(txtBeta.Text);
            int eniyi = 0;

            for (int iterasyon = 1; iterasyon <= Convert.ToInt16(txtIterasyonSay.Text); iterasyon++)
            {

                Double yedekdizi;
                for (int i = 0; i < S; i++)//dizide tutulan baslangic pdeğerleri siraya diziliyor
                {
                    for (int j = 0; j < S; j++)
                    {
                        if (Fxdizi[i] > Fxdizi[j])
                        {
                            yedekdizi = x1dizi[j];
                            x1dizi[j] = x1dizi[i];
                            x1dizi[i] = yedekdizi;

                            yedekdizi = x2dizi[j];
                            x2dizi[j] = x2dizi[i];
                            x2dizi[i] = yedekdizi;

                            yedekdizi = Fxdizi[j];
                            Fxdizi[j] = Fxdizi[i];
                            Fxdizi[i] = yedekdizi;

                        }

                    }
                }

                for (int i = 0; i < S; i++)//sirali populasyon olusturulur.tabloya yazilir.
                {
                    i = dgSiraliPop.Rows.Add();
                    dgSiraliPop.Rows[i].Cells[0].Value = i;
                    dgSiraliPop.Rows[i].Cells[1].Value = x1dizi[i];
                    dgSiraliPop.Rows[i].Cells[2].Value = x2dizi[i];
                    dgSiraliPop.Rows[i].Cells[3].Value = Fxdizi[i];
                }

                for (int i = 0; i < n; i++)//Isleme alinacak n sayida antikor tabloya yazdırılıyor
                {
                    i = dgnAntikor.Rows.Add();
                    dgnAntikor.Rows[i].Cells[0].Value = i;
                    dgnAntikor.Rows[i].Cells[1].Value = x1dizi[i];
                    dgnAntikor.Rows[i].Cells[2].Value = x2dizi[i];
                    dgnAntikor.Rows[i].Cells[3].Value = Fxdizi[i];
                }

                double toplam = 0.0;
                for (int i = 1; i <= n; i++)//Toplam klon sayısı
                {
                    double C = (Beta * S) / i;
                    toplam += C;
                }
                for (int i = 1; i <= n; i++)//Klonlama
                {
                    int indis = 0;//klon tablosu satir sayisini tutuyor
                    double C = (Beta * S) / i;
                    for (int j = 0; j < C; j++)
                    {
                        indis = dgKlon.Rows.Add();
                        dgKlon.Rows[indis].Cells[0].Value = indis;
                        dgKlon.Rows[indis].Cells[1].Value = x1dizi[i - 1];
                        dgKlon.Rows[indis].Cells[2].Value = x2dizi[i - 1];
                        dgKlon.Rows[indis].Cells[3].Value = Fxdizi[i - 1];
                        indis++;
                    }

                }
                int index = 0;
                for (int t = 0; t < toplam; t++)//Mutasyon yapiliyor
                {
                    double mutasyon = Convert.ToDouble(txtMutasyon.Text);
                    Double x1 = -3 + r.NextDouble() * 15.1;
                    Double x2 = 4.1 + r.NextDouble() * 1.7;
                    t = dgMutasyon.Rows.Add();
                    dgMutasyon.Rows[t].Cells[0].Value = t;

                    double mutasyon1 = r.NextDouble();
                    double mutasyon2 = r.NextDouble();
                    if (mutasyon1 < mutasyon)
                        dgMutasyon.Rows[t].Cells[1].Value = x1;
                    else
                        dgMutasyon.Rows[t].Cells[1].Value = Convert.ToDouble(dgKlon.Rows[index].Cells[1].Value);


                    if (mutasyon2 < mutasyon)
                        dgMutasyon.Rows[t].Cells[2].Value = x2;
                    else
                        dgMutasyon.Rows[t].Cells[2].Value = Convert.ToDouble(dgKlon.Rows[index].Cells[2].Value);
                    index++;
                    dgMutasyon.Rows[t].Cells[3].Value = Fx(Convert.ToDouble(dgMutasyon.Rows[t].Cells[1].Value), Convert.ToDouble(dgMutasyon.Rows[t].Cells[2].Value));
                }

                int smIndis = 0;
                for (int j = 0; j < toplam; j++)//siralanmak icin veriler diger tabloya atiliyor
                {
                    smIndis = dgSiraliMut.Rows.Add();
                    dgSiraliMut.Rows[smIndis].Cells[0].Value = Convert.ToDouble(dgMutasyon.Rows[j].Cells[0].Value);
                    dgSiraliMut.Rows[smIndis].Cells[1].Value = Convert.ToDouble(dgMutasyon.Rows[j].Cells[1].Value);
                    dgSiraliMut.Rows[smIndis].Cells[2].Value = Convert.ToDouble(dgMutasyon.Rows[j].Cells[2].Value);
                    dgSiraliMut.Rows[smIndis].Cells[3].Value = Convert.ToDouble(dgMutasyon.Rows[j].Cells[3].Value);
                    smIndis++;
                }

                Double sayi;
                for (int i = 0; i < toplam; i++)//dizide tutulan baslangic pdeğerleri siraya diziliyor
                {
                    for (int j = 0; j < toplam; j++)
                    {
                        if (Convert.ToDouble(dgSiraliMut.Rows[i].Cells[3].Value) > Convert.ToDouble(dgSiraliMut.Rows[j].Cells[3].Value))
                        {
                            sayi = Convert.ToDouble(dgSiraliMut.Rows[j].Cells[1].Value);
                            dgSiraliMut.Rows[j].Cells[1].Value = Convert.ToDouble(dgSiraliMut.Rows[i].Cells[1].Value);
                            dgSiraliMut.Rows[i].Cells[1].Value = sayi;

                            sayi = Convert.ToDouble(dgSiraliMut.Rows[j].Cells[2].Value);
                            dgSiraliMut.Rows[j].Cells[2].Value = Convert.ToDouble(dgSiraliMut.Rows[i].Cells[2].Value);
                            dgSiraliMut.Rows[i].Cells[2].Value = sayi;

                            sayi = Convert.ToDouble(dgSiraliMut.Rows[j].Cells[3].Value);
                            dgSiraliMut.Rows[j].Cells[3].Value = Convert.ToDouble(dgSiraliMut.Rows[i].Cells[3].Value);
                            dgSiraliMut.Rows[i].Cells[3].Value = sayi;

                        }

                    }
                }

                int mIt = 0;
                for (int a = 0; a < n; a++)
                {

                    mIt = dgAntikor2.Rows.Add();
                    dgAntikor2.Rows[mIt].Cells[0].Value = a;
                    dgAntikor2.Rows[mIt].Cells[1].Value = Convert.ToDouble(dgSiraliMut.Rows[a].Cells[1].Value);
                    dgAntikor2.Rows[mIt].Cells[2].Value = Convert.ToDouble(dgSiraliMut.Rows[a].Cells[2].Value);
                    dgAntikor2.Rows[mIt].Cells[3].Value = Convert.ToDouble(dgSiraliMut.Rows[a].Cells[3].Value);
                    mIt++;
                }


                //yeni baslangic  populasyonu olusturma

                {
                    int bpIndis = 0;
                    for (int k = 0; k < S; k++)
                    {

                        bpIndis = dgYeniPop.Rows.Add();
                        if (k < n)
                        {
                            dgYeniPop.Rows[bpIndis].Cells[0].Value = k;
                            dgYeniPop.Rows[bpIndis].Cells[1].Value = Convert.ToDouble(dgAntikor2.Rows[k].Cells[1].Value);
                            dgYeniPop.Rows[bpIndis].Cells[2].Value = Convert.ToDouble(dgAntikor2.Rows[k].Cells[2].Value);
                            dgYeniPop.Rows[bpIndis].Cells[3].Value = Convert.ToDouble(dgAntikor2.Rows[k].Cells[3].Value);
                            x1dizi[bpIndis] = Convert.ToDouble(dgAntikor2.Rows[k].Cells[1].Value);
                            x2dizi[bpIndis] = Convert.ToDouble(dgAntikor2.Rows[k].Cells[2].Value);
                            Fxdizi[bpIndis] = Convert.ToDouble(dgAntikor2.Rows[k].Cells[3].Value);
                        }
                        else if (k >= n && k < S)
                        {
                            Double x1 = -3 + r.NextDouble() * 15.1;
                            Double x2 = 4.1 + r.NextDouble() * 1.7;
                            dgYeniPop.Rows[bpIndis].Cells[0].Value = k;
                            dgYeniPop.Rows[bpIndis].Cells[1].Value = x1;
                            dgYeniPop.Rows[bpIndis].Cells[2].Value = x2;
                            dgYeniPop.Rows[bpIndis].Cells[3].Value = Fx(x1, x2);
                            x1dizi[bpIndis] = x1;           //sonraki iterasyon icin baslangic degerlerini
                            x2dizi[bpIndis] = x2;          //tutuyorlar.
                            Fxdizi[bpIndis] = Fx(x1, x2);
                        }
                        bpIndis++;
                    }
                    //yeni baslangic populasyon tablosu olusmus oldu
                }

                //yeni populasyonun en iyisini bulma
                double makx1 = Convert.ToDouble(dgYeniPop.Rows[0].Cells[1].Value);
                double makx2 = Convert.ToDouble(dgYeniPop.Rows[0].Cells[2].Value);
                double maksimum = Convert.ToDouble(dgYeniPop.Rows[0].Cells[3].Value);
                for (int i = 1; i < S; i++)
                {
                    if (maksimum < Convert.ToDouble(dgYeniPop.Rows[i].Cells[3].Value))
                    {
                        maksimum = Convert.ToDouble(dgYeniPop.Rows[i].Cells[3].Value);
                        makx1 = Convert.ToDouble(dgYeniPop.Rows[i].Cells[1].Value);
                        makx2 = Convert.ToDouble(dgYeniPop.Rows[i].Cells[2].Value);
                    }
                }
                //iterasyonun en iyisini en iyi tablosuna ekleme

                eniyi = dgEnIyi.Rows.Add();
                dgEnIyi.Rows[eniyi].Cells[0].Value = iterasyon;
                dgEnIyi.Rows[eniyi].Cells[1].Value = makx1;
                dgEnIyi.Rows[eniyi].Cells[2].Value = makx2;
                dgEnIyi.Rows[eniyi].Cells[3].Value = maksimum;

     

              dgSiraliPop.Rows.Clear();
                dgnAntikor.Rows.Clear();
                dgKlon.Rows.Clear();
                dgMutasyon.Rows.Clear();
                dgSiraliMut.Rows.Clear();
                dgAntikor2.Rows.Clear();
                dgYeniPop.Rows.Clear();

                eniyi++;
            }

        }
    }
}

