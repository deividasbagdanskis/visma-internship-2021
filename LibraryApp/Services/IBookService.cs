using LibraryApp.Models;
using System.Collections.Generic;

namespace LibraryApp.Services
{
    public interface IBookService
    {
        string AddNewBook(string[] args);
        string DeleteBook(string[] args);
        IList<Book> GetAListOfAllBooks();
    }
}