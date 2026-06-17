using App.Core.Enums;
using System;

namespace App.Core.Models
{
    public class Loan
    {
        public Loan()
        {
            Id = "LN-" + Guid.NewGuid().ToString("N").Substring(0, 9);
        }

        public string Id { get; set; }
        public string BookId { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.Today;
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(14);
        public DateTime? ReturnDate { get; set; } = null;
        public LoanStatus Status { get; set; } = LoanStatus.Issued;
    }
}
