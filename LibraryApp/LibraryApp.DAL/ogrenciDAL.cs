using LibraryApp.Entity;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace LibraryApp.DAL
{
    public class ogrenciDAL
    {
        private DBconnection dbconnection; // DBconnection nesnesinden dbconnection adlı değişken oluşturduk.
        public ogrenciDAL()
        {
            dbconnection = new DBconnection(); // oluşturduğumuz değişkene nesnesi atadık.
        }

        public List<Ogrenci_Entity> getAllStudents() // Bu method tabloya databasedeki öğrenci tablosundan bütün kayıtları çeker.
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = "SELECT * FROM Öğrenci;";
            List<Ogrenci_Entity> ogrenci = new List<Ogrenci_Entity>();
            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Ogrenci_Entity ogr = new Ogrenci_Entity();
                ogr.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                ogr.ogrenci_kimlik = rdr["ogrenci_tc"].ToString();
                ogr.ogrenci_ad = rdr["ogrenci_ad"].ToString();
                ogr.ogrenci_soyad = rdr["ogrenci_soyad"].ToString();
                ogr.ogrenci_cinsiyet = rdr["ogrenci_cinsiyet"].ToString();
                ogr.ogrenci_telno = rdr["ogrenci_telno"].ToString();
                ogr.ogrenci_email = rdr["ogrenci_email"].ToString();

                ogrenci.Add(ogr);
            }
            rdr.Close();
            cmd.Connection.Close();
            return ogrenci;

        }

        // Tabloya yeni bir öğrenci ekle parametre olarak ogrenci_Entity sınıfında bir öğrenci nesnesi alır.
        public void addNewStudent(Ogrenci_Entity ogrenci)
        {
            string cmdtext = "Insert Into Öğrenci(ogrenci_no, ogrenci_tc, ogrenci_ad, ogrenci_soyad, ogrenci_cinsiyet,ogrenci_telno,ogrenci_email) values('" + ogrenci.ogrenci_no + "','" + ogrenci.ogrenci_kimlik + "','" + ogrenci.ogrenci_ad + "','" + ogrenci.ogrenci_soyad + "','" + ogrenci.ogrenci_cinsiyet + "','" + ogrenci.ogrenci_telno + "','" + ogrenci.ogrenci_email + "')";
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = cmdtext;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        // Bu method silinmek istenen öğrenciye ait kitap var mı yok mu kontrol eder. Eğer var ise true , yok ise false döndürür.
        public bool checkDeliveredBook(int ogrenci_no) 
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();

            cmd.CommandText = string.Format("SELECT Kayit_gecmisi.ogrenci_no, Kayit_gecmisi.teslim_durumu FROM Kayit_gecmisi WHERE(((Kayit_gecmisi.ogrenci_no) = {0}) AND((Kayit_gecmisi.teslim_durumu) = False))", ogrenci_no);
            OleDbDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
                return true;
            else 
                return false;
        }

        // Bu method öğrenci silmek istedndiğinde kullanılır. parametre olarak öğrenci_no tutar.
        public void deleteStudent(int ogrenci_no)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = String.Format("Delete from Öğrenci where ogrenci_no={0}", ogrenci_no);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        // Bu method öğrenci bilgileri güncellenmek istendiğinde çalışır. parametre olarak ogrenci_entity sınıfından bir nesne alır.
        public void updateStudent(Ogrenci_Entity ogrenci)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            cmd.CommandText = string.Format("UPDATE Öğrenci SET Öğrenci.ogrenci_no = {0}, Öğrenci.ogrenci_tc = '{1}', Öğrenci.ogrenci_ad = '{2}', Öğrenci.ogrenci_soyad = '{3}', Öğrenci.ogrenci_cinsiyet = '{4}', Öğrenci.ogrenci_telno = '{5}', Öğrenci.ogrenci_email = '{6}'  WHERE(((Öğrenci.ogrenci_no) = {7}))", ogrenci.ogrenci_no, ogrenci.ogrenci_kimlik, ogrenci.ogrenci_ad, ogrenci.ogrenci_soyad, ogrenci.ogrenci_cinsiyet, ogrenci.ogrenci_telno, ogrenci.ogrenci_email, ogrenci.ogrenci_no);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        //Bu method türüne göre öğrenci aramak istendiğinde çalışır. Parametre olarak arama türü ve aranacak texti alır.
        public List<Ogrenci_Entity> getChooseStudent(string searchoptions,string searchvalue)
        {
            OleDbCommand cmd = dbconnection.GetOleDbCommand();
            if (searchoptions == "ogrenci_ad")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_ad)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_soyad")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_soyad)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_kimlik")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_tc)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_no")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_no)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_email")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_email)Like '%{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_telno")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_telno)Like '{0}%'));", searchvalue);
            else if (searchoptions == "ogrenci_cinsiyet")
                cmd.CommandText = string.Format("SELECT  Öğrenci.ogrenci_no, Öğrenci.ogrenci_tc, Öğrenci.ogrenci_ad, Öğrenci.ogrenci_soyad, Öğrenci.ogrenci_cinsiyet, Öğrenci.ogrenci_telno, Öğrenci.ogrenci_email FROM Öğrenci WHERE(((Öğrenci.ogrenci_cinsiyet)Like '{0}%'));", searchvalue);
            List<Ogrenci_Entity> ogrenci = new List<Ogrenci_Entity>();
            OleDbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Ogrenci_Entity ogr = new Ogrenci_Entity();
                ogr.ogrenci_no = Int32.Parse(rdr["ogrenci_no"].ToString());
                ogr.ogrenci_kimlik = rdr["ogrenci_tc"].ToString();
                ogr.ogrenci_ad = rdr["ogrenci_ad"].ToString();
                ogr.ogrenci_soyad = rdr["ogrenci_soyad"].ToString();
                ogr.ogrenci_cinsiyet = rdr["ogrenci_cinsiyet"].ToString();
                ogr.ogrenci_telno = rdr["ogrenci_telno"].ToString();
                ogr.ogrenci_email = rdr["ogrenci_email"].ToString();

                ogrenci.Add(ogr);
            }
            rdr.Close();
            cmd.Connection.Close();
            return ogrenci;

        }



    }
}
