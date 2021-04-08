using LibraryApp.Models;
using LibraryApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryApp.Tests.RepositoriesTests
{
    public class BookJsonRepositoryTests
    {
        private IBookRepository _bookRepository;

        public BookJsonRepositoryTests()
        {
            string testBooksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\testBooks.json";
            _bookRepository = new BookJsonRepository(testBooksFilePath);
        }

        [Fact]
        public void GetAll_Pass()
        {
            AddTestBooks();

            IList<Book> testBooks = GetTestBooks();

            IList<Book> returnedBooks = _bookRepository.GetAll();

            Assert.Equal(testBooks.Count, returnedBooks.Count);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByName_Name_1984_Pass()
        {
            AddTestBooks();

            string expectedName = "1984";

            Book returnedBook = _bookRepository.GetBooksByName(expectedName).FirstOrDefault();

            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByAuthor_Author_George_Orwell_Pass()
        {
            AddTestBooks();

            string expectedAuthor = "George Orwell";

            Book returnedBook = _bookRepository.GetBooksByAuthor(expectedAuthor).FirstOrDefault();

            string actualAuthor = returnedBook.Author;

            Assert.Equal(expectedAuthor, actualAuthor);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByCateogory_Category_Fiction_Satire_Pass()
        {
            AddTestBooks();

            string expectedCategory = "Fiction Satire";

            Book returnedBook = _bookRepository.GetBooksByCategory(expectedCategory).FirstOrDefault();

            string actualCategory = returnedBook.Category;

            Assert.Equal(expectedCategory, actualCategory);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByLanguage_Language_English_Pass()
        {
            AddTestBooks();

            string requestedLanguage = "English";

            IList<Book> returnedBooks = _bookRepository.GetBooksByLanguage(requestedLanguage);

            int expectedCount = GetTestBooks().Where(b => b.Language == requestedLanguage).Count();
            int actualCount = returnedBooks.Count;

            Assert.Equal(expectedCount, actualCount);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByISBN_ISBN_9780062316097_Pass()
        {
            AddTestBooks();

            string requestedISBN = "9780062316097";

            Book returnedBook = _bookRepository.GetBooksByISBN(requestedISBN).FirstOrDefault();

            string expectedName = GetTestBooks().Where(b => b.ISBN == requestedISBN).FirstOrDefault().Name;
            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByAvailability_Availability_Taken_Pass()
        {
            AddTestBooks();

            string requestedAvailability = "Taken";

            Book returnedBook = _bookRepository.GetBooksByAvailability(requestedAvailability).FirstOrDefault();

            string expectedName = GetTestBooks().Where(b => b.Availability == BookAvailability.Taken).FirstOrDefault().Name;
            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            DeleteTestBooks();
        }

        [Fact]
        public void GetBooksByBorrower_Borrower_Joe_Doe_Pass()
        {
            AddTestBooks();

            string requestedBorrower = "Joe Doe";

            Book returnedBook = _bookRepository.GetBooksByBorrower(requestedBorrower).FirstOrDefault();

            string expectedName = GetTestBooks().Where(b => b.Borrower == requestedBorrower).FirstOrDefault().Name;
            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            DeleteTestBooks();
        }

        [Fact]
        public void Add_Book_Pass()
        {
            Book book = new Book()
            {
                Name = "The Stranger",
                Author = "Albert Camus",
                Category = "Classic",
                Language = "English",
                PublicationDate = new DateTime(1989, 3, 13),
                ISBN = "9780679720201",
                Availability = BookAvailability.Available,
            };

            _bookRepository.Add(book);

            Book returnedBook = _bookRepository.GetBooksByISBN(book.ISBN).FirstOrDefault();

            string expectedName = book.Name;
            string actualName = returnedBook.Name;

            Assert.Equal(expectedName, actualName);

            _bookRepository.Delete(book.ISBN);
        }

        [Fact]
        public void Update_Book_Pass()
        {
            AddTestBooks();
            
            string bookName = "1984";

            Book book = _bookRepository.GetBooksByName(bookName).FirstOrDefault();
            book.Borrower = "Bob";

            _bookRepository.Update(book);

            Book returnedBook = _bookRepository.GetBooksByName(bookName).FirstOrDefault();

            string expectedBorrower = book.Borrower;
            string actualBorrower = returnedBook.Borrower;

            Assert.Equal(expectedBorrower, actualBorrower);

            DeleteTestBooks();
        }

        [Fact]
        public void Delete_Book_Pass()
        {
            Book book = new Book()
            {
                Name = "The Stranger",
                Author = "Albert Camus",
                Category = "Classic",
                Language = "English",
                PublicationDate = new DateTime(1989, 3, 13),
                ISBN = "9780679720201",
                Availability = BookAvailability.Available,
            };

            _bookRepository.Add(book);

            bool result = _bookRepository.Delete(book.ISBN);

            Assert.True(result);
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
