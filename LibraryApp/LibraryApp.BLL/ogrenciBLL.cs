using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DAL;
using LibraryApp.Entity;
using System.Data.OleDb;

namespace LibraryApp.BLL
{
    public class ogrenciBLL
    {
        private ogrenciDAL ogrenciDal;
        public ogrenciBLL() 
        {
            ogrenciDal = new ogrenciDAL();
        }
        public List<Ogrenci_Entity> getAllStudents() 
        {
            return ogrenciDal.getAllStudents();
        }
        public void addNewStudent(Ogrenci_Entity ogrenci) 
        {
            ogrenciDal.addNewStudent(ogrenci);           
        }
        public void deleteStudent(int ogrenci_no) 
        {
            ogrenciDal.deleteStudent(ogrenci_no);
        }
        public void updateStudent(Ogrenci_Entity ogrenci) 
        {
            ogrenciDal.updateStudent(ogrenci);
        }
        public List<Ogrenci_Entity> getChooseStudent(string searchoptions, string searchvalue) 
        {
            return ogrenciDal.getChooseStudent(searchoptions, searchvalue);
        }
        public bool checkDeliveredBook(int ogrenci_no) 
        {
            return ogrenciDal.checkDeliveredBook(ogrenci_no);
        }
    }
}
