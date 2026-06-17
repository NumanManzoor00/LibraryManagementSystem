using App.Core.Interfaces;
using App.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly string _connectionString;

        public MemberService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        }

        public void AddMember(Member member)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "INSERT INTO Members(Id, Name, Email, Phone) VALUES (@Id, @Name, @Email, @Phone)";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, member);
            cmd.ExecuteNonQuery();
        }

        public void UpdateMember(Member member)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "UPDATE Members SET Name=@Name, Email=@Email, Phone=@Phone WHERE Id=@Id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            MapParams(cmd, member);
            cmd.ExecuteNonQuery();
        }

        public void DeleteMember(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("DELETE FROM Members WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Members", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) members.Add(ReadMember(reader));
            return members;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            List<Member> members = new List<Member>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Members", conn);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync()) members.Add(ReadMember(reader));
            return members;
        }

        public Member? GetMemberById(string id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Members WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) return ReadMember(reader);
            return null;
        }

        private static void MapParams(SqlCommand cmd, Member member)
        {
            cmd.Parameters.AddWithValue("@Id", member.Id);
            cmd.Parameters.AddWithValue("@Name", member.Name);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@Phone", member.Phone);
        }

        private static Member ReadMember(SqlDataReader reader)
        {
            Member m = new Member();
            m.Id = reader["Id"].ToString() ?? string.Empty;
            m.Name = reader["Name"].ToString() ?? string.Empty;
            m.Email = reader["Email"].ToString() ?? string.Empty;
            m.Phone = reader["Phone"].ToString() ?? string.Empty;
            return m;
        }
    }
}
