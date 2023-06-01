using System;
using System.Linq;
using System.Threading.Tasks;
using Proxa.Data;
using Proxa.Models;

namespace Proxa.Services
{
    public class APIKeyManagementService
    {
        private readonly ApplicationDbContext _context;

        public APIKeyManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<APIKey> CreateAPIKey(Guid userId)
        {
            var apiKey = new APIKey
            {
                Id = Guid.NewGuid(),
                Key = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                UserId = userId
            };

            _context.APIKeys.Add(apiKey);
            await _context.SaveChangesAsync();

            return apiKey;
        }

        public APIKey GetAPIKey(Guid id)
        {
            return _context.APIKeys.FirstOrDefault(x => x.Id == id);
        }

        public async Task DeleteAPIKey(Guid id)
        {
            var apiKey = _context.APIKeys.FirstOrDefault(x => x.Id == id);
            if (apiKey != null)
            {
                _context.APIKeys.Remove(apiKey);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignAPIKey(Guid userId, Guid apiKeyId)
        {
            var apiKey = _context.APIKeys.FirstOrDefault(x => x.Id == apiKeyId);
            if (apiKey != null)
            {
                apiKey.UserId = userId;
                apiKey.UpdatedAt = DateTime.UtcNow;
                _context.APIKeys.Update(apiKey);
                await _context.SaveChangesAsync();
            }
        }
    }
}
