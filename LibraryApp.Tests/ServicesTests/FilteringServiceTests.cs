using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryApp.Tests.ServicesTests
{
    public class FilteringServiceTests
    {
        private IFilteringService _filteringService;
        private IBookRepository _bookRepository;

        public FilteringServiceTests()
        {
            string testBooksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\testBooks.json";

            _bookRepository = new BookJsonRepository(testBooksFilePath);
            _filteringService = new FilteringService(_bookRepository);
        }

        [Fact]
        public void Filter_Name_1984_Pass()
        {
            AddTestBooks();

            string filteringArg = "--name=1984";

            Book returnedBook = _filteringService.FilterBooks(filteringArg).FirstOrDefault();

            string expectedName = "1984";
            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            DeleteTestBooks();
        }

        [Fact]
        public void Filter_Author_GeorgeOrwell_Pass()
        {
            AddTestBooks();

            string filteringArg = "--author=George Orwell";

            Book returnedBook = _filteringService.FilterBooks(filteringArg).FirstOrDefault();

            string expectedAuthor = "George Orwell";
            string actualAuthor = returnedBook.Author;

            Assert.Equal(expectedAuthor, actualAuthor);

            DeleteTestBooks();
        }

        [Fact]
        public void Filter_Category_Fiction_Satire_Pass()
        {
            AddTestBooks();

            string filteringArg = "--category=Fiction Satire";

            Book returnedBook = _filteringService.FilterBooks(filteringArg).FirstOrDefault();

            string expectedCategory = "Fiction Satire";
            string actualCategory = returnedBook.Category;

            Assert.Equal(expectedCategory, actualCategory);

            DeleteTestBooks();
        }

        [Fact]
        public void Filter_Language_English_Pass()
        {
            AddTestBooks();

            string filteringArg = "--language=English";

            IList<Book> returnedBooks = _filteringService.FilterBooks(filteringArg);

            int expectedCount = GetTestBooks().Where(b => b.Language == "English").Count();
            int actualCount = returnedBooks.Count;

            Assert.Equal(expectedCount, actualCount);

            DeleteTestBooks();
        }

        [Fact]
        public void Filter_ISBN_9780062316097_Pass()
        {
            AddTestBooks();

            string filteringArg = "--ISBN=9780062316097";

            Book returnedBook = _filteringService.FilterBooks(filteringArg).FirstOrDefault();

            string expectedISBN = "9780062316097";
            string actualISBN = returnedBook.ISBN;

            Assert.Equal(expectedISBN, actualISBN);

            DeleteTestBooks();
        }

        [Fact]
        public void Filter_Availability_Taken_Pass()
        {
            AddTestBooks();

            string filteringArg = "--availability=Taken";

            IList<Book> returnedBooks = _filteringService.FilterBooks(filteringArg);

            int expectedCount = GetTestBooks().Where(b => b.Availability == BookAvailability.Taken).Count();
            int actualCount = returnedBooks.Count;

            Assert.Equal(expectedCount, actualCount);

            DeleteTestBooks();
        }

        private void AddTestBooks()
        {
            IList<Book> testBooks = GetTestBooks();

            foreach (Book book in testBooks)
            {
                _bookRepository.Add(book);
            }
        }

        private void DeleteTestBooks()
        {
            IList<Book> testBooks = GetTestBooks();

            foreach (Book book in testBooks)
            {
                _bookRepository.Delete(book.ISBN);
            }
        }

        private IList<Book> GetTestBooks()
        {
            IList<Book> testBooks = new List<Book>()
            {
                new Book()
                {
                    Name = "1984",
                    Author = "George Orwell",
                    Category = "Fiction Satire",
                    Language = "English",
                    PublicationDate = new DateTime(2003, 6, 1),
                    ISBN = "978-0151010264",
                    Availability = BookAvailability.Available,
                },
                new Book()
                {
                    Name = "Sapiens: A Brief History of Humankind",
                    Author = "Yuval Noah Harari",
                    Category = "History",
                    Language = "English",
                    PublicationDate = new DateTime(2015, 2, 10),
                    ISBN = "9780062316097",
                    Availability = BookAvailability.Taken,
                    Borrower = "Joe Doe",
                }
            };

            return testBooks;
        }
    }
}
