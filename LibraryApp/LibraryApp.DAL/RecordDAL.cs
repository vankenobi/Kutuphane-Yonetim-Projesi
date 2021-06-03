using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using LibraryApp.Entity;

namespace LibraryApp.DAL
{
    public class RecordDAL
    {
        private DBconnection dbconnection; // DBconnection sınıfından dbconnection adında bir değişken oluşturduk.

        public RecordDAL() 
        {
            dbconnection = new DBconnection(); // oluşturulan değişkene DBconnection adında bir nesne atadık.
        }

        // Bu method kayıt geçmişi tablosundaki büyün kayıtları döndürür.
        public List<BookRecordHistory> getAllRecords() 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Kayit_gecmisi.kayit_id, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no;";

            List<BookRecordHistory> record = new List<BookRecordHistory>();

            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                BookRecordHistory rec = new BookRecordHistory();
                rec.kayit_id = Int32.Parse(rdr["kayit_id"].ToString());
                rec.kitap_isbn = (rdr["isbn"]).ToString();
                rec.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                rec.ogrenci_ad = rdr["ogrenci_ad"].ToString();
                rec.ogrenci_soyad = rdr["ogrenci_soyad"].ToString();
                rec.kitap_ad = rdr["ad"].ToString();
                rec.alindigi_tarih =rdr["alindigi_tarih"].ToString();
                rec.son_teslim_tarih =rdr["son_teslim_tarihi"].ToString();
                rec.teslim_tarih = rdr["Teslim_tarihi"].ToString();
                rec.teslim_durumu = bool.Parse(rdr["teslim_durumu"].ToString());
                rec.borc_bilgisi = int.Parse(rdr["borc_bilgisi"].ToString());
                record.Add(rec);
            }
            rdr.Close();
            cmd.Connection.Close();
            return record;
        }

        // Öğrenciye ait geçmiş kitap kayıtlarını ve elinde bulunan kitap kayıtlarını gösterir.  Parametre olarak öğrenci numarasını alır.
        public List<BookRecordHistory> getStudentRecordHistory(int ogrenci_no) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("SELECT Kayit_gecmisi.ogrenci_no, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no WHERE(((Kayit_gecmisi.ogrenci_no) = {0}))",ogrenci_no);
            List<BookRecordHistory> record = new List<BookRecordHistory>();
            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                BookRecordHistory rec = new BookRecordHistory();
                rec.kitap_isbn = rdr["isbn"].ToString();
                rec.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                rec.kitap_ad = rdr["ad"].ToString();
                rec.alindigi_tarih = rdr["alindigi_tarih"].ToString();
                rec.son_teslim_tarih = rdr["son_teslim_tarihi"].ToString();
                rec.teslim_tarih = rdr["Teslim_tarihi"].ToString();
                rec.teslim_durumu = bool.Parse(rdr["teslim_durumu"].ToString());
                rec.borc_bilgisi = int.Parse(rdr["borc_bilgisi"].ToString());
                record.Add(rec);
            }

            rdr.Close();
            cmd.Connection.Close();
            return record;
        }

        // Bu method seçilen kitaba ait o kitap elinde olan ve o kitabı alıp teslim etmiş olan öğrencileri getirir.
        public List<BookRecordHistory> getBookRecordHistory(string kitap_isbn) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("SELECT Kitap.isbn, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.borc_bilgisi, Kayit_gecmisi.teslim_durumu FROM Kitap INNER JOIN(Öğrenci INNER JOIN Kayit_gecmisi ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no) ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn WHERE(((Kitap.isbn) = '{0}'))", kitap_isbn);
            List<BookRecordHistory> record = new List<BookRecordHistory>();
            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                BookRecordHistory rec = new BookRecordHistory();
                rec.kitap_isbn = rdr["isbn"].ToString();
                rec.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                rec.ogrenci_ad = rdr["ogrenci_ad"].ToString();
                rec.ogrenci_soyad = rdr["ogrenci_soyad"].ToString();
                rec.alindigi_tarih = rdr["alindigi_tarih"].ToString();
                rec.son_teslim_tarih = rdr["son_teslim_tarihi"].ToString();
                rec.teslim_tarih = rdr["Teslim_tarihi"].ToString();
                rec.teslim_durumu = bool.Parse(rdr["teslim_durumu"].ToString());
                rec.borc_bilgisi = int.Parse(rdr["borc_bilgisi"].ToString());
                record.Add(rec);
            }

            rdr.Close();
            cmd.Connection.Close();
            return record;
        }

        // Bu method türüne göre kayıt geçmişi tablosundaki kayıtları arar. Parametre olarak arama türü ve arama stringini alır.
        public List<BookRecordHistory> getChooseRecords(string searchoptions,string searchvalue) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            if (searchoptions == "kitap_isbn")
                cmd.CommandText = string.Format("SELECT Kayit_gecmisi.kayit_id, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no WHERE(((Kitap.isbn)Like '%{0}%')); ", searchvalue);
            else if (searchoptions == "kitap_ad")
                cmd.CommandText = string.Format("SELECT Kayit_gecmisi.kayit_id, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no WHERE(((Kitap.ad)Like '%{0}%')); ", searchvalue);
            else if (searchoptions == "ogrenci_no")
                cmd.CommandText = string.Format("SELECT Kayit_gecmisi.kayit_id, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no WHERE(((Öğrenci.ogrenci_no)Like '%{0}%')); ", searchvalue);
            else if (searchoptions == "ogrenci_ad")
                cmd.CommandText = string.Format("SELECT Kayit_gecmisi.kayit_id, Öğrenci.ogrenci_no, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Kitap.isbn, Kitap.ad, Kayit_gecmisi.alindigi_tarih, Kayit_gecmisi.son_teslim_tarihi, Kayit_gecmisi.Teslim_tarihi, Kayit_gecmisi.teslim_durumu, Kayit_gecmisi.borc_bilgisi FROM Öğrenci INNER JOIN(Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn) ON Öğrenci.ogrenci_no = Kayit_gecmisi.ogrenci_no WHERE(((Öğrenci.ogrenci_ad)Like '%{0}%'));", searchvalue);
            List<BookRecordHistory> record = new List<BookRecordHistory>();

            OleDbDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                BookRecordHistory rec = new BookRecordHistory();
                rec.kayit_id = Int32.Parse(rdr["kayit_id"].ToString());
                rec.kitap_isbn = (rdr["isbn"]).ToString();
                rec.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                rec.ogrenci_ad = rdr["ogrenci_ad"].ToString();
                rec.ogrenci_soyad = rdr["ogrenci_soyad"].ToString();
                rec.kitap_ad = rdr["ad"].ToString();
                rec.alindigi_tarih = rdr["alindigi_tarih"].ToString();
                rec.son_teslim_tarih = rdr["son_teslim_tarihi"].ToString();
                rec.teslim_tarih = rdr["Teslim_tarihi"].ToString();
                rec.teslim_durumu = bool.Parse(rdr["teslim_durumu"].ToString());
                rec.borc_bilgisi = int.Parse(rdr["borc_bilgisi"].ToString());
                record.Add(rec);
            }
            rdr.Close();
            cmd.Connection.Close();
            return record;
        }

        // yeni bir öğrenciye teslim kaydı gerçekleşeceği esnada bu method kullanılır. parametre olarak bookrecordhistory adlı sınıftan bir nesne alır.
        public void addRecord(BookRecordHistory rcr) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "Insert Into Kayit_gecmisi(kitap_isbn,ogrenci_no) values('" + rcr.kitap_id + "','" + rcr.ogrenci_no + "')";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void updateDebts() // Bu method kayit_gecmisi tablosundaki borcları güncelleştirir. 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "UPDATE Kayit_gecmisi SET Kayit_gecmisi.borc_bilgisi = IIf([Kayit_gecmisi]![Teslim_tarihi]>[Kayit_gecmisi]![son_teslim_tarihi],[Kayit_gecmisi]![Teslim_tarihi]-[Kayit_gecmisi]![son_teslim_tarihi],IIf(Now()>[Kayit_gecmisi]![son_teslim_tarihi],Now()-[Kayit_gecmisi]![son_teslim_tarihi]-1,0))";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        // bu method öğrenciye verilen kitabın öğrenciden geri alınmasında kullanılır. Bir parametre alır.  (kayit_id)
        // method kayit geçmişi tablosundaki teslim edilen kitabın teslim durumunu True yapar teslim tarihini anlık olarak alır ve teslim tarihi sütununa girer ve verilen kitabın kitap
        // tablosundaki stok değerini bir artırır.
        public void returnBook(int kayit_id) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("UPDATE Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn SET Kayit_gecmisi.Teslim_tarihi = Date(), Kayit_gecmisi.teslim_durumu = True, Kitap.stok = [Kitap].[stok]+1 WHERE(((Kayit_gecmisi.kayit_id) = {0})); ", kayit_id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }
        
        // Bu method kütüphane databasinde kayıtlı tüm kitapları sayar ve bu değeri döndürür.
        public int getAllBookCount() 
        {
            int totalbook;
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Sum([Kitap].[stok]) AS toplamkitapsayisi FROM Kitap;";
            OleDbDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            totalbook = int.Parse(rdr["toplamkitapsayisi"].ToString());
            rdr.Close();
            cmd.Connection.Close();
            return totalbook;
        }

        // Bu method öğrencilerde bulunan toplam kitap sayısını getirir ve bu değeri döndürür.
        public int getNumberofbooksperstudent() 
        {
            int numberofbookperstudent;
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Count(*) AS ogrenciyeverilen FROM Kitap INNER JOIN Kayit_gecmisi ON Kitap.kitap_id = Kayit_gecmisi.kitap_isbn WHERE(([Kayit_gecmisi].[teslim_durumu] = False));";
            OleDbDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            numberofbookperstudent = int.Parse(rdr["ogrenciyeverilen"].ToString());
            rdr.Close();
            cmd.Connection.Close();
            return numberofbookperstudent;
        }



    }
}
