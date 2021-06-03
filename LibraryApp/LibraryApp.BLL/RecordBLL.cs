using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DAL;
using LibraryApp.Entity;

namespace LibraryApp.BLL
{
    public class RecordBLL
    {
        private RecordDAL record;
        public RecordBLL() 
        {
            record = new RecordDAL();
        }
        public List<BookRecordHistory> getAllRecords() 
        {
            return record.getAllRecords();
        }
        public void addRecord(BookRecordHistory rcr) 
        {
            record.addRecord(rcr);
        }
        public void updateDebts() 
        {
            record.updateDebts();
        }
        public List<BookRecordHistory> getChooseRecords(string searchoptions, string searchvalue) 
        {
            return record.getChooseRecords(searchoptions,searchvalue);
        }
        public void returnBook(int kayit_id) 
        {
            record.returnBook(kayit_id);
        }
        public List<BookRecordHistory> getStudentRecordHistory(int ogrenci_no) 
        {
            return record.getStudentRecordHistory(ogrenci_no);
        }
        public List<BookRecordHistory> getBookRecordHistory(string kitap_isbn) 
        {
            return record.getBookRecordHistory(kitap_isbn);
        }
        public int getAllBookCount() 
        {
            return record.getAllBookCount();
        }
        public int getNumberofbooksperstudent() 
        {
            return record.getNumberofbooksperstudent();
        }
    }
}
