using App.Core.Enums;
using System;

namespace App.Core.Models
{
    public class Book
    {
        public Book()
        {
            Id = "BK-" + Guid.NewGuid().ToString("N").Substring(0, 9);
        }

        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public BookGenre Genre { get; set; } = BookGenre.Fiction;
        public BookStatus Status { get; set; } = BookStatus.Available;
        public DateTime AddedDate { get; set; } = DateTime.Today;
    }
}
