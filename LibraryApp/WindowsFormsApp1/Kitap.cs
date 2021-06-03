using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Text;
using System.Windows.Forms;
using LibraryApp.BLL;
using LibraryApp.Entity;
using System.Data.OleDb;

namespace LibraryApp.UI
{
    public partial class Kitap : Form
    {
        private Kitap_Entity ktp;       // kitap entity, kitapBLL, RecordBLL sınıflarından bir değişken oluşturuyorum.
        private kitapBLL kitap;
        private RecordBLL rcr;


        public Kitap()
        {
            InitializeComponent();
            ktp = new Kitap_Entity();   // Oluşturulan değişkenlere yeni nesneler atıyorum.
            kitap = new kitapBLL();
            rcr = new RecordBLL();
        }
        public void loadCombobox() 
        {
            Dictionary<int, string> newlistyayinevi = kitap.yayineviGet();   // databaseden yayinevi,kategori ve yazar tablolarından gelen bilgilerin id ve değerlerini tutmak için sözlük yapısından yararlanıldı.
            Dictionary<int, string> newlistkategori = kitap.kategoriGet();
            Dictionary<int, string> newlistyazar = kitap.yazarGet();

            foreach (KeyValuePair<int, string> i in newlistyazar)            // sözlük içerisindeki değerleri comboboxlara yüklemek için foreach blokları kullanıldı. Ayni işlemler yayinevi ve kategori için tekrarlandı.
            {
                comboBox1.DataSource = new BindingSource(newlistyazar, null);
                comboBox6.DataSource = new BindingSource(newlistyazar, null);
                comboBox10.DataSource = new BindingSource(newlistyazar, null);

                comboBox1.DisplayMember = "Value";
                comboBox6.DisplayMember = "Value";
                comboBox10.DisplayMember = "Value";

                comboBox1.ValueMember = "Key";
                comboBox6.ValueMember = "Key";
                comboBox10.ValueMember = "Key";
            }
            foreach (KeyValuePair<int, string> i in newlistyayinevi)
            {
                comboBox2.DataSource = new BindingSource(newlistyayinevi, null);
                comboBox5.DataSource = new BindingSource(newlistyayinevi, null);
                comboBox8.DataSource = new BindingSource(newlistyayinevi, null);

                comboBox2.DisplayMember = "Value";
                comboBox5.DisplayMember = "Value";
                comboBox8.DisplayMember = "Value";

                comboBox2.ValueMember = "Key";
                comboBox5.ValueMember = "Key";
                comboBox8.ValueMember = "Key";
            }
            foreach (KeyValuePair<int, string> i in newlistkategori)
            {
                comboBox3.DataSource = new BindingSource(newlistkategori, null);
                comboBox4.DataSource = new BindingSource(newlistkategori, null);
                comboBox9.DataSource = new BindingSource(newlistkategori, null);

                comboBox3.DisplayMember = "Value";
                comboBox4.DisplayMember = "Value";
                comboBox9.DisplayMember = "Value";

                comboBox3.ValueMember = "Key";
                comboBox4.ValueMember = "Key";
                comboBox9.ValueMember = "Key";
            }
        }

