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

        public ProgramController(IBookService bookService, IConsoleWriter consoleWriter,
            IFilteringService filteringService)
        {
            _bookService = bookService;
            _consoleWriter = consoleWriter;
            _filteringService = filteringService;
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
