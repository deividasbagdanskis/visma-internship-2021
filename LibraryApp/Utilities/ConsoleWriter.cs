using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryApp.Utilities
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void PrintBooks(IList<Book> books)
        {
            if (books.Count != 0)
            {
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            else
            {
                Console.WriteLine("No books were found");
            }
        }

        public void PrintResultMessage(string result)
        {
            Console.WriteLine(result);
        }

        public void PrintManual()
        {
            string manualPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\manual.txt";

            string text = File.ReadAllText(manualPath);

            Console.WriteLine(text);
        }
    }
}
