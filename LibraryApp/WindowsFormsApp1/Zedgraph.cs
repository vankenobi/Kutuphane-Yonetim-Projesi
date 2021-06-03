using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using LibraryApp.BLL;


namespace LibraryApp.UI
{
    public partial class Zedgraph : Form
    {
        RecordBLL rcr; // recordbll sınıfından bir değişken oluşturdum
        public Zedgraph()
        {
            InitializeComponent();
            rcr = new RecordBLL(); // oluşturulan değişkene nesne atadık.
        }

        private void Zedgraph_Load(object sender, EventArgs e) // Pencere yüklendiğinde grafik içerisine veriler giriliyor.
        {
            GraphPane mypane;                   
            mypane = zedGraphControl1.GraphPane;

            mypane.Title.Text = "Kütüphane İstatistik";
            mypane.XAxis.Title.Text = "Tür";
            mypane.YAxis.Title.Text = "Adet";

            double[] a = { 0 }; // iki adet double dizi oluşturuldu.
            double[] b = { 0 };

            a[0] = rcr.getNumberofbooksperstudent(); // a dizisinin 0. indisine öğrencilere teslim edilen toplam kitap sayısı aktarıldı.
            b[0] = rcr.getAllBookCount();   // b dizisinin 0. indisine kütüphaneye ait toplam kitap sayısı aktarıldı.

            BarItem aBar = mypane.AddBar("Öğrenciye verilen kitap sayısı", null, a, Color.Red); // iki adet bar oluşturuluyor.
            BarItem bBar = mypane.AddBar("Toplam Kitap Sayısı", null, b, Color.Green);








        }
    }
}
