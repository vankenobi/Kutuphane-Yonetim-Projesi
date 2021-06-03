using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryApp.BLL;
using LibraryApp.UI;

namespace WindowsFormsApp1
{
    public partial class anamenu : Form
    {
        public anamenu()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Ogrenci ogrenci = new Ogrenci();   // Öğrenci penceresinden bir nesne oluşturuyorum.
            ogrenci.ShowDialog();               // Burada oluşturulan penceriyi açıyorum.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kitap kitap = new Kitap();          // Kitap penceresinden bir nesne oluşturuyorum.
            kitap.ShowDialog();                 // Burada oluşturulan penceriyi açıyorum.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Teslim_Iade teslim_Iade = new Teslim_Iade();        // Teslim İade penceresinden bir nesne oluşturuyorum.
            teslim_Iade.ShowDialog();                           // Burada oluşturulan penceriyi açıyorum.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Zedgraph zedgraph = new Zedgraph();             // Zedgraph penceresinden bir nesne oluşturuyorum.
            zedgraph.ShowDialog();                          // Burada oluşturulan penceriyi açıyorum.
        }   
    }
}
