using LibraryApp.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibraryApp.Entity;
using System.Data.OleDb;

namespace LibraryApp.UI
{
    public partial class Ogrenci : Form
    {
        private ogrenciBLL ogrenci;         // ogrenciBLL, Ogrenci_Entity, RecordBLL sınıflarından bir değişken oluşturuyorum.
        private Ogrenci_Entity ogr;
        private RecordBLL rcr;
        public Ogrenci()
        {
            InitializeComponent();      // Oluşturulan değişkenlere yeni nesneler atıyorum.
            ogrenci = new ogrenciBLL();
            ogr = new Ogrenci_Entity();
            rcr = new RecordBLL();
        }

        public void field_clear()       //field_clear fonksiyonu tabpage2 içerisindeki itemları item türüne göre (textbox,radiobutton,maskedtextbox) temizler.
        {
            foreach (var item in tabPage2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox textB = item as TextBox;
                    textB.Clear();
                }
                else if (item is RadioButton)
                {
                    RadioButton rdb = item as RadioButton;
                    rdb.Checked = false;
                }
                else if (item is MaskedTextBox) 
                {
                    MaskedTextBox masked = item as MaskedTextBox;
                    masked.Clear();
                }
            }
        }

        
        private void Ogrenci_Load(object sender, EventArgs e)
        {
            updateAllTables(); // öğrenci penceresi yüklendiğinde updateAlltables methodu çalışır.
            comboBox1.SelectedIndex = 0;
            
        }
        public void updateAllTables()   
        {
            
            dataGridView1.DataSource = ogrenci.getAllStudents();     // İlgili tablolara kayıtların yüklemesi yapılıyor.
            dataGridView3.DataSource = ogrenci.getAllStudents();
            dataGridView2.DataSource = ogrenci.getAllStudents();
            dataGridView4.DataSource = ogrenci.getAllStudents();
            dataGridView6.DataSource = ogrenci.getAllStudents();
            dataGridView7.DataSource = ogrenci.getAllStudents();
        }

        

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridview içerisinde hücreye bir kez tıklandığında ilgili componentlere satır üzerinde bulunan bilgiler aktarılıyor.
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView1.Rows[e.RowIndex];
                textBox10.Text = dgvRow.Cells[0].Value.ToString();
                textBox9.Text = dgvRow.Cells[1].Value.ToString();
                textBox8.Text = dgvRow.Cells[2].Value.ToString();
                textBox7.Text = dgvRow.Cells[3].Value.ToString();
                textBox6.Text = dgvRow.Cells[6].Value.ToString();
                if (dgvRow.Cells[4].Value.ToString() == "Erkek")
                {
                    radioButton3.PerformClick();
                }
                else
                {
                    radioButton4.PerformClick();
                }
                maskedTextBox2.Text = dgvRow.Cells[5].Value.ToString();
            }
        }

        // Öğrenci ekle
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                ogr.ogrenci_no = Int32.Parse(textBox1.Text);
                ogr.ogrenci_kimlik = textBox2.Text;
                ogr.ogrenci_ad = textBox3.Text;
                ogr.ogrenci_soyad = textBox4.Text;
                ogr.ogrenci_cinsiyet = radioButton1.Checked ? "Erkek" : "Kadın";
                ogr.ogrenci_telno = maskedTextBox1.Text;
                ogr.ogrenci_email = textBox5.Text;
                ogrenci.addNewStudent(ogr);
                MessageBox.Show(string.Format("{0} {1} adlı öğrenci başarıyla eklendi", ogr.ogrenci_ad, ogr.ogrenci_soyad), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateAllTables();
                field_clear();       //öğrenci eklendikten sonra alanları temizleyen fonksiyonu çağırdık.
            }
            // Yukarıdaki kod bloğu çalışınca çıkabilecek hatalar yakalandı ve ilgili mesajlar kullanıcıya verildi.
            catch (System.FormatException) 
            {
                MessageBox.Show("Lütfen ilgili alanları doğru doldurduğunuzdan emin olun.\n\n* Öğrenci numarası 6 haneli olmalıdır.\n* Tc kimlik no 11 haneli olmalıdır.\n* Bütün alanlar doldurulmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OleDbException) 
            {
                MessageBox.Show("Lütfen ilgili alanları doğru doldurduğunuzdan emin olun.\n\n* Benzer öğrenci numarası veya Tc kimlik numarası kaydetmeye çalışmadığınızdan emin olun.\n* Bütün alanlar doldurulmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Öğrenci Güncelleme işlemi
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ogr.ogrenci_no = Int32.Parse(textBox10.Text);
                ogr.ogrenci_kimlik = textBox9.Text;
                ogr.ogrenci_ad = textBox8.Text;
                ogr.ogrenci_soyad = textBox7.Text;
                ogr.ogrenci_cinsiyet = radioButton3.Checked ? "Erkek" : "Kadın";
                ogr.ogrenci_telno = maskedTextBox2.Text;
                ogr.ogrenci_email = textBox6.Text;

                ogrenci.updateStudent(ogr);
                MessageBox.Show(string.Format("{0} {1} adlı {2} numaralı öğrencinin  bilgileri güncellenmiştir. ", ogr.ogrenci_ad, ogr.ogrenci_soyad, ogr.ogrenci_no), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateAllTables();
            }
            // Yukarıdaki kod bloğu çalıştığı esnada çıkabilecek hatalar yakalandı ve ilgili mesajlar kullanıcıya verildi.
            catch (System.FormatException)
            {
                MessageBox.Show("Lütfen bilgilerini güncellemek istediğiniz öğrenciyi tablodan seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OleDbException) 
            {
                MessageBox.Show("Lütfen bütün bilgileri girdiğinizden emin olun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        // Listele işlemidir.
        private void button3_Click(object sender, EventArgs e)
        {
            updateAllTables();
        }

        // Combobox içerisindeki arama tipi değişince maskedtextbox komponentinin maskeleme türünü değiştirir.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ogrenci_telno")
            {
                maskedTextBox3.Mask = "(999) 000 00 00";
            }
            else
            {
                maskedTextBox3.Mask = "";
            }
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            // maskedtextbox içerisindeki text değiştikçe getChooseStudent metodu çalışarak datagridview'a istenilen kayıtları getirir.
            dataGridView2.DataSource = ogrenci.getChooseStudent(comboBox1.Text, maskedTextBox3.Text);
            label18.Text = String.Format("Arama sonucu {0} kayit bulundu.", dataGridView2.Rows.Count.ToString());
        }

       

        // Tablo içerisinde herhangi bir hücreye tıklandığı zaman o hücredeki öğrenci bilgilerini öğrenci bilgileri groupbox içerisine aktarır.
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView1.Rows[e.RowIndex];
                label26.Text = dgvRow.Cells[0].Value.ToString();
                label27.Text = dgvRow.Cells[1].Value.ToString();
                label28.Text = dgvRow.Cells[2].Value.ToString();
                label29.Text = dgvRow.Cells[3].Value.ToString();
                label32.Text = dgvRow.Cells[6].Value.ToString();
                label30.Text = dgvRow.Cells[4].Value.ToString();
                label31.Text = dgvRow.Cells[5].Value.ToString();
            }
        }
        
        public void labelClear() // Öğrenci bilgileri groupboxı içerisindeki ilgili label alanlarını siler.
        {
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label30.Text = "";
            label31.Text = "";
            label32.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ogrenci.checkDeliveredBook(int.Parse(label26.Text)) == false) // silinmek istenen öğrenciye ait kitap var mı yok mu ? eğer var ise bildir yoksa sildir.
                {
                    ogrenci.deleteStudent(int.Parse(label26.Text));
                    updateAllTables();
                    MessageBox.Show(string.Format("{0} adlı {1} numaralı öğrenci silme işlemi başarı ile gerçekleşti.", label28.Text + " " + label29.Text, label26.Text), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labelClear();
                }

                else // Silinmek istenen öğrenciye ait kitap bulunduğunda bloğun içerisine girer ve bunu kullanıcıya bildirir.
                {
                    MessageBox.Show(string.Format("{0} adlı {1} numaralı öğrenciye ait iade edilmemiş kitaplar bulunduğu için silme işlemi iptal edildi.", label28.Text + " " + label29.Text, label26.Text), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labelClear();
                }
            }
            catch (System.FormatException) // kullanıcı silmek istediği öğrenciyi tablo üzerinden seçmediğinde kullanıcıya uyarı mesajı döndürür.
            {
                MessageBox.Show("Lütfen tablodan silmek istediğiniz bir öğrenciyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // Geçmiş tabında bulunan öğrenci tablosu içerisinde herhangi bir öğrenciye tıklandığında o öğrencinin elinde olan ve teslim ettiği kitapları listeler.
                DataGridViewRow dgvRow = dataGridView7.Rows[e.RowIndex];
                dataGridView8.DataSource = rcr.getStudentRecordHistory(int.Parse(dgvRow.Cells[0].Value.ToString()));
                dataGridView8.Columns["kayit_id"].Visible = false; //Tablo içerisinde gelen kayit_id,ogrenci_ad,ogrenci_soyad alanları gizliyoruz.
                dataGridView8.Columns["ogrenci_ad"].Visible = false;
                dataGridView8.Columns["ogrenci_soyad"].Visible = false;
                dataGridView8.Columns["kitap_id"].Visible = false;
            }
        }
    }
}
