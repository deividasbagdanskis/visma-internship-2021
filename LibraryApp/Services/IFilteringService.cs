using LibraryApp.Models;
using System.Collections.Generic;

namespace LibraryApp.Services
{
    public interface IFilteringService
    {
        IList<Book> FilterBooks(string filteringArg);
    }
}