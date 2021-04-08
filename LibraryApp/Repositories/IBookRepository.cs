using LibraryApp.Models;
using System.Collections.Generic;

namespace LibraryApp.Repositories
{
    public interface IBookRepository
    {
        IList<Book> GetAll();
        IList<Book> GetBooksByAuthor(string author);
        IList<Book> GetBooksByAvailability(string availability);
        IList<Book> GetBooksByBorrower(string borrower);
        IList<Book> GetBooksByCategory(string category);
        IList<Book> GetBooksByISBN(string ISBN);
        IList<Book> GetBooksByLanguage(string language);
        IList<Book> GetBooksByName(string name);
        void Add(Book book);
        bool Delete(string isbn);
        bool Update(Book book);
    }
}