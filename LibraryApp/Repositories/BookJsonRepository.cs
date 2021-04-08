using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LibraryApp.Repositories
{
    public class BookJsonRepository : IBookRepository
    {
        private IList<Book> _books;
        private string _filePath;

        public BookJsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IList<Book> GetAll()
        {
            ReadJson();

            return _books;
        }

        public IList<Book> GetBooksByName(string name)
        {
            ReadJson();

            return _books.Where(b => b.Name == name).ToList();
        }

        public IList<Book> GetBooksByAuthor(string author)
        {
            ReadJson();

            return _books.Where(b => b.Author == author).ToList();
        }

        public IList<Book> GetBooksByCategory(string category)
        {
            ReadJson();

            return _books.Where(b => b.Category == category).ToList();
        }

        public IList<Book> GetBooksByLanguage(string language)
        {
            ReadJson();

            return _books.Where(b => b.Language == language).ToList();
        }

        public IList<Book> GetBooksByISBN(string ISBN)
        {
            ReadJson();

            return _books.Where(b => b.ISBN == ISBN).ToList();
        }

        public IList<Book> GetBooksByAvailability(string availability)
        {
            ReadJson();

            BookAvailability parsedAvailability;

            try
            {
                parsedAvailability = (BookAvailability)Enum.Parse(typeof(BookAvailability), availability);
            }
            catch (Exception)
            {
                throw;
            }

            return _books.Where(b => b.Availability == parsedAvailability).ToList();
        }

        public IList<Book> GetBooksByBorrower(string borrower)
        {
            ReadJson();

            return _books.Where(b => b.Borrower == borrower).ToList();
        }

        public void Add(Book book)
        {
            ReadJson();

            _books.Add(book);

            WriteToJson();
        }

        public bool Update(Book book)
        {
            Book oldBook = GetBooksByISBN(book.ISBN).FirstOrDefault();

            _books.Insert(_books.IndexOf(oldBook), book);
            bool result = _books.Remove(oldBook);

            WriteToJson();

            return result;
        }

        public bool Delete(Book book)
        {
            bool result = _books.Remove(book);

            WriteToJson();

            return result;
        }

        private void ReadJson()
        {
            if (!File.Exists(_filePath))
            {
                CreateEmptyJsonFile();
            }

            string jsonString = File.ReadAllText(_filePath);
            _books = JsonSerializer.Deserialize<List<Book>>(jsonString);
        }

        private void WriteToJson()
        {
            if (!File.Exists(_filePath))
            {
                CreateEmptyJsonFile();
            }

            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(_books, options);
            File.WriteAllText(_filePath, jsonString);
        }

        private void CreateEmptyJsonFile()
        {
            FileStream fileStream = File.Create(_filePath);
            fileStream.Dispose();

            _books = new List<Book>();

            WriteToJson();
        }
    }
}
