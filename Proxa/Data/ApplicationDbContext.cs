using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Proxa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Proxy> Proxies { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<APIKey> APIKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.APIKeys)
                .WithOne()
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Proxies)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Lists)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<List>()
                .HasMany(l => l.Proxies)
                .WithMany(p => p.Lists)
                .UsingEntity(j => j.ToTable("ListProxies"));
        }
    }
}
