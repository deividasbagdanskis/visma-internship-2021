using System;

namespace LibraryApp.Models
{
    public enum BookAvailability
    {
        Available,
        Taken
    }

    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public BookAvailability Availability { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Borrower { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nAuthor: {Author}\nCategory: {Category}\nPublication " +
                $"date: {PublicationDate.Date.ToString("yyyy-MM-dd")}\nISBN: {ISBN}\nAvailability: " +
                $"{Availability}\n";
        }
    }
}