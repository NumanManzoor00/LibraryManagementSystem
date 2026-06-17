using App.Core.Interfaces;
using App.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class LibrarianService : ILibrarianService
    {
        private readonly string _connectionString;

        public LibrarianService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        }

        public void AddLibrarian(Librarian librarian)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("INSERT INTO Librarians(Id, Name, Email) VALUES (@Id, @Name, @Email)", conn);
            MapParams(cmd, librarian);
            cmd.ExecuteNonQuery();
        }

        public void UpdateLibrarian(Librarian librarian)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("UPDATE Librarians SET Name=@Name, Email=@Email WHERE Id=@Id", conn);
            MapParams(cmd, librarian);
            cmd.ExecuteNonQuery();
        }

        public void DeleteLibrarian(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("DELETE FROM Librarians WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Librarian> GetAllLibrarians()
        {
            List<Librarian> librarians = new List<Librarian>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Librarians", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) librarians.Add(ReadLibrarian(reader));
            return librarians;
        }

        public async Task<List<Librarian>> GetAllLibrariansAsync()
        {
            List<Librarian> librarians = new List<Librarian>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Librarians", conn);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) librarians.Add(ReadLibrarian(reader));
            return librarians;
        }

        public Librarian? GetLibrarianById(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Librarians WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) return ReadLibrarian(reader);
            return null;
        }

        private static void MapParams(SqlCommand cmd, Librarian librarian)
        {
            cmd.Parameters.AddWithValue("@Id", librarian.Id);
            cmd.Parameters.AddWithValue("@Name", librarian.Name);
            cmd.Parameters.AddWithValue("@Email", librarian.Email);
        }

        private static Librarian ReadLibrarian(SqlDataReader reader)
        {
            Librarian l = new Librarian();
            l.Id = reader["Id"].ToString() ?? string.Empty;
            l.Name = reader["Name"].ToString() ?? string.Empty;
            l.Email = reader["Email"].ToString() ?? string.Empty;
            return l;
        }
    }
}
