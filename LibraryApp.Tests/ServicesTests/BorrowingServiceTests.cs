using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Services;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LibraryApp.Tests.ServicesTests
{
    public class BorrowingServiceTests
    {
        private IBorrowingService _borrowingService;
        private IBookRepository _bookRepository;

        public BorrowingServiceTests()
        {
            string testBooksFilePath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\testBooks.json";

            _bookRepository = new BookJsonRepository(testBooksFilePath);
            IArgumentChecker argumentChecker = new ArgumentChecker();
            IArgumentParser argumentParser = new ArgumentParser();

            _borrowingService = new BorrowingService(_bookRepository, argumentParser, argumentChecker); 
        }

        [Fact]
        public void TakeABook_Success_Pass()
        {
            AddTestBooks();

            int days = 30;
            string isbn = "9780062316097";

            string[] borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={isbn}", $"--period={days}" };

            string expectedMessage = "The book has been taken successfully\n";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

            Assert.Contains(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void TakeABook_BookNotFound_Pass()
        {
            AddTestBooks();

            int days = 30;
            string isbn = "9780062316091";

            string[] borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={isbn}", $"--period={days}" };

            string expectedMessage = "Book was not found";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void TakeABook_ForLongerThan2Months_Pass()
        {
            AddTestBooks();

            int days = 65;
            string isbn = "9780062316097";

            string[] borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={isbn}", $"--period={days}" };

            string expectedMessage = "Taking the book for longer than 2 months is not allowed";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void TakeABook_AlreadyTaken_Pass()
        {
            AddTestBooks();

            int days = 30;
            string isbn = "9780062316097";

            string[] borrowingArgs = new string[] { "take", "--borrower=Bob", $"--ISBN={isbn}", $"--period={days}" };
            _borrowingService.TakeABook(borrowingArgs);

            borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={isbn}", $"--period={days}" };

            string expectedMessage = "This book is already taken";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void TakeABook_TakingMoreThan3BooksIsNotAllowed_Pass()
        {
            AddTestBooks();
            
            List<string> booksISBN = GetTestBooks().Select(b => b.ISBN).ToList();

            int days = 30;

            string[] borrowingArgs;

            for (int i = 0; i < booksISBN.Count - 1; i++)
            {
                borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={booksISBN[i]}",
                    $"--period={days}" };
                
                _borrowingService.TakeABook(borrowingArgs);
            }

            borrowingArgs = new string[] { "take", "--borrower=Joe Doe", $"--ISBN={booksISBN[booksISBN.Count - 1]}",
                $"--period={days}" };

            string expectedMessage = "Taking more than 3 books is not allowed";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

            Assert.Equal(expectedMessage, actualMessage);

            DeleteTestBooks();
        }

        [Fact]
        public void TakeABook_ArgsMissing_Pass()
        {
            AddTestBooks();

            int days = 30;
            string isbn = "9780062316097";

            string[] borrowingArgs = new string[] { "take", $"--ISBN={isbn}", $"--period={days}" };

            string expectedMessage = "Some arguments are missing";

            string actualMessage = _borrowingService.TakeABook(borrowingArgs);

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
                    Availability = BookAvailability.Available
                },
                new Book()
                {
                    Name = "Sapiens: A Brief History of Humankind",
                    Author = "Yuval Noah Harari",
                    Category = "History",
                    Language = "English",
                    PublicationDate = new DateTime(2015, 2, 10),
                    ISBN = "9780062316097",
                    Availability = BookAvailability.Available
                },
                new Book()
                {
                    Name = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Category = "Fantasy",
                    Language = "English",
                    PublicationDate = new DateTime(2020, 10, 19),
                    ISBN = "0358653037",
                    Availability = BookAvailability.Available
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
