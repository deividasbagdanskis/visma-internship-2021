using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Services
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IArgumentParser _argumentParser;
        private IArgumentChecker _argumentChecker;

        public BookService(IArgumentParser argumentParser, IBookRepository bookRepository,
            IArgumentChecker argumentChecker)
        {
            _argumentParser = argumentParser;
            _bookRepository = bookRepository;
            _argumentChecker = argumentChecker;
        }

        public IList<Book> GetAListOfAllBooks()
        {
            IList<Book> books = _bookRepository.GetAll();

            return books;
        }

        public string AddNewBook(string[] args)
        {
            IDictionary<string, string> bookAttributes = _argumentParser.ParseArgsArrayIntoArgsDictionary(args);

            bool allAttributesExist = _argumentChecker.CheckIfAllArgsExists(bookAttributes,
                new string[] { "name", "author", "category", "language", "publicationDate", "ISBN" });

            string result = "";

            if (allAttributesExist)
            {
                Book newBook;

                try
                {
                    newBook = CreateANewBook(bookAttributes);
                }
                catch (FormatException ex)
                {
                    return ex.Message;
                }

                if (newBook != null)
                {
                    _bookRepository.Add(newBook);
                    result = "The book was addded successfully";
                }
            }
            else
            {
                result = "Some parameters are missing";
            }

            return result;
        }

        private Book CreateANewBook(IDictionary<string, string> bookAttributes)
        {
            Book book = new Book
            {
                Name = bookAttributes["name"],
                Author = bookAttributes["author"],
                Category = bookAttributes["category"],
                Language = bookAttributes["language"],
                ISBN = bookAttributes["ISBN"]
            };

            try
            {
                book.PublicationDate = DateTime.Parse(bookAttributes["publicationDate"]);
            }
            catch (FormatException)
            {
                throw;
            }

            book.Availability = BookAvailability.Available;

            return book;
        }

        public string DeleteBook(string[] args)
        {
            IDictionary<string, string> arguments = _argumentParser.ParseArgsArrayIntoArgsDictionary(args);

            bool allAttributesExist = _argumentChecker.CheckIfAllArgsExists(arguments, new string[] { "ISBN" });

            string result;

            if (allAttributesExist)
            {
                Book book = _bookRepository.GetBooksByISBN(arguments["ISBN"]).FirstOrDefault();

                if (book != null)
                {
                    bool bookWasDeleted = _bookRepository.Delete(book.ISBN);

                    if (bookWasDeleted)
                    {
                        result = "Book was successfully deleted";
                    }
                    else
                    {
                        result = "Book was not deleted";
                    }
                }
                else
                {
                    result = "Book was not found";
                }
            }
            else
            {
                result = "Some parameters are missing";
            }

            return result;
        }
    }
}
