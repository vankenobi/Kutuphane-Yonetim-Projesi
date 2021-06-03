using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DAL;
using LibraryApp.Entity;

namespace LibraryApp.BLL
{
    public class kitapBLL
    {
        private kitapDAL kitap;
        public kitapBLL()
        {
            kitap = new kitapDAL();
        }
        public List<Kitap_Entity> getAllBooks()
        {
            return kitap.getAllBooks();
        }
        public void addNewBook(Kitap_Entity ktp)
        {
            kitap.addNewBook(ktp);
        }

        public void updateBook(Kitap_Entity ktp)
        {
            kitap.updateBook(ktp);
        }
        public void bookOneIncrement(Kitap_Entity ktp)
        {
            kitap.bookOneIncrement(ktp);
        }
        public List<Kitap_Entity> getChooseBook(string searchoptions, string searchvalue)
        {
            return kitap.getChooseBook(searchoptions,searchvalue);
        }
        public void deleteBook(string isbn) 
        {
            kitap.deleteBook(isbn);
        }
        public Dictionary<int,string> yayineviGet() 
        {
            return kitap.yayineviGet();
        }
        public Dictionary<int,string> yazarGet()
        {
            return kitap.yazarGet();
        }
        public Dictionary<int,string> kategoriGet()
        {
            return kitap.kategoriGet();
        }

        public void addPublisher(string puslisher)
        {
            kitap.addPublisher(puslisher);
        }
        public void deletePublisher(int publisherid)
        {
            kitap.deletePublisher(publisherid);
        }
        public void addAuthor(string author)
        {
            kitap.addAuthor(author);
        }
        public void deleteAuthor(int authorid)
        {
            kitap.deleteAuthor(authorid);
        }
        public void addCategory(string category)
        {
            kitap.addCategory(category);
        }
        public void deleteCategory(int categoryid)
        {
            kitap.deleteCategory(categoryid);
        }

    }
}
