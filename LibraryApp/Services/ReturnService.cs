using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Services
{
    public class ReturnService : IReturnService
    {
        private IBookRepository _bookRepository;
        private IArgumentParser _argumentParser;
        private IArgumentChecker _argumentChecker;

        public ReturnService(IBookRepository bookRepository, IArgumentParser argumentParser,
            IArgumentChecker argumentChecker)
        {
            _bookRepository = bookRepository;
            _argumentParser = argumentParser;
            _argumentChecker = argumentChecker;
        }

        public string ReturnBook(string[] args)
        {
            IDictionary<string, string> returnArgs = _argumentParser.ParseArgsArrayIntoArgsDictionary(args);

            bool returnArgsExits = _argumentChecker.CheckIfAllArgsExists(returnArgs, new string[] { "borrower", "ISBN" });

            if (returnArgsExits)
            {
                string returningBooksISBN = returnArgs["ISBN"];
                string borrower = returnArgs["borrower"];
                DateTime returnDateTime = DateTime.UtcNow;

                Book returningBook = _bookRepository.GetBooksByISBN(returningBooksISBN).FirstOrDefault();

                if (returningBook == null)
                {
                    return "Book was not found";
                }

                if (!returningBook.Borrower.Equals(borrower))
                {
                    return "You cannot return this book, because you did not borrow it.";
                }
                else
                {
                    string result = "";
                    double dateDifference = (returnDateTime - returningBook.ReturnDate).TotalSeconds;

                    if (dateDifference > 0)
                    {
                        result += "You are late\n";
                    }

                    returningBook.Availability = BookAvailability.Available;
                    returningBook.ReturnDate = DateTime.MinValue;
                    returningBook.Borrower = null;

                    _bookRepository.Update(returningBook);

                    result += "The book was returned successfully\n";

                    return result;
                }
            }
            else
            {
                return "Some arguments are missing";
            }
        }
    }
}