        public void updateAllTables() 
        {
            
            dataGridView2.DataSource = kitap.getAllBooks();        // İlgili tablolara kayıtların yüklemesi yapılıyor.
            dataGridView1.DataSource = kitap.getAllBooks();
            dataGridView3.DataSource = kitap.getAllBooks();
            dataGridView4.DataSource = kitap.getAllBooks();
            dataGridView5.DataSource = kitap.getAllBooks();
            

            dataGridView2.Columns["yayinevi_id"].Visible = false; //Tabloya kitap sınıfının içerisinden gelen yayinevi,kategori ve yazar idlerini sildim.
            dataGridView2.Columns["kategori_id"].Visible = false;
            dataGridView2.Columns["yazar_id"].Visible = false;

            dataGridView1.Columns["yayinevi_id"].Visible = false; //Tabloya kitap sınıfının içerisinden gelen yayinevi,kategori ve yazar idlerini sildim.
            dataGridView1.Columns["kategori_id"].Visible = false;
            dataGridView1.Columns["yazar_id"].Visible = false;

            dataGridView3.Columns["yayinevi_id"].Visible = false; //Tabloya kitap sınıfının içerisinden gelen yayinevi,kategori ve yazar idlerini sildim.
            dataGridView3.Columns["kategori_id"].Visible = false;
            dataGridView3.Columns["yazar_id"].Visible = false;
            
            dataGridView4.Columns["yayinevi_id"].Visible = false; //Tabloya kitap sınıfının içerisinden gelen yayinevi,kategori ve yazar idlerini sildim.
            dataGridView4.Columns["kategori_id"].Visible = false;
            dataGridView4.Columns["yazar_id"].Visible = false;
            dataGridView4.Columns["kitap_id"].Visible = false;

            dataGridView5.Columns["yayinevi_id"].Visible = false; //Tabloya kitap sınıfının içerisinden gelen yayinevi,kategori ve yazar idlerini sildim.
            dataGridView5.Columns["kategori_id"].Visible = false;
            dataGridView5.Columns["yazar_id"].Visible = false;
            dataGridView5.Columns["kitap_id"].Visible = false;

        }
        // Yayinevi Yazar ve Kategori comboboxlarının içerisine tablolarından gelen değerleri dolduruyorum.
        // Bu şekilde yanlış veri girişini engellemiş oluyorum.
        private void Kitap_Load(object sender, EventArgs e)
        {
            updateAllTables(); // Kitap_load penceresi yüklendiğinde tablolara kayıtları yükleyen updateAlltables fonksiyonu çalışıyor
            loadCombobox();    // comboboxlar içerisine verileri çekiyorum.
            comboBox7.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || maskedTextBox1.Text.Length != 13) // bazı alanların kullanıcı tarafından boş geçilmesi engellendi ve ISBN numarası 13 haneli olmalıdır.
                {
                    MessageBox.Show("* Lütfen bütün alanları doldurduğunuzdan emin olun\n* Isbn numarası 13 haneli olmalıdır.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else 
                {
                    ktp.kitap_isbn = maskedTextBox1.Text;
                    ktp.kitap_ad = textBox1.Text;
                    ktp.yazar_id = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key; // combobox içerisinde seçilen itemların idlerini ktp nesnesi içersindeki yazar_id değişkenine yazıyor. yayinevi ve kategori içinde aynı işlemler mevcut.
                    ktp.yayinevi_id = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Key;
                    ktp.kategori_id = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
                    ktp.kitap_basimyili = int.Parse(textBox2.Text);
                    ktp.kitap_sayfa = int.Parse(textBox4.Text);
                    kitap.addNewBook(ktp);
                    MessageBox.Show(string.Format("{0} adlı kitap başarıyla eklenmiştir.", ktp.kitap_ad), MessageBoxIcon.Information.ToString(), MessageBoxButtons.OK);
                    updateAllTables();
                    clearTextbox();
                }
            }
            
            catch (OleDbException)  // Bu blokta hatalar yakalanıp hata mesajı kullanıcıya gösterildi.
            {
                // Eğer eklenmek istenen kitap zaten mevcutsa stok değeri bir artırılıyor.
                MessageBox.Show("Eklenmek istenen kitap zaten mevcut kitaba ait stok bilgisi bir artırıldı.");
                kitap.bookOneIncrement(ktp);
                updateAllTables();
                clearTextbox();
            }
           

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            updateAllTables();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || maskedTextBox2.Text == "" || textBox6.Text == "" || textBox3.Text == "" || maskedTextBox2.Text.Length != 13)
            {
                MessageBox.Show("* Lütfen boş alan bırakmayınız.\n* ISBN numarası 13 haneli olmak zorundadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                ktp.kitap_isbn = maskedTextBox2.Text;
                ktp.kitap_ad = textBox6.Text;
                ktp.yazar_id = ((KeyValuePair<int, string>)comboBox6.SelectedItem).Key;  // combobox içerisinde seçilen itemların idlerini ktp nesnesi içersindeki yazar_id değişkenine yazıyor. yayinevi ve kategori içinde aynı işlemler mevcut.
                ktp.yayinevi_id = ((KeyValuePair<int, string>)comboBox5.SelectedItem).Key;
                ktp.kategori_id = ((KeyValuePair<int, string>)comboBox4.SelectedItem).Key;
                ktp.kitap_basimyili = int.Parse(textBox3.Text);
                ktp.kitap_sayfa = int.Parse(textBox5.Text);

                kitap.updateBook(ktp);
                MessageBox.Show(string.Format("{0} {1} adlı numaralı kitabın  bilgileri güncellenmiştir. ", ktp.kitap_ad, ktp.kitap_isbn), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateAllTables();
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagridview içerisinde hücreye bir kez tıklandığında ilgili componentlere satır üzerinde bulunan bilgiler giriyor.
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView3.Rows[e.RowIndex];
                maskedTextBox2.Text = dgvRow.Cells[1].Value.ToString();
                textBox6.Text = dgvRow.Cells[2].Value.ToString();
                comboBox6.Text = dgvRow.Cells[4].Value.ToString();
                comboBox5.Text = dgvRow.Cells[5].Value.ToString();
                comboBox4.Text = dgvRow.Cells[3].Value.ToString();
                textBox3.Text = dgvRow.Cells[9].Value.ToString();
                textBox5.Text = dgvRow.Cells[10].Value.ToString();
            }
        }

        

        private void maskedTextBox3_TextChanged(object sender, EventArgs e)
        {
            // maskedtextbox içerisindeki text değiştikçe getchoosebook metodu çalışarak datagridview'a istenilen kayıtları getirir.
            dataGridView4.DataSource = kitap.getChooseBook(comboBox7.Text, maskedTextBox3.Text);
            label32.Text = String.Format("Arama sonucu {0} kayit bulundu.", dataGridView4.Rows.Count.ToString());
        }
        public void clearTextbox() 
        {
            // Bu fonksiyon ilgili alanları temizler
            maskedTextBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                // datagridview içerisinde hücreye bir kez tıklandığında ilgili componentlere satır üzerinde bulunan bilgiler giriyor.
                DataGridViewRow dgvRow = dataGridView1.Rows[e.RowIndex];
                label34.Text = dgvRow.Cells[1].Value.ToString();
                label35.Text = dgvRow.Cells[2].Value.ToString();
                label36.Text = dgvRow.Cells[9].Value.ToString();
                label37.Text = dgvRow.Cells[5].Value.ToString();
                label39.Text = dgvRow.Cells[4].Value.ToString();
                label40.Text = dgvRow.Cells[3].Value.ToString();
                label41.Text = dgvRow.Cells[10].Value.ToString();
                label42.Text = dgvRow.Cells[11].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(label42.Text) == 0) // Eğer silinmek istenen kitabın stok bilgisi 0 ise aşağıdaki kod bloğunu çalıştırır.
                {
                    MessageBox.Show(string.Format("{0} adlı kitap zaten stokta bulunmamaktadır.", label35.Text), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labelClear();
                }
                else // yukarıdaki koşul değilse kitabın stok bilgisi değerini bir azaltır.
                {
                    kitap.deleteBook(label34.Text.ToString());
                    MessageBox.Show(string.Format("{0} adlı kitap başarıyla silindi.", label35.Text), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    labelClear();
                }
                updateAllTables();
            }
            catch (FormatException) // Eğer kullanıcı silmek istediği kitabı seçmediyse ona bir hata mesajı gösterir.
            {
                MessageBox.Show("Lütfen silmek istediğiniz kitabı tablo içerisinden seçiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }
        public void labelClear() 
        {
            // Bu fonksiyon ilgili alanları temizler
            label34.Text = "";
            label35.Text = "";
            label36.Text = "";
            label37.Text = "";
            label39.Text = "";
            label40.Text = "";
            label41.Text = "";
            label42.Text = "";
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // geçmiş tabında bulunan kitap tablosu içerisinde herhangi bir kitaba tıklandığında o kitabı alan ve alıp teslim eden öğrencileri listeler.
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dataGridView5.Rows[e.RowIndex];
                dataGridView6.DataSource = rcr.getBookRecordHistory(dgvRow.Cells[1].Value.ToString());
                dataGridView6.Columns["kayit_id"].Visible = false;  //Tablo içerisinde gelen kayit_id,ogrenci_ad,ogrenci_soyad alanları gizliyoruz.
                dataGridView6.Columns["kitap_ad"].Visible = false;
                dataGridView6.Columns["kitap_id"].Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                kitap.addPublisher(textBox7.Text);
                textBox7.Text = "";
                loadCombobox();
                MessageBox.Show("Başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException)
            {
                MessageBox.Show("Lütfen eklemek için birşeyler yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            kitap.deletePublisher(((KeyValuePair<int, string>)comboBox8.SelectedItem).Key);
            loadCombobox();
            MessageBox.Show("Başarılı bir şekilde silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                kitap.addCategory(textBox8.Text);
                textBox8.Text = "";
                loadCombobox();
                MessageBox.Show("Başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException)
            {
                MessageBox.Show("Lütfen eklemek için birşeyler yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            kitap.deleteCategory(((KeyValuePair<int, string>)comboBox9.SelectedItem).Key);
            loadCombobox();
            MessageBox.Show("Başarılı bir şekilde silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                kitap.addAuthor(textBox9.Text);
                textBox9.Text = "";
                loadCombobox();
                MessageBox.Show("Başarılı bir şekilde eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OleDbException)
            {
                MessageBox.Show("Lütfen eklemek için birşeyler yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            kitap.deleteAuthor(((KeyValuePair<int, string>)comboBox10.SelectedItem).Key);
            loadCombobox();
            MessageBox.Show("Başarılı bir şekilde silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            
        }
    }
}
