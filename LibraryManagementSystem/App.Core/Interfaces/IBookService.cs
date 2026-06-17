using App.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(string id);
        List<Book> GetAllBooks();
        Book? GetBookById(string id);
        Task<List<Book>> GetAllBooksAsync();
    }
}
