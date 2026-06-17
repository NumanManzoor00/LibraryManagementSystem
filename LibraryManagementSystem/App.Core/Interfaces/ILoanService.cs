using App.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface ILoanService
    {
        void AddLoan(Loan loan);
        void UpdateLoan(Loan loan);
        void DeleteLoan(string id);
        List<Loan> GetAllLoans();
        Loan? GetLoanById(string id);
        Task<List<Loan>> GetAllLoansAsync();
    }
}
