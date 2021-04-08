using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryApp.Tests.ServicesTests
{
    public class ReturnServiceTests
    {
        private IBookRepository _bookRepository;
        private IReturnService _returnService;

        public ReturnServiceTests()
        {
            string testBooksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\testBooks.json";

            _bookRepository = new BookJsonRepository(testBooksFilePath);
            IArgumentChecker argumentChecker = new ArgumentChecker();
            IArgumentParser argumentParser = new ArgumentParser();

            _returnService = new ReturnService(_bookRepository, argumentParser, argumentChecker);
        }

        [Fact]
        public void ReturnBook_Success_Pass()
        {
            AddTestBooks();

            string[] returnArgs = new string[] { "return", "--borrower=Joe Doe", "--ISBN=978-0151010264" };

            string expectedMessage = "The book was returned successfully\n";
            string actualMessage = _returnService.ReturnBook(returnArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void ReturnBook_BeingLate_Pass()
        {
            AddTestBooks();

            string[] returnArgs = new string[] { "return", "--borrower=Joe Doe", "--ISBN=9780062316097" };

            string expectedMessage = "You are late";
            string actualMessage = _returnService.ReturnBook(returnArgs);

            Assert.Contains(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void ReturnBook_CantReturnABookWhichYouDidntTake_Pass()
        {
            AddTestBooks();

            string[] returnArgs = new string[] { "return", "--borrower=Joe Doe", "--ISBN=0358653037" };

            string expectedMessage = "You cannot return this book, because you did not borrow it.";
            string actualMessage = _returnService.ReturnBook(returnArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void ReturnBook_BookWasNotFound_Pass()
        {
            AddTestBooks();

            string[] returnArgs = new string[] { "return", "--borrower=Joe Doe", "--ISBN=0358643035" };

            string expectedMessage = "Book was not found";
            string actualMessage = _returnService.ReturnBook(returnArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void ReturnBook_ArgsAreMissing_Pass()
        {
            AddTestBooks();

            string[] returnArgs = new string[] { "return", "--borrower=Joe Doe" };

            string expectedMessage = "Some arguments are missing";
            string actualMessage = _returnService.ReturnBook(returnArgs);

            Assert.Equal(expectedMessage, actualMessage);

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
                    Availability = BookAvailability.Taken,
                    ReturnDate = DateTime.UtcNow.AddDays(30),
                    Borrower = "Joe Doe"
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
                    ReturnDate = DateTime.UtcNow.AddDays(-5),
                    Borrower = "Joe Doe",
                },
                new Book()
                {
                    Name = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Category = "Fantasy",
                    Language = "English",
                    PublicationDate = new DateTime(2020, 10, 19),
                    ISBN = "0358653037",
                    Availability = BookAvailability.Taken,
                    ReturnDate = DateTime.UtcNow.AddDays(25),
                    Borrower = "Bob"
                },
                new Book()
                {
                    Name = "The Stranger",
                    Author = "Albert Camus",
                    Category = "Classic",
                    Language = "English",
                    PublicationDate = new DateTime(1989, 3, 13),
                    ISBN = "9780679720201",
                    Availability = BookAvailability.Available
                }
            };

            return testBooks;
        }
    }
}
