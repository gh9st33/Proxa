using Microsoft.Graph;
using System;

namespace Proxa.Models
{
    public class APIKey
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
