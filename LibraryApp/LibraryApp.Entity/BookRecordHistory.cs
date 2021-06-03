using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Entity
{
    public class BookRecordHistory
    {
        public int kayit_id { get; set; }
        public int ogrenci_no { get; set; }
        public string ogrenci_ad { get; set; }
        public string ogrenci_soyad { get; set; }
        public string kitap_isbn { get; set; }
        public int kitap_id { get; set; }
        public string kitap_ad { get; set; }
        public string alindigi_tarih { get; set; }
        public string son_teslim_tarih { get; set; }
        public string teslim_tarih { get; set; }
        public bool teslim_durumu { get; set; }
        public int borc_bilgisi { get; set; }
    }
}
