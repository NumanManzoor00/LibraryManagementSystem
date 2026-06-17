using App.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
    public interface IMemberService
    {
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(string id);
        List<Member> GetAllMembers();
        Member? GetMemberById(string id);
        Task<List<Member>> GetAllMembersAsync();
    }
}
