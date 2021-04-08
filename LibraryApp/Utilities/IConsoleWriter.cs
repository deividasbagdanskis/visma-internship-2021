using LibraryApp.Models;
using System.Collections.Generic;

namespace LibraryApp.Utilities
{
    public interface IConsoleWriter
    {
        void PrintBooks(IList<Book> books);
        void PrintResultMessage(string result);
        void PrintManual();
    }
}