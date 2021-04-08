using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibraryApp.Tests.ServicesTests
{
    public class BookServiceTests
    {
        private IBookService _bookService;
        private IArgumentParser _argumentParser;

        public BookServiceTests()
        {
            string testBooksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\testBooks.json";

            IBookRepository bookRepository = new BookJsonRepository(testBooksFilePath);
            _argumentParser = new ArgumentParser();
            IArgumentChecker argumentChecker = new ArgumentChecker();

            _bookService = new BookService(_argumentParser, bookRepository, argumentChecker);
        }

        [Fact]
        public void GetAListOfAllBooks_Pass()
        {
            AddTestBooks();

            IList<string[]> testBooks = GetTestBooks();

            IList<Book> returnedBooks = _bookService.GetAListOfAllBooks();

            Assert.Equal(testBooks.Count, returnedBooks.Count);

            DeleteTestBooks();
        }


        [Fact]
        public void AddNewBook_All_Args_Pass()
        {
            string[] args = new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                "--language=English", "--publicationDate=2003-06-01", "--ISBN=978-0151010264" };

            string expectedMessage = "The book was addded successfully";

            string actualMessage = _bookService.AddNewBook(args);

            Assert.Equal(expectedMessage, actualMessage);

            string[] isbnArgs = new string[] { "delete", "--ISBN=978-0151010264" };

            _bookService.DeleteBook(isbnArgs);
        }

        [Fact]
        public void AddNewBook_Not_All_Args_Pass()
        {
            string[] args = new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                "--language=English", "--publicationDate=2003-06-01" };

            string expectedMessage = "Some arguments are missing";

            string actualMessage = _bookService.AddNewBook(args);

            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void DeleteBook_All_Args_Pass()
        {
            string[] args = new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                "--language=English", "--publicationDate=2003-06-01", "--ISBN=978-0151010264" };

            _bookService.AddNewBook(args);

            string[] deletionArgs = new string[] { "delete", "--ISBN=978-0151010264" };

            string expectedMessage = "The book was successfully deleted";

            string actualMessage = _bookService.DeleteBook(deletionArgs);

            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void DeleteBook_NonexistantBook_Pass()
        {
            string[] args = new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                "--language=English", "--publicationDate=2003-06-01", "--ISBN=978-0151010264" };

            _bookService.AddNewBook(args);

            string[] deletionArgs = new string[] { "delete", "--ISBN=978-0151010262" };

            string expectedMessage = "The book was not found";

            string actualMessage = _bookService.DeleteBook(deletionArgs);

            Assert.Equal(expectedMessage, actualMessage);

            deletionArgs = new string[] { "delete", "--ISBN=978-0151010264" };

            _bookService.DeleteBook(deletionArgs);
        }

        [Fact]
        public void DeleteBook_MissingArgs_Pass()
        {
            string[] args = new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                "--language=English", "--publicationDate=2003-06-01", "--ISBN=978-0151010264" };

            _bookService.AddNewBook(args);

            string[] deletionArgs = new string[] { "delete", "--name=978-0151010262" };

            string expectedMessage = "Some arguments are missing";

            string actualMessage = _bookService.DeleteBook(deletionArgs);

            Assert.Equal(expectedMessage, actualMessage);

            deletionArgs = new string[] { "delete", "--ISBN=978-0151010264" };

            _bookService.DeleteBook(deletionArgs);
        }

        private void AddTestBooks()
        {
            IList<string[]> testBooks = GetTestBooks();

            foreach (string[] book in testBooks)
            {
                _bookService.AddNewBook(book);
            }
        }

        private IList<string[]> GetTestBooks()
        {
            IList<string[]> testBooks = new List<string[]>()
            {
                new string[] {"add", "--name=1984", "--author=George Orwell", "--category=Fiction Satire",
                    "--language=English", "--publicationDate=2003-06-01", "--ISBN=978-0151010264" },
                new string[] {"add", "--name=Sapiens: A Brief History of Humankind", "--author=Yuval Noah Harari",
                    "--category=History", "--language=English", "--publicationDate=2015-05-10", "--ISBN=9780062316097" },
            };

            return testBooks;
        }

        private void DeleteTestBooks()
        {
            IList<string[]> testBooks = GetTestBooks();

            foreach (string[] book in testBooks)
            {
                string isbn = _argumentParser.ParseArgsArrayIntoArgsDictionary(book)["ISBN"];

                _bookService.DeleteBook(new string[] { "delete", $"--ISBN={isbn}" });
            }
        }
    }
}
