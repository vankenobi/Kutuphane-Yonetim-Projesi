using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using LibraryApp.Entity;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Data;

namespace LibraryApp.DAL
{
    public class kitapDAL
    {
        private DBconnection dbconnection;
        public kitapDAL() 
        {
            dbconnection = new DBconnection();
        }
        
        public List<Kitap_Entity> getAllBooks()  // getAllBooks metodu kitap tablosundaki bütün kitapları döndürür.
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kategori.kategori_ad, Kitap.sayfa_sayisi, Kitap.stok FROM Kategori INNER JOIN(Yazar INNER JOIN (Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad";
            List<Kitap_Entity> kitap = new List<Kitap_Entity>();   // Kitap_entity sınıfından bir liste oluşturuyoruz.
            OleDbDataReader rdr = cmd.ExecuteReader();             // Oledbdatareaderdan bir rdr değişkeni oluşturu 

            while (rdr.Read()) 
            {
                //rdr içerisinde gelen veriler okunur ve ktp adlı nesneye aktarılır.
                Kitap_Entity ktp = new Kitap_Entity();
                ktp.kitap_id = rdr.GetInt32(0);
                ktp.kitap_isbn = rdr["isbn"].ToString();
                ktp.kitap_ad = rdr["ad"].ToString();
                ktp.kitap_basimyili = rdr.GetInt32(3);
                ktp.kitap_yazar = rdr["yazar_ad"].ToString();
                ktp.kitap_kategori = rdr["kategori_ad"].ToString();
                ktp.kitap_yayinevi = rdr["yayinevi_ad"].ToString();
                ktp.kitap_sayfa = rdr.GetInt32(7);
                ktp.kitap_stok = rdr.GetInt32(8);
                kitap.Add(ktp);         // kitap adında oluşturulan listeye ktp adlı nesne ekleniyor.
            }
            rdr.Close();
            cmd.Connection.Close();
            return kitap;         // en son oluşturulan nesne döndürülüyor.
        }

        // Bu method kitap tablosunu yeni bir kitap eklenmek istendiğinde çalışır.
        public void addNewBook(Kitap_Entity ktp) 
        {
                
                OleDbCommand cmd = dbconnection.GetOleDbCommand();
                cmd.CommandText = "Insert Into kitap(isbn, ad, basim_yili, yayinevi_ad,yazar_ad,kategori_ad,sayfa_sayisi,stok) values('" + ktp.kitap_isbn + "','" + ktp.kitap_ad + "','" + ktp.kitap_basimyili + "','" + ktp.yayinevi_id + "','" + ktp.yazar_id + "','" + ktp.kategori_id + "','" + ktp.kitap_sayfa + "',1)";
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
        }

        // Bu method kitap güncellenmek istendiğinde çalışır. Parametre olarak Kitap_entity sınıfından nesne alır.
        public void updateBook(Kitap_Entity kitap)
        {   
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("UPDATE Yazar INNER JOIN (Yayınevi INNER JOIN (Kategori INNER JOIN Kitap ON Kategori.kategori_id = Kitap.kategori_ad) ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad SET Kitap.isbn = '{0}', Kitap.ad = '{1}', Kitap.basim_yili = {2}, Kitap.yayinevi_ad = {3}, Kitap.yazar_ad = {4}, Kitap.kategori_ad = {5}, Kitap.sayfa_sayisi = {6} WHERE(((Kitap.isbn) = '{7}'))", kitap.kitap_isbn, kitap.kitap_ad, kitap.kitap_basimyili, kitap.yayinevi_id, kitap.yazar_id, kitap.kategori_id, kitap.kitap_sayfa,kitap.kitap_isbn);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        // Bu method kitabın stok bilgisi bir artırılmak istendiğinde çalışır.
        public void  bookOneIncrement(Kitap_Entity ktp) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("UPDATE Kitap SET Kitap.stok = [Kitap].[stok]+1 WHERE(((Kitap.isbn) = '{0}')); ", ktp.kitap_isbn, ktp.kitap_isbn);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        
        // Bu method combobox'a yayinevi id ve değerlerini databaseden göndermek için kullanılır. Dictionart tipinde değer döndürür.
        public Dictionary<int,string> yayineviGet() 
        {
            var yayinevi_list = new Dictionary<int,string>();
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Yayınevi.yayinevi_id,Yayınevi.yayinevi_ad FROM Yayınevi";
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) 
            {
                yayinevi_list.Add(rdr.GetInt32(0),rdr.GetString(1));
            }
            return yayinevi_list;
        }

        // Bu method combobox'a yazar id ve değerlerini databaseden göndermek için kullanılır. Dictionart tipinde değer döndürür.
        public Dictionary<int,string> yazarGet()
        {
            var yazar_list = new Dictionary<int,string>();
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Yazar.yazar_id, Yazar.yazar_ad FROM Yazar";
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                yazar_list.Add(rdr.GetInt32(0),rdr.GetString(1));
            }
            return yazar_list;
        }

