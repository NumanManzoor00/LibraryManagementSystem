using System;

namespace App.Core.Models
{
    public class Librarian
    {
        public Librarian()
        {
            Id = "LB-" + Guid.NewGuid().ToString("N").Substring(0, 9);
        }

        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
