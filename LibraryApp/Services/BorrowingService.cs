using LibraryApp.Models;
using LibraryApp.Repositories;
using LibraryApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Services
{
    public class BorrowingService : IBorrowingService
    {
        private IBookRepository _bookRepository;
        private IArgumentParser _argumentParser;
        private IArgumentChecker _argumentChecker;

        public BorrowingService(IBookRepository bookRepository, IArgumentParser argumentParser,
            IArgumentChecker argumentChecker)
        {
            _bookRepository = bookRepository;
            _argumentParser = argumentParser;
            _argumentChecker = argumentChecker;
        }

        public string TakeABook(string[] args)
        {
            IDictionary<string, string> borrowingArgs = _argumentParser.ParseArgsArrayIntoArgsDictionary(args);

            bool borrowingArgsExists = _argumentChecker.CheckIfAllArgsExists(borrowingArgs,
                new string[] { "borrower", "ISBN", "period" });

            if (borrowingArgsExists)
            {
                string borrowingBookISBN = borrowingArgs["ISBN"];
                string borrower = borrowingArgs["borrower"];
                double borrowingPeriod;

                try
                {
                    borrowingPeriod = double.Parse(borrowingArgs["period"]);

                    if (borrowingPeriod > 60)
                    {
                        throw new Exception("Taking the book for longer than 2 months is not allowed");
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                int alreadyBorrowedBookCount = _bookRepository.GetBooksByBorrower(borrower).Count;

                if (alreadyBorrowedBookCount < 3)
                {
                    Book takenBook = _bookRepository.GetBooksByISBN(borrowingBookISBN).FirstOrDefault();

                    if (takenBook == null)
                    {
                        return "Book was not found";
                    }

                    takenBook.Borrower = borrower;
                    takenBook.Availability = BookAvailability.Taken;
                    takenBook.ReturnDate = DateTime.UtcNow.AddDays(borrowingPeriod);

                    _bookRepository.Update(takenBook);

                    string result = "";

                    result += "The book has been taken successfully\n";
                    result += "\nReturn date: " + takenBook.ReturnDate.ToString("yyyy-MM-dd HH:mm:ss") + "\n";
                    result += takenBook.ToString();

                    return result;
                }
                else
                {
                    return "Taking more than 3 books is not allowed";
                }
            }
            else
            {
                return "Some parameters are missing";
            }
        }
    }
}