        // Bu method combobox'a kategori id ve değerlerini databaseden göndermek için kullanılır. Dictionart tipinde değer döndürür.
        public Dictionary<int,string> kategoriGet()
        {
            var kategori_list = new Dictionary<int,string>();
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT Kategori.kategori_id, Kategori.kategori_ad FROM Kategori";
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                kategori_list.Add(rdr.GetInt32(0), rdr.GetString(1));
            }
            return kategori_list;
        }

        // bu method arayüzde seçilen türe göre kitap tablosundan arama işlemi yapar.
        public List<Kitap_Entity> getChooseBook(string searchoptions, string searchvalue)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            if (searchoptions == "kitap_isbn")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN (Yazar INNER JOIN (Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE (((Kitap.isbn) Like '%{0}%'));", searchvalue);
            else if (searchoptions == "kitap_ad")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN(Yazar INNER JOIN(Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE(((Kitap.ad)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "kitap_kategori")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN(Yazar INNER JOIN(Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE(((Kategori.kategori_ad) Like '%{0}%'));", searchvalue);
            else if (searchoptions == "kitap_yazar")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN(Yazar INNER JOIN(Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE(((Yazar.yazar_ad) Like '%{0}%'));", searchvalue);
            else if (searchoptions == "kitap_yayinevi")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN(Yazar INNER JOIN(Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE(((Yayınevi.yayinevi_ad) Like '%{0}%'));", searchvalue);
            else if (searchoptions == "kitap_basim_yili")
                cmd.CommandText = string.Format("SELECT Kitap.kitap_id, Kitap.isbn, Kitap.ad, Kitap.basim_yili, Yayınevi.yayinevi_ad, Yazar.yazar_ad, Kitap.sayfa_sayisi, Kitap.stok, Kategori.kategori_ad FROM Kategori INNER JOIN(Yazar INNER JOIN(Yayınevi INNER JOIN Kitap ON Yayınevi.yayinevi_id = Kitap.yayinevi_ad) ON Yazar.yazar_id = Kitap.yazar_ad) ON Kategori.kategori_id = Kitap.kategori_ad WHERE(((Kitap.basim_yili) Like '%{0}%'));", searchvalue);

            List<Kitap_Entity> kitap = new List<Kitap_Entity>(); 
            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read()) // reader ile okunan veriler ktp adlı nesneye atılıyor.
            {
                Kitap_Entity ktp = new Kitap_Entity();
                ktp.kitap_id = int.Parse(rdr["kitap_id"].ToString());
                ktp.kitap_isbn = rdr["isbn"].ToString();
                ktp.kitap_ad = rdr["ad"].ToString();
                ktp.kitap_basimyili = int.Parse(rdr["basim_yili"].ToString());
                ktp.kitap_yayinevi = rdr["yayinevi_ad"].ToString();
                ktp.kitap_yazar = rdr["yazar_ad"].ToString();
                ktp.kitap_kategori = rdr["kategori_ad"].ToString();
                ktp.kitap_sayfa = int.Parse(rdr["sayfa_sayisi"].ToString());
                ktp.kitap_stok = int.Parse(rdr["stok"].ToString());
                kitap.Add(ktp); // ktp adlı nesne kitap adlı listeye ekleniyor.
            }
            rdr.Close();
            cmd.Connection.Close(); // sorgu sonunda database ile olan bağlantı koparılıyor.
            return kitap;

        }

        // bu method kitap silmek için kullanılıyor. Parametre olarak kitap isbn numarasını alıyor. 
        public void deleteBook(string isbn) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand(); 
            cmd.CommandText = string.Format("UPDATE Kitap SET Kitap.stok = [Kitap].[stok]-1 WHERE(((Kitap.isbn) = '{0}'));", isbn);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void addPublisher(string puslisher) 
        {
         
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "Insert Into Yayınevi(yayinevi_ad) values('" + puslisher + "');";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
         
            
        }
        public void deletePublisher(int publisherid) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("DELETE Yayınevi.yayinevi_id, Yayınevi.yayinevi_ad FROM Yayınevi WHERE(((Yayınevi.yayinevi_id) = {0}))",publisherid);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void addAuthor(string author)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "Insert Into Yazar(yazar_ad) values('" + author + "');";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void deleteAuthor(int authorid)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("DELETE Yazar.yazar_id, Yazar.yazar_ad FROM Yazar WHERE(((Yazar.yazar_id) = {0}))", authorid);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void addCategory(string category)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "Insert Into Kategori(kategori_ad) values('" + category + "');";
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void deleteCategory(int categoryid)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("DELETE Kategori.kategori_id, Kategori.kategori_ad FROM Kategori WHERE(((Kategori.kategori_id) = {0}))", categoryid);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

    }
}
