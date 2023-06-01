using Proxa.Models;
using System;
using System.Collections.Generic;

namespace Proxa.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Credits { get; set; }
        public string SubscriptionType { get; set; }

        public ICollection<APIKey> APIKeys { get; set; }
        public ICollection<Proxy> Proxies { get; set; }
        public ICollection<List> Lists { get; set; }
    }
}
