using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Entity
{
    public class Kitap_Entity
    {
        public int kitap_id { get; set; }
        public string kitap_isbn { get; set; }
        public string kitap_ad { get; set; }
        public string kitap_kategori { get; set; }
        public string kitap_yazar { get; set; }
        public string kitap_yayinevi { get; set; }
        public int kategori_id { get; set; }
        public int yayinevi_id { get; set; }
        public int yazar_id { get; set; }
        public int kitap_basimyili { get; set; }
        public int kitap_sayfa { get; set; }
        public int kitap_stok { get; set; }
    }
}
