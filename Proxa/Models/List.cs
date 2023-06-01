using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace Proxa.Models
{
    public class List
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Proxy> Proxies { get; set; }
    }
}
