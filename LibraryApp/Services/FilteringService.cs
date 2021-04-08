using LibraryApp.Models;
using LibraryApp.Repositories;
using System;
using System.Collections.Generic;

namespace LibraryApp.Services
{
    public class FilteringService : IFilteringService
    {
        private IBookRepository _bookRepository;

        public FilteringService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IList<Book> FilterBooks(string filteringArg)
        {
            string[] filteringArgArray = filteringArg.Split('=');

            string attribute = filteringArgArray[0];
            string value = filteringArgArray[1];

            IList<Book> filteredBooks = new List<Book>();

            try
            {
                switch (attribute)
                {
                    case "--name":
                        filteredBooks = _bookRepository.GetBooksByName(value);
                        break;
                    case "--author":
                        filteredBooks = _bookRepository.GetBooksByAuthor(value);
                        break;
                    case "--category":
                        filteredBooks = _bookRepository.GetBooksByCategory(value);
                        break;
                    case "--language":
                        filteredBooks = _bookRepository.GetBooksByLanguage(value);
                        break;
                    case "--ISBN":
                        filteredBooks = _bookRepository.GetBooksByISBN(value);
                        break;
                    case "--availability":
                        filteredBooks = _bookRepository.GetBooksByAvailability(value);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return filteredBooks;
        }
    }
}
