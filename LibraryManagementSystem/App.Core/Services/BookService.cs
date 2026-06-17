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
    public class BookService : IBookService
    {
        private readonly string _connectionString;

        public BookService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        }

        public void AddBook(Book book)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "INSERT INTO Books(Id, Title, Author, ISBN, Genre, Status, AddedDate) " +
                         "VALUES (@Id, @Title, @Author, @ISBN, @Genre, @Status, @AddedDate)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, book);
            cmd.ExecuteNonQuery();
        }

        public void UpdateBook(Book book)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "UPDATE Books SET Title=@Title, Author=@Author, ISBN=@ISBN, " +
                         "Genre=@Genre, Status=@Status WHERE Id=@Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, book);
            cmd.ExecuteNonQuery();
        }

        public void DeleteBook(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) books.Add(ReadBook(reader));
            return books;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            List<Book> books = new List<Book>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) books.Add(ReadBook(reader));
            return books;
        }

        public Book? GetBookById(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) return ReadBook(reader);
            return null;
        }

        private static void MapParams(SqlCommand cmd, Book book)
        {
            cmd.Parameters.AddWithValue("@Id", book.Id);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@Genre", book.Genre.ToString());
            cmd.Parameters.AddWithValue("@Status", book.Status.ToString());
            cmd.Parameters.AddWithValue("@AddedDate", book.AddedDate);
        }

        private static Book ReadBook(SqlDataReader reader)
        {
            Book b = new Book();
            b.Id = reader["Id"].ToString() ?? string.Empty;
            b.Title = reader["Title"].ToString() ?? string.Empty;
            b.Author = reader["Author"].ToString() ?? string.Empty;
            b.ISBN = reader["ISBN"].ToString() ?? string.Empty;
            b.Genre = Enum.TryParse<BookGenre>(reader["Genre"].ToString(), true, out var genre) ? genre : BookGenre.Fiction;
            b.Status = Enum.TryParse<BookStatus>(reader["Status"].ToString(), true, out var status) ? status : BookStatus.Available;
            b.AddedDate = Convert.ToDateTime(reader["AddedDate"]);
            return b;
        }
    }
}
