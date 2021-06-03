using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibraryApp.BLL;
using LibraryApp.Entity;


namespace LibraryApp.UI
{
    public partial class Teslim_Iade : Form
    {
        private kitapBLL kitap;             // kitapBLL, RecordBLL, ogrenciBLL sınıflarından bir değişken oluşturuyorum.
        private ogrenciBLL ogrenci;
        private RecordBLL rec;
        public Teslim_Iade()
        {
            InitializeComponent();       // Oluşturulan değişkenlere yeni nesneler atıyorum.
            kitap = new kitapBLL();
            ogrenci = new ogrenciBLL();
            rec = new RecordBLL();
            
        }


        private void Teslim_Iade_Load(object sender, EventArgs e)
        {
            updateAllTables();              // Form yüklendiğinde update all tables metodu çalışacak.
            comboBox1.SelectedIndex = 0;    // Ve ilgili comboboxların seçili indexleri 0. indeksleri olacak.
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        public void updateAllTables() 
        {
            rec.updateDebts();   // Kira tablosu içerisinde borç güncellemelerini yapar.

            dataGridView1.DataSource = ogrenci.getAllStudents();        // İlgili tablolara kayıtların yüklemesi yapılıyor.
            dataGridView2.DataSource = kitap.getAllBooks();
            dataGridView3.DataSource = rec.getAllRecords();
            dataGridView4.DataSource = rec.getAllRecords();

            dataGridView2.Columns["yayinevi_id"].Visible = false; // Tabloya kitap sınıfının içerisinden gelen yayinevi_id,kategori_id,yazar_id bilgilerini sildim.
            dataGridView2.Columns["kategori_id"].Visible = false;  
            dataGridView2.Columns["yazar_id"].Visible = false;

            dataGridView3.Columns["kitap_id"].Visible = false;
            dataGridView4.Columns["kitap_id"].Visible = false;

            paintToTable(dataGridView4);
            paintToTable(dataGridView3);
            
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            // maskedtextbox içerisindeki text değiştikçe getchooseStudent metodu çalışarak datagridview'a istenilen kayıtları getirir. 
            dataGridView1.DataSource = ogrenci.getChooseStudent(comboBox1.Text, maskedTextBox1.Text);
            label47.Text = String.Format("Arama sonucu {0} kayit bulundu.", dataGridView1.Rows.Count.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // datagridview içerisinde hücreye bir kez tıklandığında ilgili componentlere satır üzerinde bulunan bilgiler giriyor.
                DataGridViewRow dgvRow = dataGridView1.Rows[e.RowIndex];
                label7.Text = dgvRow.Cells[0].Value.ToString();
                label6.Text = dgvRow.Cells[1].Value.ToString();
                label5.Text = dgvRow.Cells[2].Value.ToString();
                label4.Text = dgvRow.Cells[3].Value.ToString();
                label3.Text = dgvRow.Cells[4].Value.ToString();
                label2.Text = dgvRow.Cells[5].Value.ToString();
                label1.Text = dgvRow.Cells[6].Value.ToString();
            }
            dataGridView1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Combobox içerisindeki arama tipi değişince maskedtextbox komponentinin maskeleme türünü değiştirir.
            if (comboBox1.Text == "ogrenci_telno")
            {
                maskedTextBox1.Mask = "(999) 000 00 00";
            }
            else
            {
                maskedTextBox1.Mask = "";
            }
        }

        public void studentInfoClear() 
        {
            // Bu fonksiyon ilgili alanları temizler
            label7.Text = "";
            label6.Text = "";
            label5.Text = "";
            label4.Text = "";
            label3.Text = "";
            label2.Text = "";
            label1.Text = "";
            dataGridView1.Enabled = true;
        }
        public void bookInfoClear() 
        {
            // Bu fonksiyon ilgili alanları temizler
            label81.Text = "";
            label37.Text = "";
            label32.Text = "";
            label24.Text = "";
            label23.Text = "";
            label22.Text = "";
            label21.Text = "";
            label20.Text = "";
            label19.Text = "";
            dataGridView2.Enabled = true;
        } 
        public void returnBookclear() 
        {
            // Bu fonksiyon ilgili alanları temizler
            label70.Text = "";
            label68.Text = "";
            label67.Text = "";
            label51.Text = "";
            label48.Text = "";
            label55.Text = "";
        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            // maskedtextbox içerisindeki text değiştikçe getchoosebook metodu çalışarak datagridview'a istenilen kayıtları getirir.
            kitapBLL kitap = new kitapBLL();
            dataGridView2.DataSource = kitap.getChooseBook(comboBox2.Text, maskedTextBox2.Text);
            label46.Text = String.Format("Arama sonucu {0} kayit bulundu.", dataGridView2.Rows.Count.ToString()); // datagridview içerisindeki kayıtların adedini label'a aktarır.
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridview içerisinde hücreye bir kez tıklandığında ilgili componentlere satır üzerinde bulunan bilgiler giriyor.
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView2.Rows[e.RowIndex];
                label37.Text = dgvRow.Cells[1].Value.ToString();
                label32.Text = dgvRow.Cells[2].Value.ToString();
                label24.Text = dgvRow.Cells[9].Value.ToString();
                label23.Text = dgvRow.Cells[5].Value.ToString();
                label22.Text = dgvRow.Cells[4].Value.ToString();
                label21.Text = dgvRow.Cells[3].Value.ToString();
                label20.Text = dgvRow.Cells[10].Value.ToString();
                label19.Text = dgvRow.Cells[11].Value.ToString();
                label81.Text = dgvRow.Cells[0].Value.ToString();
            }
            dataGridView2.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            BookRecordHistory rcr = new BookRecordHistory();
            try
            {
                if (int.Parse(label19.Text) == 0) // Eğer teslim edilmek istenen kitabın stok bilgisi 0 ise aşağıdaki blok içerisindeki mesajı kullanıcıya gösterir. 
                {
                    MessageBox.Show(string.Format("{0} adlı kitaptan stokta bulunmuyor.", label32.Text),"Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else // Değilse kitabı öğrenciye teslim eder ve ilgili kitabın stoğunu öğrenciye verildiği için bir azaltır.
                {
                    rcr.ogrenci_no = int.Parse(label7.Text);
                    rcr.kitap_id = int.Parse(label81.Text);
                    rec.addRecord(rcr);
                    kitap.deleteBook(label37.Text);
                    MessageBox.Show(string.Format("{0} adlı kitap {1} adlı öğrenciye teslim edilmiştir.", label32.Text, label5.Text + " " + label4.Text), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                updateAllTables(); // tüm tabloları günceller.
                studentInfoClear(); // ilgili alanları temizler.
                bookInfoClear(); // ilgili alanları temizler.
            }
            catch (FormatException) 
            {
                MessageBox.Show("Kitap teslim için lütfen tablolardan bir öğrenci birde kitap seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        
        public void paintToTable(DataGridView dt)   // paintToTable methodu tablolarda parametresini aldığı tablonun ilgili alanlarını kırmızı,yeşil ve sarıya boyar.
        {
            DateTime now = DateTime.Now; // Bugünün tarihini now değişkenin içerisine atıyoruz.
            
            for (int i = 0; i < dt.Rows.Count; i++) //tablo içerisinde tüm satırları gezer.
            {
                DateTime date = Convert.ToDateTime(dt.Rows[i].Cells[8].Value.ToString()); // tablo içerisindeki son teslim tarihi sütunundaki veriyi alır ve datetime sınıfından date adlı değişkene atar.
                System.TimeSpan diff = date.Subtract(now);                                // date değişkeninden şuanki zamanı çıkarır.
                int value = Convert.ToInt32(diff.Days.ToString());                      

                if (Convert.ToBoolean(dt.Rows[i].Cells[10].Value) == true) // Eğer teslim durumu True ise satırı yeşil renge boyar.
                {
                    dt.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }

                else if (value == 2)                                       // Eğer teslim gününe 2 gün kaldıysa satırı sarı renge boyar.
                {
                    dt.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }

                else if  (value < 0)                                       // Eğer teslim gününü geçtiyse satırı kırmızı renge boyar.
                {
                    dt.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e) // seçimi temizle butonlarına basıldığında bilgileri temizler.
        {
            studentInfoClear();
        }
        private void button3_Click(object sender, EventArgs e) // seçimi temizle butonlarına basıldığında bilgileri temizler.
        {
            bookInfoClear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) // tabpage1'e tıklandığında tüm tabloları günceller.
        {
            updateAllTables();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView4.Rows[e.RowIndex];

                if (Convert.ToBoolean(dgvRow.Cells[10].Value) == true) // eğer kullanıcı teslim edilen bir kitabı iade etmek için tablodan seçtiğinde aşağıdaki mesaj kullanıcıya gösterilir.
                {
                    MessageBox.Show("Teslim edilen kitapları tekrar seçemezsiniz. Seçmeye çalıştığınız kitap zaten teslim edildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    label48.Text = dgvRow.Cells[4].Value.ToString(); // eğer seçilen kitap iade edilmemişse seçilen satırdaki bilgiler ilgili alanlara doldurulur.
                    label55.Text = dgvRow.Cells[6].Value.ToString();
                    label68.Text = dgvRow.Cells[2].Value.ToString();
                    label67.Text = dgvRow.Cells[3].Value.ToString();
                    label70.Text = dgvRow.Cells[1].Value.ToString();
                    label51.Text = dgvRow.Cells[0].Value.ToString();
                }
            }
        }

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            // maskedtextbox içerisindeki text değiştikçe getchooseRecords metodu çalışarak datagridview'a istenilen kayıtları getirir. 
            dataGridView4.DataSource = rec.getChooseRecords(comboBox3.Text, maskedTextBox3.Text);
            label49.Text = String.Format("Arama sonucu {0} kayit bulundu.", dataGridView4.Rows.Count.ToString());
            paintToTable(dataGridView4); // painttotable fonksiyonu parametresini aldığı tablo için boyama işlemlerini yapar.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                rec.returnBook(int.Parse(label51.Text)); 
                updateAllTables();
                MessageBox.Show(string.Format("{0} adlı öğrenciden {1} adlı kitap geri alındı.", label68.Text.ToString() + label67.Text.ToString(), label55.Text.ToString()), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                returnBookclear();
            }
            catch (FormatException) // Kullanıcı iade edilecek kitabı tablo içerisinden seçmeyip butona bastığı zaman çıkacak olan mesaj 
            {
                MessageBox.Show("Lütfen iade edilecek kitabı tablo içerisinden seçiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }
    }
}
