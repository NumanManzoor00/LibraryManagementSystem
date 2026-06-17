using App.Core.Enums;
using App.Core.Interfaces;
using App.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly string _connectionString;

        public LoanService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        }

        public void AddLoan(Loan loan)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "INSERT INTO Loans(Id, BookId, MemberId, IssueDate, DueDate, ReturnDate, Status) " +
                         "VALUES (@Id, @BookId, @MemberId, @IssueDate, @DueDate, @ReturnDate, @Status)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, loan);
            cmd.ExecuteNonQuery();
        }

        public void UpdateLoan(Loan loan)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "UPDATE Loans SET BookId=@BookId, MemberId=@MemberId, IssueDate=@IssueDate, " +
                         "DueDate=@DueDate, ReturnDate=@ReturnDate, Status=@Status WHERE Id=@Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, loan);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLoan(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("DELETE FROM Loans WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = @"SELECT l.*, b.Title AS BookTitle, m.Name AS MemberName
                           FROM Loans l
                           LEFT JOIN Books b ON l.BookId = b.Id
                           LEFT JOIN Members m ON l.MemberId = m.Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) loans.Add(ReadLoan(reader));
            return loans;
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            List<Loan> loans = new List<Loan>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            string sql = @"SELECT l.*, b.Title AS BookTitle, m.Name AS MemberName
                           FROM Loans l
                           LEFT JOIN Books b ON l.BookId = b.Id
                           LEFT JOIN Members m ON l.MemberId = m.Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) loans.Add(ReadLoan(reader));
            return loans;
        }

        public Loan? GetLoanById(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = @"SELECT l.*, b.Title AS BookTitle, m.Name AS MemberName
                           FROM Loans l
                           LEFT JOIN Books b ON l.BookId = b.Id
                           LEFT JOIN Members m ON l.MemberId = m.Id
                           WHERE l.Id=@Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) return ReadLoan(reader);
            return null;
        }

        private static void MapParams(SqlCommand cmd, Loan loan)
        {
            cmd.Parameters.AddWithValue("@Id", loan.Id);
            cmd.Parameters.AddWithValue("@BookId", loan.BookId);
            cmd.Parameters.AddWithValue("@MemberId", loan.MemberId);
            cmd.Parameters.AddWithValue("@IssueDate", loan.IssueDate);
            cmd.Parameters.AddWithValue("@DueDate", loan.DueDate);
            cmd.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate.HasValue ? (object)loan.ReturnDate.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", loan.Status.ToString());
        }

        private static Loan ReadLoan(SqlDataReader reader)
        {
            Loan l = new Loan();
            l.Id = reader["Id"].ToString() ?? string.Empty;
            l.BookId = reader["BookId"].ToString() ?? string.Empty;
            l.BookTitle = reader["BookTitle"].ToString() ?? string.Empty;
            l.MemberId = reader["MemberId"].ToString() ?? string.Empty;
            l.MemberName = reader["MemberName"].ToString() ?? string.Empty;
            l.IssueDate = Convert.ToDateTime(reader["IssueDate"]);
            l.DueDate = Convert.ToDateTime(reader["DueDate"]);
            l.ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ReturnDate"]);
            l.Status = Enum.TryParse<LoanStatus>(reader["Status"].ToString(), true, out var status) ? status : LoanStatus.Issued;
            return l;
        }
    }
}
