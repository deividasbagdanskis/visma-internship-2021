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
            if (args.Length != 0)
            {
                string action = args[0];
                string result = "";
                IList<Book> books = null;

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
                    case "help":
                        _consoleWriter.PrintManual();
                        break;
                    default:
                        _consoleWriter.PrintManual();
                        break;
                }

                if (!string.IsNullOrWhiteSpace(result))
                {
                    _consoleWriter.PrintResultMessage(result);
                }
                else if (books != null)
                {
                    _consoleWriter.PrintBooks(books);
                }
            }
            else
            {
                _consoleWriter.PrintManual();
            }
        }
    }
}
