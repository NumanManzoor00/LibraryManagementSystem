using App.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface ILibrarianService
    {
        void AddLibrarian(Librarian librarian);
        void UpdateLibrarian(Librarian librarian);
        void DeleteLibrarian(string id);
        List<Librarian> GetAllLibrarians();
        Librarian? GetLibrarianById(string id);
        Task<List<Librarian>> GetAllLibrariansAsync();
    }
}
