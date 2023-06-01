using System;
using System.Threading.Tasks;
using Proxa.Models;
using Proxa.Data;

namespace Services
{
    public class AccountManagementService
    {
        private readonly ApplicationDbContext _context;

        public AccountManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAccountAsync(string email, string passwordHash)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Credits = 0,
                SubscriptionType = "Free"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetAccountAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> UpdateAccountAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAccountAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> AdjustCreditsAsync(Guid userId, int credits)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Credits += credits;
                user.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> AdjustSubscriptionAsync(Guid userId, string subscriptionType)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.SubscriptionType = subscriptionType;
                user.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
