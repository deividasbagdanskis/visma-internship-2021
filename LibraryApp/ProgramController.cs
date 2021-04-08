using LibraryApp.Models;
using LibraryApp.Services;
using LibraryApp.Utilities;
using System.Collections.Generic;

namespace LibraryApp
{
    public class ProgramController
    {
        private IBookService _bookService;
        private IConsoleWriter _consoleWriter;
        private IFilteringService _filteringService;
        private IBorrowingService _borrowingService;
        private IReturnService _returnService;

        public ProgramController(IBookService bookService, IConsoleWriter consoleWriter,
            IFilteringService filteringService, IBorrowingService borrowingService, IReturnService returnService)
        {
            _bookService = bookService;
            _consoleWriter = consoleWriter;
            _filteringService = filteringService;
            _borrowingService = borrowingService;
            _returnService = returnService;
        }

        public void Start(string[] args)
        {
            string action = args[0];
            string result = "";
            IList<Book> books = new List<Book>();

            switch (action)
            {
                case "add":
                    result = _bookService.AddNewBook(args);
                    break;
                case "list-all":
                    books = _bookService.GetAListOfAllBooks();
                    break;
                case "delete":
                    result = _bookService.DeleteBook(args);
                    break;
                case "filter":
                    books = _filteringService.FilterBooks(args[1]);
                    break;
                case "take":
                    result = _borrowingService.TakeABook(args);
                    break;
                case "return":
                    result = _returnService.ReturnBook(args);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                _consoleWriter.PrintResultMessage(result);
            }
            else
            {
                _consoleWriter.PrintBooks(books);
            }
        }
    }
}
