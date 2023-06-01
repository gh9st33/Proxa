using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace Proxa.Models
{
    public class Proxy
    {
        public Guid Id { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
